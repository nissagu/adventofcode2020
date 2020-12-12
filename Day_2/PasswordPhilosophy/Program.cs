using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PasswordPhilosophy
{
    class Program
    {
        static void Main(string[] args)
        {
            var workingFile = File.ReadLines("/Users/Ruth/Projects/Advent_of_Code/Day_2/passphrases.txt");
            var passwords = ParseData(workingFile);

            //Part One
            var countValidPasswords = CountValidEntries(passwords);
            Console.WriteLine("Anzahl 1. Teil: " + countValidPasswords);

            //Part Two
            var countNewValidPasswords = CountValidEntriesByPositions(passwords);
            Console.WriteLine("Anzahl 2. Teil: " + countNewValidPasswords);

        }

        private static int CountValidEntriesByPositions(List<Password> passwords)
        {
            int count = 0;

            foreach (var entry in passwords)
            {

                if (IsValidByPositions(entry))
                {

                    count++;

                }

            }

            return count;
        }

        private static bool IsValidByPositions(Password entry)
        {
            var chosenCharacter = entry.Character;
            string passphrase = entry.PassPhrase;
            var firstPosition = entry.MinValue;
            var secondPosition = entry.MaxValue;

            var foundPositions = new List<int>();

            for (int index = passphrase.IndexOf(chosenCharacter); index > -1; index = passphrase.IndexOf(chosenCharacter, index + 1))
            {
                // for loop ends when i=-1 (character not found)
                foundPositions.Add(index+1);
            }

            bool firstPositionValid = foundPositions.Contains(firstPosition);
            var secondPositionValid = foundPositions.Contains(secondPosition);

            if (firstPositionValid & !secondPositionValid || !firstPositionValid & secondPositionValid)
            {
                return true;
            }

            return false;
        }

        private static List<Password> ParseData(IEnumerable<string> data)
        {
            List<Password> passwords = new List<Password>();

            foreach (var line in data)
            {
                var parts = line.Split(new char[0]);
                var values = parts[0];
                //Console.WriteLine(values);
                string character = parts[1];
                //Console.WriteLine(character);
                var passphrase = parts[2];
                //Console.WriteLine(passphrase);

                var valuesSplit = values.Split('-');
                var minValue = Convert.ToInt32(valuesSplit[0]);
                int maxValue = Convert.ToInt32(valuesSplit[1]);

                var characterSplit = character.Split(':');
                string chosenCharacter = characterSplit[0];

                passwords.Add(new Password(minValue, maxValue, chosenCharacter, passphrase));

            }

            return passwords;

        }

        private static bool IsValid(Password password)
        {

            // Passwort enthält den ausgewählten Buchstaben nicht
            if (!password.PassPhrase.Contains(password.Character))
            {
                return false;
            }
            
            var minCount = password.MinValue;
            var maxCount = password.MaxValue;
            var passphrase = password.PassPhrase;
            var chosenCharacter = Convert.ToChar(password.Character);

            int characterCount = passphrase.Count(c => c == chosenCharacter);

            if (characterCount < minCount || characterCount > maxCount)
            {
                return false;
            }


            return true;

        }

        static int CountValidEntries(List<Password> passwords)
        {

            int count = 0;

            foreach (var entry in passwords)
            {

                if (IsValid(entry))
                {

                    count++;

                }

            }

            return count;
        }
    }
}
