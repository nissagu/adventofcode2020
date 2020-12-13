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

            using (StreamReader streamReader = new StreamReader("/Users/Ruth/Projects/Advent_of_Code/Day_7/bagrules.txt"))
            {

                while (streamReader.Peek() >= 0)
                {
                    var input = streamReader.ReadLine();
                    allEntries.Add(input);
                }

            }

            allBags = BagParser.GenerateData(allEntries);
            Console.WriteLine(allBags.Count());

            // part 1
            var color = "shiny gold";
            Console.WriteLine(GetAllOuterBagsForColor(allBags, color));

            // part 2
            var shinyGoldBag = allBags.Find(bag => bag.OuterColor.Equals("shiny gold"));

            var amount = GetAmountOfSpecificBag(allBags, shinyGoldBag);
            Console.WriteLine(amount);

        }

        // returns amount of all the bags inside a specific bag (including the nested inner bags)
        private static int GetAmountOfSpecificBag(List<Bag> allBags, Bag bag)
        {
            var amount = 0;

            // get a list of all inner bags for the committed bag
            var allInnerBags = GetAllInnerBagsForSpecificBag(allBags, bag);

            // if the bag doesn't have any bags inside, just retourn 0 as amount
            if (allInnerBags.Any())
            {
                // go through each of the inner bags
                foreach (var innerBag in allInnerBags)
                {
                    // find out how many bags of the inner bag are inside the committed bag
                    // inner bag's outer color = key in bag's color dictionary
                    var innerBagAmount = bag.ColorDictionary.FirstOrDefault(entry => entry.Key.Equals(innerBag.OuterColor)).Value;

                    // 
                    var innerAmountOfInnerBag = GetAmountOfSpecificBag(allBags, innerBag);

                    //
                    var innerBagProduct = innerBagAmount * innerAmountOfInnerBag;

                    //
                    amount += innerBagProduct + innerBagAmount;
                }
            }

            return amount;
        }

        // return list of the bag objects that a specific bag contains
        private static List<Bag> GetAllInnerBagsForSpecificBag(List<Bag> allBags, Bag bag)
        {

            var allColorsOfInnerBags = new List<string>();
            var allInnerBags = new List<Bag>();

            // first step: add color/s that initial bag contains to color list
            foreach (var entry in bag.ColorDictionary)
            {
                allColorsOfInnerBags.Add(entry.Key);
            }

            // go through all the initial inner colors and find all bags that contain them
            foreach (var color in allColorsOfInnerBags)
            {
                allInnerBags.Add(allBags.Find(bag => bag.OuterColor.Equals(color)));

            }

            return allInnerBags;

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
                var bagEntryColor = allColorContainingBags[index].OuterColor;
                var bagsWithColor = GetColorContainingBags(allBags, bagEntryColor);
                AddBagsToList(bagsWithColor, allColorContainingBags);
            }

            return allColorContainingBags.Count();
        }

        private static List<Bag> AddBagsToList(List<Bag> inputList, List<Bag> outputList)
        {
            foreach (var entry in inputList)
            {
                var colorAlreadyInList = outputList.Find(bag => bag.OuterColor.Equals(entry.OuterColor)) is not null;

                if (!colorAlreadyInList)
                {
                    outputList.Add(entry);
                }
            }

            return outputList;
        }

        private static List<Bag> GetColorContainingBags(List<Bag> allBags, string color)
        {
            return allBags.FindAll(entry => entry.ColorDictionary.ContainsKey(color));

        }
    }
}