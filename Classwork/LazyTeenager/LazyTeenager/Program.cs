using System;

namespace LazyTeenager
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            bool success = false;
            int attempts = 0;
            int roll = rand.Next(100);

            do
            {
                Console.WriteLine($"Clean your room!! (x{++attempts})");

                // exit loop and ground that teenager!
                if (attempts == 15)
                {
                    break;
                }
                
                success = roll < attempts * 5;
            }
            while (!success);

            if (success)
            {
                Console.WriteLine("FINE! I'LL CLEAN MY ROOM. BUT I REFUSE TO EAT MY PEAS.");
            }
            else
            {
                Console.WriteLine("Clean your room!! That's IT, I'm doing it!!! YOU'RE GROUNDED AND I'M TAKING YOUR XBOX!");
            }
        }
    }
}
