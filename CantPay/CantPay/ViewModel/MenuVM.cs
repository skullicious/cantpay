using CantPay.Interfaces;
using CantPay.Models;
using CantPay.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace CantPay.ViewModel
{
    public class MenuVM : INavigable
    {

        public ObservableCollection<MenuItemModel> ObservedMenuItems { get; set; }

        public NavigationCommand NavCommand { get; set; }
               
        public MenuVM()
        {
           NavCommand = new NavigationCommand(this);

            ObservedMenuItems = new ObservableCollection<MenuItemModel>()
            {

                new MenuItemModel(){Title="Home", BgImageSource="XXX", NavigationTarget= new HomePage()},
                 new MenuItemModel(){Title="Map", BgImageSource="XXX", NavigationTarget= new MapPage()},
                  new MenuItemModel(){Title="History", BgImageSource="XXX", NavigationTarget= new HistoryPage()},
                   new MenuItemModel(){Title="Settings", BgImageSource="XXX", NavigationTarget= new SettingsPage()},
                    new MenuItemModel(){Title="Notes", BgImageSource="XXX",NavigationTarget= new NotesPage()},
                     new MenuItemModel(){Title="Help", BgImageSource="XXX", NavigationTarget= new HelpPage()},
                      new MenuItemModel(){Title="Notes", BgImageSource="XXX", NavigationTarget= new NotesPage()},
                        new MenuItemModel(){Title="About", BgImageSource="XXX", NavigationTarget= new AboutPage()}
            };
                        
        }

        public  async void Navigate(object parameter)
        
        {
            var targetPage = (ContentPage)parameter;

            await App.Current.MainPage.Navigation.PushAsync(targetPage);
        }
    }
}
