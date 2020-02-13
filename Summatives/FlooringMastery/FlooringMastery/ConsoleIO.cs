using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery
{
    public class ConsoleIO
    {
        public static void DisplayOrderDetails(Order order)
        {
            Console.WriteLine($"Order Number: {order.OrderNumber}");
            Console.WriteLine($"CustomerName: {order.CustomerName}");
            Console.WriteLine($"State: {order.State}");
            Console.WriteLine($"TaxRate: {order.TaxRate}");
            Console.WriteLine($"ProductType: {order.ProductType}");
            Console.WriteLine($"Area: {order.Area}");
            Console.WriteLine($"CostPerSquareFoot: {order.CostPerSquareFoot}");
            Console.WriteLine($"LaborCostPerSquareFoot: {order.LaborCostPerSquareFoot}");
            Console.WriteLine($"MaterialCost: {order.MaterialCost}");
            Console.WriteLine($"LaborCost: {order.LaborCost}");
            Console.WriteLine($"Tax: {order.Tax}");
            Console.WriteLine($"Total: {order.Total}");
        }
        public static void DisplayProductDetails(Product product)
        {
            Console.WriteLine($"ProductType: {product.ProductType}");
            Console.WriteLine($"CostPerSquareFoot: {product.CostPerSquareFoot}");
            Console.WriteLine($"LaborCostPerSquareFoot: {product.LaborCostPerSquareFoot}");
        }
        public static void DisplayTaxDetails(State state)
        {
            throw new NotImplementedException();
        }

        public static DateTime GetDateTime(string message)
        {
            DateTime result;

            do
            {
                Console.WriteLine(message);
                Console.Write("> ");
            }
            while (!DateTime.TryParse(Console.ReadLine(), out result));

            return result;
        }

        public static decimal GetDecimal(string message)
        {
            decimal result;

            do
            {
                Console.WriteLine(message);
                Console.Write("> ");
            }
            while (!decimal.TryParse(Console.ReadLine(), out result));

            return result;
        }

        public static int GetInteger(string message)
        {
            int result;

            do
            {
                Console.WriteLine(message);
                Console.Write("> ");
            }
            while (!int.TryParse(Console.ReadLine(), out result));

            return result;
        }

        public static bool GetBool(string message, string trueReply, string falseReply)
        {
            bool? result = null;
            do
            {
                Console.WriteLine(message);
                Console.Write($"({trueReply}/{falseReply})> ");
                string input = Console.ReadLine();
                if (input == falseReply)
                {
                    result = false;
                }
                else if (input == trueReply)
                {
                    result = true;
                }
            }
            while (!result.HasValue);
            return result.Value;
        }
    }
}
