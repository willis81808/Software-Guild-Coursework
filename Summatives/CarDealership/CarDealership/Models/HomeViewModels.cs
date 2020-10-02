using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDealership.Models
{
    public class HomepageViewModel
    {
        public IEnumerable<SpecialsViewModel> Specials { get; set; }
        public IEnumerable<CarModel> Featured { get; set; }
    }

    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Message { get; set; }
    }
}