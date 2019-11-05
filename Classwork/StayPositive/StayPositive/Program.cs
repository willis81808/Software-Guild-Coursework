using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayPositive
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("What number should I count down from? ");
            int response = int.Parse(Console.ReadLine());

            Console.WriteLine("\nHere goes!\n");

            int counter = 0;
            for (int i = 0; i <= response; i++)
            {
                Console.Write(response - i + " ");

                if (++counter >= 10)
                {
                    Console.WriteLine();
                    counter = 0;
                }

            }
            
            Console.WriteLine("\nWhew, better stop there...!");
        }
    }
}
