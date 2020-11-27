using CantPay.Helpers;
using CantPay.Models;
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

          
            map.IsShowingUser = true;

            map.HasZoomEnabled = true;


            DisplayAlert("Alert", gameManager.test, "OK");

            

                        
        }



        private void DisplayGameLocations(List<GameLocation> gameLocations)
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
	}
}