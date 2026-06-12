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
using DVLD.Forms.People;

namespace DVLD.Controls
{
    public partial class usrlPersonCardWithFilter : UserControl
    {

        public event Action<int> OnSearch;
        enum enSearchOption
        {
            Person_ID,
            National_No,
        }
        private int _PersonID;
        public usrlPersonCardWithFilter()
        {
            InitializeComponent();
            this._PersonID = -1;
        }

        public void LoadInfo(int personId)
        {
            if (personId == -1) return;
            this._PersonID = personId;
            ctrlPersonCard.LoadInfo(this._PersonID);

            cmbSearchOptions.SelectedIndex = cmbSearchOptions.FindStringExact(_GetSearchOptionString(enSearchOption.Person_ID));
            txtFilterValue.Text = this._PersonID.ToString();
            ctrlPersonCard.LoadInfo(personId);
        }
        public int GetPersonID()
        {
            return this._PersonID;
        }
        public void DisableFilter()
        {
            gbFilter.Enabled = false;
        }
        public void EnableFilter()
        {
            gbFilter.Enabled = true;
        }
        
        private string _GetSearchOptionString(enSearchOption filterOption)
        {
            return filterOption.ToString().Replace("_"," ");
        }
        private enSearchOption _GetSelectedSearchOption()
        {
            return _GetSearchOptionEnum(cmbSearchOptions.SelectedItem.ToString());
        }
        private enSearchOption _GetSearchOptionEnum(string filterOption)
        {
            return (enSearchOption) Enum.Parse(typeof(enSearchOption), filterOption.Replace(" ","_"));   
        }
       
        private void _ResetSearchOptoinsComboBox()
        {
            cmbSearchOptions.Items.Clear();
            foreach (enSearchOption filterOption in  Enum.GetValues(typeof(enSearchOption)) )
            {
                cmbSearchOptions.Items.Add(_GetSearchOptionString(filterOption));    
            }
            cmbSearchOptions.SelectedIndex = cmbSearchOptions.FindStringExact(_GetSearchOptionString(enSearchOption.Person_ID));
        }
        private void _ClearAllSearchInput()
        {
            txtFilterValue.Clear();
        }
        private void _HideAllSearchInput()
        {
            txtFilterValue.Visible = false;
        }
        private void _ResetSearchInputs()
        {
            _ClearAllSearchInput();
            _HideAllSearchInput();// this line is not make sense here but if we have multiple element that users can write search value  like  checkboxes , drop down list , numeric box , etc .. this line will make sense and its essential
        }
        private void _ResetForm()
        {
            _ResetSearchInputs();
            _ResetSearchOptoinsComboBox();
        }
        private void ursPersonFilter_Load(object sender, EventArgs e)
        {
            _ResetForm();
        }

        private void _ShowSearchInputBySearchOption()
        {
            enSearchOption searchOption = _GetSelectedSearchOption();
            switch (searchOption) { 
                case enSearchOption.Person_ID:
                case enSearchOption.National_No:
                    txtFilterValue.Visible = true;
                    txtFilterValue.Focus();
                    break;
            }
        }
        private void cmbSearchOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ResetSearchInputs();
            _ShowSearchInputBySearchOption();
        }

        private string _GetSearchValue()
        {
            enSearchOption searchOption = _GetSelectedSearchOption();
            switch (searchOption) {
                case enSearchOption.Person_ID:
                case enSearchOption.National_No:
                    return txtFilterValue.Text;
            }
            return null;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string value = _GetSearchValue();// this method is not essential here but for handling multiple type of inputs it will be essential


            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Please enter a search value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _HandleSearch(value);
        }

        private void _SetSearchInputsToDefault() 
        {
            txtFilterValue.Text = "";
        }
        private void _HandleSearch(string searchValue)
        {
            enSearchOption searchOption = _GetSelectedSearchOption();
            clsPerson person = new clsPerson();
            switch (searchOption)
            {
                case enSearchOption.Person_ID:
                    person = clsPerson.FindById(int.Parse(searchValue));
                    if(person == null)
                    {
                        MessageBox.Show($"Person With Id {searchValue} Not Exists");
                        _SetSearchInputsToDefault();   
                    }
                    break;
                case enSearchOption.National_No:
                    person = clsPerson.FindByNationalNo(searchValue);
                    if (person == null)
                    {
                        MessageBox.Show($"Person With National No {searchValue} Not Exists");
                        _SetSearchInputsToDefault();
                    }
                    break;
            }
            
            this._PersonID = person == null ? -1 : person.PersonId;
            ctrlPersonCard.LoadInfo(this._PersonID);
            OnSearch?.Invoke(this._PersonID);

        }
        private void _HandleInputValidation()
        {
            enSearchOption searchOption = _GetSelectedSearchOption();

            switch (searchOption)
            {
                case enSearchOption.Person_ID:
                    if (!int.TryParse(txtFilterValue.Text, out int result))
                    {
                        txtFilterValue.Text = "";
                        return;
                    }
                    break;
                case enSearchOption.National_No:
                    break;
            }
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _HandleInputValidation();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.OnPersonSaved += _HandleNewPersonAdded;
            frm.ShowDialog();
        }

        private void _HandleNewPersonAdded(int personId)
        {
            clsPerson person = clsPerson.FindById(personId);
            if (person == null) {
                _ResetForm();
                return;
            }
            this._PersonID = personId;
            cmbSearchOptions.SelectedIndex = cmbSearchOptions.FindStringExact(_GetSearchOptionString(enSearchOption.Person_ID));
            txtFilterValue.Text = person.PersonId.ToString();
            ctrlPersonCard.LoadInfo(personId);
        }

    }
}
