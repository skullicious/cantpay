using CantPay.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CantPay.ViewModel
{
    public class MapVM
    {
        public ObservableCollection<GameLocation> GameLocations { get; set; }

        public MapVM()
        {

            GameLocations = new ObservableCollection<GameLocation>()
            {

            };

        }
        
    }
}
