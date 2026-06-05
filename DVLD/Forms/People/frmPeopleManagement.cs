using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Business;
using DVLD.Forms.People;

namespace DVLD.Forms
{
    public partial class frmPeopleManagement : Form
    {
        private DataView _dvPeople;
        enum enGendor
        {
            All,
            Male,
            Female
        }

        enum enFilterOption {
            None,
            Person_ID,
            National_NO,
            First_Name,
            Second_Name,
            Third_Name,
            Last_Name,
            Nationality,
            Gendor,
            Phone,
            Email
        }
        public frmPeopleManagement()
        {
            InitializeComponent();
        }

        private void _CenterHeader()
        {
            
            foreach (Control control in pnlHeader.Controls)
            {
                
                int xAccess = (pnlHeader.Width / 2) - (control.Width / 2);
                int yAccess = control.Location.Y;
                control.Location = new Point(xAccess,yAccess);
               
            }
        }
        private void _InitFilterOptionsComboBox()
        {
            cmbFilterOptions.Items.Clear();
            Array filterOptions = Enum.GetValues(typeof(enFilterOption));
            foreach (enFilterOption filterOption in filterOptions)
            {
                cmbFilterOptions.Items.Add(_GetFilterOptionString(filterOption));
            }
            cmbFilterOptions.SelectedIndex = cmbFilterOptions.FindString(_GetFilterOptionString(enFilterOption.None));
        }
        private void _InitGendorComboBox()
        {
            cmbGendor.Items.Clear();
            foreach (enGendor gendor in Enum.GetValues(typeof(enGendor)))
            {
                cmbGendor.Items.Add(gendor.ToString().Replace("_", " "));
            }
            cmbGendor.SelectedIndex = cmbGendor.FindStringExact(enGendor.All.ToString());
        }

        private void _ClearAllFilterElements()
        {
            foreach (Control control in pnlFilters.Controls)
            {
                control.Controls.Clear();
            }
        }
        private void _HideAllFiltersElements()
        {
            foreach (Control control in pnlFilters.Controls)
            {
                control.Visible = false;
            }
        }
        

        private void _ResetFilterElements()
        {
            _ClearAllFilterElements();
            _HideAllFiltersElements();
        }

        private void _ResetForm()
        {
            _CenterHeader();
            _InitFilterOptionsComboBox();
            _InitGendorComboBox();
            _ResetFilterElements();
            _RefreshPeopleDataGridView();
        }
        private void frmPeopleManagement_Load(object sender, EventArgs e)
        {
            _ResetForm();
        }
        
        private void _HandleFilterOptionChanged(enFilterOption filterOption)
        {
            _ResetFilterElements();
            _RefreshPeopleDataGridView();
           
            switch (filterOption)
            {
                case enFilterOption.None:
                    break;
                case enFilterOption.Person_ID:
                    txtPersonID.Visible = true;
                    break;
                case enFilterOption.National_NO:
                    txtNationalNo.Visible = true;
                    break;
                case enFilterOption.First_Name:
                    txtFirstName.Visible = true;
                    break;
                case enFilterOption.Second_Name:
                    txtSecondName.Visible = true;
                    break;
                case enFilterOption.Third_Name:
                    txtThirdName.Visible = true;
                    break;
                case enFilterOption.Last_Name:
                    txtLastName.Visible = true;
                    break;
                case enFilterOption.Gendor:
                    cmbGendor.Visible = true;
                    break;
                case enFilterOption.Phone:
                    txtPhone.Visible = true;
                    break;
                case enFilterOption.Email:
                    txtEmail.Visible = true;
                    break;
                case enFilterOption.Nationality:
                    txtNationality.Visible = true;
                    break;
            }

        }

        private void _RefreshPeopleDataGridView()
        {
            DataTable dataTable = clsPerson.GetAll();
            _dvPeople = dataTable.DefaultView;

            dgvPeople.DataSource = _dvPeople;
            lblTotal.Text = _dvPeople.Count.ToString();
        }
        private void _OnPersonSaved(int personId)
        {
            _ResetForm();
        }
        private void OpenAddEditPersonForm(int personID)
        {
            frmAddEditPerson frm = new frmAddEditPerson(personID);
            frm.OnPersonSaved += _OnPersonSaved;
            frm.ShowDialog();
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            OpenAddEditPersonForm(-1);
        }

        private void cmiAdd_Click(object sender, EventArgs e)
        {
            OpenAddEditPersonForm(-1);
        }
        private int _GetSelectedPersonId()
        {
            int personId = -1;
            if (dgvPeople.CurrentRow != null)
            {
                 personId = Convert.ToInt32(dgvPeople.CurrentRow.Cells["Person ID"].Value);
            }
            return personId;
        }
        private void cmiEdit_Click(object sender, EventArgs e)
        {
            int personId = _GetSelectedPersonId();
            OpenAddEditPersonForm(personId);
           
        }

        private void dgvPeople_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Ensure the user right-clicked a valid data row, not the header boundary
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Clear old selections
                dgvPeople.ClearSelection();

                // Select the row that was right-clicked
                dgvPeople.Rows[e.RowIndex].Selected = true;

                // Force the grid focus engine to update its pointer index
                dgvPeople.CurrentCell = dgvPeople.Rows[e.RowIndex].Cells[e.ColumnIndex == -1 ? 0 : e.ColumnIndex];
            }
        }

        private void dgvPeople_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Target your exact Gender column name (or use its integer cell index)
            if (dgvPeople.Columns[e.ColumnIndex].Name == "Gendor" && e.Value != null)
            {
                // 2. Safely parse the value coming out of the bound underlying row structure
                if (byte.TryParse(e.Value.ToString(), out byte genderValue))
                {
                    // 3. Switch the display layout text manually inside the grid viewer pipeline
                    if (genderValue == 0)
                    {
                        e.Value = "Male";
                    }
                    else
                    {
                        e.Value = "Female";
                    }

                    // Tell Windows Forms it's done formatting this cell
                    e.FormattingApplied = true;
                }
            }
        }

        private void cmbFilterOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = cmbFilterOptions.SelectedItem.ToString();
            _HandleFilterOptionChanged((enFilterOption)Enum.Parse(typeof(enFilterOption),selectedItem.Replace(" ","_")) );
            
        }

        private void cmbGendor_SelectedIndexChanged(object sender, EventArgs e)
        {

            string filterOption = _GetFilterOptionString(enFilterOption.Gendor);



            string selectedGendor = cmbGendor.SelectedItem?.ToString();
            string value = "";
            switch(Enum.Parse(typeof(enGendor), selectedGendor))
            {
                case enGendor.Male:
                    value = "0";
                    break;
                case enGendor.Female:
                    value = "1";
                    break;
                case enGendor.All:
                    value = "";
                    break;
            }
            _FilterDataGridViewBy(filterOption, value);
        }


        private void _FilterDataGridViewBy(string column,string value)
        {
            if (_dvPeople == null) return;
            if (string.IsNullOrEmpty(value))
            {
                _dvPeople.RowFilter = "";
            }else
            {
                _dvPeople.RowFilter = $"CONVERT([{column}], 'System.String') LIKE '%{value}%'";
            }
            lblTotal.Text = _dvPeople.Count.ToString();
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            string filterOption = _GetFilterOptionString(enFilterOption.First_Name);
            _FilterDataGridViewBy(filterOption, (sender as TextBox).Text);
        }


        private void txtSecondName_TextChanged(object sender, EventArgs e)
        {
            string filterOption = _GetFilterOptionString(enFilterOption.Second_Name);
            _FilterDataGridViewBy(filterOption , (sender as TextBox).Text);
        }

        private void txtThirdName_TextChanged(object sender, EventArgs e)
        {
            string filterOption = _GetFilterOptionString(enFilterOption.Third_Name);
            _FilterDataGridViewBy(filterOption, (sender as TextBox).Text);
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            string filterOption = _GetFilterOptionString(enFilterOption.Last_Name);
            _FilterDataGridViewBy(filterOption, (sender as TextBox).Text);
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            string filterOption = _GetFilterOptionString(enFilterOption.Phone);

            
            if ( (! string.IsNullOrEmpty(txtPhone.Text)) &&  (!int.TryParse(txtPhone.Text, out int value) ))
            {
                txtPhone.Text = "";
                return;
            }
            _FilterDataGridViewBy(filterOption, (sender as TextBox).Text);
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string filterOption = _GetFilterOptionString(enFilterOption.Email);
            _FilterDataGridViewBy(filterOption, (sender as TextBox).Text);
        }


        private void txtPersonID_TextChanged(object sender, EventArgs e)
        {
            string filterOption = _GetFilterOptionString(enFilterOption.Person_ID);
            if ( (! string.IsNullOrEmpty(txtPersonID.Text) )&&  (!int.TryParse(txtPersonID.Text, out int value) ) )
            {
                int lastIndex = (txtPersonID.Text.Length - 1 >= 0) ? (txtPersonID.Text.Length - 1): -1;
                if(lastIndex >= 0)
                {
                    txtPersonID.Text = txtPersonID.Text.Remove(lastIndex);
                }
                return;
            }
            _FilterDataGridViewBy(filterOption, (sender as TextBox).Text);
        }

        private void txtNationalNo_TextChanged(object sender, EventArgs e)
        {
            
            string filterOption = _GetFilterOptionString(enFilterOption.National_NO);
            _FilterDataGridViewBy(filterOption, (sender as TextBox).Text);
        }

        private void txtNationality_TextChanged(object sender, EventArgs e)
        {
            string filterOption = _GetFilterOptionString(enFilterOption.Nationality);
            _FilterDataGridViewBy(filterOption, (sender as TextBox).Text);
        }
        private string _GetFilterOptionString(enFilterOption filterOption)
        {
            return filterOption.ToString().Replace("_", " ");
        }

        private void cmiDelete_Click(object sender, EventArgs e)
        {
           
        }

        private void cmiShowDetails_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_GetSelectedPersonId());
            frm.OnPersonDetailsUpdated += _OnPersonSaved;
            frm.ShowDialog();

            _OnPersonSaved(0);
        }
    }
}
