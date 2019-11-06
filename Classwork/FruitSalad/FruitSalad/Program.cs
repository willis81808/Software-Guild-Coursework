using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitSalad
{
    class Program
    {
        private static Random rand = new Random();

        static void Main(string[] args)
        {
            List<string> fruits = new List<string>(new string[] { "Kiwi Fruit", "Gala Apple", "Granny Smith Apple", "Cherry Tomato", "Gooseberry", "Beefsteak Tomato", "Braeburn Apple", "Blueberry", "Strawberry", "Navel Orange", "Pink Pearl Apple", "Raspberry", "Blood Orange", "Sungold Tomato", "Fuji Apple", "Blackberry", "Banana", "Pineapple", "Florida Orange", "Kiku Apple", "Mango", "Satsuma Orange", "Watermelon", "Snozzberry" });
            List<string> fruitSalad = new List<string>();

            // sort ingredients
            var berries = from string item in fruits
                          where ContainsIgnoreCase(item, "berry")
                          select item;

            var apples  = from string item in fruits
                          where ContainsIgnoreCase(item, "apple") && item != "Pineapple"
                          select item;

            var oranges = from string item in fruits
                          where ContainsIgnoreCase(item, "orange")
                          select item;

            var misc    = from string item in fruits
                          where !berries.Contains(item) && !apples.Contains(item) && !oranges.Contains(item) && !ContainsIgnoreCase(item, "tomato")
                          select item;

            ////////////////////////////////////////////////////////////////
            // The following code selects up to three (3) random apples,  //
            // up to two (2) random oranges, all available berries, then  //
            // adds as many misc fruits as possible until reaching        //
            // twelve (12) total ingredients for our salad.               //
            ////////////////////////////////////////////////////////////////
            fruitSalad.AddRange(GetRandomElements(apples, rand.Next(3) + 1));
            fruitSalad.AddRange(GetRandomElements(oranges, rand.Next(2) + 1));
            fruitSalad.AddRange(berries);
            fruitSalad.AddRange(GetRandomElements(misc, 12 - fruitSalad.Count));

            // print recipe
            for (int i = 0; i < fruitSalad.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {fruitSalad[i]}");
            }
        }

        private static string[] GetRandomElements(IEnumerable<string> collection, int count)
        {
            List<string> list = new List<string>(collection);

            while (list.Count > count)
            {
                list.RemoveAt(rand.Next(list.Count));
            }

            return list.ToArray();
        }
        
        private static bool ContainsIgnoreCase(string input, string substring)
        {
            return input.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
