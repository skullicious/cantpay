using CantPay.Interfaces;
using CantPay.Models;
using CantPay.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CantPay.ViewModel
{
    class MenuVM : INavigable
    {

        public ObservableCollection<MenuItemModel> ObservedMenuItems { get; set; }

        public NavigationCommand NavCommand { get; set; }

        public MenuVM()
        {
            NavCommand = new NavigationCommand(this);

            ObservedMenuItems = new ObservableCollection<MenuItemModel>()
            {

                new MenuItemModel(){Title="Home", BgImageSource="XXX"},
                 new MenuItemModel(){Title="Map", BgImageSource="XXX"},
                  new MenuItemModel(){Title="History", BgImageSource="XXX"},
                   new MenuItemModel(){Title="Settings", BgImageSource="XXX"},
                    new MenuItemModel(){Title="Notes", BgImageSource="XXX"},
                     new MenuItemModel(){Title="Help", BgImageSource="XXX"},
                      new MenuItemModel(){Title="Notes", BgImageSource="XXX"},
                        new MenuItemModel(){Title="Test", BgImageSource="XXX"}
            };

        }


        public async void Navigate()
        {
          
        }
    }
}
