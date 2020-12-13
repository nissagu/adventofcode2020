using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HandyHaversacks
{
    public class BagParser
    {
        public static List<Bag> GenerateData(List<string> allEntries)
        {

            List<Bag> bags = new List<Bag>();

            foreach (var entry in allEntries)
            {
                var bag = new Bag();

                var splitEntry = SplitOuterFromInnerBags(entry);

                // left side = the bag's color
                bag.OuterColor = GetOuterBagColor(splitEntry[0]);

                // right side = the colors of bags inside the bag
                bag.ColorDictionary = GetInnerBagColorDictionary(splitEntry[1]);

                bag.ContainingBagsSum = GetContainaingBagsSum(bag);
                
                bags.Add(bag);
            }

            return bags;

        }

        private static int GetContainaingBagsSum(Bag bag)
        {
            var sum = 0;

            foreach (var value in bag.ColorDictionary.Values)
            {
                sum += Int32.Parse(value.ToString());
            }

            return sum;

        }

        private static Dictionary<string, int> GetInnerBagColorDictionary(string entry)
        {
            var dictionary = new Dictionary<string, int>();

            // there's no comma as delimiter
            // can contain only 1 other bag or none
            if (!entry.Contains(","))
            {
                dictionary = CreateDictionaryFromOneBagEntry(entry);
            }
            // can contain multiple other bags
            // there are commata as delimiters
            else
            {
                dictionary = CreateDictionaryFromMultipleBagEntries(entry);
            }

            return dictionary;
        }

        private static Dictionary<string, int> CreateDictionaryFromMultipleBagEntries(string entry)
        {
            // multiple entries have always the pattern: digit word word comma/dot
            var pattern = new Regex(@"\d* \w* \w* \w*(?=[\,\.])");

            var dictionary = new Dictionary<string, int>();

            // get all parts left from the commata
            var innerBagEntries = pattern.Matches(entry);

            // go through all inner bags and generate dictionary entries
            foreach (var innerBagEntry in innerBagEntries)
            {
                var entryAsString = innerBagEntry.ToString();

                // only contains 1 bag
                // can't look for "bag" because it would also be true for bags
                if (!entryAsString.Contains("bags"))
                {
                    dictionary = CreateDictionaryWithEntryForBag(dictionary, entryAsString);
                }
                // contains more bags
                else
                {
                    dictionary = CreateDictionaryWithEntryForBags(dictionary, entryAsString);
                }
            }

            return dictionary;
        }

        private static Dictionary<string, int> CreateDictionaryFromOneBagEntry(string entry)
        {
            // key = color of inside bag
            // value = possible amount of this inside bag

            var dictionary = new Dictionary<string, int>();

            if (!entry.Contains("no other bags"))
            {
                if (entry.Contains("bags"))
                {
                    dictionary = CreateDictionaryWithEntryForBags(dictionary, entry);
                }
                // it contains "bag"
                else
                {
                    dictionary = CreateDictionaryWithEntryForBag(dictionary, entry);

                }
            }
            // can contain "no other bags" – if that's the case, do nothing
            else
            {
                //dictionary.Add("none", 0);

            }

            return dictionary;
        }

        private static Dictionary<string, int> CreateDictionaryWithEntryForBag(Dictionary<string, int> dictionary, string entry)
        {
            var bagEntry = entry.Split("bag", StringSplitOptions.TrimEntries);
            var value = GetAmountOfBags(bagEntry);
            var key = GetColorOfBags(bagEntry);
            dictionary.Add(key, value);

            return dictionary;
        }

        private static Dictionary<string, int> CreateDictionaryWithEntryForBags(Dictionary<string, int> dictionary, string entry)
        {
            var bagEntry = entry.Split("bags", StringSplitOptions.TrimEntries);
            var value = GetAmountOfBags(bagEntry);
            var key = GetColorOfBags(bagEntry);
            dictionary.Add(key, value);

            return dictionary;
        }

        private static string GetColorOfBags(string[] bagEntry)
        {
            return bagEntry[0].Substring(2);
        }

        private static int GetAmountOfBags(string[] bagEntry)
        {
            return Convert.ToInt32(bagEntry[0].Substring(0, 1));
        }

        private static string GetOuterBagColor(string entry)
        {
            // input is always in the format "color color bags"
            var colorPart = entry.Split("bags", StringSplitOptions.TrimEntries);
            return colorPart[0];
        }

        static string[] SplitOuterFromInnerBags(string entry)
        {
            return entry.Split("contain");
        }

    }
}
