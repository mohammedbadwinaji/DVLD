using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsUserDataAccess
    {
        public static DataTable GetAll()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.connectionString);
            string query = @"SELECT	u.UserID as 'User ID',
		                            u.PersonID as 'Person ID',
		                            (
			                            p.FirstName + ' ' +
			                            p.SecondName + ' ' +
			                            p.ThirdName + ' ' +
			                            p.LastName
		                            ) as 'Full Name',
                                    u.UserName,
		                            u.IsActive as 'Is Active'   
                            FROM Users u
                            INNER JOIN People p
                            ON u.PersonID = p.PersonID";
            SqlCommand command= new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                dt.Load(reader);

                reader.Close();
                connection.Close();
            }
            catch (Exception ex) {
                throw ex;
            }
            return dt;
        }
        public static bool GetByUsernameAndPassword
            (
            string username, string password, ref bool isActive , ref int userId
            )
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettings.connectionString);

            string query = @"SELECT u.UserID,
                                    u.UserName,
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
                    userId = (int)reader["UserID"];
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

       public static bool GetByID
            (   
                int userId, ref int personId,ref string username,
                ref string password, ref bool isActive
            )
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection( clsSettings.connectionString );
            string query = @"SELECT u.*
                            FROM Users u
                            WHERE u.UserID = @UserID;";

            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@UserID", userId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {
                    isFound = true;
                    personId = (int)reader["PersonID"];
                    username = (string)reader["Username"];
                    password = (string)reader["Password"];
                    isActive = (bool)reader["IsActive"];
                }else
                {
                    isFound = false;
                }
            }
            catch (Exception ex) {
                throw ex;
            }

            return isFound;
        }



        public static int CreateNew(int personId, string username , string password, bool isActive)
        {
            int addedUserId = -1;
            SqlConnection connection = new SqlConnection(clsSettings.connectionString);

            string query = @"INSERT INTO [dbo].[Users]
                    ([PersonID]
                    ,[UserName]
                    ,[Password]
                    ,[IsActive])
                 VALUES
                    (@PersonID,
                     @UserName,
                     @Password,
                     @IsActive);
                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personId);
            command.Parameters.AddWithValue("@UserName", username);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@IsActive", isActive);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null) {
                    addedUserId = int.Parse(result.ToString());  
                }
                connection.Close();
            }
            catch (Exception ex) {
                throw ex;
            }

            return addedUserId;

        }

        public static bool Update(int userId, string username, string password, bool isActive) {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.connectionString);

            string query = @"UPDATE [dbo].[Users]
                     SET [UserName] = @UserName
                        ,[Password] = @Password
                        ,[IsActive] = @IsActive
                     WHERE [UserID] = @UserID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID",userId);
            command.Parameters.AddWithValue("@UserName",username);
            command.Parameters.AddWithValue("@Password",password);
            command.Parameters.AddWithValue("@IsActive",isActive);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return rowsAffected > 0;
        }

        public static bool UpdatePassword(int userId,string currentPassword, string newPassword)
        {
            
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(   clsSettings.connectionString );

            string query = @"UPDATE Users
                             SET Password = @NewPassword
                             WHERE UserID = @UserID AND
                             Password = @Password";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Password", currentPassword);
            command.Parameters.AddWithValue("@NewPassword", newPassword);
            command.Parameters.AddWithValue("@UserID", userId);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery ();

                
                connection.Close();

            }catch(Exception ex)
            {
                connection.Close();
                throw ex;
            }

            return rowsAffected > 0;
        }

    }
        
}


