using FlooringMastery.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Views
{
    class DisplayOrdersView : IView
    {
        private static readonly string DIVIDER = "--------------------------------------------------";

        public void Execute()
        {
            DataManager repository = DataManagerFactory.Create();

            Console.WriteLine();
            Console.WriteLine(DIVIDER);
            foreach (var key in repository.Orders.Keys)
            {
                string dateString = key.ToShortDateString();
                Console.WriteLine($"{{0, {(DIVIDER.Length / 2) + (dateString.Length / 2)}}}", dateString);

                var collection = repository.Orders[key];
                foreach (var order in collection)
                {
                    Console.WriteLine(DIVIDER);
                    ConsoleIO.DisplayOrderDetails(order);
                }
                Console.WriteLine(DIVIDER);
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
