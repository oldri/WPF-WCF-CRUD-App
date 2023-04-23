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
using AS2_991666875_S.UserControls;

namespace AS2_991666875_S.ViewModels
{
    //View Model Class for Employees
    internal class EmployeeViewModel
    {
        //Crud Operations for Employees
        public NorthwindService.WCFNorthwindServiceClient client = new NorthwindService.WCFNorthwindServiceClient();
       
        //List of Data for Employee
        public ObservableCollection<Employee> EmployeeList { get; set; } = new ObservableCollection<Employee>();
        //Create Object views for Employees
        private TextBox _tbEmployeeFirstName;

        public TextBox TbEmployeeFirstName
        {
            get { return _tbEmployeeFirstName; }
            set { _tbEmployeeFirstName = value; }
        }

        private TextBox _tbEmployeeLastName;

        public TextBox TbEmployeeLastName
        {
            get { return _tbEmployeeLastName; }
            set { _tbEmployeeLastName = value; }
        }

        private TextBox _tbEmployeeTitle;

        public TextBox TbEmployeeTitle
        {
            get { return _tbEmployeeTitle; }
            set { _tbEmployeeTitle = value; }
        }

        private TextBox _tbEmployeeCity;

        public TextBox TbEmployeeCity
        {
            get { return _tbEmployeeCity; }
            set { _tbEmployeeCity = value; }
        }

        public int EmployeeId { get; set; }

        #region Load Northwind Database
        public void GetEmployees()
        {
            {
                var employees = client.GetEmployees();
                foreach (var employee in employees)
                {
                    var _Employee = new Employee
                    {
                        EmployeeFirstName = employee.FirstName,
                        EmployeeLastName = employee.LastName,
                        EmployeeTitle = employee.Title,
                        EmployeeCity = employee.City,
                        EmployeeId = employee.EmployeeID
                    };
                    EmployeeList.Add(_Employee);
                }
            }
        }
        public void LoadData()
        {
            if (EmployeeList != null)
            {
                EmployeeList.Clear();
                GetEmployees();
            }
        }
        #endregion

        #region Constructor
        public EmployeeViewModel()
        {
            _tbEmployeeFirstName = new TextBox();
            _tbEmployeeLastName = new TextBox();
            _tbEmployeeTitle = new TextBox();
            _tbEmployeeCity = new TextBox();
            LoadData();
        }
        #endregion

        #region Helper
        public void ClearUserInput()
        {
            TbEmployeeFirstName.Text = string.Empty;
            TbEmployeeLastName.Text = string.Empty;
            TbEmployeeTitle.Text = string.Empty;
            TbEmployeeCity.Text = string.Empty;
        }

        public void Refresh_Page()
        {
            ClearUserInput();
            LoadData();
            EmployeeId = -1;
        }
        #endregion

        #region Initlalize User Input TextBox
        public void InitializeUserInput(TextBox textBoxFirstName, TextBox textBoxLastName, TextBox textBoxTitle, TextBox textBoxCity)
        {
            _tbEmployeeFirstName = textBoxFirstName;
            _tbEmployeeLastName = textBoxLastName;
            _tbEmployeeTitle = textBoxTitle;
            _tbEmployeeCity = textBoxCity;
        }
        #endregion

        #region Helper
        public bool IsValidStr(string str)
        {
            bool isValid;
            if (!string.IsNullOrEmpty(str)) isValid = true;
            else
                isValid = false;
            return isValid;
        }
        #endregion

        #region Commands for EmployeeUC Buttons
       
        public void SelectEmployee(int id)
        {
            var employee = client.ReadEmployee(id);
            _tbEmployeeFirstName.Text = employee.FirstName;
            _tbEmployeeLastName.Text = employee.LastName;
            _tbEmployeeTitle.Text = employee.Title;
            _tbEmployeeCity.Text = employee.City;
            EmployeeId = id;
        }

        public void AddEmployee()
        {
            bool isValid = true;
            if (!IsValidStr(_tbEmployeeFirstName.Text) || !IsValidStr(_tbEmployeeLastName.Text) || !IsValidStr(_tbEmployeeTitle.Text) || !IsValidStr(_tbEmployeeCity.Text))
            {
                MessageBox.Show("Enter a valid field.");
                isValid = false;
            }

            if (isValid)
            {
                var employeeData = new NorthwindService.EmployeeData
                {
                    FirstName = _tbEmployeeFirstName.Text,
                    LastName = _tbEmployeeLastName.Text,
                    Title = _tbEmployeeTitle.Text,
                    City = _tbEmployeeCity.Text,
                };
                client.CreateEmployee(employeeData);
                Refresh_Page();
            }
            else
            {
                Refresh_Page();
            }
        }

        public void EditEmployee()
        {
            bool isValid = true;
            if (!IsValidStr(_tbEmployeeFirstName.Text) || !IsValidStr(_tbEmployeeLastName.Text) || !IsValidStr(_tbEmployeeTitle.Text) || !IsValidStr(_tbEmployeeCity.Text))
            {
                MessageBox.Show("Enter a valid field.");
                isValid = false;
            }

            if (isValid)
            {
                var employeeData = new NorthwindService.EmployeeData
                {
                    FirstName = _tbEmployeeFirstName.Text,
                    LastName = _tbEmployeeLastName.Text,
                    Title = _tbEmployeeTitle.Text,
                    City = _tbEmployeeCity.Text,
                };
                
                client.UpdateEmployee(EmployeeId, employeeData);
                Refresh_Page();
            }
            else
            {
                Refresh_Page();
            }
        }

        public void DeleteEmployee(int id)
        {
            try
            {
                client.DeleteEmployee(id);
                Refresh_Page();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Employee: {ex.Message}");
            }
        }

        public void Refresh_Employee()
        {
            Refresh_Page();
        }
        #endregion
    }
}
