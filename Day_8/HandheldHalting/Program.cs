using System;
using System.Collections.Generic;
using System.IO;

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

            // first part output
            Console.WriteLine(GetAccSum(allInstructions));

            // second part output
            Console.WriteLine(GetAccSumFromSwitchedInstruction(allInstructions));

        }

        static int GetAccSumFromSwitchedInstruction(List<Instruction> instructions)
        {
            var accSum = 0;
            var isTerminating = false;

            // go through every instruction
            for (int index = 0; !isTerminating && index < instructions.Count; )
            {
                var currentInstruction = instructions[index];

                // first: switch operations
                if (currentInstruction.Text == "jmp" || currentInstruction.Text == "nop")
                {
                    currentInstruction.Text = SwitchInstructionText(currentInstruction.Text);

                    // second: evaluate if the program terminates (terminating = not looping)
                    var result = EvalProgramTerminationAndAccSum(instructions);
                    var isLooping = result.Item1;
                    accSum = result.Item2;

                    // third: if it does, print out the accumulator's sum
                    //else set everything back
                    if (isLooping)
                    {
                        //reset all values
                        accSum = 0;
                        currentInstruction.Text = SwitchInstructionText(currentInstruction.Text);
                        instructions = ResetVisitedFlags(instructions);
                        index++;
                    }
                    else
                    {
                        isTerminating = true;
                    }

                }
                else
                {
                    index++;
                }

            }

            return accSum;

        }

        static List<Instruction> ResetVisitedFlags(List<Instruction> instructions)
        {
            foreach (var entry in instructions)
            {
                entry.Visited = false;
            }

            return instructions;
        }

        static string SwitchInstructionText(string text)
        {
            switch (text)
            {
                case "nop":
                    {
                        return "jmp";
                    }
                case "jmp":
                    {
                        return "nop";
                    }
                default:
                    {
                        return text;
                    }
            }
        }

        static Tuple<bool,int> EvalProgramTerminationAndAccSum(List<Instruction> instructions)
        {
            var isLooping = false;
            var accSum = 0;

            for (int index = 0; !isLooping && index < instructions.Count;)
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
                                currentInstruction.Visited = true;
                                if (currentInstruction.Prefix == "+")
                                {
                                    accSum += currentInstruction.Value;
                                }
                                else
                                {
                                    accSum -= currentInstruction.Value;
                                }
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
                    isLooping = true;
                }

            }

            return new Tuple<bool, int>(isLooping, accSum);
        }

        // used for first part where everything happened in one method
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
