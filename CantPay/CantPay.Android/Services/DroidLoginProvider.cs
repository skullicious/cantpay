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
using CantPay.Interfaces;
using Microsoft.WindowsAzure.MobileServices;


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

        public async Task LoginAsync(MobileServiceClient client)
        {
            await client.LoginAsync(context, "facebook");
        }
    }
}