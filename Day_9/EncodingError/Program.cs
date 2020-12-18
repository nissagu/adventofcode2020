using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EncodingError
{
    class Program
    {
        static void Main(string[] args)
        {
            List<long> allEntries = new List<long>();

            using (StreamReader streamReader = new StreamReader("/Users/Ruth/Projects/Advent_of_Code/Day_9/input.txt"))
            {

                while (streamReader.Peek() >= 0)
                {
                    var input = Convert.ToInt64(streamReader.ReadLine());
                    allEntries.Add(input);
                }

            }
            var firstNotSumEntry = FindFirstNotSumEntry(allEntries, 25);
            Console.WriteLine(firstNotSumEntry);
            Console.WriteLine(FindEncryptionWeakness(allEntries, firstNotSumEntry));

        }

        private static long FindEncryptionWeakness(List<long> entries, long searchedForSum)
        {
            var isSumFound = false;
            var summandsList = new List<long>();

            // outer loop for the first summand as starting point
            // loops through (all) entries
            for (int index = 0; !isSumFound && index < entries.Count; index++)
            {

                var result = FindCombinationOfSums(searchedForSum, entries, index);

                if (result.Item2)
                {
                    isSumFound = true;
                    summandsList = result.Item1;
                }

            }

            long minValue = summandsList.Min();
            long maxValue = summandsList.Max();
            long encryptionWeaknessSum = minValue + maxValue;

            return encryptionWeaknessSum;

        }


        private static List<long> ReturnRemainingEntries(List<long> entries, int startIndex)
        {
            var remainingEntries = new List<long>();

            for (int index = startIndex; index < entries.Count; index++)
            {
                remainingEntries.Add(entries[index]);
            }

            return remainingEntries;
        }

        private static Tuple<List<long>, bool> FindCombinationOfSums(long searchedForSum, List<long> allEntries, int startIndex)
        {

            var remainingEntries = ReturnRemainingEntries(allEntries, startIndex);

            var isSumFound = false;

            List<long> summands = new List<long>();

            for (int index = 0; !isSumFound && index < remainingEntries.Count; index++)
            {
                var currentEntry = remainingEntries[index];
                var currentSum = ComputeSummandsSum(summands);
                currentSum += currentEntry;

                if (currentSum <= searchedForSum)
                {
                    summands.Add(currentEntry);

                    if (currentSum == searchedForSum)
                    {
                        isSumFound = true;
                    }
                }

            }

            return new Tuple<List<long>, bool>(summands, isSumFound);

        }

        private static long ComputeSummandsSum(List<long> summandsList)
        {
            var sum = 0L;

            foreach (var entry in summandsList)
            {
                sum += entry;
            }

            return sum;
        }

        private static long FindFirstNotSumEntry(List<long> entries, int preambleAmount)
        {
            var firstNotSum = 0L;
            var firstNotSumFound = false;

            var firstEntryToCheckIndex = preambleAmount + 1;

            // outer loop that goes through all entries in the input list
            for (int index = firstEntryToCheckIndex; !firstNotSumFound && index < entries.Count; index++)
            {

                var currentEntry = entries[index];

                // get all preamble entries regarding the current entry's position in the list
                var preambleEntries = GetPreambleEntriesAsList(entries, preambleAmount, index);

                // check if the current entry is somehow a sum from the preamble list entries
                var isSumFromPreambleEntries = EvalSumForPreambleEntries(preambleEntries, currentEntry);

                // set found flag to true to stop loop and return found "notsum"
                if (!isSumFromPreambleEntries)
                {
                    firstNotSum = currentEntry;
                    firstNotSumFound = true;
                }

            }


            return firstNotSum;
        }

        private static bool EvalSumForPreambleEntries(List<long> entries, long sum)
        {
            var equalsSum = false;

            for (int index = 0; !equalsSum && index < entries.Count; index++)
            {

                var firstNumber = entries[index];

                for (int innerIndex = (index + 1); innerIndex < (entries.Count - index); innerIndex++)
                {

                    var secondNumber = entries[innerIndex];

                    if ((firstNumber + secondNumber) == sum)
                    {
                        equalsSum = true;
                    }

                }

            }

            return equalsSum;

        }

        private static List<long> GetPreambleEntriesAsList(List<long> allEntries, int preambleAmount, int startIndex)
        {
            var firstToCopyIndex = startIndex - preambleAmount;
            var lastToCopyIndex = startIndex + preambleAmount;

            var preambleList = new List<long>();

            for (int index = firstToCopyIndex; index < lastToCopyIndex; index++)
            {
                preambleList.Add(allEntries[index]);
            }

            return preambleList;
        }
    }
}
