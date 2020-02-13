using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class FileDataRepository : IDataRepository
    {
        public Dictionary<DateTime, List<Order>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Product[] GetProducts()
        {
            throw new NotImplementedException();
        }

        public State[] GetStateTaxes()
        {
            throw new NotImplementedException();
        }

        public Order LoadOrder(DateTime date, int orderNumber)
        {
            throw new NotImplementedException();
        }

        public bool RemoveOrder(DateTime date, int orderNumber)
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(DateTime date, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
