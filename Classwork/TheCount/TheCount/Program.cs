using System;

namespace TheCount
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** I LOVE TO COUNT! LET ME SHARE MY COUNTING WITH YOU! ***");

            int start = GetNumber("Start at: ");
            int stop  = GetNumber(" Stop at: ");
            int step  = GetNumber("Count By: ");

            Console.WriteLine();

            int counter = 0;
            for (int i = start; i <= stop; i += step)
            {
                Console.Write(i + " ");

                if (++counter == 3)
                {
                    Console.WriteLine("- Ah ah ah!");
                    counter = 0;
                }

            }

            Console.WriteLine();
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
