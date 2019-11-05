using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForTimesFor
{
    class Program
    {
        static void Main(string[] args)
        {
            int multiple = GetNumber("Which times table shall I recite? ");

            // give test
            int score = 0;
            for (int i = 1; i <= 15; i++)
            {
                int response = GetNumber($"{i} * {multiple} is: ");
                int answer = i * multiple;

                if (response == answer)
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine($"Sorry no, the answer is: {answer}");
                }
            }

            // display results
            float grade = score / 15f;
            if (grade < .5f)
            {
                Console.WriteLine("Oof! Somebody needs to study more...");
            }
            else if (grade > .9f)
            {
                Console.WriteLine("We've got a whiz kid over here, look at you!");
            }
            Console.WriteLine($"You got {score} correct.");
        }

        // I'm proud of this! Probably gonna reuse this method a lot...
        private static int GetNumber(string prompt)
        {
            int result;

            Console.Write(prompt);
            while(!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("You entered an invalid number!");
                Console.Write(prompt);
            }

            return result;
        }
    }
}
