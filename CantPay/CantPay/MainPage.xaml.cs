using CantPay.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CantPay
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            var assembly = typeof(MainPage);

            splashImage.Source = ImageSource.FromResource("CantPay.Assets.Images.splashimage.png", assembly);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuPage());
        }

        private void SeedButton_Clicked(object sender, EventArgs e)
        {
            var seedEngine = new SeedEngine();

            seedEngine.Seed();

        }
    }
}
