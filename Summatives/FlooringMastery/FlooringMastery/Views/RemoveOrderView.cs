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
    class RemoveOrderView : IView
    {
        public void Execute()
        {
            DataManager repository = DataManagerFactory.Create();

            Console.Clear();
            DateTime targetDate = ConsoleIO.GetDateTime("Enter the date of the order you'd like to remove:");

            if (repository.Orders.ContainsKey(targetDate))
            {
                int orderNumber = ConsoleIO.GetInteger("Enter the order number of the order to remove:");
                OrderLookupResponse result = repository.LookupOrder(targetDate, orderNumber);
                if (result.Success)
                {
                    Console.WriteLine();
                    ConsoleIO.DisplayOrderDetails(result.Order);
                    if (ConsoleIO.GetBool("Would you like to delete this order?", "Y", "N", false))
                    {
                        repository.RemoveOrder(targetDate, orderNumber);
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