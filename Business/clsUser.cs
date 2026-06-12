using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class clsUser
    {

        enum enMode
        {
            AddNew,Edit
        }
        public int UserID { get; private set; }
        public string Username { get;  set; }
        public string Password { get;  set; }
        public bool IsActive { get;  set; }

        public clsPerson Person { get;  set; }

        private enMode _Mode;

        public clsUser()
        {
            this.UserID = -1;
            this._Mode = enMode.AddNew;
        }
        private clsUser
            (
            int userID, string username,
            string password ,bool isActive,clsPerson person
            )
        {
            
            UserID = userID;
            Username = username;
            Password = password;
            IsActive = isActive;
            this.Person = person;
            this._Mode = enMode.Edit;
        }

        public static clsUser Login(string username, string password,out string errorMessage)
        {
            // TO DO Validation
            errorMessage = "";

            bool isActive = false;
            int userId = -1;
            bool isAuthenticated  = clsUserDataAccess.GetByUsernameAndPassword(username, password, ref isActive,ref userId);

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

            user.UserID = userId;
            user.Username = username;
            user.Password = password;
            user.IsActive = isActive;
            user.Person = clsPerson.FindById(user.UserID);
            
            return user;
        }

        public static DataTable GetAll()
        {
            return clsUserDataAccess.GetAll();
        }


        //public static clsUser FindByPersonID(int personId)
        //{

        //}
        public static clsUser FindByID (int userId)
        {

            int personId = -1;
            string username = "", password = "";
            bool isActive = false;
            bool isExists = clsUserDataAccess.GetByID(userId,ref personId, ref username,ref password,ref isActive);

            if(!isExists)
            {
                return null;
            }
            
            clsPerson person = clsPerson.FindById(personId);
            if (person == null) {
                throw new Exception("User Must Linked With Person , Talk With Admin");
            }

            return new clsUser(userId,username,password,isActive,person);
        }


        private int AddNew()
        {
            this.UserID = clsUserDataAccess.CreateNew
                (
                    this.Person.PersonId,this.Username,this.Password,this.IsActive
                );
            this._Mode = enMode.Edit;

            return this.UserID;
        }

        public bool Edit()
        {
            return clsUserDataAccess.Update(this.UserID,this.Username,this.Password,this.IsActive);
        }
        public bool Save()
        {
            switch(this._Mode)
            {
                case enMode.AddNew:
                    return AddNew() != -1;
                case enMode.Edit:
                    return Edit();
            }
            return false;
        }
        
    }
}
