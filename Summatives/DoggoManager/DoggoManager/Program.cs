using DoggoManager.Controllers;
using System;

namespace DoggoManager
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            Console.WriteLine("Welcome to the Doggo Manager 9000!");

            do
            {
                Console.WriteLine("\nWould you like to:");
                Console.WriteLine(" 1) Add a Doggo\n 2) List all Doggos\n 3) Find a Doggo\n 4) Edit a Doggo\n 5) Remove a Doggo\n 6) Exit\n");
                switch (Utils.GetNumber("> ", Utils.RangePredicate(1, 6)))
                {
                    case 1:
                        DoggoController.CreateDoggo();
                        break;
                    case 2:
                        DoggoController.DisplayDoggos();
                        break;
                    case 3:
                        DoggoController.FindDoggo();
                        break;
                    case 4:
                        DoggoController.EditDoggo();
                        break;
                    case 5:
                        DoggoController.RemoveDoggo();
                        break;
                    case 6:
                        running = false;
                        break;
                }
            }
            while (running);
        }

    }

    static class Utils
    {
        public static int GetNumber(string prompt, Func<int, bool> predicate)
        {
            int result;

            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out result) || !predicate(result))
            {
                Console.WriteLine("You entered an invalid number!");
                Console.Write(prompt);
            }
            
            return result;
        }
        public static int GetNumber(string prompt, string error, Func<int, bool> predicate)
        {
            int result;

            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out result) || !predicate(result))
            {
                Console.WriteLine(error);
                Console.Write(prompt);
            }
            
            return result;
        }
        public static int GetNumber(string prompt)
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
        public static Func<int, bool> RangePredicate(int min, int max)
        {
            return num => { return num >= min && num <= max; };
        }
    }
}
