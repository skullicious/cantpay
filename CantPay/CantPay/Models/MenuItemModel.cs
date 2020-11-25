using CantPay.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CantPay.Models
{
    public class MenuItemModel
    {
        public string Title { get; set; }

        public ContentPage NavigationTarget { get; set; }
        
        public string BgImageSource { get; set; }

        public MapPage MapPage { get; set; }
    }
}
