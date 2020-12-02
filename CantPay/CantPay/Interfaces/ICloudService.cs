using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CantPay.Interfaces
{
    public interface ICloudService
    {
        ICloudTable<T> GetTable<T>() where T : TableData;

        Task LoginAsync(string authgType);
    }
}
