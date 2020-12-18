using CantPay.Helpers;
using CantPay.Interfaces;
using CantPay.UWP.Services;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

[assembly: Xamarin.Forms.Dependency(typeof(UWPLoginProvider))]
namespace CantPay.UWP.Services
{
    public class UWPLoginProvider : ILoginProvider
       
    {

        public PasswordVault PasswordVault { get; private set; }

        public UWPLoginProvider()
        {
            PasswordVault = new PasswordVault();
        }


        public void RemoveTokenFromSecureStore()
        {
            try
            {
                //Check if token is in vault
                var acct = PasswordVault.FindAllByResource("cantpay").FirstOrDefault();
                if (acct != null)
                {
                    PasswordVault.Remove(acct);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving existing token: {ex.Message}");
            }
        }

        public MobileServiceUser RetrieveTokenFromSecureStore()
        {
            try
            {
                //check for token if available in vault         
             
                var acct = PasswordVault.FindAllByResource("cantpay").FirstOrDefault();

               //acct = null;
                
                if (acct != null)
                {
                    var token = PasswordVault.Retrieve("cantpay", acct.UserName).Password;
                    if (token != null && token.Length > 0)
                    {
                        return new MobileServiceUser(acct.UserName)
                        {
                            MobileServiceAuthenticationToken = token
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving token:  {ex.Message}" );
            }
            return null;
        }




        //Login via ADAL - client flow - gets token
        public async Task<string>LoginADALASync()
        {
            Uri returnUri = new Uri(Locations.AadRedirectUri);

            var authContext = new AuthenticationContext(Locations.AadAuthority);
            if (authContext.TokenCache.ReadItems().Count() >0)
            {
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            }
            var authResult = await authContext.AcquireTokenAsync(
                 Locations.AlternateLoginHost, /* Resource we want to access */
                //Locations.AppServiceUrl, /* Resource we want to access */
                Locations.AadClientId, /*Client Id of Native app */
                returnUri,  /* return Uri we configured*/
                new PlatformParameters(PromptBehavior.Auto, false));
            return authResult.AccessToken;

        }
                
                
  
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, string authtype)
        {
            // Client Flow
            var accessToken = await LoginADALASync();
            var zumoPayload = new JObject();
            zumoPayload["access_token"] = accessToken;  
            return await client.LoginAsync(authtype, zumoPayload);

            // Server-Flow Version
            // return await client.LoginAsync("aad");
        }

        public void StoreTokenInSecureStore(MobileServiceUser user)
        {
            PasswordVault.Add(new PasswordCredential("cantpay", user.UserId, user.MobileServiceAuthenticationToken));
        }
    }
}
