using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models;

namespace CarDealership.Data
{
    class CarRepositoryMock : ICarRepository
    {
        private List<Car> cars;
        private List<Make> makes;
        private List<MakeModel> models;
        private List<Transmission> transmissions;
        private List<Color> colors;
        private List<Interior> interiors;
        private List<Body> bodies;
        private List<Special> specials;
        private List<Contact> contacts;
        private List<Purchase> purchases;

        public CarRepositoryMock()
        {
            contacts = new List<Contact>();
            purchases = new List<Purchase>();

            makes = new List<Make>()
            {
                new Make(1, "Ferrari"),
                new Make(2, "Ford"),
                new Make(3, "Subaru")
            };

            models = new List<MakeModel>()
            {
                new MakeModel(1, GetMake(1), "612 Scaglietti"),
                new MakeModel(2, GetMake(2), "Taurus"),
                new MakeModel(3, GetMake(3), "CrossTrek"),
                new MakeModel(4, GetMake(2), "F-150")
            };

            transmissions = new List<Transmission>()
            {
                new Transmission(1, "Manual"),
                new Transmission(2, "Automatic"),
                new Transmission(3, "Semi-Automatic"),
                new Transmission(4, "CVT")
            };

            colors = new List<Color>()
            {
                new Color(1, "Black"),
                new Color(2, "White"),
                new Color(3, "Gray"),
                new Color(4, "Red"),
                new Color(5, "Silver"),
                new Color(6, "Brown")
            };

            interiors = new List<Interior>()
            {
                new Interior(1, "Vinyl"),
                new Interior(2, "Leather"),
                new Interior(3, "Nylon"),
                new Interior(4, "Polyester")
            };

            bodies = new List<Body>()
            {
                new Body(1, "Sedan"),
                new Body(2, "Coupe"),
                new Body(3, "Hatchback"),
                new Body(4, "Minivan/Van"),
                new Body(5, "Truck"),
                new Body(6, "Station Wagon"),
                new Body(7, "Convertable"),
                new Body(8, "SUV")
            };

            cars = new List<Car>()
            {
                // Ferrari 612 Scaglietti
                new Car()
                {
                    Id = 1,
                    Year = 2007,
                    MakeModel = GetMakeModel(1),
                    Body = GetBody(2),
                    Transmission = GetTransmission(2),
                    CarColor = GetColor(4),
                    Interior = GetInterior(2),
                    InteriorColor = GetColor(6),
                    VIN = "E9AS760LHLF287V04",
                    Mileage = 223773,
                    Price = 13221.65m,
                    MSRP = 19964.69m
                },

                // Ford Taurus
                new Car()
                {
                    Id = 2,
                    Year = 2012,
                    MakeModel = GetMakeModel(2),
                    Body = GetBody(1),
                    Transmission = GetTransmission(2),
                    CarColor = GetColor(1),
                    Interior = GetInterior(2),
                    InteriorColor = GetColor(1),
                    VIN = "X534T859D3MA41965",
                    Mileage = 137806,
                    Price = 9031.07m,
                    MSRP = 11830.70m
                },

                // Subaru Crosstrek
                new Car()
                {
                    Id = 3,
                    Year = 2016,
                    MakeModel = GetMakeModel(3),
                    Body = GetBody(8),
                    Transmission = GetTransmission(3),
                    CarColor = GetColor(3),
                    Interior = GetInterior(4),
                    InteriorColor = GetColor(1),
                    VIN = "JF2GPABC0G8290339",
                    Mileage = 220150,
                    Price = 14135.05m,
                    MSRP = 19999.00m,
                    Featured = true
                },

                // Ford F-150
                new Car()
                {
                    Id = 4,
                    Year = 2020,
                    MakeModel = GetMakeModel(2),
                    Body = GetBody(5),
                    Transmission = GetTransmission(3),
                    CarColor = GetColor(5),
                    Interior = GetInterior(1),
                    InteriorColor = GetColor(1),
                    VIN = "1FTEW1E5XLFA51618",
                    Mileage = 0,
                    Price = 26013.00m,
                    MSRP = 28745.00m,
                    Featured = true
                }
            };
            
            specials = new List<Special>
            {
                new Special
                {
                    Title = "Special 1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt utlabore " +
                                  "et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut " +
                                  "aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum " +
                                  "dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia " +
                                  "deserunt mollit anim id est laborum."
                },
                new Special
                {
                    Title = "Special 2",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt utlabore " +
                                  "et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut " +
                                  "aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum " +
                                  "dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia " +
                                  "deserunt mollit anim id est laborum."
                },
                new Special
                {
                    Title = "Special 3",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt utlabore " +
                                  "et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut " +
                                  "aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum " +
                                  "dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia " +
                                  "deserunt mollit anim id est laborum."
                }
            };
        }
        
        public IEnumerable<Make> GetMakes()
        {
            return makes;
        }

        public IEnumerable<MakeModel> GetModels()
        {
            return models;
        }

        private MakeModel GetMakeModel(int id)
        {
            return models.Where(m => m.ModelId == id).FirstOrDefault();
        }
        private Make GetMake(int id)
        {
            return makes.Where(m => m.MakeId == id).FirstOrDefault();
        }
        private Body GetBody(int id)
        {
            return bodies.Where(b => b.BodyId == id).FirstOrDefault();
        }
        private Transmission GetTransmission(int id)
        {
            return transmissions.Where(t => t.TransmissionId == id).FirstOrDefault();
        }
        private Color GetColor(int id)
        {
            return colors.Where(c => c.ColorId == id).FirstOrDefault();
        }
        private Interior GetInterior(int id)
        {
            return interiors.Where(i => i.InteriorId == id).FirstOrDefault();
        }

        public IEnumerable<CarModel> GetAllCars()
        {
            return from c in cars
                   join b in bodies on c.Body.BodyId equals b.BodyId
                   join i in interiors on c.Interior.InteriorId equals i.InteriorId
                   join col in colors on c.CarColor.ColorId equals col.ColorId
                   join coli in colors on c.InteriorColor.ColorId equals coli.ColorId
                   join t in transmissions on c.Transmission.TransmissionId equals t.TransmissionId
                   join mo in models on c.MakeModel.ModelId equals mo.ModelId
                   join ma in makes on mo.Make.MakeId equals ma.MakeId
                   select CarModel.FromCar(c);
        }

        public CarModel GetCarById(int id)
        {
            return (
                from c in cars
                where c.Id == id
                join b in bodies on c.Body.BodyId equals b.BodyId
                join i in interiors on c.Interior.InteriorId equals i.InteriorId
                join col in colors on c.CarColor.ColorId equals col.ColorId
                join coli in colors on c.InteriorColor.ColorId equals coli.ColorId
                join t in transmissions on c.Transmission.TransmissionId equals t.TransmissionId
                join mo in models on c.MakeModel.ModelId equals mo.ModelId
                join ma in makes on mo.Make.MakeId equals ma.MakeId
                select CarModel.FromCar(c)
            ).FirstOrDefault();
        }

        public IEnumerable<CarModel> GetCarsByMake(string make)
        {
            return from c in cars
                   where c.MakeModel.Make.Name == make
                   join b in bodies on c.Body.BodyId equals b.BodyId
                   join i in interiors on c.Interior.InteriorId equals i.InteriorId
                   join col in colors on c.CarColor.ColorId equals col.ColorId
                   join coli in colors on c.InteriorColor.ColorId equals coli.ColorId
                   join t in transmissions on c.Transmission.TransmissionId equals t.TransmissionId
                   join mo in models on c.MakeModel.ModelId equals mo.ModelId
                   join ma in makes on mo.Make.MakeId equals ma.MakeId
                   select CarModel.FromCar(c);
        }

        public IEnumerable<CarModel> GetCarsByModel(string model)
        {
            return from c in cars
                   where c.MakeModel.Name == model
                   join b in bodies on c.Body.BodyId equals b.BodyId
                   join i in interiors on c.Interior.InteriorId equals i.InteriorId
                   join col in colors on c.CarColor.ColorId equals col.ColorId
                   join coli in colors on c.InteriorColor.ColorId equals coli.ColorId
                   join t in transmissions on c.Transmission.TransmissionId equals t.TransmissionId
                   join mo in models on c.MakeModel.ModelId equals mo.ModelId
                   join ma in makes on mo.Make.MakeId equals ma.MakeId
                   select CarModel.FromCar(c);
        }

        public IEnumerable<CarModel> GetCarsByYear(int year)
        {
            return from c in cars
                   where c.Year == year
                   join b in bodies on c.Body.BodyId equals b.BodyId
                   join i in interiors on c.Interior.InteriorId equals i.InteriorId
                   join col in colors on c.CarColor.ColorId equals col.ColorId
                   join coli in colors on c.InteriorColor.ColorId equals coli.ColorId
                   join t in transmissions on c.Transmission.TransmissionId equals t.TransmissionId
                   join mo in models on c.MakeModel.ModelId equals mo.ModelId
                   join ma in makes on mo.Make.MakeId equals ma.MakeId
                   select CarModel.FromCar(c);
        }

        public IEnumerable<CarModel> GetFeatured()
        {
            return from c in cars
                   where c.Featured
                   join b in bodies on c.Body.BodyId equals b.BodyId
                   join i in interiors on c.Interior.InteriorId equals i.InteriorId
                   join col in colors on c.CarColor.ColorId equals col.ColorId
                   join coli in colors on c.InteriorColor.ColorId equals coli.ColorId
                   join t in transmissions on c.Transmission.TransmissionId equals t.TransmissionId
                   join mo in models on c.MakeModel.ModelId equals mo.ModelId
                   join ma in makes on mo.Make.MakeId equals ma.MakeId
                   select CarModel.FromCar(c);
        }

        public IEnumerable<Special> GetSpecials()
        {
            return specials;
        }

        public void EditCar(CarModel car)
        {
            DeleteCar(car.Id);
            AddCar(car);
        }
        
        public bool DeleteCar(int id)
        {
            return cars.RemoveAll(c => c.Id == id) > 0;
        }

        public bool AddMake(string name)
        {
            var make = makes.Where(m => m.Name == name).FirstOrDefault();
            if (make != null)
                return false;
            GetOrCreateMake(name);
            return true;
        }

        public bool AddModel(string make, string name)
        {
            var model = models.Where(m => m.Name == name && m.Make.Name == make).FirstOrDefault();
            if (model != null)
                return false;
            GetOrCreateMakeModel(make, name);
            return true;
        }

        Body GetOrCreateBody(string type)
        {
            var body = bodies.Where(b => b.Type == type).FirstOrDefault();
            if (body == null)
            {
                body = new Body
                {
                    BodyId = bodies.Count == 0 ? 1 : bodies.ReturnMax(b => b.BodyId).BodyId + 1,
                    Type = type
                };
                bodies.Add(body);
            }
            return body;
        }

        Color GetOrCreateColor(string name)
        {
            var color = colors.Where(c => c.Name == name).FirstOrDefault();
            if (color == null)
            {
                color = new Color
                {
                    ColorId = colors.Count == 0 ? 1 : colors.ReturnMax(c => c.ColorId).ColorId + 1,
                    Name = name
                };
                colors.Add(color);
            }
            return color;
        }

        Transmission GetOrCreateTransmission(string type)
        {
            var transmission = transmissions.Where(t => t.Type == type).FirstOrDefault();
            if (transmission == null)
            {
                transmission = new Transmission
                {
                    TransmissionId = transmissions.Count == 0 ? 1 : transmissions.ReturnMax(t => t.TransmissionId).TransmissionId + 1,
                    Type = type
                };
                transmissions.Add(transmission);
            }
            return transmission;
        }

        Make GetOrCreateMake(string makeName)
        {
            var make = makes.Where(m => m.Name == makeName).FirstOrDefault();
            if (make == null)
            {
                make = new Make
                {
                    MakeId = makes.Count == 0 ? 1 : makes.ReturnMax(m => m.MakeId).MakeId + 1,
                    Name = makeName
                };
                makes.Add(make);
            }
            return make;
        }

        MakeModel GetOrCreateMakeModel(string makeName, string modelName)
        {
            var model = models.Where(m => m.Name == modelName && m.Make.Name == makeName).FirstOrDefault();
            if (model == null)
            {
                model = new MakeModel
                {
                    ModelId = models.Count == 0 ? 1 : models.ReturnMax(m => m.ModelId).ModelId + 1,
                    Name = modelName,
                    Make = GetOrCreateMake(makeName)
                };
            }
            return model;
        }

        public Car AddCar(CarModel car)
        {
            var c = new Car
            {
                Id = car.Id,
                Year = car.Year,
                Mileage = car.Mileage,
                VIN = car.VIN,
                Price = car.Price,
                MSRP = car.MSRP,
                Body = GetOrCreateBody(car.Body),
                CarColor = GetOrCreateColor(car.CarColor),
                InteriorColor = GetOrCreateColor(car.InteriorColor),
                MakeModel = GetOrCreateMakeModel(car.Make, car.Model),
                Transmission = GetOrCreateTransmission(car.Transmission),
                Featured = car.Featured
            };
            cars.Add(c);
            return c;
        }

        public bool AddSpecial(string title, string description)
        {
            // a special already exists with that same title
            if (specials.Any(s => s.Title == title))
                return false;

            specials.Add(new Special
            {
                Title = title,
                Description = description
            });
            return true;
        }

        public bool EditSpecial(SpecialsViewModel special)
        {
            // a different special already exists with this title
            if (specials.Any(s => s.Title == special.Title && s.SpecialId != special.SpecialId))
                return false;

            // no specials found with that ID
            if (!DeleteSpecial(special.SpecialId))
                return false;

            specials.Add(new Special
            {
                Title = special.Title,
                Description = special.Description
            });
            return true;
        }

        public bool DeleteSpecial(int id)
        {
            var count = specials.RemoveAll(s => s.SpecialId == id);
            return count > 0;
        }

        public IEnumerable<Color> GetColors()
        {
            return colors;
        }

        public IEnumerable<Transmission> GetTransmissions()
        {
            return transmissions;
        }

        public IEnumerable<Body> GetBodies()
        {
            return bodies;
        }

        public IEnumerable<Interior> GetInteriors()
        {
            return interiors;
        }

        public void AddContact(string name, string email, string phone, string message)
        {
            contacts.Add(new Contact
            {
                Name = name,
                Email = email,
                Phone = phone,
                Message = message
            });
        }

        public IEnumerable<CarModel> GetAvailableCars()
        {
            // select all cars that are not in the purchased table
            return from c in cars
                   where !(from p in purchases
                           select p.VehicleId)
                           .Contains(c.Id)
                   select CarModel.FromCar(c);
        }

        public void AddPurchase(PurchaseViewModel purchase, string sellerId)
        {
            var car = GetCarById(purchase.CarId);
            if (car == null)
                return;
            car.Featured = false;
            purchases.Add(new Purchase
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
        }

        public bool IsCarPurchased(int id)
        {
            return purchases.Any(p => p.VehicleId == id);
        }

        public IEnumerable<CarModel> GetPurchasedCars()
        {
            return from p in purchases
                   select GetCarById(p.VehicleId);
        }

        public IEnumerable<Purchase> GetPurchases()
        {
            return purchases;
        }
    }
}
