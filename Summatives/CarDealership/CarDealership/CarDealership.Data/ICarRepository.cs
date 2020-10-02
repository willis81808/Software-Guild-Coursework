using System.Collections.Generic;
using CarDealership.Models;

namespace CarDealership.Data
{
    public interface ICarRepository
    {
        IEnumerable<CarModel> GetAllCars();
        IEnumerable<CarModel> GetAvailableCars();
        IEnumerable<CarModel> GetPurchasedCars();
        IEnumerable<CarModel> GetFeatured();
        IEnumerable<CarModel> GetCarsByYear(int year);
        IEnumerable<CarModel> GetCarsByMake(string make);
        IEnumerable<CarModel> GetCarsByModel(string model);

        IEnumerable<Make> GetMakes();
        IEnumerable<Color> GetColors();
        IEnumerable<Transmission> GetTransmissions();
        IEnumerable<Body> GetBodies();
        IEnumerable<Interior> GetInteriors();
        IEnumerable<MakeModel> GetModels();
        IEnumerable<Special> GetSpecials();
        IEnumerable<Purchase> GetPurchases();

        CarModel GetCarById(int id);

        Car AddCar(CarModel car);
        bool AddMake(string name);
        bool AddModel(string make, string name);
        bool AddSpecial(string title, string description);
        void AddContact(string name, string email, string phone, string message);
        void AddPurchase(PurchaseViewModel purchase, string sellerId);

        void EditCar(CarModel car);
        bool EditSpecial(SpecialsViewModel special);

        bool DeleteCar(int id);
        bool DeleteSpecial(int id);

        bool IsCarPurchased(int id);
    }
}
