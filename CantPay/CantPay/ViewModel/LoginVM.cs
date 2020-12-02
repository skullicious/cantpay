using CantPay.Helpers;
using CantPay.Interfaces;
using CantPay.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CantPay.ViewModel
{
    public class LoginVM : BaseVM
    {

        public LoginCommand LoginCommand { get; set; }

        public LoginVM()
        {
            LoginCommand = new LoginCommand(this);
        }

         public async void Login()
         {

            if (IsBusy)
                return;                 //For Async management
            IsBusy = true;            

            try
            {
                var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();

                await cloudService.LoginAsync();

                await Application.Current.MainPage.Navigation.PushAsync(new MenuPage());
            }
            catch(Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
         
         }
    }
}
