using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealership.Data;
using CarDealership.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace CarDealership.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sales()
        {
            var model = AggregateSales(null, null, null);
            return View(model);
        }
        [HttpPost]
        public ActionResult Sales(string user, DateTime? fromDate, DateTime? toDate)
        {
            var model = AggregateSales(user, fromDate, toDate);
            return View(model);
        }

        public ActionResult Inventory()
        {
            var newStock = new Dictionary<string, VehicleStock>();
            var usedStock = new Dictionary<string, VehicleStock>();
            var inventory = DataManager.Instance.GetAvailableCars();
            foreach (var car in inventory)
            {
                var isNew = car.Mileage == 0;
                var key = $"{car.Year} {car.Make} {car.Model}";
                if (!newStock.ContainsKey(key) && !usedStock.ContainsKey(key))
                {
                    (isNew ? newStock : usedStock).Add(key, new VehicleStock
                    {
                        Year = car.Year,
                        Make = car.Make,
                        Model = car.Model,
                        Count = 0,
                        StockValue = 1000
                    });
                }

                var vals = (isNew ? newStock : usedStock)[key];
                vals.StockValue += car.MSRP;
                vals.Count++;
            }

            var model = new InventoryReportViewModel
            {
                NewStock= newStock.Values.OrderBy(s => s.Make).ThenBy(s => s.Model).ThenByDescending(s => s.Year),
                UsedStock = usedStock.Values.OrderBy(s => s.Make).ThenBy(s => s.Model).ThenByDescending(s => s.Year)
            };
            return View(model);
        }

        SalesReportViewModel AggregateSales(string targetUser, DateTime? fromDate, DateTime? toDate)
        {
            var stats = new Dictionary<string, UserSales>();
            var purchases = from p in DataManager.Instance.GetPurchases()
                            where (fromDate.HasValue ? p.PurchaseDate > fromDate.Value : true) &&
                                  (toDate.HasValue ? p.PurchaseDate < toDate.Value.AddDays(1) : true)
                            select p;
            var users = new List<string>();
            var UserManager = AccountController.GetUserManager(HttpContext);
            foreach (var p in purchases)
            {
                var user = UserManager.FindById(p.SalesPersonId);
                var name = $"{user.FirstName} {user.LastName}";
                if (!stats.ContainsKey(p.SalesPersonId))
                {
                    if (targetUser.IsNullOrWhiteSpace() || targetUser == name)
                    {
                        users.Add(name);
                        stats.Add(p.SalesPersonId, new UserSales
                        {
                            Name = name,
                            TotalSales = 0,
                            TotalVehicles = 0
                        });
                    }
                }

                if (targetUser.IsNullOrWhiteSpace() || targetUser == name)
                {
                    var vals = stats[p.SalesPersonId];
                    vals.TotalSales += p.Price;
                    vals.TotalVehicles++;
                }
            }
            return new SalesReportViewModel
            {
                Statistics = stats.Values,
                Users = from u in users
                        select new SelectListItem { Value = u, Text = u }
            };
        }
    }
}