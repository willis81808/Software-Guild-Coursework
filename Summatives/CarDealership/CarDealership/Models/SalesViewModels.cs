using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Models
{
    public class PurchaseViewModel
    {
        public int CarId { get; set; }

        [Required]
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Range(10000, 99999, ErrorMessage = "Zip code must be 5 digits")]
        [Required]
        public int ZIP { get; set; }
        [Required]
        public decimal PurchasePrice { get; set; }
        [Required]
        public string PurchaseType { get; set; }

        public IEnumerable<SelectListItem> GetPurchaseTypes()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "Bank Finance", Text = "Bank Finance" });
            list.Add(new SelectListItem { Value = "Cash", Text = "Cash" });
            list.Add(new SelectListItem { Value = "Dealer Finance", Text = "Dealer Finance" });
            return list;
        }
        public IEnumerable<SelectListItem> GetStates()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "AL", Text = "AL" });
            list.Add(new SelectListItem { Value = "AK", Text = "AK" });
            list.Add(new SelectListItem { Value = "AZ", Text = "AZ" });
            list.Add(new SelectListItem { Value = "AR", Text = "AR" });
            list.Add(new SelectListItem { Value = "CA", Text = "CA" });
            list.Add(new SelectListItem { Value = "CO", Text = "CO" });
            list.Add(new SelectListItem { Value = "CT", Text = "CT" });
            list.Add(new SelectListItem { Value = "DE", Text = "DE" });
            list.Add(new SelectListItem { Value = "FL", Text = "FL" });
            list.Add(new SelectListItem { Value = "GA", Text = "GA" });
            list.Add(new SelectListItem { Value = "HI", Text = "HI" });
            list.Add(new SelectListItem { Value = "ID", Text = "ID" });
            list.Add(new SelectListItem { Value = "IL", Text = "IL" });
            list.Add(new SelectListItem { Value = "IN", Text = "IN" });
            list.Add(new SelectListItem { Value = "IA", Text = "IA" });
            list.Add(new SelectListItem { Value = "KS", Text = "KS" });
            list.Add(new SelectListItem { Value = "KY", Text = "KY" });
            list.Add(new SelectListItem { Value = "LA", Text = "LA" });
            list.Add(new SelectListItem { Value = "ME", Text = "ME" });
            list.Add(new SelectListItem { Value = "MD", Text = "MD" });
            list.Add(new SelectListItem { Value = "MA", Text = "MA" });
            list.Add(new SelectListItem { Value = "MI", Text = "MI" });
            list.Add(new SelectListItem { Value = "MN", Text = "MN" });
            list.Add(new SelectListItem { Value = "MS", Text = "MS" });
            list.Add(new SelectListItem { Value = "MO", Text = "MO" });
            list.Add(new SelectListItem { Value = "MT", Text = "MT" });
            list.Add(new SelectListItem { Value = "NE", Text = "NE" });
            list.Add(new SelectListItem { Value = "NV", Text = "NV" });
            list.Add(new SelectListItem { Value = "NH", Text = "NH" });
            list.Add(new SelectListItem { Value = "NJ", Text = "NJ" });
            list.Add(new SelectListItem { Value = "NM", Text = "NM" });
            list.Add(new SelectListItem { Value = "NY", Text = "NY" });
            list.Add(new SelectListItem { Value = "NC", Text = "NC" });
            list.Add(new SelectListItem { Value = "ND", Text = "ND" });
            list.Add(new SelectListItem { Value = "OH", Text = "OH" });
            list.Add(new SelectListItem { Value = "OK", Text = "OK" });
            list.Add(new SelectListItem { Value = "OR", Text = "OR" });
            list.Add(new SelectListItem { Value = "PA", Text = "PA" });
            list.Add(new SelectListItem { Value = "RI", Text = "RI" });
            list.Add(new SelectListItem { Value = "SC", Text = "SC" });
            list.Add(new SelectListItem { Value = "SD", Text = "SD" });
            list.Add(new SelectListItem { Value = "TN", Text = "TN" });
            list.Add(new SelectListItem { Value = "TX", Text = "TX" });
            list.Add(new SelectListItem { Value = "UT", Text = "UT" });
            list.Add(new SelectListItem { Value = "VT", Text = "VT" });
            list.Add(new SelectListItem { Value = "VA", Text = "VA" });
            list.Add(new SelectListItem { Value = "WA", Text = "WA" });
            list.Add(new SelectListItem { Value = "WV", Text = "WV" });
            list.Add(new SelectListItem { Value = "WI", Text = "WI" });
            list.Add(new SelectListItem { Value = "WY", Text = "WY" });
            return list;
        }
    }
}