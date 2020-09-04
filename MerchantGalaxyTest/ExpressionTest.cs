using MerchantGalaxy.Common;
using MerchantGalaxy.RomanNumerals;
using MerchantGalaxy.RomanNumerals.Expressions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace MerchantGuideToGalaxyTest
{
    public class ExpressionTest
    {
        [Fact]
        public void AliasExpressionTest()
        {
            AliasMapper aliasMap = new AliasMapper();
            AliasExpressionType expression = new AliasExpressionType(aliasMap);
            Assert.True(expression.Match("glob is I"));
            Assert.False(expression.Match("glob is N"));
            expression.Execute("glob is I");
            Assert.True(aliasMap.Exists("glob"));
            Assert.Equal("I", aliasMap.GetValueForAlias("glob"));
        }

        [Fact]
        public void AliasMetalExpressionTest()
        {
            AliasMapper aliasMap = new AliasMapper();
            RomanToDecimalConvertor converter = new RomanToDecimalConvertor();
            AliasMetalMapper metalMap = new AliasMetalMapper();
            aliasMap.AddAlias("glob", "I");
            aliasMap.AddAlias("pish", "X");
            ExpressionValidationHelper helper = new ExpressionValidationHelper(aliasMap, metalMap);
            MetalExpressionType expression = new MetalExpressionType(metalMap, aliasMap, converter, helper);
            expression.Execute("pish glob Iron is 110 Credits");
            Assert.True(metalMap.Exists("Iron"));
            Assert.Equal<double>(10, metalMap.GetPriceByMetal("Iron"));
            expression.Execute("glob pish Iron is 6300 Credits");
            Assert.Equal<double>(700, metalMap.GetPriceByMetal("Iron"));
        }
    }
}
