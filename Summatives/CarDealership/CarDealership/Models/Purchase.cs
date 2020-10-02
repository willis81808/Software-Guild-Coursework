using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDealership.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public virtual Car Vehicle { get; set; }
        public string SalesPersonId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZIP { get; set; }
        public decimal Price { get; set; }
        public string PurchaseType { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}