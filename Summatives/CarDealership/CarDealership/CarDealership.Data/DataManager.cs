using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data
{
    public class DataManager
    {
        private static ICarRepository _instance;
        public static ICarRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Create();
                }
                return _instance;
            }
        }

        private static ICarRepository Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "SampleData":
                    return new CarRepositoryMock();
                case "Production":
                    return new EFRepository();
                default:
                    throw new Exception($"Repository mode '{mode}' is not valid!");
            }
        }
    }
}
