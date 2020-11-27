using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CantPay.Models
{
    public class GameLocation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
       
    }
}
