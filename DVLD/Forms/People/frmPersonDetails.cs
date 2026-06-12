using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Forms.People
{
    public partial class frmPersonDetails : Form
    {
        public event Action<int> OnPersonDetailsUpdated;
        public frmPersonDetails(int personId)
        {
            InitializeComponent();
            usrPersonInfo.LoadInfo(personId);
            usrPersonInfo.OnPersonDetailsUpdated += _HandlePersonUpdated;
        }
        private void _HandlePersonUpdated(int personId)
        {
            // Force the User Control to re-read the database and paint new labels
            usrPersonInfo.LoadInfo(personId);

            // Bubble the notification up to notify the Main Form DataGridView
            OnPersonDetailsUpdated?.Invoke(personId);
        }

    }
}
