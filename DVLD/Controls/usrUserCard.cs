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
    public partial class usrUserCard : UserControl
    {
        public usrUserCard()
        {
            InitializeComponent();
        }

        public void LoadInfo(int userId)
        {
            clsUser user = clsUser.FindByID(userId);
            if(user ==null)
            {
                return;
            }
            ctrlPersonCard.LoadInfo(user.Person.PersonId);
            lblUserID.Text = user.UserID.ToString();
            lblUsername.Text = user.Username;
            lblIsActive.Text = user.IsActive ? "Yes" :"No";
        }
    }
}
