using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace HandheldHalting
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Instruction> allInstructions = new List<Instruction>();

            using (StreamReader streamReader = new StreamReader("/Users/Ruth/Projects/Advent_of_Code/Day_8/instructions.txt"))
            {

                while (streamReader.Peek() >= 0)
                {
                    var input = streamReader.ReadLine();
                    var splittedInput = input.Split(null);
                    var instructionText = splittedInput[0];
                    var instructionPrefix = splittedInput[1].Substring(0,1);
                    var instructionValue = Convert.ToInt32(splittedInput[1].Substring(1));

                    var instruction = new Instruction() { Text = instructionText, Prefix = instructionPrefix, Value = instructionValue, Visited = false };
                    allInstructions.Add(instruction);
                }

            }

            Console.WriteLine(GetAccSum(allInstructions));

        }

        static int GetAccSum(List<Instruction> instructions)
        {
            var inLoop = false;

            var accumulatorSum = 0;

            for (int index = 0; !inLoop && index < instructions.Count;)
            {

                var currentInstruction = instructions[index];

                if (!currentInstruction.Visited)
                {

                    switch (currentInstruction.Text)
                    {
                        case "nop":
                            {
                                currentInstruction.Visited = true;
                                index++;
                                break;
                            }
                        case "acc":
                            {
                                if (currentInstruction.Prefix == "+")
                                {
                                    accumulatorSum += currentInstruction.Value;
                                }
                                else
                                {
                                    accumulatorSum -= currentInstruction.Value;
                                }
                                currentInstruction.Visited = true;
                                index++;
                                break;
                            }
                        case "jmp":
                            {
                                if (currentInstruction.Prefix == "+")
                                {
                                    index += currentInstruction.Value;
                                }
                                else
                                {
                                    index -= currentInstruction.Value;
                                }
                                currentInstruction.Visited = true;
                                break;
                            }

                    }
                }
                else
                {
                    inLoop = true;
                }

            }

            return accumulatorSum;
        }

    }
}
