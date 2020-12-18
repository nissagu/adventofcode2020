using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdapterArray
{
    class Program
    {
        static void Main(string[] args)
        {
            var allEntries = new List<int>();

            using (StreamReader streamReader = new StreamReader("/Users/Ruth/Projects/Advent_of_Code/Day_10/input.txt"))
            {

                while (streamReader.Peek() >= 0)
                {
                    var input = Convert.ToInt32(streamReader.ReadLine());
                    allEntries.Add(input);
                }

            }

            Console.WriteLine(FindOneAndThreeJoltageDifferences(allEntries));
            Console.WriteLine(FindAmountOfDifferentArrangements(allEntries));

        }

        private static int FindAmountOfDifferentArrangements(List<int> allEntries)
        {
            var arrangementAmount = 0;

            var joltingsInAscendingOrder = allEntries.AsEnumerable().OrderBy(number => number).ToList();

            // evaluate the following number
            // if difference = 3 then there's only one option for the arrangement
            // if difference < 3 ( 1 || 2) there's multiple ways

            return arrangementAmount;
        }

        private static int FindOneAndThreeJoltageDifferences(List<int> allEntries)
        {

            // identity function used as comparator for ascending order
            var joltingsInAscendingOrder = allEntries.AsEnumerable().OrderBy(number => number).ToList();

            // add values aside from puzzle input to list
            var chargingOutlet = 0;
            joltingsInAscendingOrder.Insert(0, chargingOutlet);

            var maxJoltage = joltingsInAscendingOrder.Max();
            var builtInJoltage = maxJoltage + 3;
            joltingsInAscendingOrder.Insert(joltingsInAscendingOrder.Count, builtInJoltage);

            var threeJoltDifferenceAmount = 0;
            var twoJoltDifferenceAmount = 0;
            var oneJoltDifferenceAmount = 0;

            // Count - 1 so that the entry before the last one is checked
            for (int index = 0; index < joltingsInAscendingOrder.Count - 1; index++)
            {
                var currentEntry = joltingsInAscendingOrder[index];
                var nextEntry = joltingsInAscendingOrder[index + 1];

                switch (nextEntry - currentEntry)
                {
                    case 1:
                        {
                            oneJoltDifferenceAmount++;
                            break;
                        }
                    case 2:
                        {
                            twoJoltDifferenceAmount++;
                            break;
                        }
                    case 3:
                        {
                            threeJoltDifferenceAmount++;
                            break;
                        }
                }
            }

            var differenceSum = oneJoltDifferenceAmount * threeJoltDifferenceAmount;

            return differenceSum;
        }

    }
}
