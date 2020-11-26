using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CantPay.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            // BING KEY 
            // 88oaL9fxECEGV8mqWUWJ~gYs6QIjzv5f8BYeXK_WX9A~AswlvpKDVhmRMasv-bOy-dE-NEf7_eJeMWC1pV1CkrGmqB9O7YvjbA2olgbKT4LY

            this.InitializeComponent();

            Xamarin.FormsMaps.Init("88oaL9fxECEGV8mqWUWJ~gYs6QIjzv5f8BYeXK_WX9A~AswlvpKDVhmRMasv-bOy-dE-NEf7_eJeMWC1pV1CkrGmqB9O7YvjbA2olgbKT4LY"); // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/map/setup#universal-windows-platform

            LoadApplication(new CantPay.App());
        }
    }
}
