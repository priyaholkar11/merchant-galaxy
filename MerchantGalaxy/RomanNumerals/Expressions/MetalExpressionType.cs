
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
    /// This class checks for and parses unit expressions. For e.g.,
    /// pish pish Iron is 3910 Credits
    /// </summary>
    public class MetalExpressionType : IExpression
    {
        private AliasMapper aliasMap;
        private AliasMetalMapper meatlMap;
        private IDecimalConverter converter;
        private ExpressionValidationHelper helper;

        public MetalExpressionType(AliasMetalMapper meatlMap, AliasMapper aliasMap,
                    IDecimalConverter converter, ExpressionValidationHelper helper)
        {
            this.meatlMap = meatlMap;
            this.aliasMap = aliasMap;
            this.converter = converter;
            this.helper = helper;
        }

        #region IExpression implementation
        public void Execute(string input)
        {
            string[] parts = input.Split(new string[] { GlobalConstants.Is },
                             StringSplitOptions.RemoveEmptyEntries);
            string[] wordsInFirstPart = parts[0].Split(new char[] { ' ' },
                             StringSplitOptions.RemoveEmptyEntries);
            string[] wordsInSecondPart = parts[1].Split(new char[] { ' ' },
                            StringSplitOptions.RemoveEmptyEntries);

            double decimalPrice = 0;
            Double.TryParse(wordsInSecondPart[0], out decimalPrice);

            string metal = wordsInFirstPart[wordsInFirstPart.Length - 1];
            string aliasValue = string.Empty;

            //Create Roman Numeral from aliases
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < wordsInFirstPart.Length - 1; i++)
            {
                sb.Append(aliasMap.GetValueForAlias(wordsInFirstPart[i]));
            }

            //Convert Roman to decimal
            double? totalUnits = converter.ToDecimal(sb.ToString());

            //Calculate and store per unit price of metal
            if (totalUnits.HasValue)
                meatlMap.AddMetal(metal, decimalPrice / totalUnits.Value);
            else
                Console.WriteLine("Error occurred while calculating commodity price");
        }

        public bool Match(string input)
        {
            string[] parts = input.Split(new string[] { GlobalConstants.Is },
                        StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) return false;

            string[] wordsInFirstPart = parts[0].Split(new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);
            string[] wordsInSecondPart = parts[1].Split(new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);
            double output;

            return input.EndsWith(GlobalConstants.credits, StringComparison.OrdinalIgnoreCase) &&
                    !input.StartsWith(GlobalConstants.howMany, StringComparison.OrdinalIgnoreCase) &&
                    parts.Length == 2 &&
                    wordsInSecondPart.Length == 2 &&
                    Double.TryParse(wordsInSecondPart[0], out output) &&
                    helper.AreWordsValidAliases(wordsInFirstPart.Take(wordsInFirstPart.Length - 1).ToArray());
        }
        #endregion IExpression implementation
    }
}
