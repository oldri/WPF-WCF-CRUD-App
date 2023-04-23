using AS2_991666875_S.Models;
using AS2_991666875_S.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;
using System.Xml.Linq;

namespace AS2_991666875_S.ViewModels
{
    //View Model Class for Smart Watches
    internal class SmartWatchViewModel
    {
        //Crud Operations for Smart Watches
        public CrudOperationsInDataSetSmartWatches crud = new CrudOperationsInDataSetSmartWatches();
        //List of Data for Smart Watches
        public ObservableCollection<SmartWatch> SmartWatchList { get; set; } = new ObservableCollection<SmartWatch>();
        //Create Object views for Smart Watches
        private TextBox _tbSmartWatchName;

        public TextBox TbSmartWatchName
        {
            get { return _tbSmartWatchName; }
            set { _tbSmartWatchName = value; }
        }

        private TextBox _tbSmartWatchPrice;

        public TextBox TbSmartWatchPrice
        {
            get { return _tbSmartWatchPrice; }
            set { _tbSmartWatchPrice = value; }
        }

        private TextBox _tbUnitsInStock;

        public TextBox TbUnitsInStock
        {
            get { return _tbUnitsInStock; }
            set { _tbUnitsInStock = value; }
        }

        public int SmartWatchId { get; set; }

        #region Load Northwind Database
        public void LoadData()
        {
            if (SmartWatchList != null)
            {
                SmartWatchList.Clear();
                crud.FillDataSet();
                crud.GetSmartWatches(SmartWatchList);
            }
        }
        #endregion

        #region Constructor
        public SmartWatchViewModel()
        {
            _tbSmartWatchName = new TextBox();
            _tbSmartWatchPrice = new TextBox();
            _tbUnitsInStock = new TextBox();
            LoadData();
        }
        #endregion

        #region Helper
        public void ClearUserInput()
        {
            TbSmartWatchName.Text = string.Empty;
            TbSmartWatchPrice.Text = string.Empty;
            TbUnitsInStock.Text = string.Empty;
        }

        public void Refresh_Page()
        {
            ClearUserInput();
            LoadData();
            SmartWatchId = -1;
        }
        #endregion

        #region Initlalize User Input TextBox
        public void InitializeUserInput(TextBox textBoxName, TextBox textBoxPrice, TextBox textBoxUnits)
        {
            _tbSmartWatchName = textBoxName;
            _tbSmartWatchPrice = textBoxPrice;
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


        #region Commands for SmartWatchUC Buttons
        public void SelectSmartWatch(int id)
        {
            DataRow row = crud.tblSmartWatches.Rows.Find(id);
            _tbSmartWatchName.Text = (String)row[1];
            _tbSmartWatchPrice.Text = row[2].ToString();
            _tbUnitsInStock.Text = row[3].ToString();

            SmartWatchId = id;
        }

        public void AddSmartWatch()
        {
            bool isValid = true;
            if (!IsValidName(_tbSmartWatchName.Text))
            {
                MessageBox.Show("Enter a valid name.");
                isValid = false;
            }
            if (!IsValidPrice(_tbSmartWatchPrice.Text))
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
                var SmartWatchName = _tbSmartWatchName.Text;
                float SmartWatchPrice = float.Parse(_tbSmartWatchPrice.Text);
                short unitInStock = Convert.ToInt16(_tbUnitsInStock.Text);
                crud.InsertSmartWatch(SmartWatchName, SmartWatchPrice, unitInStock);
                Refresh_Page();
            }
            else
            {
                Refresh_Page();
            }
}

        public void EditSmartWatch()
        {
            bool isValid = true;
            if (!IsValidName(_tbSmartWatchName.Text))
            {
                MessageBox.Show("Enter a valid name.");
                isValid = false;
            }
            if (!IsValidPrice(_tbSmartWatchPrice.Text))
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
                var SmartWatchName = _tbSmartWatchName.Text;
                float SmartWatchPrice = float.Parse(_tbSmartWatchPrice.Text);
                short unitInStock = Convert.ToInt16(_tbUnitsInStock.Text);
                crud.EditSmartWatch(SmartWatchId, SmartWatchName, SmartWatchPrice, unitInStock);
                Refresh_Page();
            }
            else
            {
                Refresh_Page();
            }
        }

        public void DeleteSmartWatch(int id)
        {
            try
            {
                crud.DeleteSmartWatch(id);
                Refresh_Page();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting SmartWatch: {ex.Message}");
            }
        }

        public void Refresh_SmartWatch()
        {
            Refresh_Page();
        }
        #endregion
    }
}
