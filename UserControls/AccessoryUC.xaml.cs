using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml.Linq;
using AS2_991666875_S.Models;
using AS2_991666875_S.ViewModels;

namespace AS2_991666875_S.UserControls
{
    /// This is the User Control code for Accessories
    public partial class AccessoryUC : UserControl
    {

        /// Create the Accessory View Model
        AccessoryViewModel ViewModel = new AccessoryViewModel();
        // Constructor for initializing the object 
        public AccessoryUC()
        {
            InitializeComponent();

            ViewModel.InitializeUserInput(tbAccessoryName, tbAccessoryPrice ,tbUnitsInStock);
            this.DataContext = ViewModel;
        }

        // All Click Events below are assigned in the AccessoryUC.xaml attributes
        private void AccessoryItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Accessory selectedAccessory = (Accessory)button.DataContext;
            int accessoryId = selectedAccessory.AccessoryId;
            ViewModel.AccessoryId = accessoryId;
            ViewModel.SelectAccessory(accessoryId);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.EditAccessory();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddAccessory();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.AccessoryId >= 0)
            {
                ViewModel.DeleteAccessory(ViewModel.AccessoryId);
                ViewModel.Refresh_Page();
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadData();
        }
    }
}
