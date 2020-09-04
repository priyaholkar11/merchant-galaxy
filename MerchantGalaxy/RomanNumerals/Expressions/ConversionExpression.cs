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
        public class ConversionExpression : IExpression
        {
                private AliasMapper aliasMap;
                private AliasMetalMapper metalMap;
                private IDecimalConverter converter;
                private ExpressionValidationHelper helper;

                public ConversionExpression(AliasMetalMapper metalMap, AliasMapper aliasMap, 
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

                        string[] preIsWords = parts[0].Split(new char[] { ' ' }, 
                                         StringSplitOptions.RemoveEmptyEntries);
                        string[] postIsWords = parts[1].Split(new char[] { ' ' }, 
                                         StringSplitOptions.RemoveEmptyEntries);

                        string sourceMetal = postIsWords.Skip(postIsWords.Length - 1).ToString();
                        string destinationMetal = preIsWords[2];

                        string[] aliases = postIsWords.Take(postIsWords.Length - 1).ToArray();
                        
                        StringBuilder sb = new StringBuilder();

                        //Create Roman Numeral from aliases
                        for (int i = 0; i < aliases.Length - 1; i++)
                        {
                            sb.Append(aliasMap.GetValueForAlias(aliases[i]));
                        }

                        double sourceMetalPrice = metalMap.GetPriceByMetal(sourceMetal);
                        double destinationMetalPrice = metalMap.GetPriceByMetal(destinationMetal);

                        //Convert Roman to Decimal
                        double? totalUnits = converter.ToDecimal(sb.ToString());
                        if (totalUnits.HasValue)
                        {
                                double totalSourceMetal = sourceMetalPrice * totalUnits.Value;
                                Console.WriteLine(String.Format("{0} {1} {2} {3}", parts[1],
                                GlobalConstants.Is, (totalSourceMetal/destinationMetalPrice), 
                                destinationMetal));
                        }
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

                        string[] preIsWords = parts[0].Split(new char[] { ' ' }, 
                                              StringSplitOptions.RemoveEmptyEntries);
                        if (preIsWords.Length < 3) return false;

                        string[] postIsWords = parts[1].Split(new char[] { ' ' }, 
                                               StringSplitOptions.RemoveEmptyEntries);
                        if (postIsWords.Length < 2) return false;

                        return helper.AreWordsValidMetals(preIsWords.Skip(preIsWords.Length - 1).ToArray()) &&
                               helper.AreWordsValidMetals(postIsWords.Skip(postIsWords.Length - 1).ToArray()) && 
                               helper.AreWordsValidAliases(postIsWords.Take(postIsWords.Length -1).ToArray());
                }
                
                #endregion IExpression implementation
    }
}
