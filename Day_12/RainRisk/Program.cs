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
                    var instructionAction = input.Substring(0,1);

                    allInstructions.Add(new Tuple<string, int>(instructionAction, instructionAmount));
                }

            }

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

            Console.WriteLine($"manhattan distance = " + (eastPosition + southPosition + westPosition + northPosition));
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
