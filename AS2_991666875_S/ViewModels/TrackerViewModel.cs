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

namespace AS2_991666875_S.ViewModels
{
    //View Model Class for Trackers
    internal class TrackerViewModel
    {
        //Crud Operations for Trackers
        public CrudOperationsInDataSetTrackers crud = new CrudOperationsInDataSetTrackers();
        //List of Data for Tracker
        public ObservableCollection<Tracker> TrackerList { get; set; } = new ObservableCollection<Tracker>();
        //Create Object views for Trackers
        private TextBox _tbTrackerName;

        public TextBox TbTrackerName
        {
            get { return _tbTrackerName; }
            set { _tbTrackerName = value; }
        }

        private TextBox _tbTrackerPrice;

        public TextBox TbTrackerPrice
        {
            get { return _tbTrackerPrice; }
            set { _tbTrackerPrice = value; }
        }

        private TextBox _tbUnitsInStock;

        public TextBox TbUnitsInStock
        {
            get { return _tbUnitsInStock; }
            set { _tbUnitsInStock = value; }
        }

        public int TrackerId { get; set; }

        #region Load Northwind Database
        public void LoadData()
        {
            if (TrackerList != null)
            {
                TrackerList.Clear();
                crud.FillDataSet();
                crud.GetTrackers(TrackerList);
            }
        }
        #endregion

        #region Constructor
        public TrackerViewModel()
        {
            _tbTrackerName = new TextBox();
            _tbTrackerPrice = new TextBox();
            _tbUnitsInStock = new TextBox();
            LoadData();
        }
        #endregion

        #region Helper
        public void ClearUserInput()
        {
            TbTrackerName.Text = string.Empty;
            TbTrackerPrice.Text = string.Empty;
            TbUnitsInStock.Text = string.Empty;
        }

        public void Refresh_Page()
        {
            ClearUserInput();
            LoadData();
            TrackerId = -1;
        }
        #endregion

        #region Initlalize User Input TextBox
        public void InitializeUserInput(TextBox textBoxName, TextBox textBoxPrice, TextBox textBoxUnits)
        {
            _tbTrackerName = textBoxName;
            _tbTrackerPrice = textBoxPrice;
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

        #region Commands for TrackerUC Buttons
        public void SelectTracker(int id)
        {
            DataRow row = crud.tblTrackers.Rows.Find(id);
            _tbTrackerName.Text = (String)row[1];
            _tbTrackerPrice.Text = row[2].ToString();
            _tbUnitsInStock.Text = row[3].ToString();

            TrackerId = id;
        }

        public void AddTracker()
        {
            bool isValid = true;
            if (!IsValidName(_tbTrackerName.Text))
            {
                MessageBox.Show("Enter a valid name.");
                isValid = false;
            }
            if (!IsValidPrice(_tbTrackerPrice.Text))
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
                var TrackerName = _tbTrackerName.Text;
                float TrackerPrice = float.Parse(_tbTrackerPrice.Text);
                short unitInStock = Convert.ToInt16(_tbUnitsInStock.Text);
                crud.InsertTracker(TrackerName, TrackerPrice, unitInStock);
                Refresh_Page();
            }
            else
            {
                Refresh_Page();
            }
            
        }

        public void EditTracker()
        {
            bool isValid = true;
            if (!IsValidName(_tbTrackerName.Text))
            {
                MessageBox.Show("Enter a valid name.");
                isValid = false;
            }
            if (!IsValidPrice(_tbTrackerPrice.Text))
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
                var TrackerName = _tbTrackerName.Text;
                float TrackerPrice = float.Parse(_tbTrackerPrice.Text);
                short unitInStock = Convert.ToInt16(_tbUnitsInStock.Text);
                crud.EditTracker(TrackerId, TrackerName, TrackerPrice, unitInStock);
                Refresh_Page();
            }
            else
            {
                Refresh_Page();
            }
            

        }

        public void DeleteTracker(int id)
        {
            try
            {
                crud.DeleteTracker(id);
                Refresh_Page();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Tracker: {ex.Message}");
            }
        }

        public void Refresh_Tracker()
        {
            Refresh_Page();
        }
        #endregion
    }
}
