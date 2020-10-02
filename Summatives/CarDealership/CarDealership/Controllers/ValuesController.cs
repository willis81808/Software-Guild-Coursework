using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using CarDealership.Models;
using CarDealership.Data;

namespace CarDealership.Controllers
{
    public class ValuesController : ApiController
    {
        [Route("car/all/")]
        public ResponseModel GetAllCars() => 
            QueryCars(DataManager.Instance.GetAllCars(), "Car database is empty!");
        
        [Route("car/make/")]
        [AcceptVerbs("GET")]
        public ResponseModel GetCarsByMake(string make) => 
            QueryCars(DataManager.Instance.GetCarsByMake(make), $"No cars found of make: {make}");

        [Route("car/model/")]
        [AcceptVerbs("GET")]
        public ResponseModel GetCarsByModel(string model) => 
            QueryCars(DataManager.Instance.GetCarsByModel(model), $"No cars found of model: {model}");

        [Route("car/year/")]
        [AcceptVerbs("GET")]
        public ResponseModel GetCarsByYear(int year) => 
            QueryCars(DataManager.Instance.GetCarsByYear(year), $"No cars found for year: {year}");

        [Route("car/featured")]
        public ResponseModel GetFeatured() =>
            QueryCars(DataManager.Instance.GetFeatured(), "Error occured fetching featured vehicles.");

        [Route("car/specials")]
        public ResponseModel GetSpecials()
        {
            var response = new ResponseModel();
            response.Data = DataManager.Instance.GetSpecials();

            if (response.Data == null || ((IEnumerable<Special>)response.Data).Count() == 0)
            {
                response.Message = "There are no specials at this time.";
                response.Status = false;
            }
            else
            {
                response.Message = "Success";
                response.Status = true;
            }

            return response;
        }

        [Route("car/get/")]
        [AcceptVerbs("GET")]
        public ResponseModel GetCarById(int id)
        {
            var response = new ResponseModel();

            var car = DataManager.Instance.GetCarById(id);

            if (car != null)
            {
                response.Message = "Success";
                response.Status = true;
                response.Data = car;
            }
            else
            {
                response.Message = $"No car was found with id: {id}";
                response.Status = false;
            }

            return response;
        }

        private static ResponseModel QueryCars(IEnumerable<CarModel> cars, string errorMessage)
        {
            var response = new ResponseModel();

            if (cars == null || cars.Count() == 0)
            {
                response.Message = errorMessage;
                response.Status = false;
            }
            else
            {
                response.Message = "Success";
                response.Status = true;
                response.Data = cars;
            }

            return response;
        }
    }
}
