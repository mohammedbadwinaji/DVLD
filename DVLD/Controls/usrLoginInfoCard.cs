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

namespace DVLD.Controls
{
    public partial class usrLoginInfoCard : UserControl
    {
        public usrLoginInfoCard()
        {
            InitializeComponent();
        }
        public void LoadInfo(int userId)
        {
            clsUser user = clsUser.FindByID(userId);
            if (user == null) {
                return;
            }

            lblUserID.Text = user.UserID.ToString();
            lblUsername.Text = user.Username;
            lblIsActive.Text = user.IsActive ? "Yes" : "No";
        }

        private void _SetDefaultValues()
        {
            lblUserID.Text = "???";
            lblUsername.Text = "???";
            lblIsActive.Text = "???";   
        }
        private void usrLoginInfoCard_Load(object sender, EventArgs e)
        {
            _SetDefaultValues();
        }
    }
}
