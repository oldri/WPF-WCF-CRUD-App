using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using AS2_991666875_S.Models;

namespace AS2_991666875_S.Services
{
    //This code is the CRUD functionality for the Accessories
    public class CrudOperationsInDataSetAccessories
    {
        #region Private Members
        private SqlConnection conn { get; set; }
        private SqlCommandBuilder cmdBuilder { get; set; }
        private SqlDataAdapter adapter { get; set; }
        private DataSet ds { get; set; }
        public DataTable tblAccessories { get; set; }

        #endregion


        #region Connection String to MSSQLLOCALDB 
        public static string GetConnectionString(string connectionStringName)
        {
            var connStr = ConfigurationManager.AppSettings.Get(connectionStringName);
            return connStr;
        }
        #endregion

        #region Constructor
        public CrudOperationsInDataSetAccessories()
        {
            var cs = GetConnectionString("FitBitLocalDB");
            var query = "SELECT AccessoryID, AccessoryName, AccessoryPrice, UnitsInStock \r\nFROM Accessories ORDER BY AccessoryName ASC";
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
            adapter.Fill(ds, "Accessories");
            tblAccessories = ds.Tables["Accessories"];
            DataColumn[] pk = new DataColumn[1];
            pk[0] = tblAccessories.Columns["AccessoryID"];
            tblAccessories.PrimaryKey = pk;
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
        public void GetAccessories(ObservableCollection<Accessory> accessories)
        {
            try
            {
                if (tblAccessories != null)
                {
                    foreach (DataRow row in tblAccessories.Rows)
                    {
                        var _accessory = new Accessory
                        {
                            AccessoryId = (int)row["AccessoryID"],
                            AccessoryName = (string)row["AccessoryName"],
                            AccessoryPrice = float.Parse(row["AccessoryPrice"].ToString()),
                            UnitInStock = (short)row["UnitsInStock"]
                        };
                        accessories.Add(_accessory);
                    }
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        public void InsertAccessory(string name, float price, short units)
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
                int maxID = tblAccessories.AsEnumerable().Max(row => row.Field<int>("AccessoryID"));
                DataRow newRow = tblAccessories.NewRow();
                newRow["AccessoryID"] = maxID + 1;
                newRow["AccessoryName"] = name;
                newRow["AccessoryPrice"] = price;
                newRow["UnitsInStock"] = units;

                tblAccessories.Rows.Add(newRow);
                adapter.InsertCommand = cmdBuilder.GetInsertCommand();
                adapter.Update(tblAccessories);
                FillDataSet();
                MessageBox.Show($"\nAccessory {name} - {price} - {units} has been added!");
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        public void EditAccessory(int id, string name, float price, short units)
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
                DataRow row = tblAccessories.Rows.Find(id);
                if (row != null)
                {
                    row["AccessoryName"] = name;
                    row["AccessoryPrice"] = price;
                    row["UnitsInStock"] = units;
                    adapter.UpdateCommand = cmdBuilder.GetUpdateCommand();
                    adapter.Update(tblAccessories);
                    FillDataSet();
                    MessageBox.Show($"\nAccessory {name} - {price} - {units} has been updated!");
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        public void DeleteAccessory(int id)
        {
            try
            {
                DataRow row = tblAccessories.Rows.Find(id);
                if (row != null)
                {
                    var name = row["AccessoryName"];
                    var price = row["AccessoryPrice"];
                    var units = row["UnitsInStock"];
                    row.Delete();
                    adapter.DeleteCommand = cmdBuilder.GetDeleteCommand();
                    adapter.Update(tblAccessories);
                    FillDataSet();
                    MessageBox.Show($"\nAccessory {name} - {price} - {units} has been deleted!");
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