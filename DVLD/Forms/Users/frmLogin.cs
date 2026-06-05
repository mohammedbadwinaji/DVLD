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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            bool rememberMe = chkRemeberMe.Checked;

            string errorMessage = "";
            clsUser user = clsUser.Login(username, password,out errorMessage);

            if (user == null) {
                MessageBox.Show(errorMessage);
                return;
            }

            if(rememberMe)
            {
                bool isUserRemebered = clsRememberMeManager.SaveCredentials(username, password,out errorMessage);

                if(!isUserRemebered)
                {
                    MessageBox.Show("Remember me Feature is not work");
                }
            }else
            {
                clsRememberMeManager.ClearCredentials(out errorMessage);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void _LoadUserCredentials()
        {
            string username = "";
            string password = "";
            string errorMessage = "";

            clsRememberMeManager.GetStoredCredentials(out username, out password,out errorMessage);

            txtUserName.Text = username;
            txtPassword.Text = password;
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            chkRemeberMe.Checked = true;
            _LoadUserCredentials();
          
        }
    }
}
