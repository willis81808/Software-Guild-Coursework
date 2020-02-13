using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
    public class Product
    {
        public string ProductType;
        public decimal CostPerSquareFoot;
        public decimal LaborCostPerSquareFoot;

        public Product(string type, decimal squareFootCost, decimal squareFootLaborCost)
        {
            ProductType = type;
            CostPerSquareFoot = squareFootCost;
            LaborCostPerSquareFoot = squareFootLaborCost;
        }
    }
}
