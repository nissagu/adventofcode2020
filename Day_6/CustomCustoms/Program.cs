using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CustomCustoms
{
    class Program
    {
        static void Main(string[] args)
        {
            var commonSum = 0;
            MatchCollection answers;
            List<char> letters = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            // extract all passport entries from the input file
            using (StreamReader streamReader = new StreamReader("/Users/Ruth/Projects/Advent_of_Code/Day_6/customsanswers.txt"))
            {
                var input = streamReader.ReadToEnd();


                Regex splitAnswersPattern = new Regex(@"(?s)(.*?)(?:\n\n|\z)");


                answers = splitAnswersPattern.Matches(input);

            }

            // get the sum of all the letters used in all the answer's lines (but count the letter only once)
            SumUpAllLetters(answers, letters);

            // get the sum of all the common letters used in all the answer's lines
            commonSum += SumUpCommonLetters(answers, letters);
            Console.WriteLine(commonSum);


        }

        private static int SumUpCommonLetters(MatchCollection answers, List<char> letters)
        {

            var sum = 0;

            foreach (var entry in answers)
            {
                var formattedEntry = FormatEntry(entry);

                if (formattedEntry != null && formattedEntry.Count > 0)
                {
                    var firstLineLetters = formattedEntry[0].ToString().ToCharArray();

                    // if answerLinesWithoutNewLines contains only one line, add the chars count to sum
                    if (formattedEntry.Count == 1)
                    {
                        sum += firstLineLetters.Count();
                    }
                    else
                    {
                        // go through all letters of the first line
                        foreach (var letter in firstLineLetters)
                        {
                            var isInAllLines = true;

                            // check if the character appears in the entry's other lines
                            // start with index 1 because index 0 is the "model" line
                            for (var lineIndex = 1; lineIndex < formattedEntry.Count() && isInAllLines; lineIndex++)
                            {
                                // if one line doesn't contain the letter, end checking the other lines
                                if (!formattedEntry[lineIndex].Contains(letter))
                                {
                                    isInAllLines = false;
                                }

                            }

                            // if the loop through the lines is done and isAllLines is true, add 1 to sum
                            if (isInAllLines)
                            {
                                sum += 1;
                            }

                        }
                    }
                }


            }

            return sum;

        }

        private static List<String> FormatEntry(object entry)
        {
            var answer = entry.ToString();
            var answerLines = answer.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            List<String> answerLinesWithoutNewLines = RemoveEmptyLines(answerLines);
            return answerLinesWithoutNewLines;
        }

        private static List<String> RemoveEmptyLines(String[] input)
        {

            var newList = new List<String>();

            // remove emtpy entries from array
            for (int index = 0; index < input.Length; index++)
            {
                if (!input[index].Equals(String.Empty))
                {
                    newList.Add(input[index].ToString());
                }
            }

            return newList;
        }

        private static void SumUpAllLetters(MatchCollection answers, List<char> letters)
        {
            var sum = 0;

            foreach (var entry in answers)
            {
                var allAnswersString = entry.ToString().ToCharArray();
                // count every letter in the string only once
                sum += CountDifferentLettersInAnswer(allAnswersString, letters);

            }

            Console.WriteLine(sum);
        }

        private static int CountDifferentLettersInAnswer(char[] allAnswersString, List<char> letters)
        {

            var sum = 0;

            foreach (var letter in letters)
            {
                if (allAnswersString.Count(l => l == letter) > 0)
                {
                    sum += 1;
                }
            }

            return sum;
        }
    }
}
