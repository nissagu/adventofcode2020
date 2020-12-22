using System;
using System.Collections.Generic;
using System.IO;

namespace RainRisk
{
    class Program
    {
        static void Main(string[] args)
        {
            var allInstructions = new List<Tuple<string, int>>();

            using (StreamReader streamReader = new StreamReader("/Users/Ruth/Projects/Advent_of_Code/Day_12/input.txt"))
            {

                while (streamReader.Peek() >= 0)
                {
                    var input = streamReader.ReadLine();
                    var instructionAmount = Convert.ToInt32(input.Substring(1));
                    var instructionAction = input.Substring(0, 1);

                    allInstructions.Add(new Tuple<string, int>(instructionAction, instructionAmount));
                }

            }

            // part 1
            var manhattanDistance = ComputeInstructions(allInstructions);
            Console.WriteLine($"manhattan distance = {manhattanDistance}");

            // part 2
            var manhattanDistanceWithWaypoint = ComputeInstructionsWithWaypoint(allInstructions);
            Console.WriteLine($"manhattan distance = {manhattanDistanceWithWaypoint}");
        }

        struct Waypoint
        {
            public int eastPosition;
            public int southPosition;
            public int westPosition;
            public int northPosition;

            public Waypoint(int east, int south, int west, int north)
            {
                eastPosition = east;
                southPosition = south;
                westPosition = west;
                northPosition = north;
            }
        }

        private static int ComputeInstructionsWithWaypoint(List<Tuple<string, int>> allInstructions)
        {
            var eastPosition = 0;
            var southPosition = 0;
            var westPosition = 0;
            var northPosition = 0;

            var waypoint = new Waypoint(10, 0, 0, 1);

            //var instructionNumber = 1;

            foreach (var instruction in allInstructions)
            {
                // choose operation according to action
                switch (instruction.Item1)
                {
                    // only move the waypoint

                    case "N":
                        {
                            if (waypoint.southPosition > instruction.Item2)
                            {
                                waypoint.southPosition -= instruction.Item2;
                                break;
                            }
                            else
                            {
                                waypoint.northPosition += instruction.Item2 - waypoint.southPosition;
                                waypoint.southPosition = 0;
                                break;
                            }
                        }
                    case "S":
                        {
                            if (waypoint.northPosition > instruction.Item2)
                            {
                                waypoint.northPosition -= instruction.Item2;
                                break;
                            }
                            else
                            {
                                waypoint.southPosition += instruction.Item2 - waypoint.northPosition;
                                waypoint.northPosition = 0;
                                break;
                            }
                        }
                    case "E":
                        {
                            if (waypoint.westPosition > instruction.Item2)
                            {
                                waypoint.westPosition -= instruction.Item2;
                                break;
                            }
                            else
                            {
                                waypoint.eastPosition += instruction.Item2 - waypoint.westPosition;
                                waypoint.westPosition = 0;
                                break;
                            }
                        }
                    case "W":
                        {
                            if (waypoint.eastPosition > instruction.Item2)
                            {
                                waypoint.eastPosition -= instruction.Item2;
                                break;
                            }
                            else
                            {
                                waypoint.westPosition += instruction.Item2 - waypoint.eastPosition;
                                waypoint.eastPosition = 0;
                                break;
                            }
                        }
                    // move the ship relative to the waypoint

                    case "F":
                        {
                            var eastAmountToMove = instruction.Item2 * waypoint.eastPosition;
                            var southAmountToMove = instruction.Item2 * waypoint.southPosition;
                            var westAmountToMove = instruction.Item2 * waypoint.westPosition;
                            var northAmountToMove = instruction.Item2 * waypoint.northPosition;

                            if (eastPosition > westAmountToMove)
                            {
                                eastPosition -= westAmountToMove;
                            }
                            else
                            {
                                westPosition += westAmountToMove - eastPosition;
                                eastPosition = 0;
                            }

                            if (southPosition > northAmountToMove)
                            {
                                southPosition -= northAmountToMove;
                            }
                            else
                            {
                                northPosition += northAmountToMove - southPosition;
                                southPosition = 0;
                            }

                            if (westPosition > eastAmountToMove)
                            {
                                westPosition -= eastAmountToMove;
                            }
                            else
                            {
                                eastPosition += eastAmountToMove - westPosition;
                                westPosition = 0;
                            }

                            if (northPosition > southAmountToMove)
                            {
                                northPosition -= southAmountToMove;
                            }
                            else
                            {
                                southPosition += southAmountToMove - northPosition;
                                northPosition = 0;
                            }

                            break;
                        }
                    default:
                        {
                            waypoint = RotateWaypoint(waypoint, instruction.Item1, instruction.Item2);
                            break;
                        }

                }

                // for debug
                //Console.WriteLine($"{instructionNumber}: {instruction.Item1},{instruction.Item2} - waypoint: east: {waypoint.eastPosition}, south: {waypoint.southPosition}, west: {waypoint.westPosition}, north: {waypoint.northPosition} - east: {eastPosition}, south: {southPosition}, west: {westPosition}, north: {northPosition}");
                //instructionNumber++;
            }

            return eastPosition + southPosition + westPosition + northPosition;
        }

        private static Waypoint RotateWaypoint(Waypoint currentWaypoint, string rotatingDirection, int degree)
        {
            var currentEastPosition = currentWaypoint.eastPosition;
            var currentSouthPosition = currentWaypoint.southPosition;
            var currentWestPosition = currentWaypoint.westPosition;
            var currentNorthPosition = currentWaypoint.northPosition;

            var eastPosition = 0;
            var southPosition = 0;
            var westPosition = 0;
            var northPosition = 0;

            // divide amount of degrees by 90 to get the amount of (counter)clockwise rotations
            for (int rotationCount = 0; rotationCount < degree / 90; rotationCount++)
            {

                if (rotatingDirection == "R")
                {
                    eastPosition = currentNorthPosition;
                    southPosition = currentEastPosition;
                    westPosition = currentSouthPosition;
                    northPosition = currentWestPosition;
                }

                if (rotatingDirection == "L")
                {
                    eastPosition = currentSouthPosition;
                    southPosition = currentWestPosition;
                    westPosition = currentNorthPosition;
                    northPosition = currentEastPosition;
                }

                currentEastPosition = eastPosition;
                currentSouthPosition = southPosition;
                currentWestPosition = westPosition;
                currentNorthPosition = northPosition;

            }

            return new Waypoint(eastPosition, southPosition, westPosition, northPosition);
        }

        private static int ComputeInstructions(List<Tuple<string, int>> allInstructions)
        {
            var direction = FacingDirections.east;
            var eastPosition = 0;
            var southPosition = 0;
            var westPosition = 0;
            var northPosition = 0;

            //var instructionNumber = 1;

            foreach (var instruction in allInstructions)
            {
                // choose operation according to action
                switch (instruction.Item1)
                {
                    case "N":
                        {
                            if (southPosition > instruction.Item2)
                            {
                                southPosition -= instruction.Item2;
                                break;
                            }
                            else
                            {
                                northPosition += instruction.Item2 - southPosition;
                                southPosition = 0;
                                break;
                            }
                        }
                    case "S":
                        {
                            if (northPosition > instruction.Item2)
                            {
                                northPosition -= instruction.Item2;
                                break;
                            }
                            else
                            {
                                southPosition += instruction.Item2 - northPosition;
                                northPosition = 0;
                                break;
                            }
                        }
                    case "E":
                        {
                            if (westPosition > instruction.Item2)
                            {
                                westPosition -= instruction.Item2;
                                break;
                            }
                            else
                            {
                                eastPosition += instruction.Item2 - westPosition;
                                westPosition = 0;
                                break;
                            }
                        }
                    case "W":
                        {
                            if (eastPosition > instruction.Item2)
                            {
                                eastPosition -= instruction.Item2;
                                break;
                            }
                            else
                            {
                                westPosition += instruction.Item2 - eastPosition;
                                eastPosition = 0;
                                break;
                            }
                        }
                    case "F":
                        {
                            if (direction == FacingDirections.east)
                            {
                                // moving east from a western position
                                if (westPosition < instruction.Item2)
                                {
                                    eastPosition += (instruction.Item2 - westPosition);
                                    westPosition = 0;
                                    break;
                                }
                                else
                                {
                                    westPosition -= instruction.Item2;
                                    break;
                                }
                            }
                            if (direction == FacingDirections.south)
                            {
                                // moving south from a northern position
                                if (northPosition < instruction.Item2)
                                {
                                    southPosition += (instruction.Item2 - northPosition);
                                    northPosition = 0;
                                    break;
                                }
                                else
                                {
                                    northPosition -= instruction.Item2;
                                    break;
                                }
                            }
                            if (direction == FacingDirections.west)
                            {
                                // moving south from a northern position
                                if (eastPosition < instruction.Item2)
                                {
                                    westPosition += (instruction.Item2 - eastPosition);
                                    eastPosition = 0;
                                    break;
                                }
                                else
                                {
                                    eastPosition -= instruction.Item2;
                                    break;
                                }
                            }
                            if (direction == FacingDirections.north)
                            {
                                // moving north from a southern position
                                if (southPosition < instruction.Item2)
                                {
                                    northPosition += (instruction.Item2 - southPosition);
                                    southPosition = 0;
                                    break;
                                }
                                else
                                {
                                    southPosition -= instruction.Item2;
                                    break;
                                }
                            }
                            break;
                        }
                    default:
                        {
                            direction = ChangeFacingDirection(direction, instruction.Item1, instruction.Item2);
                            break;
                        }

                }

                // for debug
                //Console.WriteLine($"{instructionNumber}: {instruction.Item1},{instruction.Item2} - facing direction: {direction} - east: {eastPosition}, south: {southPosition}, west: {westPosition}, north: {northPosition}");
                //instructionNumber++;
            }

            return eastPosition + southPosition + westPosition + northPosition;
        }

        private static FacingDirections ChangeFacingDirection(FacingDirections facingDirection, string rotatingDirection, int degrees)
        {
            var newDirection = 0;
            
            if (rotatingDirection == "R")
            {
                newDirection = (int) facingDirection + degrees;
                if (newDirection < 0)
                {
                    newDirection += 360;
                }
                if (newDirection >= 360)
                {
                    newDirection -= 360;
                }
            }

            if (rotatingDirection == "L")
            {
                newDirection = (int) facingDirection - degrees;
                if (newDirection >= 360)
                {
                    newDirection -= 360;
                }
                if (newDirection < 0)
                {
                    newDirection += 360;
                }
            }

            return (FacingDirections) newDirection;
        }

        enum FacingDirections
        {
            east = 90,
            south = 180,
            west = 270,
            north = 0
        }
    }
}
