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
using DVLD.Controls;

namespace DVLD.Forms.Users
{
    public partial class frmAddEditUser : Form
    {
        public event Action<int> OnUserSave;
        enum enMode
        {
            AddNew , Edit
        }
        private enMode _Mode;

        private clsUser _User;
        private int _UserID;


        private bool _IsAbleToMoveTabs;
        private void _ResetMode()
        {
            if(_User.UserID == -1)
            {
                _Mode = enMode.AddNew;
            }else
            {
                _Mode = enMode.Edit;
            }
        }
        public frmAddEditUser(int userID = -1)
        {
            InitializeComponent();
            _UserID = userID;
            _User = new clsUser();
            _IsAbleToMoveTabs = false;
            ctrlPersonCardWithFilter.OnSearch += _HandlePersonSearch;
        }
        
        private void _HandlePersonSearch(int personId)
        {
            _User.Person = clsPerson.FindById(personId);
        }

        private void _ResetSearchPersonControl()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    ctrlPersonCardWithFilter.EnableFilter();
                    break;
                case enMode.Edit:
                    ctrlPersonCardWithFilter.DisableFilter();
                    break;
            }
        }
        private void _ResetTitle()
        {
            lblTitle.Text = _Mode == enMode.AddNew ? "Add New User" : $"Update User {_User.UserID}";
        }
        
        private void _ResetSaveButton()
        {
            
        }


        private void _LoadUserInfo()
        {
            clsUser user = clsUser.FindByID(this._UserID);
            if (user != null)
            {
                _UserID = user.UserID;
                _User = user;

                lblUserID.Text = _User.UserID.ToString();
                txtUsername.Text = _User.Username;
                txtPassword.Text = _User.Password;
                txtPasswordConfirmation.Text = _User.Password;
                chkIsActive.Checked = _User.IsActive;
                ctrlPersonCardWithFilter.LoadInfo(_User.Person.PersonId);
            }
        }
        private void _ResetForm()
        {
            _LoadUserInfo();
            _ResetMode();
            _ResetSearchPersonControl();
            _ResetTitle();
            _ResetSaveButton();
        }
        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetForm();
        }

        private void _GoToNextTab()
        {
            _IsAbleToMoveTabs = true;
            if(tbc.SelectedIndex > tbc.TabCount - 1)
            {
                return;
            }
            tbc.SelectedIndex = tbc.SelectedIndex + 1;
            _IsAbleToMoveTabs = false;
        }
        private void _GoToPrevTab()
        {
            _IsAbleToMoveTabs = true;
            if (tbc.SelectedIndex <= 0)
            {
                return;
            }
            tbc.SelectedIndex = tbc.SelectedIndex - 1;
            _IsAbleToMoveTabs = false;
        }

        private int _GetSelectedPersonID()
        {
            return ctrlPersonCardWithFilter.GetPersonID();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            int selectedPersonId = _GetSelectedPersonID();
            if(selectedPersonId == -1)
            {
                MessageBox.Show("Choose Person First");
                    return;
            }

            if(_Mode == enMode.AddNew)
            {
                bool isUser = clsPerson.IsUser(selectedPersonId);
                if (isUser)
                {
                    MessageBox.Show("Selected Person Already User, Choose Another One");
                    return;
                }
            }

            _GoToNextTab();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            _GoToPrevTab();
        }

        private void tbc_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(!_IsAbleToMoveTabs)
            {
                e.Cancel = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorMessage;
            if(!IsUserInfoValid(out errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }
            _LoadUserInfoFromUI();

            try
            {
                if (_User.Save())
                {
                    MessageBox.Show("User Saved Successfully");
                    _UserID = _User.UserID;
                    _ResetForm();
                    OnUserSave?.Invoke(_User.UserID);
                }
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.ToString());
            }
        }

        private void _LoadUserInfoFromUI()
        {
            _User.Username = txtUsername.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = chkIsActive.Checked;
            _User.Person = clsPerson.FindById(_GetSelectedPersonID());
        }

        private bool IsUserInfoValid(out string errorMessage)
        {
            bool IsValid = true;
            errorMessage = "";
            if (!clsPerson.IsPersonExists(_GetSelectedPersonID()))
            {
                IsValid = false;
                errorMessage = $"Person With Id {_GetSelectedPersonID()} Is Not Exists, Choose Another One\n";
            }

            if(string.IsNullOrEmpty(txtUsername.Text))
            {
                IsValid = false;
                errorMessage += "Username Is Required\n";
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                IsValid = false;
                errorMessage += "Password Is Required\n";
            }
            else
            {
                if (txtPassword.Text != txtPasswordConfirmation.Text)
                {
                    IsValid = false;
                    errorMessage += "Password Confirmation Must Match With Password";
                }
            }
            return IsValid;
        }





    }
}
