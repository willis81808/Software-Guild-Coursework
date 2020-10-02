using CarDealership.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Models
{
    public class AddCarViewModel
    {
        public CarModel Car { get; set; }
        public IEnumerable<SelectListItem> Makes;
        public IEnumerable<SelectListItem> Models;
        public IEnumerable<SelectListItem> Transmissions;
        public IEnumerable<SelectListItem> Bodies;
        public IEnumerable<SelectListItem> Colors;
        public IEnumerable<SelectListItem> Interiors;

        public AddCarViewModel Populate()
        {
            this.Makes = from m in DataManager.Instance.GetMakes()
                          select new SelectListItem
                          {
                              Value = m.Name,
                              Text = m.Name
                          };
            this.Transmissions = from t in DataManager.Instance.GetTransmissions()
                                  select new SelectListItem
                                  {
                                      Value = t.Type,
                                      Text = t.Type
                                  };
            this.Bodies = from b in DataManager.Instance.GetBodies()
                          select new SelectListItem
                          {
                              Value = b.Type,
                              Text = b.Type
                          };
            this.Colors = from c in DataManager.Instance.GetColors()
                          select new SelectListItem
                          {
                              Value = c.Name,
                              Text = c.Name
                          };
            this.Interiors = from i in DataManager.Instance.GetInteriors()
                             select new SelectListItem
                             {
                                 Value = i.Type,
                                 Text = i.Type
                             };
            this.Models = new List<SelectListItem>();
            return this;
        }
    }

    public class UserViewModel
    {
        public string UserId { get; set; }
        
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }

    public class VehicleSearchViewModel
    {
        public IEnumerable<CarModel> Results { get; set; }
        public string Query { get; set; }
        public string PriceMin { get; set; }
        public string PriceMax { get; set; }
        public string YearMin { get; set; }
        public string YearMax { get; set; }

        public IEnumerable<SelectListItem> Years
        {
            get
            {
                var list = new List<SelectListItem>();
                for (int i = 1900; i <= DateTime.Now.Year; i++)
                {
                    list.Add(new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = i.ToString()
                    });
                }
                list.Reverse();
                return list;
            }
        }
        public IEnumerable<SelectListItem> Prices
        {
            get
            {
                var list = new List<SelectListItem>();
                for (int i = 0; i <= 100000; i += 10000)
                {
                    list.Add(new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = "$" + i.ToString()
                    });
                }
                list.Reverse();
                return list;
            }
        }
    }

    public class AdminEditUserViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public string Role { get; set; }
        public IEnumerable<SelectListItem> RoleOptions { get; set; }
    }

    public class AddMakeViewModel
    {
        [Required]
        public string Make { get; set; }
    }

    public class AddModelViewModel
    {
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }

        public IEnumerable<SelectListItem> Makes
        {
            get
            {
                return from m in DataManager.Instance.GetMakes()
                       select new SelectListItem
                       {
                           Value = m.Name,
                           Text = m.Name
                       };
            }
        }
    }

    public class SpecialsViewModel
    {
        public int SpecialId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}