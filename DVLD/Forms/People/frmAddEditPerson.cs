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
using DVLD.Validations;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD.Forms.People
{
    public partial class frmAddEditPerson : Form
    {
        public event Action<int> OnPersonSaved;
        enum enGendor
        {
            Male,Female
        }

        private int _PersonID;
        private clsPerson _Person;
        private bool _IsPersonHasImage;
        public frmAddEditPerson(int PersonID = -1)
        {
            InitializeComponent();
            this._PersonID = PersonID;
            this._Person = new clsPerson();
            this._IsPersonHasImage = false; 
        }
        private void _InitTitle()
        {
            string title = "";
            if(this._PersonID == -1)
            {
                this.Text = "Add New Person";
                title = "Add New Person";
            }else
            {
                this.Text = "Edit Person";
                title = $"Edit Person {this._PersonID}";
            }
            lblTitle.Text = title;
        }
        private void _InitGendorRadioButtons()
        {
            rbMale.Checked = true;
        }
        private void _InitDateOfBirthTimePicker()
        {
            DateTime today = DateTime.Now;
            DateTime eighteenYearsAgo = today.AddYears(-18).AddDays(-1);


            dtpDateOfBirth.MaxDate = eighteenYearsAgo;
            dtpDateOfBirth.Value = eighteenYearsAgo;
        }
        private void _InitCountryComboBox()
        {
            DataTable countries = clsCountry.GetAll();

            foreach (DataRow country in countries.Rows)
            {
                cmbCountry.Items.Add(country["CountryName"]);
            }
            int index = cmbCountry.FindStringExact("Syria");
            if (index != -1)
            {
                cmbCountry.SelectedIndex = index;
            }
            else
            {
                cmbCountry.SelectedIndex = 0;
            }
        }
        private void _InitLinkLableImages()
        {
            llRemoveImage.Visible= false;
        }
        private void _ResetForm()
        {
            _InitTitle();
            _InitGendorRadioButtons();
            _InitDateOfBirthTimePicker();
            _InitCountryComboBox();
            _InitLinkLableImages();

            if (this._PersonID != -1)
            {
                _LoadPersonInfoToUI();
                txtNationalNo.Enabled = false;
            }
        }
        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetForm();
        }

      
        private void _LoadPersonInfoToUI()
        {
            try
            {
                this._Person = clsPerson.FindById(this._PersonID);
                if(this._Person == null)
                {
                    MessageBox.Show($"Person With ID {this._PersonID} Is Not Exists", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                } else
                {
                    lblPersonId.Text = this._Person.PersonId.ToString();
                    txtNationalNo.Text = this._Person.NationalNo;
                    txtFirstName.Text = this._Person.FirstName;
                    txtSecondName.Text = this._Person.SecondName;
                    txtThirdName.Text = this._Person.ThirdName;
                    txtLastName.Text = this._Person.LastName;
                    txtPhone.Text = this._Person.Phone;
                    txtEmail.Text = this._Person.Email;
                    txtAddress.Text = this._Person.Address;
                    
                    dtpDateOfBirth.Value = this._Person.DateOfBirth;
                    
                    if(this._Person.Gendor == 0)
                    {
                        rbMale.Checked = true;
                    }else
                    {
                        rbFemale.Checked = true;
                    }

                    cmbCountry.SelectedIndex = cmbCountry.FindStringExact(this._Person.CountryName);  

                    if(!string.IsNullOrEmpty(this._Person.ImagePath))
                    {
                        pbImage.ImageLocation = this._Person.ImagePath;
                        llRemoveImage.Visible = true;
                        _IsPersonHasImage = true;

                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }




        private void _HandleGendorChanges(enGendor gendor)
        {
            if (_IsPersonHasImage) return;
            
            switch (gendor) {
                case enGendor.Male:
                    pbImage.ImageLocation = clsSettings.MaleImagePath;
                    break;
                case enGendor.Female:
                    pbImage.ImageLocation = clsSettings.FemaleImagePath;
                    
                    break;
            }
        }
        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            _HandleGendorChanges(enGendor.Male);
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            _HandleGendorChanges(enGendor.Female);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private bool _CheckIfDataValid()
        {
            if (this._PersonID == -1)
            {
                     return clsAddPersonValidator.IsValid
                     (
                        txtNationalNo.Text, txtFirstName.Text, txtSecondName.Text,
                        txtThirdName.Text, txtLastName.Text, txtPhone.Text, txtEmail.Text,
                        cmbCountry.Text, txtAddress.Text, (byte)(rbMale.Checked == true ? 0 : 1), dtpDateOfBirth.Value, pbImage.ImageLocation
                     );
            }else
            {
                return  clsEditPersonValidator.IsValid
                     (
                        this._Person.PersonId, txtFirstName.Text, txtSecondName.Text,
                        txtThirdName.Text, txtLastName.Text, txtPhone.Text, txtEmail.Text,
                        cmbCountry.Text, txtAddress.Text, (byte)(rbMale.Checked == true ? 0 : 1), dtpDateOfBirth.Value, pbImage.ImageLocation
                     );
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            
           try
            {
                bool isPersonDataValid = _CheckIfDataValid();
                
                

                if (!isPersonDataValid)
                {
                    MessageBox.Show("Error In System Call Admin");
                    return;
                }

                _LoadPersonInfoFromUI();
                
                

                if (this._Person.Save())
                {
                    this._PersonID = this._Person.PersonId;
                    _ResetForm();
                    MessageBox.Show($"Person With ID {this._Person.PersonId} Saved Successfully");
                    OnPersonSaved?.Invoke(this._Person.PersonId);

                } else
                {
                    MessageBox.Show("Failed Saved Person");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void _LoadPersonInfoFromUI()
        {
            this._Person.NationalNo = txtNationalNo.Text.Trim();
            this._Person.FirstName = txtFirstName.Text.Trim();
            this._Person.SecondName = txtSecondName.Text.Trim();
            this._Person.ThirdName = txtThirdName.Text.Trim();
            this._Person.LastName = txtLastName.Text.Trim();
            this._Person.Phone = txtPhone.Text.Trim();
            this._Person.Email = txtEmail.Text.Trim();
            this._Person.Address = txtAddress.Text.Trim();
            this._Person.DateOfBirth = dtpDateOfBirth.Value;
            this._Person.Gendor = (byte)(rbMale.Checked ? 0 : 1);
            this._Person.ImagePath = _IsPersonHasImage ? pbImage.ImageLocation : null;
            this._Person.CountryName = cmbCountry.Text.Trim();

        }
    

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofd.Title = "Select Person Profile Image";
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif|" +
                                "JPEG Image (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                "PNG Image (*.png)|*.png|" +
                                "Bitmap Image (*.bmp)|*.bmp";

            ofd.FilterIndex = 1;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbImage.ImageLocation = ofd.FileName;
                pbImage.Tag = ofd.FileName;
                llRemoveImage.Visible = true;
                _IsPersonHasImage = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!_IsPersonHasImage) return;

            Image imgToDispose = pbImage.Image;
            pbImage.Image = null;
            pbImage.ImageLocation = null;
            pbImage.Tag = null;

            imgToDispose?.Dispose();

            _IsPersonHasImage = false;
            _HandleGendorChanges(rbMale.Checked ? enGendor.Male : enGendor.Female);

            llRemoveImage.Visible = false;
        }
    }
}
