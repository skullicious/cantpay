using System;
using System.Linq;
using System.Threading.Tasks;
using CantPay.Helpers;
using CantPay.Interfaces;
using CantPay.iOS.Services;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(iOSLoginProvider))]
namespace CantPay.iOS.Services
{
    public class iOSLoginProvider : ILoginProvider
    {


        //Login via ADAL
        public async Task<string> LoginADALAsync(UIViewController view)
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
                new PlatformParameters(view));

            return authResult.AccessToken;           

        }


        public async Task LoginAsync(MobileServiceClient client, string authType)
        {
            var rootView = UIApplication.SharedApplication.KeyWindow.RootViewController;

            //Client flow
            var accessToken = await LoginADALAsync(rootView);
            var zumoPayload = new JObject();
            zumoPayload["access_token"] = accessToken;
            await client.LoginAsync(authType, zumoPayload);

            //Server flow    
            //await client.LoginAsync(RootView, authType);
            //await client.LoginAsync(RootView, "facebook");
        }

        //public UIViewController RootView => UIApplication.SharedApplication.KeyWindow.RootViewController;
    }
}