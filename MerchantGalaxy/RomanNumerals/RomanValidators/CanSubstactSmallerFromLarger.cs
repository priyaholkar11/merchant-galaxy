using MerchantGalaxy.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxy.RomanNumerals.RomanValidators
{
    public class CanSubstactSmallerFromLarger : IRomanValidator
    {
        public bool Execute(string input)
        {
            if (input.Length < 3) return true;

            for (int i = input.Length - 1; i >= 2; i--)
            {
                if (RomanNotation.IsSmaller(input[i - 1].ToString(), input[i].ToString()) &&
                    RomanNotation.IsSmaller(input[i - 2].ToString(), input[i].ToString()))
                {
                    Console.WriteLine("CanSubstactSmallerFromLarger Rule has been violated");
                    return false;
                }
            }

            return true;
        }
    }
}
