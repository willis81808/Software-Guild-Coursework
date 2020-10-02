using System.Collections.Generic;

namespace CarDealership.Models
{
    public partial class Body
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Body()
        {
            this.Cars = new HashSet<Car>();
        }

        public Body(int id, string type)
        {
            this.BodyId = id;
            this.Type = type;
            this.Cars = new HashSet<Car>();
        }

        public int BodyId { get; set; }
        public string Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Car> Cars { get; set; }
    }
}