using MerchantGalaxy.Interface;
using MerchantGalaxy.RomanNumerals.RomanValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantGalaxy.RomanNumerals
{
    public class RomanToDecimalConvertor : IDecimalConverter
    {
        Dictionary<string, int> RomanToDecimalMapper;
        List<IRomanValidator> ValidatorList;

        #region Public methods
        public RomanToDecimalConvertor()
        {
            ValidatorList = ValidatorsList();
            RomanDecimalMapper();
        }

        public double? ToDecimal(string input)
        {
            if (!ValidateRomanNumerals(input)) return null;
            return CalculateDecimalValue(input);
        }

        public bool ValidateRomanNumerals(string input)
        {
            bool result = true;

            foreach (var rule in ValidatorList)
            {
                result = rule.Execute(input);
            }
            return result;
        }

        #endregion Public methods


        #region Private methods
        private void RomanDecimalMapper()
        {
            RomanToDecimalMapper = new Dictionary<string, int>();
            RomanToDecimalMapper.Add("I", 1);
            RomanToDecimalMapper.Add("V", 5);
            RomanToDecimalMapper.Add("X", 10);
            RomanToDecimalMapper.Add("L", 50);
            RomanToDecimalMapper.Add("C", 100);
            RomanToDecimalMapper.Add("D", 500);
            RomanToDecimalMapper.Add("M", 1000);
        }

        private List<IRomanValidator> ValidatorsList()
        {
            List<IRomanValidator> ValidatorList = new List<IRomanValidator>();
            ValidatorList.Add(new CanBeRepeatedThreeTimesOnly());
            ValidatorList.Add(new CanNeverBeRepeated());
            ValidatorList.Add(new CanBeSubstractedFrom());
            ValidatorList.Add(new CanSubstactSmallerFromLarger());

            return ValidatorList;
        }
        private double CalculateDecimalValue(string input)
        {
            double current = 0, next = 0, total = 0;
            try
            {
                for (int i = 0; i < input.Length; i++)
                {
                    //gets value of specified key
                    current = RomanToDecimalMapper[input[i].ToString()];

                    if (i < input.Length - 1)
                        next = RomanToDecimalMapper[input[i + 1].ToString()];

                    if (current < next)
                    {
                        total += next - current;
                        i++;
                    }
                    else
                    {
                        total += current;
                    }

                    next = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return total;
        }
        #endregion Private methods

    }
}
