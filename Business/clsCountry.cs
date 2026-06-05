using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class clsCountry
    {
        public int CountryId { get; }
        public string CountryName { get;} 
        public static DataTable GetAll()
        {
            return clsCountryDataAcess.GetAll();
        }
        public static bool IsCountryExists(string countryName)
        {
            return clsCountryDataAcess.IsCountryExists(countryName);
        }
    }
}
