using MerchantGalaxy.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxy.RomanNumerals.RomanValidators
{
    public class CanBeRepeatedThreeTimesOnly : IRomanValidator
    {
        public bool Execute(string input)
        {
            bool result = (input.Length < 4) || !(input.Contains("IIII") || 
                                                  input.Contains("XXXX") || 
                                                  input.Contains("CCCC") || 
                                                  input.Contains("MMMM"));

            if (!result) { Console.WriteLine("CanBeRepeatedThreeTimesOnly Rule has been violated"); }

            return result;
        }
    }
}
