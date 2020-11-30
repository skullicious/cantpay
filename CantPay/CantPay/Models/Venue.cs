using CantPay.Helpers;
using CantPay.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace CantPay.Models
{
    public class Location
    {
        public string address { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int distance { get; set; }
        public string postalCode { get; set; }
        public string cc { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public IList<string> formattedAddress { get; set; }
        public string crossStreet { get; set; }
        public string neighborhood { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pluralName { get; set; }
        public string shortName { get; set; }

        public bool primary { get; set; }
    }

    public class Provider
    {
        public string name { get; set; }
    }


    public class VenuePage
    {
        public string id { get; set; }
    }

    public class Venue
    {
        public string id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public IList<Category> categories { get; set; }

        //public async static Task<List<Venue>> GetVenues(double latitude, double longitude)
        //{
        //    List<Venue> venues = new List<Venue>();
        //    var url = VenueRoot.GenerateURL(latitude, longitude);
        //    using (HttpClient client = new HttpClient())
        //    {
        //        var response = await client.GetAsync(url);
        //        var json = await response.Content.ReadAsStringAsync();
        //        var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);
        //        venues = venueRoot.response.venues as List<Venue>;
        //    }
        //    return venues;
        //}

        //Used in SEED method
        public async static Task<List<Venue>> GetGameLocation(double latitude, double longitude, string category)
        {

            List<Venue> venues = new List<Venue>();

            var url = VenueRoot.GenerateURL(Constants.CATEGORY_SEARCH, latitude, longitude, category);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);

                    var json = await response.Content.ReadAsStringAsync();

                    var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);

                    venues = venueRoot.response.venues as List<Venue>;
                }                                           
            }
            catch (Exception ex)
            {}
            return venues;            
        }
    }

    public class Response
    {
        public IList<Venue> venues { get; set; }

    }

    public class VenueRoot
    {
        public Response response { get; set; }            

        public static string GenerateURL(string stringFormat, double latitude, double longitude, string category)
        {
            return string.Format(stringFormat, category, latitude, longitude, Constants.CLIENT_ID, Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));
        }

        //public static string GenerateURL(double latitude, double longitude)
        //{
        //    return string.Format(Constants.VENUE_SEARCH, latitude, longitude, Constants.CLIENT_ID, Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));
        //}

        //public static string GenerateURL(double latitude, double longitude, string category)
        //{
        //    return string.Format(Constants.CATEGORY_SEARCH,  category, latitude, longitude, Constants.CLIENT_ID, Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));
        //}
    }

}






