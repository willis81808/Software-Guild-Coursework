using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Views
{
    class EditOrderView : IView
    {
        public void Execute()
        {
            DataManager repository = DataManagerFactory.Create();

            Console.Clear();
            DateTime targetDate = ConsoleIO.GetDateTime("Enter the date of the order you'd like to edit:");

            if (repository.Orders.ContainsKey(targetDate))
            {
                int orderNumber = ConsoleIO.GetInteger("Enter the order number of the order to edit:");
                OrderLookupResponse result = repository.LookupOrder(targetDate, orderNumber);
                if (result.Success)
                {
                    Console.WriteLine();

                    // customer name
                    Console.WriteLine($"Customer name ({result.Order.CustomerName}):");
                    Console.Write("> ");
                    string name = Console.ReadLine();
                    Console.WriteLine();

                    // state
                    Console.WriteLine($"State ({result.Order.State}):");
                    Console.Write("> ");
                    string stateText;
                    State state = null;
                    do
                    {
                        stateText = Console.ReadLine();
                        if (stateText != "")
                        {
                            state = (from s in repository.States
                                     where s.StateAbbreviation == stateText || s.StateName == stateText
                                     select s).FirstOrDefault();
                            if (state == null)
                            {
                                Console.WriteLine($"We do not do business in {stateText}.");
                            }
                        }
                        else
                        {
                            state = (from s in repository.States
                                     where s.StateAbbreviation == result.Order.State
                                     select s).FirstOrDefault();
                        }
                    }
                    while (state == null);
                    Console.WriteLine();

                    // product
                    Product product = null;
                    do
                    {
                        for (int i = 0; i < repository.Products.Length; i++)
                        {
                            Console.WriteLine($"{i + 1})");
                            ConsoleIO.DisplayProductDetails(repository.Products[i]);
                            Console.WriteLine();
                        }
                        Console.WriteLine($"Enter a product by number ({result.Order.ProductType}):");
                        Console.Write("> ");
                        if (int.TryParse(Console.ReadLine(), out int selection))
                        {
                            if (selection <= repository.Products.Length && selection >= 0)
                            {
                                product = repository.Products[selection-1];
                            }
                            else
                            {
                                Console.WriteLine($"Invalid selection: {selection}");
                                Console.WriteLine($"Expected a number in the range 1-{repository.Products.Length}");
                            }
                        }
                        else
                        {
                            product = (from p in repository.Products
                                       where p.ProductType == result.Order.ProductType
                                       select p).FirstOrDefault();
                        }
                    }
                    while (product == null);
                    Console.WriteLine();

                    // area
                    decimal? area = null;
                    do
                    {
                        Console.WriteLine($"Floor area ({result.Order.Area}):");
                        Console.Write("> ");
                        string areaText = Console.ReadLine();
                        if (areaText == "")
                        {
                            area = result.Order.Area;
                        }
                        else if (decimal.TryParse(areaText, out decimal value))
                        {
                            area = value;
                        }
                    }
                    while (!area.HasValue);
                    Console.WriteLine();

                    // summary
                    Console.Clear();
                    Order order = new Order(name == "" ? result.Order.CustomerName : name, state, product, area.Value);
                    ConsoleIO.DisplayOrderDetails(order);
                    if (ConsoleIO.GetBool("Would you like to save this order?", "Y", "N", false))
                    {
                        repository.UpdateOrder(order, targetDate, orderNumber);
                    }
                }
                else
                {
                    Console.WriteLine(result.Message);
                    Console.Write("Press any key to return to main menu...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine($"No orders were found on {targetDate.ToShortDateString()}");
                Console.Write("Press any key to return to main menu...");
                Console.ReadKey();
            }
        }
    }
}
