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

namespace DVLD.Forms.Users
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        private clsUser _User;
        public frmChangePassword(int userId)
        {
            InitializeComponent();
            _UserID = userId;
            _User = new clsUser();  
        }
      
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            if(_UserID == -1)
            {
                MessageBox.Show("No User Choosed");
                this.Close();
                return;
            }
            _User = clsUser.FindByID(this._UserID);

            if (_User == null) {
                MessageBox.Show($"User With ID {this._UserID} Not Exists");
                this.Close();
                return;
            }
            
            ctrlUserCard.LoadInfo(_UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _CheckValidation(out string errorMessage)
        {
            errorMessage = "";
            bool isValid = true;
            if (string.IsNullOrEmpty(txtCurrentPassword.Text)) {
                errorMessage += "You Must Type Current Password\n";
                isValid = false;
            } else if (txtCurrentPassword.Text != _User.Password)
            {
                errorMessage += "Current Password Is Wrong\n";
                isValid = false;
            }
            if (string.IsNullOrEmpty(txtNewPassword.Text)) {
                errorMessage += "You Must Type New Password\n";
                isValid = false;
            } else
            {
                if (string.IsNullOrEmpty(txtConfirmPassword.Text))
                {
                    errorMessage += "You Must Type Password Confirmation\n";
                    isValid = false;
                }else if (txtConfirmPassword.Text != txtNewPassword.Text)
                {
                    errorMessage += "Password Confirmation Must Match With New Password Field\n"; 
                    isValid = false;
                }
            }
            return isValid;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorMessage;
            if(!_CheckValidation(out errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }

            
                if (_User.ChangePassword(txtCurrentPassword.Text, txtNewPassword.Text))
                {
                    MessageBox.Show("Password Changed Successfully");
                } else
                {
                MessageBox.Show("Failed Updating Password");
            }
            
        }
    }
}
