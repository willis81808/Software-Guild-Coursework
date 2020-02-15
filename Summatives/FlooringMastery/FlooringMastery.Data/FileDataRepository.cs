using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class FileDataRepository : IDataRepository
    {

        private static Dictionary<DateTime, List<Order>> orders
        {
            get
            {
                var data = new Dictionary<DateTime, List<Order>>();

                foreach (var file in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Data\\Orders")))
                {
                    // parse date
                    var date = DateTime.Parse(Path.GetFileName(file)
                        .Split('_')[1]
                        .Split('.')[0]
                        .Insert(4, "/")
                        .Insert(2, "/"));

                    // parse orders
                    var text = File.ReadLines(file).ToList();
                    text.RemoveAt(0);   // remove header line
                    var list = from line in text
                               select line.Split(',') into parts
                               select new Order
                               {
                                   OrderNumber = int.Parse(parts[0]),
                                   CustomerName = parts[1],
                                   State = parts[2],
                                   TaxRate = decimal.Parse(parts[3]),
                                   ProductType = parts[4],
                                   Area = decimal.Parse(parts[5]),
                                   CostPerSquareFoot = decimal.Parse(parts[6]),
                                   LaborCostPerSquareFoot = decimal.Parse(parts[7]),
                                   MaterialCost = decimal.Parse(parts[8]),
                                   LaborCost = decimal.Parse(parts[9]),
                                   Tax = decimal.Parse(parts[10]),
                                   Total = decimal.Parse(parts[11])
                               };
                    
                    data.Add(date, list.ToList());
                }

                return data;
            }
        }
        private static List<Product> products
        {
            get
            {
                var data = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "Data\\Products.txt")).ToList();
                data.RemoveAt(0);   // remove header line
                var list = from line in data
                           select line.Split(',') into parts
                           select new Product
                           {
                               ProductType = parts[0],
                               CostPerSquareFoot = decimal.Parse(parts[1]),
                               LaborCostPerSquareFoot = decimal.Parse(parts[2])
                           };
                return list.ToList();
            }
        }
        private static List<State> states
        {
            get
            {
                var data = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "Data\\Taxes.txt")).ToList();
                data.RemoveAt(0);   // remove header line
                var list = from line in data
                           select line.Split(',') into parts
                           select new State
                           {
                               StateAbbreviation = parts[0],
                               StateName = parts[1],
                               TaxRate = decimal.Parse(parts[2])
                           };
                return list.ToList();
            }
        }

        static FileDataRepository()
        {
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Data")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Data"));
            }
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Data\\Orders")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Data\\Orders"));
            }
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
            if (orders.ContainsKey(date))
            {
                return (from o in orders[date]
                        where o.OrderNumber == orderNumber
                        select o).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public bool RemoveOrder(DateTime date, int orderNumber)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\Orders\\Orders_{date.ToString("MM/dd/yyyy").Replace("/", "")}.txt");
            if (File.Exists(path))
            {
                List<Order> data = orders[date];
                data.RemoveAll(o => o.OrderNumber == orderNumber);
                File.Delete(path);
                foreach (var order in data)
                {
                    SaveOrder(date, order);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SaveOrder(DateTime date, Order order)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\Orders\\Orders_{date.ToString("MM/dd/yyyy").Replace("/", "")}.txt");
            if (!File.Exists(path))
            {
                // create new file with required header
                using (var file = File.CreateText(path))
                {
                    file.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                }
            }
            using (var file = File.AppendText(path))
            {
                file.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                    order.OrderNumber,
                    order.CustomerName,
                    order.State,
                    order.TaxRate,
                    order.ProductType,
                    order.Area,
                    order.CostPerSquareFoot,
                    order.LaborCost,
                    order.MaterialCost,
                    order.LaborCost,
                    order.Tax,
                    order.Total));
            }
        }
    }
}
