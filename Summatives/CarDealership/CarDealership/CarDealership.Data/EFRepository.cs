using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CarDealership.Models;

namespace CarDealership.Data
{
    public class EFRepository : ICarRepository
    {
        public Car AddCar(CarModel car)
        {
            var db = ApplicationDbContext.Create();

            // get or create table entries
            var carColor = db.Colors.Where(c => c.Name == car.CarColor).FirstOrDefault() ?? new Color { Name = car.CarColor };
            var interiorColor = (car.InteriorColor == car.CarColor) ? carColor : db.Colors.Where(c => c.Name == car.InteriorColor).FirstOrDefault() ?? new Color { Name = car.InteriorColor };
            var interior = db.Interiors.Where(i => i.Type == car.Interior).FirstOrDefault() ?? new Interior { Type = car.Interior };
            var transmission = db.Transmissions.Where(t => t.Type == car.Transmission).FirstOrDefault() ?? new Transmission { Type = car.Transmission };
            var body = db.Bodies.Where(b => b.Type == car.Body).FirstOrDefault() ?? new Body { Type = car.Body };
            var make = db.Makes.Where(m => m.Name == car.Make).FirstOrDefault() ?? new Make { Name = car.Make };
            var model = db.MakeModels.Where(m => m.Name == car.Model).FirstOrDefault() ?? new MakeModel { Name = car.Model, Make = make };

            // add car
            var newCar = new Car
            {
                Year = car.Year,
                Mileage = car.Mileage,
                MakeModel = model,
                Body = body,
                Transmission = transmission,
                CarColor = carColor,
                Interior = interior,
                InteriorColor = interiorColor,
                VIN = car.VIN,
                Price = car.Price,
                MSRP = car.MSRP,
                Featured = car.Featured,
                Description = car.Description
            };
            db.Cars.Add(newCar);
            db.SaveChanges();
            return newCar;
        }

        public bool AddMake(string name)
        {
            var db = ApplicationDbContext.Create();

            if (db.Makes.Any(m => m.Name == name))
                return false;

            db.Makes.Add(new Make
            {
                Name = name
            });
            db.SaveChanges();

            return true;
        }

        public bool AddModel(string make, string name)
        {
            var db = ApplicationDbContext.Create();

            var mk = db.Makes.Where(m => m.Name == make).FirstOrDefault();
            if (mk == null)
            {
                mk = new Make { Name = make };
                db.Makes.Add(mk);
            }

            if (db.MakeModels.Any(m => m.Name == name && m.Make.Name == make))
                return false;

            var model = new MakeModel { Make = mk, Name = name };
            db.MakeModels.Add(model);
            db.SaveChanges();

            return true;
        }

        public bool DeleteCar(int id)
        {
            var db = ApplicationDbContext.Create();

            // no car exists with that id
            if (!db.Cars.Any(c => c.Id == id))
                return false;

            // delete car
            db.Cars.Remove(db.Cars.Where(c => c.Id == id).FirstOrDefault());
            db.SaveChanges();
            return true;
        }

        public void EditCar(CarModel car)
        {
            var db = ApplicationDbContext.Create();
            var item = db.Cars.Where(c => c.Id == car.Id).FirstOrDefault();
            if (item == null)
                return;

            item.CarColor = db.Colors.Where(c => c.Name == car.CarColor).FirstOrDefault() ?? new Color { Name = car.CarColor };
            item.InteriorColor = (car.InteriorColor == car.CarColor) ? item.CarColor: db.Colors.Where(c => c.Name == car.InteriorColor).FirstOrDefault() ?? new Color { Name = car.InteriorColor };
            item.Interior = db.Interiors.Where(i => i.Type == car.Interior).FirstOrDefault() ?? new Interior { Type = car.Interior };
            item.Transmission = db.Transmissions.Where(t => t.Type == car.Transmission).FirstOrDefault() ?? new Transmission { Type = car.Transmission };
            item.Body = db.Bodies.Where(b => b.Type == car.Body).FirstOrDefault() ?? new Body { Type = car.Body };
            var make = db.Makes.Where(m => m.Name == car.Make).FirstOrDefault() ?? new Make { Name = car.Make };
            item.MakeModel = db.MakeModels.Where(m => m.Name == car.Model).FirstOrDefault() ?? new MakeModel { Name = car.Model, Make = make };

            item.Year = car.Year;
            item.Mileage = car.Mileage;
            item.VIN = car.VIN;
            item.Price = car.Price;
            item.MSRP = car.MSRP;
            item.Featured = car.Featured;
            item.Description = car.Description;

            db.SaveChanges();
        }

        public IEnumerable<CarModel> GetAllCars()
        {
            var db = ApplicationDbContext.Create();
            return from c in db.Cars
                   select new CarModel()
                   {
                       Id = c.Id,
                       Year = c.Year,
                       Mileage = c.Mileage,
                       Model = c.MakeModel.Name,
                       Make = c.MakeModel.Make.Name,
                       Body = c.Body.Type,
                       Transmission = c.Transmission.Type,
                       CarColor = c.CarColor.Name,
                       InteriorColor = c.InteriorColor.Name,
                       Interior = c.Interior.Type,
                       VIN = c.VIN,
                       Price = c.Price,
                       MSRP = c.MSRP,
                       Featured = c.Featured,
                       Description = c.Description,
                   };
        }

        public IEnumerable<CarModel> GetAvailableCars()
        {
            // select all cars that are not in the purchased table
            var db = ApplicationDbContext.Create();
            return from c in db.Cars
                   where !(from p in db.Purchases
                           select p.VehicleId)
                           .Contains(c.Id)
                   select new CarModel()
                   {
                       Id = c.Id,
                       Year = c.Year,
                       Mileage = c.Mileage,
                       Model = c.MakeModel.Name,
                       Make = c.MakeModel.Make.Name,
                       Body = c.Body.Type,
                       Transmission = c.Transmission.Type,
                       CarColor = c.CarColor.Name,
                       InteriorColor = c.InteriorColor.Name,
                       Interior = c.Interior.Type,
                       VIN = c.VIN,
                       Price = c.Price,
                       MSRP = c.MSRP,
                       Featured = c.Featured,
                       Description = c.Description
                   };
        }

        public CarModel GetCarById(int id)
        {
            var db = ApplicationDbContext.Create();
            return (from c in db.Cars
                    where c.Id == id
                    select new CarModel()
                    {
                        Id = c.Id,
                        Year = c.Year,
                        Mileage = c.Mileage,
                        Model = c.MakeModel.Name,
                        Make = c.MakeModel.Make.Name,
                        Body = c.Body.Type,
                        Transmission = c.Transmission.Type,
                        CarColor = c.CarColor.Name,
                        InteriorColor = c.InteriorColor.Name,
                        Interior = c.Interior.Type,
                        VIN = c.VIN,
                        Price = c.Price,
                        MSRP = c.MSRP,
                        Featured = c.Featured,
                        Description = c.Description
                    }).FirstOrDefault();
        }

        public IEnumerable<CarModel> GetCarsByMake(string make)
        {
            var db = ApplicationDbContext.Create();
            return from c in db.Cars
                   where c.MakeModel.Make.Name.Contains(make)
                   select new CarModel()
                   {
                       Id = c.Id,
                       Year = c.Year,
                       Mileage = c.Mileage,
                       Model = c.MakeModel.Name,
                       Make = c.MakeModel.Make.Name,
                       Body = c.Body.Type,
                       Transmission = c.Transmission.Type,
                       CarColor = c.CarColor.Name,
                       InteriorColor = c.InteriorColor.Name,
                       Interior = c.Interior.Type,
                       VIN = c.VIN,
                       Price = c.Price,
                       MSRP = c.MSRP,
                       Featured = c.Featured,
                       Description = c.Description
                   };
        }

        public IEnumerable<CarModel> GetCarsByModel(string model)
        {
            var db = ApplicationDbContext.Create();
            return from c in db.Cars
                   where c.MakeModel.Name.Contains(model)
                   select new CarModel()
                   {
                       Id = c.Id,
                       Year = c.Year,
                       Mileage = c.Mileage,
                       Model = c.MakeModel.Name,
                       Make = c.MakeModel.Make.Name,
                       Body = c.Body.Type,
                       Transmission = c.Transmission.Type,
                       CarColor = c.CarColor.Name,
                       InteriorColor = c.InteriorColor.Name,
                       Interior = c.Interior.Type,
                       VIN = c.VIN,
                       Price = c.Price,
                       MSRP = c.MSRP,
                       Featured = c.Featured,
                       Description = c.Description
                   };
        }

        public IEnumerable<CarModel> GetCarsByYear(int year)
        {
            var db = ApplicationDbContext.Create();
            return from c in db.Cars
                   where c.Year == year
                   select new CarModel()
                   {
                       Id = c.Id,
                       Year = c.Year,
                       Mileage = c.Mileage,
                       Model = c.MakeModel.Name,
                       Make = c.MakeModel.Make.Name,
                       Body = c.Body.Type,
                       Transmission = c.Transmission.Type,
                       CarColor = c.CarColor.Name,
                       InteriorColor = c.InteriorColor.Name,
                       Interior = c.Interior.Type,
                       VIN = c.VIN,
                       Price = c.Price,
                       MSRP = c.MSRP,
                       Featured = c.Featured,
                       Description = c.Description
                   };
        }

        public IEnumerable<CarModel> GetFeatured()
        {
            var db = ApplicationDbContext.Create();
            return from s in db.Cars
                   where s.Featured
                   select new CarModel()
                   {
                       Id = s.Id,
                       Year = s.Year,
                       Mileage = s.Mileage,
                       Model = s.MakeModel.Name,
                       Make = s.MakeModel.Make.Name,
                       Body = s.Body.Type,
                       Transmission = s.Transmission.Type,
                       CarColor = s.CarColor.Name,
                       InteriorColor = s.InteriorColor.Name,
                       Interior = s.Interior.Type,
                       VIN = s.VIN,
                       Price = s.Price,
                       MSRP = s.MSRP,
                       Featured = s.Featured,
                       Description = s.Description
                   };
        }

        public IEnumerable<Special> GetSpecials()
        {
            return ApplicationDbContext.Create().Specials.ToList();
        }

        public IEnumerable<Make> GetMakes()
        {
            return ApplicationDbContext.Create().Makes.ToList();
        }

        public IEnumerable<MakeModel> GetModels()
        {
            return ApplicationDbContext.Create().MakeModels.ToList();
        }
        
        public IEnumerable<Color> GetColors()
        {
            return ApplicationDbContext.Create().Colors.ToList();
        }

        public IEnumerable<Transmission> GetTransmissions()
        {
            return ApplicationDbContext.Create().Transmissions.ToList();
        }

        public IEnumerable<Body> GetBodies()
        {
            return ApplicationDbContext.Create().Bodies.ToList();
        }

        public IEnumerable<Interior> GetInteriors()
        {
            return ApplicationDbContext.Create().Interiors.ToList();
        }

        public bool AddSpecial(string title, string description)
        {
            var db = ApplicationDbContext.Create();

            // a special already exists with that same title
            if (db.Specials.Any(s => s.Title == title))
                return false;

            db.Specials.Add(new Special
            {
                Title = title,
                Description = description
            });
            db.SaveChanges();
            return true;
        }

        public bool EditSpecial(SpecialsViewModel special)
        {
            var db = ApplicationDbContext.Create();

            // a different special already exists with this title
            if (db.Specials.Any(s => s.Title == special.Title && s.SpecialId != special.SpecialId))
                return false;

            var entry = db.Specials.Where(s => s.SpecialId == special.SpecialId).FirstOrDefault();

            // no special with this ID found
            if (entry == null)
                return false;

            entry.Title = special.Title;
            entry.Description = special.Description;
            db.SaveChanges();
            return true;
        }

        public bool DeleteSpecial(int id)
        {
            var db = ApplicationDbContext.Create();
            var special = db.Specials.Where(s => s.SpecialId == id).FirstOrDefault();

            // no special with this ID found
            if (special == null)
                return false; 

            db.Specials.Remove(special);
            db.SaveChanges();
            return true;
        }

        public void AddContact(string name, string email, string phone, string message)
        {
            var db = ApplicationDbContext.Create();
            db.Contacts.Add(new Contact
            {
                Name = name,
                Email = email,
                Phone = phone,
                Message = message
            });
            db.SaveChanges();
        }

        public void AddPurchase(PurchaseViewModel purchase, string sellerId)
        {
            var db = ApplicationDbContext.Create();
            var car = db.Cars.Where(c => c.Id == purchase.CarId).FirstOrDefault();
            if (car == null)
                return;
            car.Featured = false;
            db.Purchases.Add(new Purchase
            {
                VehicleId = purchase.CarId,
                SalesPersonId = sellerId,
                Name = purchase.Name,
                Email = purchase.Email,
                Street1 = purchase.Street1,
                Street2 = purchase.Street2,
                City = purchase.City,
                State = purchase.State,
                ZIP = purchase.ZIP,
                Price = purchase.PurchasePrice,
                PurchaseType = purchase.PurchaseType,
                PurchaseDate = DateTime.Now
            });
           
            db.SaveChanges();
        }

        public bool IsCarPurchased(int id)
        {
            var db = ApplicationDbContext.Create();
            return db.Purchases.Any(p => p.VehicleId == id);
        }

        public IEnumerable<CarModel> GetPurchasedCars()
        {
            var db = ApplicationDbContext.Create();
            return from p in db.Purchases
                   select GetCarById(p.VehicleId);
        }

        public IEnumerable<Purchase> GetPurchases()
        {
            return ApplicationDbContext.Create().Purchases;
        }
    }
}