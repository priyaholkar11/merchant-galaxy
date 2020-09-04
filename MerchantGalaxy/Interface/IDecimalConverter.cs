using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxy.Interface
{
    public interface IDecimalConverter
    {
        double? ToDecimal(string input);
    }
}
