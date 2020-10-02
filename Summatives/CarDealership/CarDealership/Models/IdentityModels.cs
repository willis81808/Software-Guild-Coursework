using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarDealership.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("FirstName", FirstName));
            userIdentity.AddClaim(new Claim("LastName", LastName));
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Body> Bodies { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Interior> Interiors { get; set; }
        public virtual DbSet<Make> Makes { get; set; }
        public virtual DbSet<MakeModel> MakeModels { get; set; }
        public virtual DbSet<Transmission> Transmissions { get; set; }
        public virtual DbSet<Special> Specials { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}