﻿using CantPay.Helpers;
using CantPay.Interfaces;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using CantPay.Models;

namespace CantPay.Services
{
    public class AzureCloudService : ICloudService
    {
        MobileServiceClient client;

        MobileServiceClient testClient;

        List<AppServiceIdentity> identities = null;

        public AzureCloudService()
        
        {
            //client = new MobileServiceClient("https://travelappxbackend.azurewebsites.net");
            client = new MobileServiceClient(Locations.AppServiceUrl, new AuthenticationDelegatingHandler());

            testClient = new MobileServiceClient(Locations.AlternateLoginHost, new AuthenticationDelegatingHandler());


            if (Locations.AlternateLoginHost != null)
                client.AlternateLoginHost = new Uri(Locations.AlternateLoginHost);
        }



        public async Task<AppServiceIdentity> GetIdentityAsync()
        {
            if (client.CurrentUser == null || client.CurrentUser?.MobileServiceAuthenticationToken == null)
            {
                throw new InvalidOperationException("Not Authenticated");
            }

            if (identities == null)
            {
                try
                {
                    identities = await client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");               
                   //var client = new HttpClientHandler(new Uri(Locations()))
                   //var client = new HttpClient(new Uri(Locations.AlernateLoginHost ?? Locations.AppServiceUrl));

                    //identities = await testClient.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");

                }
                catch (Exception ex)
                {
                    //polyfill style
                    identities = await testClient.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");
                }
            }

            if (identities.Count > 0)
                return identities[0];
            return null;
        }

        public ICloudTable<T> GetTable<T>() where T : TableData
        {
            return new AzureCloudTable<T>(client);
        }

        //public ICloudTable<T> GetTable<T>() where T : TableData => new AzureCloudTable<T>(client);

        public async Task LogOutAsync()
        {
            if (client.CurrentUser == null || client.CurrentUser.MobileServiceAuthenticationToken == null)
                return;

            //Logout of ID Provider if logged in
            var authUri = new Uri($"{testClient.MobileAppUri}/.auth/logout");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-ZUMO-AUTH", client.CurrentUser.MobileServiceAuthenticationToken);
                await httpClient.GetAsync(authUri);
            }

            //Remove token from cache using Dependency service
            DependencyService.Get<ILoginProvider>().RemoveTokenFromSecureStore();

            //Remove the token from Mobile Service Client
            await client.LogoutAsync();
        }

  
        public async Task<MobileServiceUser> LoginAsync(string authType)
        {
            var loginProvider = DependencyService.Get<ILoginProvider>();

            client.CurrentUser = loginProvider.RetrieveTokenFromSecureStore();

            if (client.CurrentUser != null)
            {
                //user has already been authenticated = attemp to refresh token
                try
                {
                 var refreshed = await client.RefreshUserAsync();    //IMPLEMENT
                      if (refreshed != null)
                      {
                        // loginProvider.RetrieveTokenFromSecureStore(refreshed);
                        loginProvider.StoreTokenInSecureStore(refreshed);
                         return refreshed;
                      }
                }
                catch (Exception refreshException)
                {
                    Debug.WriteLine($"Could not refresh token: {refreshException.Message}");
                }
            }

        if (client.CurrentUser != null && !IsTokenExpired(client.CurrentUser.MobileServiceAuthenticationToken))
        {
            //user has been authenticated already.. no refresh required
              return client.CurrentUser;
        }

        //Credentials are required
        await loginProvider.LoginAsync( client,authType);
        if (client.CurrentUser != null)
        {
                //We were able to log in!
                loginProvider.StoreTokenInSecureStore(client.CurrentUser);
        }

        return client.CurrentUser;
        // return loginProvider.LoginAsync(client, authType);
    }

       

        bool IsTokenExpired(string token)
        {
            //grab JWT element of token
            var jwt = token.Split(new char[] { '.' })[1];

            //Undo URL encoding
            jwt = jwt.Replace('-', '+').Replace('_', '/');

            switch (jwt.Length % 4)
            {
                case 0: break;
                case 2: jwt += "=="; break;
                case 3: jwt += "="; break;
                default:
                    throw new ArgumentException("the token is not a valid Base64 string");                    
            }

            //Convert to JSON String
            var bytes = Convert.FromBase64String(jwt);
            string jsonString = UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            //Parse as JSON object and get the exp field value which is the exp. date as a JS primitive
            JObject jsonObj = JObject.Parse(jsonString);
            var exp = Convert.ToDouble(jsonObj["exp"].ToString());

            //Calculate expiration by adding the exp value (secs) to bases date of 1/1/1970
            DateTime minTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var expire = minTime.AddSeconds(exp);
            return (expire < DateTime.UtcNow);


        }
     
    }
}
