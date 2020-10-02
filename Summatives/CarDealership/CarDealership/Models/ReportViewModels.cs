using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Models
{
    public class UserSales
    {
        public string Name { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalVehicles { get; set; }
    }
    public class SalesReportViewModel
    {
        public IEnumerable<UserSales> Statistics { get; set; }

        public string User { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }
    }

    public class VehicleStock
    {
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Count { get; set; }
        public decimal StockValue { get; set; }
    }
    public class InventoryReportViewModel
    {
        public IEnumerable<VehicleStock> NewStock { get; set; }
        public IEnumerable<VehicleStock> UsedStock { get; set; }
    }
}