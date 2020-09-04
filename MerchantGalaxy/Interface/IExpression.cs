using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxy.Interface
{
    public interface IExpression
    {
        bool Match(string input);
        void Execute(string input);
    }
}
