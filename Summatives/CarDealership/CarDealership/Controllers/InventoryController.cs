using CarDealership.Data;
using CarDealership.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    public class InventoryController : Controller
    {
        public ActionResult New()
        {
            var model = new VehicleSearchViewModel()
            {
                Results = new List<CarModel>()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult New(VehicleSearchViewModel model)
        {
            // parse search parameters
            int priceMax = model.PriceMax == null ? int.MaxValue : int.Parse(model.PriceMax);
            int priceMin = model.PriceMin == null ? int.MinValue : int.Parse(model.PriceMin);
            int yearMax = model.YearMax == null ? int.MaxValue : int.Parse(model.YearMax);
            int yearMin = model.YearMin == null ? int.MinValue : int.Parse(model.YearMin);

            // filter all cars
            var queryEmpty = model.Query.IsNullOrWhiteSpace();
            model.Results = (from c in DataManager.Instance.GetAvailableCars()
                             where c.Mileage < 1000 && c.MSRP < priceMax && c.MSRP > priceMin && c.Year < yearMax && c.Year > yearMin
                             orderby c.MSRP descending
                             select c).Take(20);

            // apply search query
            model.Results = model.Results.Where(c => (queryEmpty || (c.Year.ToString() + c.Make + c.Model).ContainsSubstring(model.Query, 3)));

            // display results
            return View(model);
        }

        public ActionResult Used()
        {
            var model = new VehicleSearchViewModel()
            {
                Results = new List<CarModel>()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Used(VehicleSearchViewModel model)
        {
            // parse search parameters
            int priceMax = model.PriceMax == null ? int.MaxValue : int.Parse(model.PriceMax);
            int priceMin = model.PriceMin == null ? int.MinValue : int.Parse(model.PriceMin);
            int yearMax = model.YearMax == null ? int.MaxValue : int.Parse(model.YearMax);
            int yearMin = model.YearMin == null ? int.MinValue : int.Parse(model.YearMin);

            // filter all cars
            var queryEmpty = model.Query.IsNullOrWhiteSpace();
            model.Results = (from c in DataManager.Instance.GetAvailableCars()
                             where c.Mileage >= 1000 && c.MSRP < priceMax && c.MSRP > priceMin && c.Year < yearMax && c.Year > yearMin
                             orderby c.MSRP descending
                             select c).Take(20);

            // apply search query
            model.Results = model.Results.Where(c => (queryEmpty || (c.Year.ToString() + c.Make + c.Model).ContainsSubstring(model.Query, 3)));

            // display results
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var car = DataManager.Instance.GetCarById(id);
            if (car == null)
                return RedirectToAction("Index", "Home");
            return View(car);
        }
    }
}