using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraditionalFizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            int units = GetNumber("How much units fizzing and buzzing do you need in your life? ");
            int count = 0;

            Console.WriteLine(0);
            for (int i = 1; count < units; i++)
            {
                bool fizz = i % 3 == 0;
                bool buzz = i % 5 == 0;

                if (fizz || buzz) count++;

                if (fizz && buzz)
                {
                    Console.WriteLine("fizz buzz");
                }
                else if (fizz)
                {
                    Console.WriteLine("fizz");
                }
                else if (buzz)
                {
                    Console.WriteLine("buzz");
                }
                else
                {
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine("TRADITION!!!!!");
        }

        private static int GetNumber(string prompt)
        {
            int result;

            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("You entered an invalid number!");
                Console.Write(prompt);
            }

            return result;
        }
    }
}
