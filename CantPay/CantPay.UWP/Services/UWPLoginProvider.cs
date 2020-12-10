using CantPay.Helpers;
using CantPay.Interfaces;
using CantPay.UWP.Services;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: Xamarin.Forms.Dependency(typeof(UWPLoginProvider))]
namespace CantPay.UWP.Services
{
    public class UWPLoginProvider : ILoginProvider
    {
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
                Locations.AppServiceUrl, /* Resource we want to access */
                Locations.AadClientId, /*Client Id of Native app */
                returnUri,  /* return Uri we configured*/
                new PlatformParameters(PromptBehavior.Auto, false));
            return authResult.AccessToken;

        }



        public async Task LoginAsync(MobileServiceClient client, string authType)
        {
            //Client flow
            //var accessToken = await LoginADALASync();
            //var zumoPayload = new JObject()
            //{
            //    ["access_token"] = accessToken
            //};
            //await client.LoginAsync(authType, zumoPayload);
            ///


            //server flow
            await client.LoginAsync(authType);
        }
    }
}
