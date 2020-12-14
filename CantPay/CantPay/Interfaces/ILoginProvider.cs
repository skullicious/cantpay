using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CantPay.Interfaces
{
    public interface ILoginProvider
    {
        Task<MobileServiceUser> LoginAsync(MobileServiceClient client, string authType);

        MobileServiceUser RetrieveTokenFromSecureStore();

        void StoreTokenInSecureStore(MobileServiceUser user);
    }
}
