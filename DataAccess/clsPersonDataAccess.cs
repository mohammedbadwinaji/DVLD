using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsPersonDataAccess
    {
        public static bool IsPersonIdExists(int peronsId)
        {
            return false;
        }
        public static bool IsNationalNoExists(string nationalNo)
        {
            SqlConnection connection = new SqlConnection(clsSettings.connectionString);
            string query = @"SELECT PersonID FROM People 
                             WHERE NationalNo=@NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", nationalNo);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                connection.Close();
                return result != null;
            }
            catch (Exception ex) {
                connection.Close();
                throw (ex);
            }
        }
        public static bool FindById
            (int personId ,ref string nationalNo,ref string firstName , ref string secondName,
            ref string thirdName, ref string lastName,ref DateTime dateOfBirth , ref byte gendor, 
            ref string address , ref string phone , ref string email , 
            ref int nationalityCountryId,ref string countryName, ref string imagePath)
        {
            SqlConnection connection = new SqlConnection(clsSettings.connectionString);
            string query = @"SELECT * FROM People p
                            INNER JOIN Countries c
                            ON p.NationalityCountryID = c.CountryID
                            WHERE PersonId = @PersonId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("PersonId", personId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(!reader.Read())
                {
                    return false;
                }
                nationalNo = (string)reader["NationalNo"];
                firstName = (string)reader["FirstName"];
                secondName = (string)reader["SecondName"];
                thirdName = (string)reader["ThirdName"];
                lastName = (string)reader["LastName"];
                dateOfBirth = (DateTime)reader["DateOfBirth"];
                gendor = (byte)reader["Gendor"];
                address = (string)reader["Address"];
                phone = (string)reader["Phone"];
                if (reader["Email"]== DBNull.Value)
                {
                    email = "";
                } else
                {
                    email = (string)reader["Email"];
                }
                nationalityCountryId = (int)reader["NationalityCountryID"];
                countryName = (string)reader["CountryName"];
                if (reader["ImagePath"] == DBNull.Value)
                {
                    imagePath = "";
                }else
                {
                    imagePath = (string)reader["ImagePath"];
                }

                reader.Close();
                connection.Close();
                return true;
            }
            catch (Exception ex) {
                connection.Close();
                throw ex;
            }
        }
        public static string _GetImagePath(int personId)
        {
            SqlConnection connection = new SqlConnection(clsSettings.connectionString);
            string query = "SELECT ImagePath FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personId);
            try
            {
                connection.Open();
                object result= command.ExecuteScalar();
                if(result != DBNull.Value )
                {
                    return (string)result; 
                }
                return null; 
            }
            catch (Exception ex)
            {
                connection.Close();
                return "";
            }
        }
        public static DataTable GetAll()
        {

            DataTable dataTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsSettings.connectionString);
            string query = @"SELECT	p.PersonID as 'Person ID',
                                    p.NationalNo as 'National No',
		                            p.FirstName as 'First Name',
		                            p.SecondName as 'Second Name',
		                            p.ThirdName as 'Third Name',
		                            p.LastName as 'Last Name',
		                            p.Gendor ,
		                            p.DateOfBirth as 'Date Of Birth',
		                            c.CountryName as Nationality,
		                            p.Phone,
		                            p.Email
                            FROM People p
                            INNER JOIN Countries c
                            ON c.CountryID = p.NationalityCountryID";           

            SqlCommand command = new SqlCommand(query,connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                
                dataTable.Load(reader);
                

                reader.Close();
                connection.Close();
            }
            catch (Exception ex) {
                connection.Close();
                throw new Exception("Database error while retrieving people data.");
            }

            return dataTable;
        }
        public static bool Edit(int personId, string firstName, string secondName,
            string thirdName, string lastName, DateTime dateOfBirth, byte gendor,
             string address, string phone, string email,
            string countryName, string imagePath)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.connectionString );

            string query = @"UPDATE [dbo].[People]
                           SET [FirstName] = @FirstName
                              ,[SecondName] = @SecondName
                              ,[ThirdName] = @ThirdName
                              ,[LastName] = @LastName
                              ,[DateOfBirth] = @DateOfBirth
                              ,[Gendor] = @Gendor
                              ,[Address] = @Address
                              ,[Phone] = @Phone
                              ,[Email] = @Email
                              ,[NationalityCountryID] = (   SELECT c.CountryID 
                                                            FROM Countries c
                                                            WHERE c.CountryName = @CountryName)
                              ,[ImagePath] = @ImagePath
                         WHERE PersonID = @PersonID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personId);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@SecondName", string.IsNullOrWhiteSpace(secondName) ? DBNull.Value : (object)secondName);
            command.Parameters.AddWithValue("@ThirdName", string.IsNullOrWhiteSpace(thirdName) ? DBNull.Value : (object)thirdName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : (object)email);

            
            string oldImagePath = _GetImagePath(personId);
            if(imagePath != oldImagePath)
            {
                string savedImagePath = clsImageDataAccess.Save(imagePath);
                clsImageDataAccess.Delete(oldImagePath);
                command.Parameters.AddWithValue("@ImagePath", savedImagePath == null ? DBNull.Value : (object)savedImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", imagePath == null ? DBNull.Value : (object)imagePath);
            }

            

            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@Gendor", gendor);
            command.Parameters.AddWithValue("@CountryName", countryName);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) {
                throw ex;
            }
            return rowsAffected > 0;
        }


        public static int AddNew( string nationalNo,  string firstName,  string secondName,
            string thirdName,  string lastName, DateTime dateOfBirth, byte gendor,
             string address, string phone,  string email,
            string countryName,  string imagePath)
        {
            SqlConnection connection = new SqlConnection(clsSettings.connectionString);

            string query = @"INSERT INTO [dbo].[People]
               ([NationalNo]
               ,[FirstName]
               ,[SecondName]
               ,[ThirdName]
               ,[LastName]
               ,[DateOfBirth]
               ,[Gendor]
               ,[Address]
               ,[Phone]
               ,[Email]
               ,[NationalityCountryID]
               ,[ImagePath])
         VALUES
               (@NationalNo
               ,@FirstName
               ,@SecondName
               ,@ThirdName
               ,@LastName
               ,@DateOfBirth
               ,@Gendor
               ,@Address
               ,@Phone
               ,@Email
               ,(SELECT CountryID From Countries WHERE CountryName = @CountryName)
               ,@ImagePath);
         SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@NationalNo", nationalNo);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@SecondName", secondName);
            command.Parameters.AddWithValue("@ThirdName", thirdName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : (object)email);

            if(string.IsNullOrEmpty(imagePath))
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }else
            {

                string savedImagePath = clsImageDataAccess.Save(imagePath);
                command.Parameters.AddWithValue("@ImagePath", savedImagePath == null ? DBNull.Value : (object)savedImagePath);
            }
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@Gendor", gendor); 
            command.Parameters.AddWithValue("@CountryName", countryName);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != DBNull.Value) {
                    return int.Parse(result.ToString());
                }
                return -1;

            }
            catch (Exception ex) {
                throw ex;
            }
            

        }
    }
}
