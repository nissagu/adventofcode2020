using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SeatingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("/Users/Ruth/Projects/Advent_of_Code/Day_11/input.txt");
            var rowCount = input.Count();
            var columnCount = input.First().Length;
            var seatingMap = TransferFileToMap(input, rowCount, columnCount);

            Console.WriteLine(CountOccupiedSeats(seatingMap,rowCount, columnCount));
        }

        private static long CountOccupiedSeats(char[,] input, int rowCount, int columnCount)
        {
            var seatingMapAfterApplyingSeatingRules = ApplySeatingRulesToMap(input, rowCount, columnCount);

            var amountOfSeats = 0L;

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    var currentEntry = seatingMapAfterApplyingSeatingRules[columnIndex, rowIndex];
                    if (currentEntry.Equals('#'))
                    {
                        //Console.WriteLine($"{columnIndex},{rowIndex} – seats: {amountOfSeats}");
                        amountOfSeats++;
                    }
                }
            }

            return amountOfSeats;
        }

        private static char[,] ApplySeatingRulesToMap(char[,] seatingMap, int rowCount, int columnCount)
        {
            // make copy of seating map

            // go through each position of the seating map and check if rules apply
            // if they apply, change the current field

            // after changing the whole map, check if the rows are the same as in the copy
            // if the first ist different -> stop checking further
            // replace the copy of seating map with the current version

            // start rule applying loop again

            var currentSeatingMap = seatingMap;
            var changedSeatingMap = new char[columnCount, rowCount];

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    var currentEntry = currentSeatingMap[columnIndex, rowIndex];

                    if (currentEntry != '.')
                    {
                        changedSeatingMap[columnIndex, rowIndex] = CheckAndChangeSeatingStatus(currentEntry, currentSeatingMap, columnIndex, rowIndex, columnCount, rowCount);
                    }
                    else
                    {
                        changedSeatingMap[columnIndex, rowIndex] = '.';
                    }
       
                }
            }

            // test output
            //for (int index = 0; index < rowCount; index++)
            //{
            //    for (int innerIndex = 0; innerIndex < columnCount; innerIndex++)
            //    {
            //        Console.Write(changedSeatingMap[innerIndex, index]);
            //    }
            //    Console.Write(Environment.NewLine);
            //}
            //Console.Write(Environment.NewLine);

            for (int rowIndex = 0; rowIndex < rowCount; )
            {
                for (int columnIndex = 0; columnIndex < columnCount; )
                {
                    var currentEntry = currentSeatingMap[columnIndex, rowIndex];
                    var changedEntry = changedSeatingMap[columnIndex, rowIndex];

                    if (changedEntry.Equals(currentEntry))
                    {
                        columnIndex++;
                    }
                    else
                    {
                        return ApplySeatingRulesToMap(changedSeatingMap, rowCount, columnCount);
                    }

                }

                rowIndex++;
            }

            return changedSeatingMap;
        }

        // part 1 method
        private static char CheckAndChangeSeatingStatus(char entry, char[,] map, int entryColumn, int entryRow, int columnCount, int rowCount)
        {
            var seatIsEmpty = entry.Equals('L');
            var seatIsOccupied = entry.Equals('#');

            var hasNoOccupiedSeatsAdjacent = (CheckForOccupiedSeats(map, entryColumn, entryRow, columnCount, rowCount) == 0);
            var hasAtLeastFourOccupiedSeatsAdjacent = (CheckForOccupiedSeats(map, entryColumn, entryRow, columnCount, rowCount) >= 4);

            // only cases where entry changes
            if (seatIsEmpty && hasNoOccupiedSeatsAdjacent)
            {
                return '#';
            }
            else if (seatIsOccupied && hasAtLeastFourOccupiedSeatsAdjacent)
            {
                return 'L';
            }

            // if no changing rule applies, return the input
            return entry;
        }

        // part 1 method
        private static int CheckForOccupiedSeats(char[,] map, int column, int row, int columnCount, int rowCount)
        {
            // check positions and count sum of #-chars

            var adjacentPositionsToCheck = GetAdjacentCoordinates(map, column, row, columnCount, rowCount);
            var amountOfOccupiedSeats = 0;

            foreach (var entry in adjacentPositionsToCheck)
            {
                if (map[entry.Item1, entry.Item2].Equals('#'))
                {
                    amountOfOccupiedSeats++;
                }
            }

            return amountOfOccupiedSeats;
        }

        private static List<Tuple<int, int>> GetAdjacentCoordinates(char[,] map, int column, int row, int columnCount, int rowCount)
        {
            // column - 1, row - 1 (upper left corner) -> not possible when column == 0 || row == 0
            // column, row - 1 (directly above) -> not possible when row == 0
            // column + 1, row -1 (upper right corner) -> not possible when column == columnCount || row == 0

            // column - 1, row (directly left) -> not possible when column == 0
            // coloumn + 1, row (directly right) -> not possible when column == columnCount

            // column - 1, row + 1 (down left corner) -> not possible when column == 0 || row = rowCount
            // column, row + 1 (directly down) -> not possible when row == rowCount
            // column + 1, row + 1 (down right corner) -> not possible when column == columnCount || row == rowCount

            var positions = new List<Tuple<int, int>>();

            if (!(column == 0 || row == 0))
            {
                positions.Add(new Tuple<int, int>(column - 1, row - 1));
            }

            if (row != 0)
            {
                positions.Add(new Tuple<int, int>(column, row - 1));
            }

            if (!(column == columnCount-1 || row == 0))
            {
                positions.Add(new Tuple<int, int>(column + 1, row - 1));
            }

            if (column != 0)
            {
                positions.Add(new Tuple<int, int>(column - 1, row));
            }

            if (column != columnCount-1)
            {
                positions.Add(new Tuple<int, int>(column + 1, row));
            }

            if (!(column == 0 || row == rowCount-1))
            {
                positions.Add(new Tuple<int, int>(column - 1, row + 1));
            }

            if (row != rowCount-1)
            {
                positions.Add(new Tuple<int, int>(column, row + 1));
            }

            if (!(column == columnCount-1 || row == rowCount-1))
            {
                positions.Add(new Tuple<int, int>(column + 1, row + 1));
            }

            return positions;
        }

        static char[,] TransferFileToMap(IEnumerable<string> input, int rowCount, int columnCount)
        {

            var mapArray = new char[columnCount, rowCount];
            var rowIndex = 0;

            foreach (string row in input)
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
            for (int index = 0; index < rowCount; index++)
            {
                for (int innerIndex = 0; innerIndex < columnCount; innerIndex++)
                {
                    Console.Write(mapArray[innerIndex, index]);
                }
                Console.Write(Environment.NewLine);
            }
            Console.Write(Environment.NewLine);

            return mapArray;

        }

    }
}
