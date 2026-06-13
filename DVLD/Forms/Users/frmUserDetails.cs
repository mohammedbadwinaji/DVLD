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
    public partial class frmUserDetails : Form
    {
        private int _UserID;
        public frmUserDetails(int userId)
        {
            InitializeComponent();
            _UserID = userId;
        }

        private void frmUserCard_Load(object sender, EventArgs e)
        {
            if(_UserID == -1)
            {
                MessageBox.Show("Choose User To View Detials");
                this.Close();
                return;
            }
            clsUser user = clsUser.FindByID(this._UserID);
            if(user == null)
            {
                MessageBox.Show($"No User With ID {this._UserID}");
                this.Close();
                return;
            }

            ctrlUserCard.LoadInfo(_UserID);
        }
    }
}
