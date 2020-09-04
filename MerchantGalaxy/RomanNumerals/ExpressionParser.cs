
using MerchantGalaxy.Common;
using MerchantGalaxy.Interface;
using MerchantGalaxy.RomanNumerals.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantGalaxy.RomanNumerals
{
    class ExpressionParser
    {
        private List<IExpression> expressions;
        private ExpressionValidationHelper helper;

        public ExpressionParser(AliasMapper aliasMap, IDecimalConverter converter, 
                                AliasMetalMapper aliasMetalMapper)
        {
            helper = new ExpressionValidationHelper(aliasMap, aliasMetalMapper);
            expressions = GetExpressions(aliasMap, converter, aliasMetalMapper, helper);
        }

        public void Parse(string input)
        {
            var matchingExpression = expressions.FirstOrDefault(e => e.Match(input));
            if (matchingExpression == null)
                Console.WriteLine("I have no idea what you are talking about");
            else
                matchingExpression.Execute(input);
        }

        public static List<IExpression> GetExpressions(AliasMapper aliasMap, 
                                                       IDecimalConverter converter,
                                                       AliasMetalMapper aliasMetalMap, 
                                                       ExpressionValidationHelper helper)
        {
            List<IExpression> expressions = new List<IExpression>();
            expressions.Add(new AliasExpressionType(aliasMap));
            expressions.Add(new MetalExpressionType(aliasMetalMap, aliasMap, converter, helper));
            expressions.Add(new AliasExpressionTypeQuestion(aliasMap, converter, helper));
            expressions.Add(new MetalExpressionTypeQuestion(aliasMetalMap, aliasMap, converter, helper));
            expressions.Add(new ConversionExpression(aliasMetalMap, aliasMap, converter, helper));

            return expressions;
        }
    }
}
