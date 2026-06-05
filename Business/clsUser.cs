using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class clsUser
    {
        public int UserID { get; private set; }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }
        public clsPerson Person { get; private set; }


        public static clsUser Login(string username, string password,out string errorMessage)
        {
            // TO DO Validation
            errorMessage = "";

            bool isActive = false;
            bool isAuthenticated  = clsUserDataAccess.GetUserByUsernameAndPassword(username, password, ref isActive);

            if(!isAuthenticated)
            {
                errorMessage = "Invalid Username Or Password";
                return null;
            }

            if(!isActive)
            {
                errorMessage = "User Account is deactivated";
                return null;
            }

            clsUser user = new clsUser();

            user.Username = username;
            user.Password = password;
            user.IsActive = isActive;
            
            return user;
        }
    }
}
