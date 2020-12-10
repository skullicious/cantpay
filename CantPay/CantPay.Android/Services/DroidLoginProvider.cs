using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CantPay.Droid.Services;
using CantPay.Helpers;
using CantPay.Interfaces;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

[assembly: Xamarin.Forms.Dependency(typeof(DroidLoginProvider))] //registers class as a platform service to allow access
namespace CantPay.Droid.Services
{
    public class DroidLoginProvider : ILoginProvider
    {
        Context context;

        public void Init(Context context)
        {
            this.context = context;
        }


        //Login via ADAL
        public async Task<string> LoginADALAsync()
        {
            Uri returnUri = new Uri(Locations.AadRedirectUri);

            var authContext = new AuthenticationContext(Locations.AadAuthority);

            if (authContext.TokenCache.ReadItems().Count() > 0)
            {
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            }           
                var authResult = await authContext.AcquireTokenAsync(
                    Locations.AppServiceUrl,
                    Locations.AadClientId,
                    returnUri,
                    new PlatformParameters((Activity)context));
                return authResult.AccessToken;                   
        }
          



        public async Task LoginAsync(MobileServiceClient client, string authType)
        {

         //   client flow;
         //   var accessToken = await LoginADALAsync();
         //   var zumoPayload = new JObject();
         //   zumoPayload["access_token"] = accessToken;
         //   await client.LoginAsync(authType, zumoPayload);


            // Server-flow
            await client.LoginAsync(context, authType);
            //await client.LoginAsync(context, "facebook");
        }
    }
}