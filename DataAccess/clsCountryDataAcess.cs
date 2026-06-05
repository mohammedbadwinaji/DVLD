using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsCountryDataAcess
    {
        public static bool IsCountryExists(string countryName)
        {
            SqlConnection connection = new SqlConnection(clsSettings.connectionString);
            string query = @"   SELECT * FROM Countries
                                WHERE CountryName = @CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", countryName);

            try
            {
                connection.Open();
                object result= command.ExecuteScalar();
                if(result == null)
                {
                    connection.Close();
                    return false;
                }
                connection.Close();
                return true;
            }
            catch (Exception ex) {
                connection.Close();
                throw (ex);
            }
        }
        public static DataTable GetAll()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.connectionString);
            string query = "SELECT * FROM Countries";

            SqlCommand command= new SqlCommand(query, connection);  

            try
            {
                connection.Open();  
                SqlDataReader readre= command.ExecuteReader();  
                
                dt.Load(readre);
                connection.Close();
                return dt;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetByName(string countryName)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.connectionString);

            string query = @"SELECT c.CountryID FROM Countries c
                            WHERE c.CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", countryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                dt.Load(reader);
                connection.Close();
                return dt;
            }catch(Exception ex) {
                connection.Close();
                throw ex; 
            }
        }
    }
}
