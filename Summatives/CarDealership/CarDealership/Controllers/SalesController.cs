using CarDealership.Data;
using CarDealership.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    [Authorize(Roles = "Sales")]
    public class SalesController : Controller
    {
        public ActionResult Index(string Message = "")
        {
            ViewBag.Message = Message;
            var model = new VehicleSearchViewModel
            {
                Results = DataManager.Instance.GetAvailableCars().OrderByDescending(c => c.MSRP)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(VehicleSearchViewModel model)
        {
            // parse search parameters
            int priceMax = model.PriceMax == null ? int.MaxValue : int.Parse(model.PriceMax);
            int priceMin = model.PriceMin == null ? int.MinValue : int.Parse(model.PriceMin);
            int yearMax = model.YearMax == null ? int.MaxValue : int.Parse(model.YearMax);
            int yearMin = model.YearMin == null ? int.MinValue : int.Parse(model.YearMin);

            // filter all cars
            bool queryEmpty = model.Query.IsNullOrWhiteSpace();
            model.Results = from c in DataManager.Instance.GetAvailableCars()
                            where c.MSRP < priceMax && c.MSRP > priceMin && c.Year < yearMax && c.Year > yearMin
                            orderby c.MSRP descending
                            select c;

            // apply search query
            model.Results = model.Results.Where(c => (queryEmpty || (c.Year.ToString() + c.Make + c.Model).ContainsSubstring(model.Query, 3)));

            // display results
            return View(model);
        }

        public ActionResult Purchase(int id)
        {
            ViewBag.Car = DataManager.Instance.GetCarById(id);

            if (ViewBag.Car == null)
                return RedirectToAction("Index");

            return View(new PurchaseViewModel
            {
                CarId = id
            });
        }

        [HttpPost]
        public ActionResult Purchase(PurchaseViewModel model)
        {
            var car = DataManager.Instance.GetCarById(model.CarId);
            ViewBag.Car = car;
            if (ViewBag.Car == null)
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                if (model.Email.IsNullOrWhiteSpace() && model.Phone.IsNullOrWhiteSpace())
                {
                    ModelState.AddModelError("", "You must include at least an email or phone number");
                    return View(model);
                }

                if (model.PurchasePrice < car.Price * 0.95m || model.PurchasePrice > car.MSRP)
                {
                    ModelState.AddModelError("PurchasePrice", "Purchase Price cannot exceed the vehicle's MSRP, and must be greater than 95% of the sales price.");
                    return View(model);
                }

                DataManager.Instance.AddPurchase(model, User.Identity.GetUserId());
                return RedirectToAction("Index", new { Message = "Purchase successfully logged." });
            }

            return View(model);
        }
    }
}