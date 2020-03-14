using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
    public class Order
    {
        public int OrderNumber;
        public string CustomerName;
        public string State;
        public decimal TaxRate;
        public string ProductType;
        public decimal Area;
        public decimal CostPerSquareFoot;
        public decimal LaborCostPerSquareFoot;
        public decimal MaterialCost;
        public decimal LaborCost;
        public decimal Tax;
        public decimal Total;

        public Order()
        {
            // do nothing
        }
        public Order(string name, State state, Product product, decimal area)
        {
            CustomerName = name;
            State = state.StateAbbreviation;
            TaxRate = state.TaxRate;
            ProductType = product.ProductType;
            Area = area;
            CostPerSquareFoot = product.CostPerSquareFoot;
            LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
            MaterialCost = Area * CostPerSquareFoot;
            LaborCost = Area * LaborCostPerSquareFoot;
            Tax = (MaterialCost + LaborCost) * (TaxRate / 100);
            Total = MaterialCost + LaborCost + Tax;
        }
    }
}
