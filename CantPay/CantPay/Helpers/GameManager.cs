using CantPay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CantPay.Helpers
{
    sealed class GameManager
    {                  

        private static GameManager _instance = null;

        private GameManager()
        {
         

        }

        static internal GameManager Instance()
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }

    }
}
