using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TobogganTrajectory
{
    class Program
    {
        static void Main(string[] args)
        {

            var travelFile = File.ReadLines("/Users/Ruth/Projects/Advent_of_Code/Day_3/map.txt");
            //var testFile = File.ReadLines("/Users/Ruth/Projects/Advent_of_Code/Day_3/testmap.txt");
            var rowCount = travelFile.Count();
            var columnCount = travelFile.First().Length;
            var walk = new List<(int, int)> { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2)};

            var map = TransferFileToMap(travelFile, rowCount, columnCount);

            var result = 1l;

            foreach (var entry in walk)
            {

                result *= CountTreesInSlope(map, rowCount, columnCount, entry.Item1, entry.Item2);

            }

            Console.WriteLine($"Result: {result}");

        }

        static long CountTreesInSlope(char[,] map, int rowCount, int columnCount, int rightSteps, int downSteps)
        {
            var treeCount = 0;

            var column = 0;

            for (int row = 0; row < rowCount; row += downSteps)
            {

                // get real column (when column is greater than input length)
                var realColumnIndex = column % (columnCount);

                if (map[realColumnIndex, row].Equals('#'))
                {
                    treeCount++;
                }

                column += rightSteps;

            }

            return treeCount;
        }

        static char[,] TransferFileToMap(IEnumerable<string> travelFile, int rowCount, int columnCount)
        {

            var mapArray = new char[columnCount, rowCount];
            var rowIndex = 0;

            foreach (string row in travelFile)
            {

                var columnIndex = 0;

                foreach (char column in row)
                {

                    mapArray[columnIndex, rowIndex] = column;

                    columnIndex++;

                }

                rowIndex++;
            }

            // test output
            //for (int index = 0; index < columnCount; index++)
            //{
            //    Console.Write(mapArray[index, 0]);
            //}

            return mapArray;

        }
    }
}
