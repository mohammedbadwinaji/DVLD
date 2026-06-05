using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsUserDataAccess
    {
        public static bool GetUserByUsernameAndPassword
            (
            string username, string password, ref bool isActive
            )
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettings.connectionString);

            string query = @"SELECT u.UserName,
                                    u.Password,
                                    u.IsActive
                            FROM Users u
                            WHERE       u.UserName = @Username
                                    AND u.Password = @Password;";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isActive = (bool)reader["IsActive"];
                    isFound = true;
                }else
                {
                    isFound = false;
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex) {
                connection.Close();
                throw ex;
            }
            return isFound;
        }
    }
        
}


