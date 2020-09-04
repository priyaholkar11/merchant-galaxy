using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxy.Common
{
    public class AliasMetalMapper
    {
        private Dictionary<string, double> aliasMetalMap;

        public AliasMetalMapper()
        {
            aliasMetalMap = new Dictionary<string, double>();
        }

        public void AddMetal(string name, double perUnitPrice)
        {
            if (!aliasMetalMap.ContainsKey(name))
                aliasMetalMap.Add(name, perUnitPrice);

            else
                aliasMetalMap[name] = perUnitPrice;
        }

        public double GetPriceByMetal(string commodity)
        {
            return aliasMetalMap[commodity];
        }

        public bool Exists(string commodity)
        {
            return aliasMetalMap.ContainsKey(commodity);
        }
    }
}
