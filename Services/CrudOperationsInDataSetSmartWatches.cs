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
    //This code is the CRUD functionality for the Smart Watches
    public class CrudOperationsInDataSetSmartWatches
    {
        #region Private Members
        private SqlConnection conn { get; set; }
        private SqlCommandBuilder cmdBuilder { get; set; }
        private SqlDataAdapter adapter { get; set; }
        private DataSet ds { get; set; }
        public DataTable tblSmartWatches { get; set; }

        #endregion


        #region Connection String to MSSQLLOCALDB 
        public static string GetConnectionString(string connectionStringName)
        {
            var connStr = ConfigurationManager.AppSettings.Get(connectionStringName);
            return connStr;
        }
        #endregion

        #region Constructor
        public CrudOperationsInDataSetSmartWatches()
        {
            var cs = GetConnectionString("FitBitLocalDB");
            var query = "SELECT SmartWatchID,SmartWatchName,SmartWatchPrice,UnitsInStock FROM SmartWatches ORDER BY SmartWatchName ASC";
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
            adapter.Fill(ds, "SmartWatches");
            tblSmartWatches = ds.Tables["SmartWatches"];
            DataColumn[] pk = new DataColumn[1];
            pk[0] = tblSmartWatches.Columns["SmartWatchID"];
            tblSmartWatches.PrimaryKey = pk;
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
        public void GetSmartWatches(ObservableCollection<SmartWatch> SmartWatches)
        {
            try
            {
                if (tblSmartWatches != null)
                {
                    foreach (DataRow row in tblSmartWatches.Rows)
                    {
                        var _SmartWatch = new SmartWatch
                        {
                            SmartWatchId = (int)row["SmartWatchID"],
                            SmartWatchName = (string)row["SmartWatchName"],
                            SmartWatchPrice = float.Parse(row["SmartWatchPrice"].ToString()),
                            UnitInStock = (short)row["UnitsInStock"]
                        };
                        SmartWatches.Add(_SmartWatch);
                    }
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        public void InsertSmartWatch(string name, float price, short units)
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
                int maxID = tblSmartWatches.AsEnumerable().Max(row => row.Field<int>("SmartWatchID"));
                DataRow newRow = tblSmartWatches.NewRow();
                newRow["SmartWatchID"] = maxID + 1;
                newRow["SmartWatchName"] = name;
                newRow["SmartWatchPrice"] = price;
                newRow["UnitsInStock"] = units;

                tblSmartWatches.Rows.Add(newRow);
                adapter.InsertCommand = cmdBuilder.GetInsertCommand();
                adapter.Update(tblSmartWatches);
                FillDataSet();
                MessageBox.Show($"\nSmartWatch {name} - {price} - {units} has been added!");
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        public void EditSmartWatch(int id, string name, float price, short units)
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
                DataRow row = tblSmartWatches.Rows.Find(id);
                if (row != null)
                {
                    row["SmartWatchName"] = name;
                    row["SmartWatchPrice"] = price;
                    row["UnitsInStock"] = units;
                    adapter.UpdateCommand = cmdBuilder.GetUpdateCommand();
                    adapter.Update(tblSmartWatches);
                    FillDataSet();
                    MessageBox.Show($"\nSmartWatch {name} - {price} - {units} has been updated!");
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        public void DeleteSmartWatch(int id)
        {
            try
            {
                DataRow row = tblSmartWatches.Rows.Find(id);
                if (row != null)
                {
                    var name = row["SmartWatchName"];
                    var price = row["SmartWatchPrice"];
                    var units = row["UnitsInStock"];
                    row.Delete();
                    adapter.DeleteCommand = cmdBuilder.GetDeleteCommand();
                    adapter.Update(tblSmartWatches);
                    FillDataSet();
                    MessageBox.Show($"\nSmartWatch {name} - {price} - {units} has been deleted!");
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
