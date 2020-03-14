using FlooringMastery.BLL;
using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Views
{
    class AddOrderView : IView
    {
        public void Execute()
        {
            DataManager repository = DataManagerFactory.Create();

            Console.Clear();
            
            // date
            DateTime date = ConsoleIO.GetDateTime("Order date (must be a future date):", false);
            Console.WriteLine();

            // name
            Console.WriteLine("Customer name:");
            Console.Write("> ");
            string name = Console.ReadLine();
            Console.WriteLine();
            
            // state
            State state = GetStateFrom(repository.States);
            if (state == null) return;
            Console.WriteLine();
            
            // product
            Product product = GetProductsFrom(repository.Products);
            Console.WriteLine();

            // area
            decimal area = ConsoleIO.GetDecimal("Floor area (square feet):");
            Console.WriteLine();

            // summary
            Console.Clear();
            Order order = new Order(name, state, product, area);
            ConsoleIO.DisplayOrderDetails(order);
            if (ConsoleIO.GetBool("Would you like to save this order?", "Y", "N", false))
            {
                repository.AddOrder(date, order);
            }
        }
        
        private State GetStateFrom(State[] states)
        {
            Console.WriteLine("State:");
            Console.Write("> ");
            string stateText = Console.ReadLine();
            State state = (from s in states
                           where s.StateAbbreviation == stateText || s.StateName == stateText
                           select s).FirstOrDefault();
            if (state == null)
            {
                Console.WriteLine($"We do not do business in {stateText}, unfortunately we cannot accept this order.");
                Console.Write("Press any key to return to the main menu...");
                Console.ReadKey();
                return null;
            }
            return state;
        }

        private Product GetProductsFrom(Product[] products)
        {
            Product product = null;
            do
            {
                for (int i = 0; i < products.Length; i++)
                {
                    Console.WriteLine($"{i + 1})");
                    ConsoleIO.DisplayProductDetails(products[i]);
                    Console.WriteLine();
                }
                int selection = ConsoleIO.GetInteger("Enter a product by number:") - 1;
                if (selection < products.Length && selection >= 0)
                {
                    product = products[selection];
                }
                else
                {
                    Console.WriteLine($"Invalid selection: {selection + 1}");
                    Console.WriteLine($"Expected a number in the range 1-{products.Length}");
                    Console.Write("Press any key to try again...");
                    Console.ReadKey();
                }
            }
            while (product == null);
            return product;
        }
    }
}
