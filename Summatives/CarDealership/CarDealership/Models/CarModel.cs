using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class VehicleIdentificationNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || ((string)value).Length == 0)
                return new ValidationResult("VIN cannot be null or empty");

            string vin = (value as string).ToLowerInvariant();
            if (vin.Contains('i') || vin.Contains('o') || vin.Contains('q'))
                return new ValidationResult("A VIN cannot contain the letters 'I', 'O', or 'Q'");

            var model = (CarModel)validationContext.ObjectInstance;
            if (model.Year < 1981)
            {
                return ValidationResult.Success;
            }
            else if (vin.Length != 17)
            {
                return new ValidationResult("The VIN must be 17 characters");
            }
            else
            {
                return VinValidation(vin) ? ValidationResult.Success : new ValidationResult("You provided an invalid VIN!");
            }
        }

        private bool VinValidation(string vin)
        {
            int[] transliterated = new int[17];
            int[] weighted = new int[17];
            for (int i = 0; i < 17; i++)
            {
                char c = vin[i];
                if (char.IsDigit(c))
                    transliterated[i] = int.Parse(c.ToString());
                else
                    transliterated[i] = letterVals[c];
                weighted[i] =  transliterated[i] * weights[i];
            }
            int remainder = weighted.Sum() % 11;
            if (remainder <= 9)
                return remainder == transliterated[8];
            else
                return vin[8] == 'x';
        }

        private Dictionary<char, int> letterVals = new Dictionary<char, int>
        {
            {'a', 1}, {'b', 2}, {'c', 3}, {'d', 4}, {'e', 5}, {'f', 6}, {'g', 7}, {'h', 8}, {'j', 1}, {'k', 2}, {'l', 3}, {'m', 4},
            {'n', 5}, {'p', 7}, {'r', 9}, {'s', 2}, {'t', 3}, {'u', 4}, {'v', 5}, {'w', 6}, {'x', 7}, {'y', 8}, {'z', 9},
        };
        private int[] weights = new[] { 8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2 };
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class IsLargerThan : ValidationAttribute
    {
        string otherProperty;

        public IsLargerThan(string otherProperty, string errorMessage)
        {
            this.otherProperty = otherProperty;
            ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var prop = validationContext.ObjectType.GetProperty(this.otherProperty);
            if (prop == null)
                return new ValidationResult($"Unknown property: {this.otherProperty}");

            var propValue = prop.GetValue(validationContext.ObjectInstance, null);
            if (propValue == null)
                return new ValidationResult("The 'IsLargerThan' attribute can only be used on integers!");
            else if ((decimal)value <= (decimal)propValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }

    public class CarModel
    {
        public static CarModel FromCar(Car car)
        {
            return new CarModel
            {
                Id = car.Id,
                Year = car.Year,
                Mileage = car.Mileage,
                Make = car.MakeModel.Make.Name,
                Model = car.MakeModel.Name,
                Body = car.Body.Type,
                Transmission = car.Transmission.Type,
                CarColor = car.CarColor.Name,
                InteriorColor = car.InteriorColor.Name,
                Interior = car.Interior.Type,
                VIN = car.VIN,
                Price = car.Price,
                MSRP = car.MSRP,
                Featured = car.Featured,
                Description = car.Description
            };
        }

        public int Id { get; set; }

        [Required]
        [Range(1900, 2020, ErrorMessage = "Invalid year model!")]
        public int Year { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Cannot enter a negative mileage!")]
        public int Mileage { get; set; }

        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Transmission { get; set; }
        [Required]
        [Display(Name = "Car Color")]
        public string CarColor { get; set; }
        [Required]
        [Display(Name = "Interior Color")]
        public string InteriorColor { get; set; }
        [Required]
        public string Interior { get; set; }
        [Required]
        public bool Featured { get; set; }

        [Required]
        [VehicleIdentificationNumber]
        public string VIN { get; set; }

        [IsLargerThan("Price", "The MSRP must be larger than the price!")]
        public decimal MSRP { get; set; }
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
