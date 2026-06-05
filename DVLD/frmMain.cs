using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;
using DVLD.Forms;

namespace DVLD
{
    public partial class frmMain : Form
    {
        public bool IsLoggedOut { get; private set; } = false;
        public frmMain()
        {
            InitializeComponent();
        }

        private void _ChangeMdiClientColor()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient)
                {
                    // Change the background color to whatever you prefer (e.g., Light Gray, Black, or White)
                    ctrl.BackColor = Color.FromArgb(255, 140, 0);
                    break;
                }
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            _ChangeMdiClientColor();
            
            try
            {
                DataTable dt = clsPerson.GetAll();


            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void _CloseAllForms()
        {
            foreach(Form children in this.MdiChildren)
            {
                children.Close();
            }

        }
        private void _CenterAllForms()
        {
            // 1. Locate the actual MdiClient surface workspace control
            MdiClient mdiClient = null;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient)
                {
                    mdiClient = (MdiClient)ctrl;
                    break;
                }
            }

            // 2. Fall back cleanly if the MdiClient engine is not fully initialized yet
            int workspaceWidth = mdiClient != null ? mdiClient.ClientRectangle.Width : this.ClientSize.Width;
            int workspaceHeight = mdiClient != null ? mdiClient.ClientRectangle.Height : this.ClientSize.Height;

            int topOffset = 83;

            foreach (Form children in this.MdiChildren)
            {
                // Skip minimized or maximized windows to prevent rendering bugs
                if (children.WindowState != FormWindowState.Normal) continue;

                // Force layout engine to accept custom positioning coordinates
                children.StartPosition = FormStartPosition.Manual;

                // Calculate exact horizontal center relative to the workspace
                int centerX = (workspaceWidth - children.Width) / 2;

                // Calculate exact vertical center inside the lower workspace boundary
                int remainingHeight = workspaceHeight - topOffset;
                int centerY = topOffset + ((remainingHeight - children.Height) / 2);

                // Clamping bounds to ensure forms do not cross the 83px line or left margin
                int finalX = Math.Max(0, centerX);
                int finalY = Math.Max(topOffset, centerY);

                // 3. FORCE RE-ASSIGNMENT: Re-apply values to make sure WinForms registers the change
                children.Location = new Point(finalX, finalY);
                children.Left = finalX;
                children.Top = finalY;
            }
        }

        private void miPeople_Click(object sender, EventArgs e)
        {
            _CloseAllForms();
            frmPeopleManagement frm = new frmPeopleManagement();
            frm.MdiParent = this;
            frm.Width = this.Width - 200;

            frm.Show();
            _CenterAllForms();
        }

        private void miLogout_Click(object sender, EventArgs e)
        {
            IsLoggedOut = true;
            this.Close();
        }

    }
}
