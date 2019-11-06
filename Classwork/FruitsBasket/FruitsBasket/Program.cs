using System;
using System.Collections.Generic;

namespace FruitBasket
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fruits = { "Orange", "Apple", "Orange", "Apple", "Orange", "Apple", "Orange", "Apple", "Orange", "Orange", "Orange", "Apple", "Orange", "Orange", "Apple", "Orange", "Orange", "Apple", "Apple", "Orange", "Apple", "Apple", "Orange", "Orange", "Apple", "Apple", "Apple", "Apple", "Orange", "Orange", "Apple", "Apple", "Orange", "Orange", "Orange", "Orange", "Apple", "Apple", "Apple", "Apple", "Orange", "Orange", "Apple", "Orange", "Orange", "Apple", "Orange", "Orange", "Apple", "Apple", "Orange", "Orange", "Apple", "Orange", "Apple", "Orange", "Apple", "Orange", "Apple", "Orange", "Orange" };

            // sort (who needs arrays?)
            Dictionary<string, int> inventory = new Dictionary<string, int>();
            foreach (var fruit in fruits)
            {
                if (inventory.ContainsKey(fruit))
                {
                    inventory[fruit]++;
                }
                else
                {
                    inventory.Add(fruit, 1);
                }
            }

            // print results
            Console.WriteLine($"Total # of Fruit in Basket: {fruits.Length}");
            foreach (var item in inventory)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
