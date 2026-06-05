using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;
using DVLD.Forms.People;

namespace DVLD.Controls.People
{
    public partial class usrPersonCard : UserControl
    {
        public event Action<int> OnPersonDetailsUpdated;
        public usrPersonCard()
        {
            InitializeComponent();
        }
        public void Load(int PersonID)
        {
            clsPerson person = clsPerson.FindById(PersonID);

            if (person == null) {
                MessageBox.Show($"Person With Id {PersonID} Not Exists");
                return;
            }

            lblPersonID.Text = person.PersonId.ToString();
            lblName.Text = _GetFullName(person.FirstName,person.SecondName,person.ThirdName,person.LastName);
            lblNationalNo.Text = person.NationalNo;
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblCountry.Text = person.CountryName;
            lblEmail.Text = person.Email;
            lblPhone.Text = person.Phone;
            lblAddress.Text = person.Address;
            if(person.Gendor == 0)
            {
                lblGendor.Text = "Male";
                pbGendor.ImageLocation = clsSettings.ManImagePath;
            }else
            {
                lblGendor.Text = "Female";
                pbGendor.ImageLocation = clsSettings.WomenImagePath;
            }
            if(string.IsNullOrEmpty(person.ImagePath))
            {
                if(person.Gendor == 0)
                {
                    pbPersonImage.ImageLocation = clsSettings.MaleImagePath;
                }else
                {
                    pbPersonImage.ImageLocation = clsSettings.FemaleImagePath;
                }
            }else
            {
                pbPersonImage.ImageLocation = person.ImagePath;
            }
        }
        private string _GetFullName(string firstName , string secondName , string thirdName , string lastName)
        {
            return firstName + " " + secondName + " " + thirdName + " " + lastName; 
        }
        private void usrPersonCard_Load(object sender, EventArgs e)
        {

        }

        private void llEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(int.TryParse(lblPersonID.Text,out int personId))
            {
                frmAddEditPerson frm = new frmAddEditPerson(personId);
                frm.OnPersonSaved += _HandlePersonSave;
                frm.ShowDialog();
            }
        }
        private void _HandlePersonSave(int updatedPersonId)
        {
            
            this.Load(updatedPersonId);

            
            OnPersonDetailsUpdated?.Invoke(updatedPersonId);
        }
    }
}
