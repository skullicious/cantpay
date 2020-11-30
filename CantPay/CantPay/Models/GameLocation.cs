using CantPay.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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


        public static async Task<List<GameLocation>> Read()
        {
            var gameLocations = new List<GameLocation>();


            return gameLocations;
        }


    }



}



