using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Problem26
{
    [TestClass]
    public class ReciprocalCycles
    {
        [TestMethod]
        public void FindLongestSequence()
        {
            Result? longestSequence = null;
            int best = 0;
            for(int i = 2; i <= 1_000; i++)
            {
                if (GetRepeatingSequence(i) is { } result)
                {
                    Console.WriteLine($"Found 1/{i} with sequence L '{result.SequenceLength}' {result.Value}");
                    if (result.SequenceLength > (longestSequence?.SequenceLength ?? 0))
                    {
                        longestSequence = result;
                        best = i;
                    }
                }
                
            }
            Console.WriteLine($"Best 1/{best} with length '{longestSequence?.SequenceLength ?? 0}' {longestSequence?.Value}");
        }

        private static Result? GetRepeatingSequence(int divisor)
        {
            //Long division: https://www.mathsisfun.com/long_division.html
            //Dividend is 1.000...0000
            //Assumption divisor will always be great that dividend

            string result = "";
            List<int> seenDividends = new();

            int dividend = 1;
            //Dividend is the thingy under the L
            //Divisor is the thingy left of the L
            while(true)
            {
                int remainder = dividend % divisor;
                int singleResult = dividend / divisor;

                result += singleResult;
                //Because dividend starts at 1. The decimal separator goes after the first digit
                if (result.Length == 1) result += ".";

                //No repeating sequence
                if (remainder == 0) return null;
                
                int partialResult = divisor * singleResult;
                dividend -= partialResult;
                dividend *= 10;

                int sequenceStartIndex = seenDividends.IndexOf(dividend);
                if (sequenceStartIndex >= 0)
                {
                    //The +2 is to account the leading 0 and decimal separator
                    result = $"{result[..(sequenceStartIndex + 2)]}({result[(sequenceStartIndex + 2)..]})";
                    return new Result(result, seenDividends.Count - sequenceStartIndex);
                }
                seenDividends.Add(dividend);
            }
        }
    }

    public record Result(string Value, int SequenceLength) { }
}
