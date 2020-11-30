using System;
using System.Collections.Generic;
using System.Text;

namespace CantPay.Helpers
{
    public class Constants
    {
      
        public const string CATEGORY_SEARCH = "https://api.foursquare.com/v2/venues/search?categoryId={0}&ll={1},{2}&client_id={3}&client_secret={4}&v={5}";
        public const string VENUE_SEARCH = "https://api.foursquare.com/v2/venues/search?ll={0},{1}&client_id={2}&client_secret={3}&v={4}";
        public const string CLIENT_ID = "DF34QQ1JL1BSD5TZWAYNV20QJ0BKJXGAHP2T4YFSVDIIOGCR";
        public const string CLIENT_SECRET = "BKEP3BTTYGLG3LGHLRVIDB4SND2NRABFCBZCMBU4XCRBEUSF";


    }
}
