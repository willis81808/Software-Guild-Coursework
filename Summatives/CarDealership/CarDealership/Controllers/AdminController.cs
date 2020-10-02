using CarDealership.Data;
using CarDealership.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ActionResult Vehicles(string Message = "", bool showPurchased = false)
        {
            ViewBag.Message = Message;
            var model = new VehicleSearchViewModel
            {
                Results = (showPurchased ? DataManager.Instance.GetPurchasedCars() : DataManager.Instance.GetAvailableCars()).OrderByDescending(c => c.MSRP)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Vehicles(VehicleSearchViewModel model, bool showPurchased = false)
        {
            // parse search parameters
            int priceMax = model.PriceMax == null ? int.MaxValue : int.Parse(model.PriceMax);
            int priceMin = model.PriceMin == null ? int.MinValue : int.Parse(model.PriceMin);
            int yearMax = model.YearMax == null ? int.MaxValue : int.Parse(model.YearMax);
            int yearMin = model.YearMin == null ? int.MinValue : int.Parse(model.YearMin);

            // filter all cars
            bool queryEmpty = model.Query.IsNullOrWhiteSpace();
            model.Results = from c in showPurchased ? DataManager.Instance.GetPurchasedCars() : DataManager.Instance.GetAvailableCars()
                            where c.MSRP < priceMax && c.MSRP > priceMin && c.Year < yearMax && c.Year > yearMin
                            orderby c.MSRP descending
                            select c;

            // apply search query
            model.Results = model.Results.Where(c => (queryEmpty || (c.Year.ToString() + c.Make + c.Model).ContainsSubstring(model.Query, 3)));

            // display results
            return View(model);
        }

        public ActionResult AddVehicle()
        {
            var model = new AddCarViewModel().Populate();
            model.Car = new CarModel();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult AddVehicle(AddCarViewModel model)
        {
            if (ModelState.IsValid)
            {
                DataManager.Instance.AddCar(model.Car);
                return RedirectToAction("Vehicles");
            }

            // if we got there then the model is invalid
            return View(model.Populate());
        }

        public ActionResult EditVehicle(int id)
        {
            var car = DataManager.Instance.GetCarById(id);
            if (car == null)
                return RedirectToAction("Vehicles");
            var model = new AddCarViewModel().Populate();
            model.Car = car;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditVehicle(AddCarViewModel model)
        {
            if (ModelState.IsValid)
            {
                DataManager.Instance.EditCar(model.Car);
                return RedirectToAction("Vehicles");
            }

            // if we got here then the model is invalid
            return View(model.Populate());
        }
        
        [HttpPost]
        public ActionResult DeleteVehicle(int id)
        {
            bool result = DataManager.Instance.DeleteCar(id);
            return RedirectToAction("Vehicles", "Admin", new { Message = (result ? "Vehicle deleted" : "") });
        }

        public async Task<ActionResult> Users(string Message = "")
        {
            ViewBag.Message = Message;

            var userManager = AccountController.GetUserManager(HttpContext);
            var users = new List<UserViewModel>();
            foreach (var user in userManager.Users.ToList())
            {
                var u = new UserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                    
                };
                u.Roles = await userManager.GetRolesAsync(user.Id);
                users.Add(u);
            }
            
            return View(users);
        }

        public ActionResult AddUser()
        {
            ViewBag.Roles = from r in ApplicationDbContext.Create().Roles
                            select new SelectListItem
                            {
                                Value = r.Name,
                                Text = r.Name
                            };

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { FirstName = model.FirstName, LastName = model.LastName, UserName = model.UserName, Email = model.Email };
                var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var currentUser = await UserManager.FindByNameAsync(user.UserName);
                    var roleResult = await UserManager.AddToRoleAsync(user.Id, model.Role);
                    
                    return RedirectToAction("Users", "Admin", new { Message = "User created" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.Roles = from r in ApplicationDbContext.Create().Roles
                            select new SelectListItem
                            {
                                Value = r.Name,
                                Text = r.Name
                            };
            return View(model);
        }
        
        public async Task<ActionResult> EditUser(string id)
        {
            var UserManager = AccountController.GetUserManager(HttpContext);
            var user = await UserManager.FindByIdAsync(id);
            var model = new AdminEditUserViewModel
            {
                UserId = id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = UserManager.GetRoles(id).FirstOrDefault(),
                RoleOptions = from r in ApplicationDbContext.Create().Roles
                              select new SelectListItem
                              {
                                  Value = r.Name,
                                  Text = r.Name
                              },
        };
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(AdminEditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var UserManager = AccountController.GetUserManager(HttpContext);

            // update name and/or email
            var user = UserManager.FindById(model.UserId);
            if (user == null)
                return RedirectToAction("Users", "Admin", new { Message = $"User {model.UserId} does not exist" });
            if (!model.FirstName.IsNullOrWhiteSpace()) user.FirstName = model.FirstName;
            if (!model.LastName.IsNullOrWhiteSpace()) user.LastName = model.LastName;
            if (!model.UserName.IsNullOrWhiteSpace()) user.UserName = model.UserName;
            if (!model.Email.IsNullOrWhiteSpace()) user.Email = model.Email;
            await UserManager.UpdateAsync(user);

            // update password
            if (!model.NewPassword.IsNullOrWhiteSpace() && model.NewPassword.Equals( model.ConfirmPassword ))
            {
                UserManager.RemovePassword(model.UserId);
                var result = UserManager.AddPassword(model.UserId, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Users", "Admin", new { Message = "User Successfully Updated" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }

                return View(model);
            }

            // update role
            var roles = await UserManager.GetRolesAsync(model.UserId);
            await UserManager.RemoveFromRolesAsync(model.UserId, roles.ToArray());
            await UserManager.AddToRoleAsync(model.UserId, model.Role);

            return RedirectToAction("Users", "Admin", new { Message = "User Successfully Updated" });
        }

        public ActionResult Makes()
        {
            return View(new AddMakeViewModel());
        }

        [HttpPost]
        public ActionResult Makes(AddMakeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // attempt to add new make
                bool success = DataManager.Instance.AddMake(model.Make);
                if (success)
                    return RedirectToAction("Vehicles", new { Message = $"Successfully created the new make: '{model.Make}'" });
                
                // if we got here then the make already exists!
                ModelState.AddModelError("Make", $"The make '{model.Make}' already exists.");
            }

            // if we got there then the submission was invalid
            return View(model);
        }

        public ActionResult Models()
        {
            return View(new AddModelViewModel());
        }

        [HttpPost]
        public ActionResult Models(AddModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool success = DataManager.Instance.AddModel(viewModel.Make, viewModel.Model);
                if (success)
                    return RedirectToAction("Vehicles", new { Message = $"Successfully added {viewModel.Model} to the {viewModel.Make} make" });

                // if we got here the model already exists for this make
                ModelState.AddModelError("Model", $"The model '{viewModel.Model}' already exists for the {viewModel.Make} make");
            }

            // if we got here the model was invalid
            return View(viewModel);
        }

        public ActionResult Specials(string Message = "")
        {
            ViewBag.Message = Message;
            ViewBag.Specials = from s in DataManager.Instance.GetSpecials()
                               select new SpecialsViewModel { SpecialId = s.SpecialId, Title = s.Title, Description = s.Description };
            return View(new SpecialsViewModel());
        }

        [HttpPost]
        public ActionResult Specials(SpecialsViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool success = DataManager.Instance.AddSpecial(model.Title, model.Description);
                if (success)
                    return RedirectToAction("Specials", new { Message = "Successfully created a new Special" });

                // if we got here then a special already exists with the given title
                ModelState.AddModelError("Title", $"A Special already exists titled '{model.Title}'");
            }

            // if we got here the model was invalid
            ViewBag.Specials = from s in DataManager.Instance.GetSpecials()
                               select new SpecialsViewModel { SpecialId = s.SpecialId, Title = s.Title, Description = s.Description };
            return View(model);
        }

        public ActionResult EditSpecial(int id)
        {
            var special = DataManager.Instance.GetSpecials().Where(s => s.SpecialId == id).FirstOrDefault();
            if (special == null)
                return RedirectToAction("Specials", new { Message = $"Special #{id} not found" });
            return View(new SpecialsViewModel
            {
                SpecialId = id,
                Title = special.Title,
                Description = special.Description
            });
        }

        [HttpPost]
        public ActionResult EditSpecial(SpecialsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var success = DataManager.Instance.EditSpecial(model);
                if (success)
                    return RedirectToAction("Specials", new { Message = "Successfully edited special" });

                if (!DataManager.Instance.GetSpecials().Any(s => s.SpecialId == model.SpecialId))
                    return RedirectToAction("Specials", new { Message = "That Special no longer exists" });

                ModelState.AddModelError("Title", $"A Special already exists titled '{model.Title}'");
            }

            // if we got here then the model was invalid
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteSpecial(int id)
        {
            bool success = DataManager.Instance.DeleteSpecial(id);
            return RedirectToAction("Specials", new { Message = success ? "Special deleted" : "" });
        }
    }
}