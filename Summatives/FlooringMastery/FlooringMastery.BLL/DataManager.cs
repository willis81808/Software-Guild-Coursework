using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class DataManager
    {
        public State[] States { get { return orderRepository.GetStateTaxes(); } }
        public Product[] Products { get { return orderRepository.GetProducts(); } }
        public Dictionary<DateTime, List<Order>> Orders { get { return orderRepository.GetOrders(); } }

        private IDataRepository orderRepository;

        public DataManager(IDataRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public OrderLookupResponse LookupOrder(DateTime date, int orderNumber)
        {
            var response = new OrderLookupResponse();

            response.Order = orderRepository.LoadOrder(date, orderNumber);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = $"Error: No order number {orderNumber} found on {date.ToShortDateString()}";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        public Response UpdateOrder(Order newOrder, DateTime date, int orderNumber)
        {
            var response = new Response();

            response.Success = orderRepository.RemoveOrder(date, orderNumber);

            if (response.Success)
            {
                orderRepository.SaveOrder(date, newOrder);
            }
            else
            {
                response.Message = $"Error: No order number {orderNumber} found on {date.ToShortDateString()}";
            }

            return response;
        }

        public void AddOrder(DateTime date, Order order)
        {
            if (Orders.ContainsKey(date))
            {
                order.OrderNumber = Orders[date].Count + 1;
            }
            else
            {
                order.OrderNumber = 1;
            }
            orderRepository.SaveOrder(date, order);
        }
    }
}
