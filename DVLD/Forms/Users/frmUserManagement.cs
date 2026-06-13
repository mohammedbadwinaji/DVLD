using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
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
        enum enIsActiveFilterValue
        {
            All,
            Active,
            Not_Active
        }
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
        enFilterOption _CurrentFilterOption;
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

        
        private void _InitComboBoxFilterInput<T>() where T : Enum
        {
            cmbFilterValue.Items.Clear();
            // here no need for this switch .....
            switch(typeof(T))
            {
                case Type t when t == typeof(enIsActiveFilterValue):
                    foreach (enIsActiveFilterValue value in Enum.GetValues(typeof(enIsActiveFilterValue)))
                    {
                        cmbFilterValue.Items.Add(_GetIsActiveFilterValueString(value));
                    }
                    cmbFilterValue.SelectedIndex = cmbFilterValue.FindStringExact(_GetIsActiveFilterValueString(enIsActiveFilterValue.All));
                    break;
            }
        }
        private void _ResetFilterInputs()
        {
            _ClearAllFilterInputs();
            _HideAllFilterInputs();
        }



        private string _GetIsActiveFilterValueString(enIsActiveFilterValue value)
        {
            return value.ToString().Replace("_", " ");
        }
        private enIsActiveFilterValue _GetIsActiveFilterValueEnum(string value)
        {
            return (enIsActiveFilterValue) Enum.Parse(typeof (enIsActiveFilterValue),value.ToString().Replace(" ", "_"));
        }


        private string _GetFilterOptionString(enFilterOption filterOption)
        {
            return filterOption.ToString().Replace("_", " ");
        }
        private enFilterOption _GetFilterOptionEnum (string filterValue)
        {
            return (enFilterOption) Enum.Parse(typeof(enFilterOption), filterValue.Replace(" ","_")); 
        }
        private enFilterOption _GetSelectedFilterOption()
        {
            return _GetFilterOptionEnum(cmbFilterOptions.Text);
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
        private int _GetSelectedUserId()
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
            _OpenAddEditUserForm(_GetSelectedUserId());
        }

        
        private void _HandleFilteration(string column ,string value)
        {
            if (string.IsNullOrEmpty(value))
                _dvUsers.RowFilter = "";
            else
            _dvUsers.RowFilter = $"CONVERT([{column}], 'System.String') LIKE '%{value}%'";
        }

        private string _ReadIsActiveValue()
        {
            if (cmbFilterValue.Text == "All")
            {
                return "";
            }
            else if (cmbFilterValue.Text == "Active")
            {
                return "1"; 
            }
            else 
            {
                return "0"; 
            }
        }
        private void cmbFilterValue_SelectedIndexChanged(object sender, EventArgs e)
        {

            // here no need for switch case because i have one comboBox and i know where data comes from
            // but if i have multiple comboBo data can comes from
            // i need for this switch case
            string value = "";
            switch (_CurrentFilterOption)
            {
                case enFilterOption.Is_Active:
                    value = _ReadIsActiveValue();
                    if (string.IsNullOrEmpty(value))
                    {
                        _dvUsers.RowFilter = "";
                    }
                    else
                    {
                        _dvUsers.RowFilter = $"[Is Active] = {value}";
                    }
                    break;
            }
        }

        private void _PreventStringValue(TextBox txtbox)
        {
            if (string.IsNullOrEmpty(txtbox.Text)) return;
            if(!int.TryParse(txtbox.Text,out int result))
            {
                txtbox.Text = txtbox.Text.Substring(0, txtbox.Text.Length - 1);
            }
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            switch(_CurrentFilterOption)
            {
                case enFilterOption.Person_ID:
                case enFilterOption.User_ID:
                    _PreventStringValue(sender as TextBox);
                    break;
                
            }

            _HandleFilteration(_GetFilterOptionString(_GetSelectedFilterOption()), txtFilterValue.Text);
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

        private void cmbFilterOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ResetFilterInputs();
            _CurrentFilterOption = _GetSelectedFilterOption();

            switch (_CurrentFilterOption)
            {
                case enFilterOption.None:
                    _HandleFilteration("Full Name",null);
                    break;
                case enFilterOption.User_ID:
                case enFilterOption.Person_ID:
                case enFilterOption.UserName:
                case enFilterOption.Full_Name:
                    txtFilterValue.Visible = true;
                    break;
                case enFilterOption.Is_Active:
                    _InitComboBoxFilterInput<enIsActiveFilterValue>();
                    cmbFilterValue.Visible = true;
                    break;

            }


        }
        private void _OpenChangePasswordForm()
        {
            frmChangePassword frm = new frmChangePassword(_GetSelectedUserId());
            frm.ShowDialog();
        }
        private void cmiChangePassword_Click(object sender, EventArgs e)
        {
            _OpenChangePasswordForm();
        }
    }
}
