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
using DVLD.Forms.People;

namespace DVLD.Forms.Users
{
    public partial class frmUserManagement : Form
    {
        enum enFilterOption
        {
            None,
            User_ID,
            Person_ID,
            Full_Name,
            UserName,
            Is_Active,
        }

        DataView _dvUsers;
        public frmUserManagement()
        {
            InitializeComponent();
        }

        
        
         private void _CenterHeader()
        {

            foreach (Control control in pnlHeader.Controls)
            {
                int xAccess = (pnlHeader.Width / 2) - (control.Width / 2);
                int yAccess = control.Location.Y;
                control.Location = new Point(xAccess, yAccess);
            }
        }

        private void _RefreshUsersDataGridView()
        {
            DataTable dt = clsUser.GetAll();
            dgvUsers.DataSource = dt;

            _dvUsers = dt.DefaultView;
            lblTotal.Text = _dvUsers.Count.ToString();
        }


        private void _ClearAllFilterInputs()
        {
            txtFilterValue.Clear();
            cmbFilterValue.Items.Clear();
        }
        private void _HideAllFilterInputs()
        {
            txtFilterValue.Hide();
            cmbFilterValue.Hide();
        }
        private void _ResetFilterInputs()
        {
            _ClearAllFilterInputs();
            _HideAllFilterInputs();
        }



        private string _GetFilterOptionString(enFilterOption filterOption)
        {
            return filterOption.ToString().Replace("_", " ");
        }
        private void _ResetFilterOptions()
        {
            cmbFilterOptions.Items.Clear();
            foreach (enFilterOption filterOption in Enum.GetValues(typeof(enFilterOption))) {
                cmbFilterOptions.Items.Add(_GetFilterOptionString(filterOption));
            }
            cmbFilterOptions.SelectedIndex = cmbFilterOptions.FindStringExact(_GetFilterOptionString(enFilterOption.None));
        }


        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            _CenterHeader();
            _RefreshUsersDataGridView();
            _ResetFilterInputs();
            _ResetFilterOptions();
        }


       
        private void _OpenAddEditUserForm(int userId)
        {
            frmAddEditUser frm = new frmAddEditUser(userId);
            frm.OnUserSave += _HandleUserSaved;
            frm.ShowDialog();
        }
        private void _HandleUserSaved(int userId)
        {
            _RefreshUsersDataGridView();
        }
        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            _OpenAddEditUserForm(-1);
        }

        private void cmiAdd_Click(object sender, EventArgs e)
        {
            _OpenAddEditUserForm(-1);
        }
        private int _GetSelectedPersonId()
        {
            int userId = -1;
            if (dgvUsers.CurrentRow != null)
            {
                userId = Convert.ToInt32(dgvUsers.CurrentRow.Cells["User ID"].Value);
            }
            return userId;
        }
        private void cmiEdit_Click(object sender, EventArgs e)
        {
            _OpenAddEditUserForm(_GetSelectedPersonId());
        }

        private void dgvUsers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Ensure the user right-clicked a valid data row, not the header boundary
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Clear old selections
                dgvUsers.ClearSelection();

                // Select the row that was right-clicked
                dgvUsers.Rows[e.RowIndex].Selected = true;

                // Force the grid focus engine to update its pointer index
                dgvUsers.CurrentCell = dgvUsers.Rows[e.RowIndex].Cells[e.ColumnIndex == -1 ? 0 : e.ColumnIndex];
            }
        }
    }
}
