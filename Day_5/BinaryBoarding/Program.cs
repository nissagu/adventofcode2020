using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryBoarding
{
    class Program
    {
        static void Main(string[] args)
        {

            List<SeatingCode> seatingCodes = new List<SeatingCode>();

            using (StreamReader streamReader = new StreamReader("/Users/Ruth/Projects/Advent_of_Code/Day_5/boardingcodes.txt"))
            {
                while (streamReader.Peek() >= 0)
                {
                    var input = streamReader.ReadLine();
                    seatingCodes.Add(new SeatingCode(input));
                }

            }

            foreach (var entry in seatingCodes)
            {
            
                entry.Row = EvalRow(entry);
                entry.Column = EvalColumn(entry);
                entry.Id = EvalId(entry);

            }

            // evaluate highest Id in the list

            var maxId = 0;

            foreach (var entry in seatingCodes)
            {
                maxId = Math.Max(maxId, entry.Id);
            }

            // find missing seat id

            var idList = new List<int>();

            foreach (var entry in seatingCodes)
            {
                idList.Add(entry.Id);
            }

            // to get minimum id
            idList.Sort();

            var missingNumbersList = Enumerable.Range(46, 915).Except(idList);

            Console.WriteLine(missingNumbersList.First());
        }

        private static int EvalId(SeatingCode entry)
        {
            return ((entry.Row * 8) + entry.Column);
        }

        private static int EvalColumn(SeatingCode entry)
        {

            var columnLetters = entry.Code[7..];
            string binaryString = String.Empty;

            // create binary representation
            foreach (var letter in columnLetters)
            {
                if (letter == 'L')
                {
                    binaryString += "0";
                }
                else
                {
                    binaryString += "1";
                }

            }

            // convert binary number into decimal number
            var result = Convert.ToInt32(binaryString, 2);

            return result;
        }

        private static int EvalRow(SeatingCode entry)
        {
            var rowLetters = entry.Code[0..7];
            string binaryString = String.Empty;

            // create binary representation
            foreach (var letter in rowLetters)
            {
                if (letter == 'F')
                {
                    binaryString += "0";
                }
                else
                {
                    binaryString += "1";
                }

            }

            // convert binary number into decimal number
            var result = Convert.ToInt32(binaryString, 2);

            return result;
        }
    }
}
