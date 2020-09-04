using MerchantGalaxy.Interface;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MerchantGalaxy.RomanNumerals.RomanValidators
{
    public class CanNeverBeRepeated : IRomanValidator
    {
        public bool Execute(string input)
        {
            bool result = (input.Length < 2) ||
                               (input.Count(c => c == 'D') <= 1 &&
                                input.Count(c => c == 'L') <= 1 &&
                                input.Count(c => c == 'V') <= 1);

            if (!result) { Console.WriteLine("CanNeverBeRepeated Rule has been violated"); }

            return result;

        }
    }
}
