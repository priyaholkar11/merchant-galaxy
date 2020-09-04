using MerchantGalaxy.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxy.RomanNumerals.RomanValidators
{
    class CanNeverBeSubstracted : IRomanValidator
    {
        public bool Execute(string input)
        {
            if (input.Length < 3) return true;

            for (int i = input.Length - 1; i >= 2; i--)
            {
                if (input[i] == 'D' && !RomanNotation.IsSmaller(input[i - 1].ToString(), input[i].ToString()) &&
                    input[i] == 'L' && !RomanNotation.IsSmaller(input[i - 1].ToString(), input[i].ToString()) &&
                    input[i] == 'V' && !RomanNotation.IsSmaller(input[i - 1].ToString(), input[i].ToString()))
                {
                    Console.WriteLine("CanNeverBeSubstracted Rule has been violated");
                    return false;
                }
            }

            return true;
        }
    }
}




