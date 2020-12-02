using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using System.IO;
using CantPay.Droid.Services;
using Xamarin.Forms;
using CantPay.Interfaces;

namespace CantPay.Droid
{
    [Activity(Label = "CantPay", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //For permissions check
        const int RequestLocationId = 0;

        readonly string[] LocationPermissions =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            ((DroidLoginProvider)DependencyService.Get<ILoginProvider>()).Init(this); //Initializes to allow login provider ref access

            Xamarin.FormsMaps.Init(this, savedInstanceState);

            string dbName = "cantpay_db.sqlite";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = Path.Combine(folderPath,dbName);

            LoadApplication(new App(fullPath));
        }

        protected override void OnStart()
        {
            base.OnStart();
            {
                base.OnStart();

                if ((int)Build.VERSION.SdkInt >= 23)
                {
                    if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                    {
                        RequestPermissions(LocationPermissions, RequestLocationId);
                    }
                    else
                    {
                        //Permissions already granted - display message?
                    }
                }

            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            Xamarin.Essentials.Platform.OnResume();            
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == RequestLocationId)
            {
                if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
                {
                    //Permissions granted - display a message
                }
                else
                {
                    //Permissions denied - display a message
                }                
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

    }
}