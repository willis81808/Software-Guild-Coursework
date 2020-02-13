using FlooringMastery.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery
{
    class Menu
    {
        public static void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Flooring Program");
                Console.WriteLine();
                Console.WriteLine("1. Display Orders");
                Console.WriteLine("2. Add an Order");
                Console.WriteLine("3. Edit an Order");
                Console.WriteLine("4. Remove an Order");
                Console.WriteLine("5. Quit");
                Console.Write("> ");

                string input = Console.ReadLine();
                
                switch (input)
                {
                    case "1":
                        new DisplayOrdersView().Execute();
                        break;
                    case "2":
                        new AddOrderView().Execute();
                        break;
                    case "3":
                        new EditOrderView().Execute();
                        break;
                    case "4":
                        new RemoveOrderView().Execute();
                        break;
                    case "5":
                        return;
                }
            }
        }
    }
}
