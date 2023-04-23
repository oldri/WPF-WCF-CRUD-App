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
    /// This is the User Control code for Trackers
    public partial class TrackerUC : UserControl
    {
        /// Create the Tracker View Model
        TrackerViewModel ViewModel = new TrackerViewModel();
        // Constructor for initializing the object
        public TrackerUC()
        {
            InitializeComponent();

            ViewModel.InitializeUserInput(tbTrackerName, tbTrackerPrice, tbUnitsInStock);
            this.DataContext = ViewModel;
        }

        // All Click Events below are assigned in the TrackerUC.xaml attributes
        private void TrackerItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Tracker selectedTracker = (Tracker)button.DataContext;
            int TrackerId = selectedTracker.TrackerId;
            ViewModel.TrackerId = TrackerId;
            ViewModel.SelectTracker(TrackerId);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.EditTracker();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddTracker();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.TrackerId >= 0)
            {
                ViewModel.DeleteTracker(ViewModel.TrackerId);
                ViewModel.Refresh_Page();
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadData();
        }
    }
}
