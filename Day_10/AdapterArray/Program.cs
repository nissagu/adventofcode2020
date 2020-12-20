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

        private static long FindAmountOfDifferentArrangements(List<int> allEntries)
        {

            var joltingsInAscendingOrder = allEntries.AsEnumerable().OrderBy(number => number).ToList();

            // add values aside from puzzle input to list
            var chargingOutlet = 0;
            joltingsInAscendingOrder.Insert(0, chargingOutlet);

            var maxJoltage = joltingsInAscendingOrder.Max();
            var builtInJoltage = maxJoltage + 3;
            joltingsInAscendingOrder.Insert(joltingsInAscendingOrder.Count, builtInJoltage);

            // only last adapter arrangement is kept as key in the dictionary
            // value is the amount of arrangements that end with the key
            Dictionary<int, long> joltingArrangements = new Dictionary<int, long>();

            foreach (var currentAdapterJolting in joltingsInAscendingOrder)
            {
                //Console.WriteLine($"checking {currentJolting}");

                // there's no entry created
                if (joltingArrangements.Count == 0)
                {

                    joltingArrangements.Add(currentAdapterJolting, 1);
                }
                // there is already at least one entry
                else
                {
                    // go through all last entries in the arrangement of adapters
                    foreach (var currentLastElementJolting in joltingArrangements.Keys.OrderBy(x => x).ToList())
                    {

                        var amountOfCurrentLastElementJolting = joltingArrangements[currentLastElementJolting];
                        // check the difference between the key (aka the arrangement's last adapter value) and the current jolting entry
                        var difference = currentAdapterJolting - currentLastElementJolting;

                        switch (difference)
                        {
                            // if the difference is 3, add the element to the arrangement
                            // adding = switching the key (new last number in the arrangement)
                            case 3:
                                {
                                    joltingArrangements.Add(currentAdapterJolting, amountOfCurrentLastElementJolting);
                                    joltingArrangements.Remove(currentLastElementJolting);
                                    break;
                                }
                            case 0:
                                {
                                    break;
                                }
                            // if the difference is < 3, dynamically add new arrangements
                            case < 3:
                                {
                                    // there is already an arrangement that contains the current adapter's jolting as last element
                                    // there is an arrangement whose last element jolting is compatible with the current adapter's jolting
                                    // add the amount of the found arrangement to the amount of the current adapter's entry
                                    if (joltingArrangements.ContainsKey(currentAdapterJolting))
                                    {
                                        joltingArrangements[currentAdapterJolting] = joltingArrangements[currentAdapterJolting] + amountOfCurrentLastElementJolting;
                                    }
                                    // the current jolting adapter can be added at the end of the arrangements that
                                    // have the current key as last element – therefore the value stays the same
                                    else
                                    {
                                        joltingArrangements.Add(currentAdapterJolting, amountOfCurrentLastElementJolting);
                                    }
                                    break;
                                }
                            // if difference is > 3, the arrangement can be removed (because there won't fit any following jolting)
                            case > 3:
                                {
                                    joltingArrangements.Remove(currentLastElementJolting);
                                    break;
                                }
                        }
                    }


                }

            }

            return joltingArrangements.Values.Sum();

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
