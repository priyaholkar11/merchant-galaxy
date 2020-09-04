
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
    /// This class checks for and parses unit question expressions. For e.g.,
    /// How many credits is pish pish Iron?
    /// </summary>
    public class MetalExpressionTypeQuestion : IExpression
    {
        private AliasMapper aliasMap;
        private AliasMetalMapper metalMap;
        private IDecimalConverter converter;
        private ExpressionValidationHelper helper;

        public MetalExpressionTypeQuestion(AliasMetalMapper metalMap, AliasMapper aliasMap,
                            IDecimalConverter converter, ExpressionValidationHelper helper)
        {
            this.metalMap = metalMap;
            this.aliasMap = aliasMap;
            this.converter = converter;
            this.helper = helper;
        }

        #region IExpression implementation
        public void Execute(string input)
        {
            //Remove question mark
            input = input.Substring(0, input.Length - 1);

            string[] parts = input.Split(new string[] { GlobalConstants.Is },
                            StringSplitOptions.RemoveEmptyEntries);
            string[] words = parts[1].Split(new char[] { ' ' },
                            StringSplitOptions.RemoveEmptyEntries);

            string commodity = words[words.Length - 1];
            StringBuilder sb = new StringBuilder();

            //Create Roman Numeral from aliases
            for (int i = 0; i < words.Length - 1; i++)
            {
                sb.Append(aliasMap.GetValueForAlias(words[i]));
            }

            //Convert Roman to Decimal
            double? totalUnits = converter.ToDecimal(sb.ToString());
            if (totalUnits.HasValue)
                Console.WriteLine(String.Format("{0} {1} {2} {3}", parts[1],GlobalConstants.Is,
                totalUnits.Value * metalMap.GetPriceByMetal(commodity),GlobalConstants.credits));
        }

        public bool Match(string input)
        {
            //Remove question mark
            input = input.Substring(0, input.Length - 1);

            bool isQuestion = (input.StartsWith(GlobalConstants.howMany,
                    StringComparison.OrdinalIgnoreCase));
            if (!isQuestion) return false;

            string[] parts = input.Split(new string[] { GlobalConstants.Is },
                    StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) return false;

            string[] words = parts[1].Split(new char[] { ' ' },
                     StringSplitOptions.RemoveEmptyEntries);
            if (words.Length < 1) return false;

            return helper.AreWordsValidAliases(words.Take(words.Length - 1).ToArray()) &&
                    helper.AreWordsValidMetals(words.Skip(words.Length - 1).ToArray());
        }

        #endregion IExpression implementation
    }
}
