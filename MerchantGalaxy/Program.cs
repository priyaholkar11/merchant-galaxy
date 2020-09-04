using MerchantGalaxy.Common;
using MerchantGalaxy.Interface;
using MerchantGalaxy.RomanNumerals;
using System;
using System.IO;

namespace MerchantGuideToGalaxyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            string path = "../../input.txt";

            string readText = File.ReadAllText(path);
            string[] lines = readText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(readText);
            Console.WriteLine();
            AliasMapper aliasMapper = new AliasMapper();
            IDecimalConverter converter = new RomanToDecimalConvertor();
            AliasMetalMapper metalMap = new AliasMetalMapper();
            ExpressionParser parser = new ExpressionParser(aliasMapper, converter, metalMap);
            foreach (string line in lines)
            {
                parser.Parse(line);
            }
            Console.ReadLine();
        }
    }
}
