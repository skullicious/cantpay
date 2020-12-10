using CantPay.Helpers;
using CantPay.Interfaces;
using CantPay.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CantPay
{
    public partial class App : Application
    {
        GameManager gameManager = GameManager.Instance();

        public static string DatabaseLocation = string.Empty;

        public static ICloudService CloudService { get; set; }

        public App()
        {
            
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            //   MainPage = new NavigationPage(new MainPage());      

        }

        public App(string databaseLocation)
        {

            InitializeComponent();

            CloudService = new AzureCloudService();

            ServiceLocator.Instance.Add<ICloudService, AzureCloudService>();

            MainPage = new NavigationPage(new MainPage());

            //gameManager.test = "123";

            DatabaseLocation = databaseLocation;

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
