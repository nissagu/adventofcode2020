using System;
using System.IO;

namespace ReportRepair
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] _numbers = File.ReadAllLines("/Users/Ruth/Projects/Advent_of_Code/Day_1/Puzzle_Input.txt");

            //computeWithTwoNumbers(_numbers);
            computeWithThreeNumbers(_numbers);

        }

        // not working
        static void computeWithThreeNumbers(string[] input)
        {

            int firstNumber;
            int secondNumber;
            int thirdNumber;
            //int multiplicationResult;

            for (int index = 0; index < input.Length; index++)
            {

                Console.WriteLine("first loop");
                if (input.Length - index > 3)
                {
                    firstNumber = Convert.ToInt32(input[index]);
                    secondNumber = Convert.ToInt32(input[index + 1]);
                    thirdNumber = Convert.ToInt32(input[index + 2]);

                    if ((firstNumber + secondNumber + thirdNumber) != 2020)
                    {

                        for (int innerindex = index + 3; innerindex < input.Length; innerindex++)
                        {
                            
                            thirdNumber = Convert.ToInt32(input[innerindex]);
                            Console.WriteLine("second loop: " + thirdNumber);

                            if ((firstNumber + secondNumber + thirdNumber) == 2020)
                            {
                                Console.WriteLine("found 3 numbers");
                                Console.WriteLine(firstNumber);
                                Console.WriteLine(secondNumber);
                                Console.WriteLine(thirdNumber);

                            }

                        }

                    }

                    //Console.WriteLine("found 3 numbers");
                    //Console.WriteLine(firstNumber);
                    //Console.WriteLine(secondNumber);
                    //Console.WriteLine(thirdNumber);

                    //Console.ReadKey();

                    // multiplicationResult = firstNumber * secondNumber;
                    // Console.WriteLine(multiplicationResult);
                }
            }


        }

         static void computeWithTwoNumbers(string[] input) {

            int firstNumber;
            int secondNumber;
            int multiplicationResult;

            for (int index = 0; index < input.Length; index++)
            {

                firstNumber = Convert.ToInt32(input[index]);

                for (int innerIndex = (index + 1); innerIndex < (input.Length - index); innerIndex++)
                {

                    secondNumber = Convert.ToInt32(input[innerIndex]);

                    if ((firstNumber + secondNumber) == 2020)
                    {
                        Console.WriteLine("found 2 numbers:");
                        Console.WriteLine(firstNumber);
                        Console.WriteLine(secondNumber);

                        multiplicationResult = firstNumber * secondNumber;
                        Console.WriteLine("Multiplication result: " + multiplicationResult);
                    }

                }

            }

         }
    }
}
