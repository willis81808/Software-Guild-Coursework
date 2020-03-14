using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class TestDataRepository : IDataRepository
    {
        private static Dictionary<DateTime, List<Order>> orders;
        private static List<Product> products;
        private static List<State> states;

        static TestDataRepository()
        {
            orders = new Dictionary<DateTime, List<Order>>()
            {
                {   new DateTime(2020, 1, 1),  new List<Order>(){
                    new Order()
                    {
                        OrderNumber = 1,
                        CustomerName = "Wise",
                        State = "OH",
                        TaxRate = 6.25m,
                        ProductType = "Wood",
                        Area = 100m,
                        CostPerSquareFoot = 5.15m,
                        LaborCostPerSquareFoot = 4.75m,
                        MaterialCost = 515m,
                        LaborCost = 475m,
                        Tax = 61.88m,
                        Total = 1051.88m
                    }
                }}
            };
            products = new List<Product>()
            {
                new Product("Wood", 5.15m, 4.75m),
                new Product("Marble", 8.10m, 7.25m),
                new Product("Tatami", 32.4m, 4m)
            };
            states = new List<State>()
            {
                new State("AK", "Alaska", 0m),
                new State("CO", "Colorado", 5.25m),
                new State("OH", "Ohio", 6.25m)
            };
        }

        public Dictionary<DateTime, List<Order>> GetOrders()
        {
            return orders;
        }

        public Product[] GetProducts()
        {
            return products.ToArray();
        }

        public State[] GetStateTaxes()
        {
            return states.ToArray();
        }

        public Order LoadOrder(DateTime date, int orderNumber)
        {
            if (!orders.ContainsKey(date))
            {
                return null;
            }

            return (from o in orders[date]
                    where o.OrderNumber == orderNumber
                    select o).FirstOrDefault();
        }

        public bool RemoveOrder(DateTime date, int orderNumber)
        {
            if (!orders.ContainsKey(date))
            {
                return false;
            }

            int removed = orders[date].RemoveAll(o => o.OrderNumber == orderNumber);
            if (orders[date].Count == 0)
            {
                orders.Remove(date);
            }
            return removed > 0;
        }

        public void SaveOrder(DateTime date, Order order)
        {
            if (!orders.ContainsKey(date))
            {
                orders.Add(date, new List<Order>());
            }
            orders[date].Add(order);
        }
    }
}
