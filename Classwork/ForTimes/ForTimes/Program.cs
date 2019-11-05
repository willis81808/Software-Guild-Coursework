using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForTimes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Which times table shall I recite? ");
            int multiple = int.Parse(Console.ReadLine());
            
            for (int i = 1; i <= 15; i++)
            {
                Console.WriteLine($"{i} * {multiple} is: {i*multiple}");
            }
        }
    }
}
