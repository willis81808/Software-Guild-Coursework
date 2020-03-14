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
            Console.WriteLine($"{"Order Number:",25} {order.OrderNumber}");
            Console.WriteLine($"{"CustomerName:",25} {order.CustomerName}");
            Console.WriteLine($"{"State:",25} {order.State}");
            Console.WriteLine($"{"TaxRate:",25} {order.TaxRate}%");
            Console.WriteLine($"{"ProductType:",25} {order.ProductType}");
            Console.WriteLine($"{"Area:",25} {order.Area} ft^2");
            Console.WriteLine($"{"CostPerSquareFoot:",25} ${order.CostPerSquareFoot:0.##}");
            Console.WriteLine($"{"LaborCostPerSquareFoot:",25} ${order.LaborCostPerSquareFoot:0.##}");
            Console.WriteLine($"{"MaterialCost:",25} ${order.MaterialCost:0.##}");
            Console.WriteLine($"{"LaborCost:",25} ${order.LaborCost:0.##}");
            Console.WriteLine($"{"Tax:",25} ${order.Tax:0.##}");
            Console.WriteLine($"{"Total:",25} ${order.Total:0.##}");
        }
        public static void DisplayProductDetails(Product product)
        {
            Console.WriteLine($"{"ProductType:",-23} {product.ProductType}");
            Console.WriteLine($"{"CostPerSquareFoot:",-23} {product.CostPerSquareFoot}");
            Console.WriteLine($"{"LaborCostPerSquareFoot:",-23} {product.LaborCostPerSquareFoot}");
        }
        public static void DisplayTaxDetails(State state)
        {
            throw new NotImplementedException();
        }

        public static DateTime GetDateTime(string message, bool pastAllowed = true)
        {
            DateTime result;

            do
            {
                do
                {
                    Console.WriteLine(message);
                    Console.Write("> ");
                }
                while (!DateTime.TryParse(Console.ReadLine(), out result));
            }
            while (!pastAllowed && DateTime.Now > result);

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

        public static bool GetBool(string message, string trueReply, string falseReply, bool caseSensitive = true)
        {
            bool? result = null;
            do
            {
                Console.WriteLine(message);
                Console.Write($"({trueReply}/{falseReply})> ");
                string input = Console.ReadLine();
                if (input == falseReply || (!caseSensitive && input.Equals(falseReply, StringComparison.OrdinalIgnoreCase)))
                {
                    result = false;
                }
                else if (input == trueReply || (!caseSensitive && input.Equals(trueReply, StringComparison.OrdinalIgnoreCase)))
                {
                    result = true;
                }
            }
            while (!result.HasValue);
            return result.Value;
        }
    }
}
