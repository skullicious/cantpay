using CantPay.Interfaces;
using CantPay.UWP.Services;
using Microsoft.WindowsAzure.MobileServices;
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
        public async Task LoginAsync(MobileServiceClient client, string authType)
        {
            await client.LoginAsync(authType);
        }
    }
}
