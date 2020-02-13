using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IDataRepository
    {
        Order LoadOrder(DateTime date, int orderNumber);
        bool RemoveOrder(DateTime date, int orderNumber);
        void SaveOrder(DateTime date, Order order);

        Dictionary<DateTime, List<Order>> GetOrders();
        Product[] GetProducts();
        State[] GetStateTaxes();
    }
}
