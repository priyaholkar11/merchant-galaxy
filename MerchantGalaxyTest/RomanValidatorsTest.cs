using MerchantGalaxy.RomanNumerals.RomanValidators;
using System;
using Xunit;

namespace MerchantGuideToGalaxyTest
{
    public class RomanValidatorsTest
    {
        [Fact]
        public void TestCanNeverBeRepeated()
        {
            CanNeverBeRepeated rule1 = new CanNeverBeRepeated();
            Assert.False(rule1.Execute("MDDCI"));
            Assert.False(rule1.Execute("MLLLDC"));
            Assert.False(rule1.Execute("MVVVCI"));
            Assert.True(rule1.Execute("MMDC"));
            Assert.True(rule1.Execute("MDCC"));
        }

        [Fact]
        public void TestCanBeRepeatedThreeTimesOnly()
        {
            CanBeRepeatedThreeTimesOnly rule2 = new CanBeRepeatedThreeTimesOnly();
            Assert.False(rule2.Execute("MXXXXCI"));
            Assert.False(rule2.Execute("MMMMDC"));
            Assert.False(rule2.Execute("MCCCCI"));
            Assert.False(rule2.Execute("MMDCIIII"));
            Assert.True(rule2.Execute("MXXXDXCC"));
            Assert.True(rule2.Execute("MMMDMCC"));
            Assert.True(rule2.Execute("MDMCCCMC"));
            Assert.True(rule2.Execute("MDMIIIXI"));
        }

        [Fact]
        public void TestSingleSubtraction()
        {
            CanSubstactSmallerFromLarger rule3 = new CanSubstactSmallerFromLarger();
            Assert.False(rule3.Execute("IIX"));
            Assert.False(rule3.Execute("CCM"));
            Assert.True(rule3.Execute("XXIV"));
        }


        [Fact]
        public void TestCanNeverBeSubstracted()
        {
            CanBeSubstractedFrom rule4 = new CanBeSubstractedFrom();
            Assert.False(rule4.Execute("CIL"));
            Assert.False(rule4.Execute("MXD"));
            Assert.True(rule4.Execute("XIX"));
        }
    }
}
