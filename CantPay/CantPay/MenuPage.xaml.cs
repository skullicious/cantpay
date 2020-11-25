using CantPay.Interfaces;
using CantPay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CantPay
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage 
    {
        public MenuPage()
        {

            MenuVM viewModel;        

            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            viewModel = new MenuVM();

            BindingContext = viewModel;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}