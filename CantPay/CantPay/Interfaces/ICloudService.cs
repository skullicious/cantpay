using System;
using System.Collections.Generic;
using System.Text;

namespace CantPay.Interfaces
{
    public interface ICloudService
    {
        ICloudTable<T> GetTable<T>() where T : TableData;
    }
}
