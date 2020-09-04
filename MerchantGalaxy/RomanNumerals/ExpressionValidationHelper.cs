using MerchantGalaxy.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxy.RomanNumerals
{
    public class ExpressionValidationHelper
    {
        private AliasMapper aliasMap;
        private AliasMetalMapper aliasMetalMapp;

        public ExpressionValidationHelper(AliasMapper aliasMap, AliasMetalMapper aliasMetalMapp)
        {
            this.aliasMap = aliasMap;
            this.aliasMetalMapp = aliasMetalMapp;
        }

        public bool AreWordsValidAliases(string[] words)
        {
            foreach (string word in words) { if (!aliasMap.Exists(word)) { return false; } }
            return true;
        }

        public bool AreWordsValidMetals(string[] words)
        {
            foreach (string word in words)
            {
                if (!aliasMetalMapp.Exists(word))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
