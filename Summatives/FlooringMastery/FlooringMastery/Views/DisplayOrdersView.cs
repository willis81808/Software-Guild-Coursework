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
        public void Execute()
        {
            DataManager repository = DataManagerFactory.Create();

            Console.WriteLine();
            foreach (var key in repository.Orders.Keys)
            {
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("%20s", key.ToShortDateString());

                var collection = repository.Orders[key];
                foreach (var order in collection)
                {
                    Console.WriteLine("-----------------------------------------");
                    ConsoleIO.DisplayOrderDetails(order);
                }
                Console.WriteLine("-----------------------------------------");
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
