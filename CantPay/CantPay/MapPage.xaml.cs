using CantPay.Helpers;
using CantPay.Models;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CantPay
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{                                
			InitializeComponent ();

            GameManager gameManager = GameManager.Instance();

            NavigationPage.SetHasNavigationBar(this, false);

          
            gameLocationsMap.IsShowingUser = true;

            gameLocationsMap.HasZoomEnabled = true;

            //DisplayAlert("Alert", gameManager.ToString , "OK");                     
                                    
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;

            locator.PositionChanged += Locator_PositionChanged;

            await locator.StartListeningAsync(TimeSpan.Zero, 100);

            var position = await locator.GetPositionAsync();

            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);

            var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);

            gameLocationsMap.MoveToRegion(span);

            var gameLocations = await GameLocation.Read(); 

            DisplayGameLocationsInMap(gameLocations);
                
         //  var gameLocations = await GameLocation.GetGameLocations(position.Latitude, position.Longitude);
                 
        }


    



        private void DisplayGameLocationsInMap(List<GameLocation> gameLocations)
        {
            foreach (var gameLocation in gameLocations)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(gameLocation.Latitude, gameLocation.Longitude);

                    var pin = new Pin()
                    {
                        Type = PinType.SavedPin,
                        Position = position,
                        Label = gameLocation.Name,
                        Address = gameLocation.Address
                    };

                }
                catch (NullReferenceException nre) { }
                catch (Exception ex) { }

            }
        }


        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var center = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);

            var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);

            gameLocationsMap.MoveToRegion(span);
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var locator = CrossGeolocator.Current;

            locator.PositionChanged -= Locator_PositionChanged;

            locator.StopListeningAsync();
        }


    }
}