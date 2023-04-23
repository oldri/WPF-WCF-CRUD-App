using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using AS2_991666875_S.Models;
using AS2_991666875_S.Services;

namespace AS2_991666875_S.ViewModels
{
    //View Model Class for Accessories
    internal class AccessoryViewModel
    {
        //Crud Operations for Accessories
        public CrudOperationsInDataSetAccessories crud = new CrudOperationsInDataSetAccessories();
        //List of Data for Accessories
        public ObservableCollection<Accessory> AccessoryList { get; set; } = new ObservableCollection<Accessory>();
        //Create Object views for Accessories
        private TextBox _tbAccessoryName;

        public TextBox TbAccessoryName
        {
            get { return _tbAccessoryName; }
            set { _tbAccessoryName = value; }
        }

        private TextBox _tbAccessoryPrice;

        public TextBox TbAccessoryPrice
        {
            get { return _tbAccessoryPrice; }
            set { _tbAccessoryPrice = value; }
        }

        private TextBox _tbUnitsInStock;

        public TextBox TbUnitsInStock
        {
            get { return _tbUnitsInStock; }
            set { _tbUnitsInStock = value; }
        }

        public int AccessoryId { get; set; }

        #region Load Northwind Database
        public void LoadData()
        {
            if (AccessoryList != null)
            {
                AccessoryList.Clear();
                crud.FillDataSet();
                crud.GetAccessories(AccessoryList);
            }
        }
        #endregion

        #region Constructor
        public AccessoryViewModel()
        {
            _tbAccessoryName = new TextBox();
            _tbAccessoryPrice = new TextBox();
            _tbUnitsInStock = new TextBox();
            LoadData();
        }
        #endregion

        #region Helper
        public void ClearUserInput()
        {
            TbAccessoryName.Text = string.Empty;
            TbAccessoryPrice.Text = string.Empty;
            TbUnitsInStock.Text = string.Empty;
        }

        public void Refresh_Page()
        {
            ClearUserInput();
            LoadData();
            AccessoryId = -1;
        }
        #endregion

        #region Initlalize User Input TextBox
        public void InitializeUserInput(TextBox textBoxName, TextBox textBoxPrice, TextBox textBoxUnits)
        {
            _tbAccessoryName = textBoxName;
            _tbAccessoryPrice = textBoxPrice;
            _tbUnitsInStock = textBoxUnits;
        }
        #endregion

        #region Helper
        public bool IsValidName(string name)
        {
            bool isValid;
            if (!string.IsNullOrEmpty(name)) isValid = true;
            else
                isValid = false;
            return isValid;
        }
        public bool IsValidPrice(string price)
        {
            bool isValid;
            if (float.TryParse(price, out float value)) isValid = true;
            else
                isValid = false;
            return isValid;
        }
        public bool IsValidUnit(string unit)
        {
            bool isValid;
            if (Int16.TryParse(unit, out short value)) isValid = true;
            else
                isValid = false;
            return isValid;
        }
        #endregion

        #region Commands for AccessoryUC Buttons
        public void SelectAccessory(int id)
        {
            DataRow row = crud.tblAccessories.Rows.Find(id);
            _tbAccessoryName.Text = (String)row[1];
            _tbAccessoryPrice.Text = row[2].ToString();
            _tbUnitsInStock.Text = row[3].ToString();

            AccessoryId = id;
        }

        public void AddAccessory()
        {
            bool isValid = true;
            if (!IsValidName(_tbAccessoryName.Text))
            {
                MessageBox.Show("Enter a valid name.");
                isValid = false;
            }
            if (!IsValidPrice(_tbAccessoryPrice.Text))
            {
                MessageBox.Show("Enter a valid price.");
                isValid = false;
            }
            if (!IsValidUnit(_tbUnitsInStock.Text))
            {
                MessageBox.Show("Enter a valid unit.");
                isValid = false;
            }

            if (isValid)
            {
                var accessoryName = _tbAccessoryName.Text;
                float accessoryPrice = float.Parse(_tbAccessoryPrice.Text);
                short unitInStock = Convert.ToInt16(_tbUnitsInStock.Text);
                crud.InsertAccessory(accessoryName, accessoryPrice, unitInStock);
                Refresh_Page();
            }
            else
            {
                Refresh_Page();
            }
            
        }

        public void EditAccessory()
        {
            bool isValid = true;
            if (!IsValidName(_tbAccessoryName.Text))
            {
                MessageBox.Show("Enter a valid name.");
                isValid = false;
            }
            if (!IsValidPrice(_tbAccessoryPrice.Text))
            {
                MessageBox.Show("Enter a valid price.");
                isValid = false;
            }
            if (!IsValidUnit(_tbUnitsInStock.Text))
            {
                MessageBox.Show("Enter a valid unit.");
                isValid = false;
            }

            if (isValid)
            {
                var accessoryName = _tbAccessoryName.Text;
                float accessoryPrice = float.Parse(_tbAccessoryPrice.Text);
                short unitInStock = Convert.ToInt16(_tbUnitsInStock.Text);
                crud.EditAccessory(AccessoryId, accessoryName, accessoryPrice, unitInStock);
                Refresh_Page();
            }
            else
            {
                Refresh_Page();
            }

        }

        public void DeleteAccessory(int id)
        {
            try
            {
                crud.DeleteAccessory(id);
                Refresh_Page();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting accessory: {ex.Message}");
            }
        }

        public void Refresh_Accessory()
        {
            Refresh_Page();
        }
        #endregion
    }
}
