using FlooringMastery.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class DataManagerFactory
    {
        public static DataManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Test":
                    return new DataManager(new TestDataRepository());
                case "Prod":
                    return new DataManager(new FileDataRepository());
                default:
                    throw new Exception($"Mode value {mode} in app config is not valid!");
            }
        }
    }
}
