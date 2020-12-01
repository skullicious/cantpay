using CantPay.Models;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CantPay.Helpers
{
    public class SeedEngine
    {

        public async void Seed()
        {
            //Initial Seed Method
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var random = new Random();
            var gameLocationList = new List<GameLocation>();

            //Get list of categories for use in the game

            var categoryListForSeed = new List<Tuple<string, string>>()
            {
                Tuple.Create("Aquarium", "4fceea171983d5d06c3e9823"),
                Tuple.Create("Bakery", "4bf58dd8d48988d16a941735"),
                Tuple.Create("Fast Food", "4bf58dd8d48988d16e941735"),
                Tuple.Create("Fish and Chips", "4edd64a0c7ddd24ca188df1a"),
                Tuple.Create("Church", "4bf58dd8d48988d132941735"),            
                Tuple.Create("Bust Station", "4bf58dd8d48988d1fe931735"),
                //Tuple.Create("Vape Store", "56aa371be4b08b9a8d57355c") //returns null/0 results
            };                                                                               

            // Need to look at null addresses and NULL/0 results for certain location types.

            foreach (var category in categoryListForSeed)
            {
              
               var results =  await Venue.GetGameLocation(position.Latitude, position.Longitude, category.Item2);
                
                //Randomize
                int index = random.Next(results.Count);

                var randomizedItem = results[index];

                var gameLocation = new GameLocation()
                {
                    Name = randomizedItem.name,
                    Address = randomizedItem.location.address,
                    Latitude = randomizedItem.location.lat,
                    Longitude = randomizedItem.location.lng
                };

                gameLocationList.Add(gameLocation);                           
                            
            };       

            // Now we have a list.. Where to we put it? How do we edit it?
                  
        }
    }
}
