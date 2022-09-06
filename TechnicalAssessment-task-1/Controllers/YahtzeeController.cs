using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAssessment_task_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YahtzeeController : ControllerBase
    {
        [HttpGet]
        public string Ping()
        {
            return "Pong";
        }

        [HttpGet]
        [Route("roll/{numberOfDice}")]
        public int[] Roll(int numberOfDice)
        {
            Console.WriteLine(numberOfDice);
            int[] dice = new int[numberOfDice];

            //pick a random number from 1 to 6 inclusive add them to the list and return them

            for (int i = 0; i <= numberOfDice - 1; i++)
            {
                Random rand = new();
                int die = rand.Next(1, 6);
                dice[i] = die;
                Console.Write(die);

            };
            return dice;
        }

        [HttpPost]
        public int Calculate(int[] diceRoles)
        {
            //a dictionary of the number of dice of given value for lookup
            Dictionary<int, int> dictionary = new();
            foreach (int die in diceRoles)
            {
                if (!dictionary.ContainsKey(die))
                {
                    dictionary.Add(die, 1);
                }
                else
                {
                    int value = dictionary[die];
                    dictionary[die] = (value += 1);
                };
            }

            int sumDice()
            {
                int sum = 0;
                for (int i = 0; i <= diceRoles.Length - 1; i++)
                {
                    sum += diceRoles[i];
                };
                return sum;
            };

            bool isYhatzee()
            {
                foreach (var (key, value) in dictionary)
                {
                    if (value == 5)
                    {
                        return true;
                    };
                };

                return false;
            };

            bool isFullHouse()
            {
                int three = 0;
                int two = 0;
                foreach (var (key, value) in dictionary)
                {
                    if (value == 3)
                    {
                        three = key * value;
                    };

                    if (value == 2)
                    {
                        two = key * value;
                    };
                };

                if (three > 0 & two > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };

            bool isLargeStraight()
            {
                //one list with the numbers in a large straight compared with the sorted dice rolls
                List<int> largeStraight = new() { 1, 2, 3, 4, 5 };
                List<int> sortedDice = diceRoles.ToList();
                sortedDice.Sort();
                bool isEqual = Enumerable.SequenceEqual(largeStraight, sortedDice);

                return isEqual;
            };

            bool isSmallStraight()
            {
                //4 sequential numbers
                //lists of the valid combinations, compared to the list of dice values form the dictionary
                int[] smallStraight1 = { 1, 2, 3, 4 };
                int[] smallStraight2 = { 2, 3, 4, 5 };
                int[] sortedDice = dictionary.Keys.ToArray();
                Array.Sort(sortedDice);
                Console.WriteLine(sortedDice);

                if (Enumerable.SequenceEqual(smallStraight1, sortedDice))
                {
                    return true;
                }
                else if (Enumerable.SequenceEqual(smallStraight2, sortedDice))
                {
                    return true;
                };
                return false;
            };

            int isTwoPairs()
            {
                int value1 = 0;
                int value2 = 0;
                foreach (var (key, value) in dictionary)
                {
                    if (value == 2)
                    {
                        value1 = key * value;
                    };

                    if (value1 > 1 & value == 2)
                    {
                        value2 = key * value;
                    };
                }

                if (value1 > 0 & value2 > 0)
                {
                    return value1 + value2;
                }
                else
                {
                    return 0;
                }
            };

            int isFourOfaKind()
            {
                foreach (var (key, value) in dictionary)
                {
                    if (value <= 4)
                    {
                        return key * value;
                    };
                };

                return 0;
            };

            int isThreeOfaKind()
            {
                foreach (var (key, value) in dictionary)
                {
                    if (value == 3)
                    {
                        return key * value;
                    };
                };
                return 0;
            };

            int aces()
            {
                int sum = 0;
                foreach (var (key, value) in dictionary)
                {
                    if (value >= 2 & key * value > sum)
                    {
                        sum = key * value;
                    }
                }
                return sum;
            }

            int score;
            bool chanceUsed = false;

            //yahtzee
            if (isYhatzee())
            {
                score = 50;
            }

            else if (isLargeStraight())
            {
                score = 40;
            }
            
            else if (isSmallStraight())
            {
                score = 30;
            }

            else if (isFullHouse())
            {
                score = 25;
            }

            else if (isFourOfaKind() > 0 & chanceUsed)
            {
                score = isFourOfaKind();
            }
            else if (isTwoPairs() > 0 & chanceUsed)
            {
                score = isTwoPairs();
            }

            else if (isThreeOfaKind() > 0 & chanceUsed)
            {
                score = isThreeOfaKind();
            }

            //aces

            else if (aces() > 0 & chanceUsed)
            {
                score = aces();
            }

            //chance
            else
            {
                int sum = sumDice();
                score = sum;
            };
            return score;
        }
    }
}
