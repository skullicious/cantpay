using CantPay.Interfaces;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CantPay.Services
{
    public class AzureCloudService : ICloudService
    {
        MobileServiceClient client;

        public AzureCloudService()
        {
            client = new MobileServiceClient("https://travelappxbackend.azurewebsites.net");
        }
       
        public ICloudTable<T> GetTable<T>() where T : TableData
        {
            return new AzureCloudTable<T>(client);
        }

        //public ICloudTable<T> GetTable<T>() where T : TableData => new AzureCloudTable<T>(client);
  
        public Task LoginAsync(string authType)
        {
            var loginProvider = DependencyService.Get<ILoginProvider>();

            return loginProvider.LoginAsync(client, authType);
        }

      
    }
}
