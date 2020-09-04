using MerchantGalaxy.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxy.RomanNumerals.RomanValidators
{
    public class CanBeSubstractedFrom : IRomanValidator
    {
        public bool Execute(string input)
        {
            bool result = (input.Length < 2) ||
                                !(input.Contains("IL") ||
                                  input.Contains("IC") ||
                                  input.Contains("ID") ||
                                  input.Contains("IM") ||
                                  input.Contains("XD") ||
                                  input.Contains("XM"));

            if (!result) { Console.WriteLine("CanBeSubstractedFrom Rule has been violated"); }

            return result;
        }
    }
}
