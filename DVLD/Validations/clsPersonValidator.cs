using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Business;

namespace DVLD.Validations
{
    internal abstract class clsPersonValidator
    {

        public static bool IsPersonIdValid(int  personId)
        {
            return personId >= 0 && clsPerson.IsPersonExists(personId);
        }
        public static bool IsNationalNoValid(string nationalNo)
        {
            return (!string.IsNullOrEmpty(nationalNo)) && (!clsPerson.IsNationalNoExists(nationalNo));
        }
        public static bool IsFirstNameValid(string firstName)
        {
            return !(string.IsNullOrEmpty(firstName));
        }
        public static bool IsSecondNameValid(string secondName)
        {
            return !(string.IsNullOrEmpty(secondName));
        }
        public static bool IsThirdNameValid(string thirdName)
        {
            return !(string.IsNullOrEmpty(thirdName));
        }
        public static bool IsLastNameValid(string lastName)
        {
            return !(string.IsNullOrEmpty(lastName));
        }
        public static bool IsPhoneValid(string phone)
        {
            return !(string.IsNullOrEmpty(phone));
        }

        public static bool IsAddressValid(string address)
        {
            return !(string.IsNullOrEmpty(address));
        }
        public static bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }
            try
            {
                MailAddress address = new MailAddress(email);
                return address.Address == email;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsGendorValid(byte Gendor)
        {
            return Gendor == 0 || Gendor == 1;
        }
        public static bool IsDateOfBirthValid(DateTime dateOfBirth)
        {
            DateTime ageCutoff = DateTime.Today.AddYears(-18);

            return (dateOfBirth <= ageCutoff);
        }
        public static bool IsImagePathValid(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return true;
            }
            if (!File.Exists(imagePath))
            {
                return false;
            }
            return true;
        }

        public static bool IsCountryNameValid(string countryName)
        {
            return clsCountry.IsCountryExists(countryName);
        }
    }
}
