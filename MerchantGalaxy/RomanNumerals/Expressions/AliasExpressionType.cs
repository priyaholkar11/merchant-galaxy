
using MerchantGalaxy.Common;
using MerchantGalaxy.Interface;
using MerchantGalaxy.RomanNumerals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantGalaxy.RomanNumerals.Expressions
{
    /// <summary>
    /// This class checks for and parses alias expressions. For e.g.,
    /// glob is I
    /// </summary>
    public class AliasExpressionType : IExpression
    {
        private AliasMapper aliasMap;


        public AliasExpressionType(AliasMapper aliasMap)
        {
            this.aliasMap = aliasMap;
        }

        #region IExpression implementation
        public void Execute(string input)
        {
            string[] parts = input.Split(new string[] { GlobalConstants.Is },
                             StringSplitOptions.RemoveEmptyEntries);

            string roman = parts[1];
            aliasMap.AddAlias(parts[0], parts[1]);
        }

        public bool Match(string input)
        {
            string romanAlphabet = RomanNotation.GetAlphabet();
            string[] parts = input.Split(new string[] { GlobalConstants.Is },
                             StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2) return false;

            string roman = parts[1];
            bool found = false;

            for (int i = 0; i < romanAlphabet.Length; i++)
            {
                if (String.Equals(roman, romanAlphabet[i].ToString(),
                        StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                    break;
                }
            }

            return found;
        }
        #endregion IExpression implementation
    }
}
