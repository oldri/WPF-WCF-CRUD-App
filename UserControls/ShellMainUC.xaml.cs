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
    /// This is the main User Control code for the Shell - It is also the navigation for the rest of the user control screens
    public partial class ShellMainUC : UserControl
    {
        // Constructor for initializing the object
        public ShellMainUC()
        {
            InitializeComponent();
        }

        #region Main Window Event Handlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SmartWatchesBtn.IsChecked = true;
            FramePages.Source = new Uri("/UserControls/SmartWatchUC.xaml", UriKind.Relative);
        }

        private void CloseBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MaximizeBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        #endregion

        #region Page Navigation Buttons
        private void SmartWatchesBtn_Click(object sender, RoutedEventArgs e)
        {
            FramePages.Source = new Uri("/UserControls/SmartWatchUC.xaml", UriKind.Relative);
        }

        private void TrackersBtn_Click(object sender, RoutedEventArgs e)
        {
            FramePages.Source = new Uri("/UserControls/TrackerUC.xaml", UriKind.Relative);
        }

        private void AccessoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            FramePages.Source = new Uri("/UserControls/AccessoryUC.xaml", UriKind.Relative);
        }

        private void AllProductsBtn_Click(object sender, RoutedEventArgs e)
        {
            FramePages.Source = new Uri("/UserControls/AllProducts.xaml", UriKind.Relative);
        }

        private void EmployeesBtn_Click(object sender, RoutedEventArgs e)
        {
            FramePages.Source = new Uri("/UserControls/EmployeeUC.xaml", UriKind.Relative);
        }


        #endregion
    }
}
