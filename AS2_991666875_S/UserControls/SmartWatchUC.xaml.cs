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
    /// This is the User Control code for Smart Watches
    public partial class SmartWatchUC : UserControl
    {

        /// Create the Smart Watch View Model
        SmartWatchViewModel ViewModel = new SmartWatchViewModel();
        // Constructor for initializing the object
        public SmartWatchUC()
        {
            InitializeComponent();

            ViewModel.InitializeUserInput(tbSmartWatchName, tbSmartWatchPrice, tbUnitsInStock);
            this.DataContext = ViewModel;
        }

        // All Click Events below are assigned in the SmartWatchUC.xaml attributes
        private void SmartWatchItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SmartWatch selectedSmartWatch = (SmartWatch)button.DataContext;
            int SmartWatchId = selectedSmartWatch.SmartWatchId;
            ViewModel.SmartWatchId = SmartWatchId;
            ViewModel.SelectSmartWatch(SmartWatchId);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.EditSmartWatch();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddSmartWatch();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SmartWatchId >= 0)
            {
                ViewModel.DeleteSmartWatch(ViewModel.SmartWatchId);
                ViewModel.Refresh_Page();
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadData();
        }
    }
}
