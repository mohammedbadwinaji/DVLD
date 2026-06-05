using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Business;
using DVLD.Validations;

namespace DVLD
{
    internal class clsAddPersonValidator : clsPersonValidator
    {
        public static bool IsValid
            (
                string nationalNo,string firstName, string secondName,
                string thirdName, string lastName , string phone,
                string email, string countryName,string address, byte gendor,DateTime dateOfBirth,string imagePath
            )

        {
         
            string errorMessage = "";
            if (!IsNationalNoValid(nationalNo))
            {
                errorMessage += $"National No Is Required And Must Be Unique\n";
            }
            if (!IsFirstNameValid(firstName))
            {
                errorMessage += $"First Name Is Required\n";
            }
            if (!IsSecondNameValid(secondName))
            {
                errorMessage += $"Second Name Is Required\n";
            }
            if (!IsThirdNameValid(thirdName))
            {
                errorMessage += $"Third Name Is Required\n";
            }
            if (!IsLastNameValid(lastName)) {
                errorMessage += $"Last Name Is Required\n";
            }
            if (!IsPhoneValid(phone)) {
                errorMessage += $"Phone Is Required\n";
            }
            if (!IsEmailValid(email)) {
                errorMessage += $"Email Is Can be Empty or Valid Email\n";
            }
            if (!IsAddressValid(address)) {
                errorMessage += $"Address Is Required\n";
            }
            if (!IsGendorValid(gendor)) {
                errorMessage += $"Address Is Required\n";
            }
            if (!IsImagePathValid(imagePath)) {
                errorMessage += $"Image Can Be Empty Or Valid Image Path\n";
            }
            if(!IsDateOfBirthValid(dateOfBirth))
            {
                errorMessage += $"Date OF Birth Required And Must Be Bigger or Equals 18 Years";
            }
            if (!IsCountryNameValid(countryName))
            {
                errorMessage += $"Country Dosn't Exists";
            }



            if (string.IsNullOrEmpty(errorMessage))
            {
                return true;
            }
            else
            {
                throw new Exception(errorMessage);
            }

        
        }
        
    
    }
}
