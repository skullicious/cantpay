using CantPay.Helpers;
using CantPay.Interfaces;
using CantPay.Models;
using CantPay.Services;
using CantPay.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace CantPay.ViewModel
{
    public class MenuVM  : BaseVM, INavigable
    {

        public ObservableCollection<MenuItemModel> ObservedMenuItems { get; set; }
               
        ICloudService cloudService;

        public ICloudTable<TodoItem> Table { get; set; }

        public NavigationCommand NavCommand { get; set; }

        public Command GetDataCommand { get; }

        public Command LogoutCommand { get; }


        ObservableRangeCollection<TodoItem> items = new ObservableRangeCollection<TodoItem>();
        public ObservableRangeCollection<TodoItem> Items
        {
            get { return items; }
            set { SetProperty(ref items, value, "Items"); }
        }

        public MenuVM()
        {

           

           cloudService = ServiceLocator.Instance.Resolve<ICloudService>();

           Table = cloudService.GetTable<TodoItem>();

           NavCommand = new NavigationCommand(this);

            GetDataCommand = new Command(async () => await ExecuteGetDataCommand());

            LogoutCommand = new Command(async () => await ExecuteLogoutCommand());

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

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine("[TaskList] OnCollectionChanged: Items have changed");
        }


        async Task ExecuteLogoutCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
                await cloudService.LogOutAsync();
           
                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Logout Failed", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

       
        async Task ExecuteGetDataCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var identity = await cloudService.GetIdentityAsync();
                if (identity != null)
                {                   
                    var name = identity.UserClaims.FirstOrDefault(c => c.Type.Equals("name")).Value;
                  
                }
                var list = await Table.ReadAllItemsAsync();
                Items.ReplaceRange(list);
            }
            catch (Exception ex)
            {
                // Handling Debug environment behaviour - need to revise
                //await Application.Current.MainPage.DisplayAlert("Items Not Loaded", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public  async void Navigate(object parameter)
        
        {
            var targetPage = (ContentPage)parameter;

            await App.Current.MainPage.Navigation.PushAsync(targetPage);
        }
    }
}
