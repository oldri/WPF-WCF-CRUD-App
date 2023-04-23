using AS2_991666875_S.Models;
using AS2_991666875_S.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AS2_991666875_S.UserControls
{
    /// <summary>
    /// Interaction logic for EmployeeUC.xaml
    /// </summary>
    /// This is the User Control code for Employees
    public partial class EmployeeUC : UserControl
    {
        /// Create the Employee View Model
        EmployeeViewModel ViewModel = new EmployeeViewModel();
        // Constructor for initializing the object
        public EmployeeUC()
        {
            InitializeComponent();

            ViewModel.InitializeUserInput(tbEmployeeFirstName, tbEmployeeLastName, tbEmployeeTitle, tbEmployeeCity);
            this.DataContext = ViewModel;
        }

        // All Click Events below are assigned in the EmployeeUC.xaml attributes
        private void EmployeeItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Employee selectedEmployee = (Employee)button.DataContext;
            int EmployeeId = selectedEmployee.EmployeeId;
            ViewModel.EmployeeId = EmployeeId;
            ViewModel.SelectEmployee(EmployeeId);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.EditEmployee();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddEmployee();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.EmployeeId >= 0)
            {
                ViewModel.DeleteEmployee(ViewModel.EmployeeId);
                ViewModel.Refresh_Page();
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadData();
        }
    }
}
