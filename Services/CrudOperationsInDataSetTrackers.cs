using AS2_991666875_S.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AS2_991666875_S.Services
{
    //This code is the CRUD functionality for the Trackers
    public class CrudOperationsInDataSetTrackers
    {
        #region Private Members
        private SqlConnection conn { get; set; }
        private SqlCommandBuilder cmdBuilder { get; set; }
        private SqlDataAdapter adapter { get; set; }
        private DataSet ds { get; set; }
        public DataTable tblTrackers { get; set; }

        #endregion


        #region Connection String to MSSQLLOCALDB 
        public static string GetConnectionString(string connectionStringName)
        {
            var connStr = ConfigurationManager.AppSettings.Get(connectionStringName);
            return connStr;
        }
        #endregion

        #region Constructor
        public CrudOperationsInDataSetTrackers()
        {
            var cs = GetConnectionString("FitBitLocalDB");
            var query = "SELECT TrackerID,TrackerName,TrackerPrice,UnitsInStock FROM Trackers ORDER BY TrackerName ASC";
            conn = new SqlConnection(cs);
            adapter = new SqlDataAdapter(query, conn);
            cmdBuilder = new SqlCommandBuilder(adapter);
            FillDataSet();
        }
        #endregion

        #region Helper
        public void FillDataSet()
        {
            ds = new DataSet();
            adapter.Fill(ds, "Trackers");
            tblTrackers = ds.Tables["Trackers"];
            DataColumn[] pk = new DataColumn[1];
            pk[0] = tblTrackers.Columns["TrackerID"];
            tblTrackers.PrimaryKey = pk;
        }

        public bool IsValidName(string name)
        {
            bool isValid = false;
            if (!string.IsNullOrEmpty(name) && name.Trim().Length > 0) isValid = true;
            return isValid;
        }
        public bool IsValidPrice(float price)
        {
            bool isValid = true;
            if (price < 0) isValid = false;
            return isValid;
        }
        public bool IsValidUnit(short unit)
        {
            bool isValid = true;
            if (unit < 0) isValid = false;
            return isValid;
        }
        #endregion

        #region Commands Insert, Edit, Delete
        public void GetTrackers(ObservableCollection<Tracker> Trackers)
        {
            try
            {
                if (tblTrackers != null)
                {
                    foreach (DataRow row in tblTrackers.Rows)
                    {
                        var _Tracker = new Tracker
                        {
                            TrackerId = (int)row["TrackerID"],
                            TrackerName = (string)row["TrackerName"],
                            TrackerPrice = float.Parse(row["TrackerPrice"].ToString()),
                            UnitInStock = (short)row["UnitsInStock"]
                        };
                        Trackers.Add(_Tracker);
                    }
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        public void InsertTracker(string name, float price, short units)
        {
            if (!IsValidName(name))
            {
                MessageBox.Show("Enter a valid name.");
                return;
            }
            if (!IsValidPrice(price))
            {
                MessageBox.Show("Enter a valid price.");
                return;
            }
            if (!IsValidUnit(units))
            {
                MessageBox.Show("Enter a valid unit.");
                return;
            }
            try
            {
                int maxID = tblTrackers.AsEnumerable().Max(row => row.Field<int>("TrackerID"));
                DataRow newRow = tblTrackers.NewRow();
                newRow["TrackerID"] = maxID + 1;
                newRow["TrackerName"] = name;
                newRow["TrackerPrice"] = price;
                newRow["UnitsInStock"] = units;

                tblTrackers.Rows.Add(newRow);
                adapter.InsertCommand = cmdBuilder.GetInsertCommand();
                adapter.Update(tblTrackers);
                FillDataSet();
                MessageBox.Show($"\nTracker {name} - {price} - {units} has been added!");
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        public void EditTracker(int id, string name, float price, short units)
        {
            if (!IsValidName(name))
            {
                MessageBox.Show("Enter a valid name.");
                return;
            }
            if (!IsValidPrice(price))
            {
                MessageBox.Show("Enter a valid price.");
                return;
            }
            if (!IsValidUnit(units))
            {
                MessageBox.Show("Enter a valid unit.");
                return;
            }
            try
            {
                DataRow row = tblTrackers.Rows.Find(id);
                if (row != null)
                {
                    row["TrackerName"] = name;
                    row["TrackerPrice"] = price;
                    row["UnitsInStock"] = units;
                    adapter.UpdateCommand = cmdBuilder.GetUpdateCommand();
                    adapter.Update(tblTrackers);
                    FillDataSet();
                    MessageBox.Show($"\nTracker {name} - {price} - {units} has been updated!");
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        public void DeleteTracker(int id)
        {
            try
            {
                DataRow row = tblTrackers.Rows.Find(id);
                if (row != null)
                {
                    var name = row["TrackerName"];
                    var price = row["TrackerPrice"];
                    var units = row["UnitsInStock"];
                    row.Delete();
                    adapter.DeleteCommand = cmdBuilder.GetDeleteCommand();
                    adapter.Update(tblTrackers);
                    FillDataSet();
                    MessageBox.Show($"\nTracker {name} - {price} - {units} has been deleted!");
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        #endregion
    }
}
