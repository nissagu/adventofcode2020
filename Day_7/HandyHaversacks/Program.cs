using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HandyHaversacks
{
    class Program
    {
        static void Main(string[] args)
        {

            List<String> allEntries = new List<string>();
            List<Bag> allBags;

            using (StreamReader streamReader = new StreamReader("/Users/Ruth/Projects/Advent_of_Code/Day_7/test.txt"))
            {

                while (streamReader.Peek() >= 0)
                {
                    var input = streamReader.ReadLine();
                    allEntries.Add(input);
                }

            }

            allBags = BagParser.GenerateData(allEntries);
            Console.WriteLine(allBags.Count());

            var color = "shiny gold";
            Console.WriteLine(GetAllOuterBagsForColor(allBags, color));

            Console.WriteLine(GetSumOfContainingBags(allBags, color));

        }

        private static int GetSumOfContainingBags(List<Bag> allBags, string color)
        {
            var sum = 0;

            return sum;
        }

        private static int GetAllOuterBagsForColor(List<Bag> allBags, string color)
        {

            var allColorContainingBags = new List<Bag>();

            // first step: get all bags that directly contain the colored bag
            var colorDirectlyContainingBags = GetColorContainingBags(allBags, color);
            allColorContainingBags = AddBagsToList(colorDirectlyContainingBags,allColorContainingBags);

            // go through all the searched for color containing bags and find all the bags that containing those bags
            for (int index = 0; index < allColorContainingBags.Count(); index++)
            {
                var bagEntryColor = allColorContainingBags[index].Color;
                var bagsWithColor = GetColorContainingBags(allBags, bagEntryColor);
                AddBagsToList(bagsWithColor, allColorContainingBags);
            }

            return allColorContainingBags.Count();
        }

        private static List<Bag> AddBagsToList(List<Bag> inputList, List<Bag> outputList)
        {
            foreach (var entry in inputList)
            {
                var colorAlreadyInList = outputList.Find(bag => bag.Color.Equals(entry.Color)) is not null;

                if (!colorAlreadyInList)
                {
                    outputList.Add(entry);
                }
            }

            return outputList;
        }

        private static List<Bag> GetColorContainingBags(List<Bag> allBags, string color)
        {
            return allBags.FindAll(entry => entry.ColorDictionary.Contains(color));

        }
    }
}