using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealership.Models
{
    public partial class Car
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string VIN { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal MSRP { get; set; }
        public bool Featured { get; set; }

        public int BodyId { get; set; }
        public int CarColorId { get; set; }
        public int? InteriorColorId { get; set; }
        public int InteriorId { get; set; }
        public int ModelId { get; set; }
        public int TransmissionId { get; set; }

        public virtual Body Body { get; set; }
        [ForeignKey("CarColorId")]
        [InverseProperty("Cars")]
        public virtual Color CarColor { get; set; }
        [ForeignKey("InteriorColorId")]
        [InverseProperty("Cars1")]
        public virtual Color InteriorColor { get; set; }
        public virtual Interior Interior { get; set; }
        [ForeignKey("ModelId")]
        public virtual MakeModel MakeModel { get; set; }
        public virtual Transmission Transmission { get; set; }
    }
}
