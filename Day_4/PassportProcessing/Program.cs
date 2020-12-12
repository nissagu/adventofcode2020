using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PassportProcessing
{
    class Program
    {
        static void Main(string[] args)
        {

            MatchCollection passportMatches;
            List<string> passports = new List<string>();
            List<string> validPassports = new List<string>();
            List<string> eyeColours = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

            Regex evalPassportsPattern = new Regex(@"(?s)(?=.*\b(byr:\S+)(?:\s*|\z)\b)(?=.*\b(iyr:\S+)(?:\s*|\z)\b)(?=.*\b(eyr:\S+)(?:\s*|\z)\b)(?=.*\b(hgt:\S+)(?:\s*|\z)\b)(?=.*\b(hcl:\S+)(?:\s*|\z)\b)(?=.*\b(ecl:\S+)(?:\s*|\z)\b)(?=.*\b(pid:\S+)(?:\s*|\z)\b).+", RegexOptions.Singleline);
            Regex validHcl = new Regex(@"^(#[0-9a-f]{6})$");

            // extract all passport entries from the input file
            using (StreamReader streamReader = new StreamReader("/Users/Ruth/Projects/Advent_of_Code/Day_4/passports.txt"))
            { 
                var input = streamReader.ReadToEnd();


                Regex splitPassportsPattern = new Regex(@"(?s)(.*?)(?:\n\n|\z)");
              

                passportMatches = splitPassportsPattern.Matches(input);

            }

            Console.WriteLine(passportMatches.Count());

            // convert passport matches to string list to evaluate it again via regex
            foreach (Match match in passportMatches)
            {
                passports.Add(match.ToString());
            }     

            // evaluate all fields according to each ones rules
            foreach (string entry in passports)
            {
                if (evalPassportsPattern.IsMatch(entry))
                {
                    var matchGroups = evalPassportsPattern.Match(entry).Groups;
                    var isFirstGroup = true;
                    var isValidEntry = true;

                    foreach (var matchGroup in matchGroups)
                    {
                        if (isFirstGroup)
                        {
                            isFirstGroup = false;
                            continue;
                        }
                        var matchGroupString = matchGroup.ToString();
                        var matchGroupSplit = matchGroupString.Split(":");
                        var key = matchGroupSplit[0];
                        var value = matchGroupSplit[1];

                        switch (key)
                        {
                            case "byr":
                                var byrInt = Convert.ToInt32(value);
                                if (byrInt < 1920 || byrInt > 2002)
                                {
                                    isValidEntry = false;
                                }
                                break;
                            case "iyr":
                                var iyrInt = Convert.ToInt32(value);
                                if (iyrInt < 2010 || iyrInt > 2020)
                                {
                                    isValidEntry = false;
                                }
                                break;
                            case "eyr":
                                var eyrInt = Convert.ToInt32(value);
                                if (eyrInt < 2020 || eyrInt > 2030)
                                {
                                    isValidEntry = false;
                                }
                                break;
                            case "hgt":
                                if (value.Contains("cm"))
                                {
                                    var hgtInt = Int32.Parse(value.Split("cm")[0]);
                                    if (hgtInt < 150 || hgtInt > 193)
                                    {
                                        isValidEntry = false;
                                    }
                                }
                                else
                                {
                                    var hgtInt = Int32.Parse(value.Split("in")[0]);
                                    if (hgtInt < 59 || hgtInt > 76)
                                    {
                                        isValidEntry = false;
                                    }
                                }
                                break;
                            case "hcl":
                                if (!validHcl.IsMatch(value))
                                {
                                    isValidEntry = false;
                                }
                                break;
                            case "ecl":
                                if (!eyeColours.Contains(value))
                                {
                                    isValidEntry = false;
                                }
                                break;
                            case "pid":
                                if (!(value.Length == 9 && Int32.TryParse(value, out _)))
                                {
                                    isValidEntry = false;
                                };
                                break;
                        }

                    }

                    if (isValidEntry)
                    {
                        validPassports.Add(entry);
                    }
                }

            }

            Console.WriteLine(validPassports.Count());

        }
    }
}
