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

            using (StreamReader streamReader = new StreamReader("/Users/Ruth/Projects/Advent_of_Code/Day_10/test2.txt"))
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

            // add values aside from puzzle input to list
            var chargingOutlet = 0;
            joltingsInAscendingOrder.Insert(0, chargingOutlet);

            var maxJoltage = joltingsInAscendingOrder.Max();
            var builtInJoltage = maxJoltage + 3;
            joltingsInAscendingOrder.Insert(joltingsInAscendingOrder.Count, builtInJoltage);

            var joltingArrangements = new List<List<int>>();

            foreach (var currentJolting in joltingsInAscendingOrder)
            {
                // there's no list created, so the first entry creates the first list
                if (joltingArrangements.Count == 0)
                {
                    var firstArrangement = new List<int>() { currentJolting };
                    joltingArrangements.Add(firstArrangement);
                }
                // there is already at least one list
                else
                {

                    for (int index = 0; index < joltingArrangements.Count; )
                    {

                        var currentArrangement = joltingArrangements[index];
                        // check the difference between the list's last entry and the current jolting entry
                        var lastEntryInList = currentArrangement.LastOrDefault();
                        var difference = currentJolting - lastEntryInList;

                        switch (difference)
                        {
                            case 3:
                                {// if the difference is 3, add the element to the current list
                                    currentArrangement.Add(currentJolting);
                                    index++;
                                    break;
                                }
                            case 0:
                                {
                                    index++;
                                    break;
                                }
                            case < 3:
                                // if the difference is < 3, dynamically add new arrangements 
                                {
                                    var newJoltingArrangement = new List<int>();
                                    newJoltingArrangement.AddRange(currentArrangement);
                                    newJoltingArrangement.Add(currentJolting);
                                    joltingArrangements.Add(newJoltingArrangement);
                                    index++;
                                    break;
                                }
                            case > 3:
                                {
                                    index++;
                                    break;
                                }
                        }
                    }


                }

            }

            // because the whole number of possible arrangements was built without the rule that the built-in
            // joltage must be the last element, we reduce the result list here regarding this predicate
            var reducedArrangements =
                joltingArrangements.Where(arrangement => arrangement.Last() == builtInJoltage).ToList();

            arrangementAmount = reducedArrangements.Count();

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
