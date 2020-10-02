using CarDealership.Data;
using CarDealership.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomepageViewModel
            {
                Specials = from s in DataManager.Instance.GetSpecials()
                           select new SpecialsViewModel { Title = s.Title, Description = s.Description },
                Featured = DataManager.Instance.GetFeatured()
            };
            return View(model);
        }

        public ActionResult Specials()
        {
            return View(DataManager.Instance.GetSpecials());
        }
        
        public ActionResult Contact(string vin = "", string Notice = "")
        {
            ViewBag.Notice = Notice;

            var model = new ContactViewModel
            {
                Message = vin.IsNullOrWhiteSpace() ? "" : $"VIN: {vin}"
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!(model.Email.IsNullOrWhiteSpace() && model.Phone.IsNullOrWhiteSpace()))
                {
                    DataManager.Instance.AddContact(model.Name, model.Email, model.Phone, model.Message);
                    return RedirectToAction("Contact", new { Notice = "Your contact has been recieved. Our representatives will reach out shortly!" });
                }

                // both email and phone number were blank
                ModelState.AddModelError("Email", "You must at least an email or phone number");
            }

            // if we got there then the model is invalid
            return View(model);
        }
    }
}