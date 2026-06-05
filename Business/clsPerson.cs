using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class clsPerson
    {
        enum enMode
        {
            AddNew , Edit
        }
        private enMode _Mode;
       

        public int PersonId { get; private set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public byte Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public int NationalityCountryID { get; set; }
        public string CountryName { get; set; }

        public clsPerson()
        {
            this._Mode = enMode.AddNew;
        }
        private clsPerson(int personId ,string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBitth, byte gendor, string address, string phone, string email, int nationalityCountryID,string countryName, string imagePath)
        {
            PersonId = personId;
            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBitth;
            Gendor = gendor;
            Address = address;
            Phone = phone;
            Email = email;
            ImagePath = imagePath;
            this.NationalityCountryID = nationalityCountryID;
            this.CountryName = countryName;
            this._Mode = enMode.Edit;
        }

        public static bool IsPersonExists(int personId)
        {
            return clsPersonDataAccess.IsPersonIdExists(personId);
        }
        public static DataTable GetAll()
        {
            return clsPersonDataAccess.GetAll();
        }
        public static bool IsNationalNoExists(string nationalNo)
        {
            return clsPersonDataAccess.IsNationalNoExists(nationalNo);
        }
        

        public static  clsPerson FindById(int id )
        {
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "", countryName = "";
            DateTime dateOfBitth = DateTime.Now;
            byte gendor = 1;
            int nationalityCountryId = -1;
            try
            {

                if (!clsPersonDataAccess.FindById(id, ref nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBitth, ref gendor, ref address, ref phone, ref email, ref nationalityCountryId, ref countryName, ref imagePath))
                {
                    return null;
                }
                else
                {
                    return new clsPerson(id, nationalNo, firstName, secondName, thirdName, lastName, dateOfBitth, gendor, address, phone, email, nationalityCountryId, countryName, imagePath);
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        private int AddNew()
        {
            
            int result =  clsPersonDataAccess.AddNew
                (this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone,
                this.Email, this.CountryName, this.ImagePath);

            if(result != -1)
            {
                this.PersonId = result;
            }
            return result;
        }
        private bool Edit()
        {
            return clsPersonDataAccess.Edit
                (this.PersonId,this.FirstName,this.SecondName,this.ThirdName,
                this.LastName,this.DateOfBirth,this.Gendor,this.Address,this.Phone,
                this.Email,this.CountryName,this.ImagePath);
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
