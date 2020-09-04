
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
    /// This class checks for and parses alias question expressions. For e.g.,
    /// How much is glob glob?
    /// </summary>
    public class AliasExpressionTypeQuestion : IExpression
    {
        private AliasMapper aliasMap;
        private IDecimalConverter converter;
        private ExpressionValidationHelper helper;

        public AliasExpressionTypeQuestion(AliasMapper aliasMap, IDecimalConverter converter,
                        ExpressionValidationHelper helper)
        {
            this.aliasMap = aliasMap;
            this.converter = converter;
            this.helper = helper;
        }

        #region IExpression implementation
        public void Execute(string input)
        {
            //Remove question mark
            input = input.Substring(0, input.Length - 1);

            StringBuilder sb = new StringBuilder();
            string[] parts = input.Split(new string[] { GlobalConstants.Is},
                             StringSplitOptions.RemoveEmptyEntries);
            string[] words = parts[1].Split(new char[] { ' ' },
                             StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                if (!aliasMap.Exists(word))
                {
                    Console.WriteLine(String.Format("Error while processing this input: {0}", input));
                    return;
                }
                sb.Append(aliasMap.GetValueForAlias(word));
            }

            Console.WriteLine(String.Format("{0} {1} {2}", 
            parts[1],GlobalConstants.Is,converter.ToDecimal(sb.ToString())));
        }

        public bool Match(string input)
        {
            //Remove question mark from the last alias
            input = input.Substring(0, input.Length - 1);

            bool isQuestion = (input.StartsWith(GlobalConstants.howMuch,
                                StringComparison.OrdinalIgnoreCase));
            if (!isQuestion)
                return false;

            string[] parts = input.Split(new string[] { GlobalConstants.Is},
                             StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                return false;

            string[] words = parts[1].Split(new char[] { ' ' },
                             StringSplitOptions.RemoveEmptyEntries);
            if (words.Length < 1) return false;

            return helper.AreWordsValidAliases(words);
        }
        #endregion IExpression implementation
    }
}
