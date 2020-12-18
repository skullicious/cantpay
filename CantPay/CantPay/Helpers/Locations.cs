using System;
using System.Collections.Generic;
using System.Text;

namespace CantPay.Helpers
{
    public static class Locations
    {
#if DEBUG

        //If working on local App Service - LoginAsync (non ADAL) Server Flow must be used until client flow has been configured

        //
        public static readonly string AppServiceUrl = "http://SWEENOTRON/TravelAppXBackend";
        public static readonly string AlternateLoginHost = "https://travelappxbackend.azurewebsites.net";
        
        //
       // public static readonly string AppServiceUrl = "https://travelappxbackend.azurewebsites.net";
       // public static readonly string AlternateLoginHost = null;


        // For AD Client flow
        public static readonly string AadClientId = "bd39c506-28bf-4c78-a98a-c73c420be30a";
        public static readonly string AadRedirectUri = "https://travelappxbackend.azurewebsites.net/.auth/login/done";
        public static readonly string AadAuthority = "https://login.windows.net/amoit2020outlook.onmicrosoft.com";
       
       
#else
        public static readonly string AppServiceUrl "https://travelappxbackend.azurewebsites.net"
        public static readonly string AlernateLoginHost = null;
#endif
    }
}
