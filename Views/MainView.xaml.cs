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
using System.Windows.Shapes;

namespace AS2_991666875_S.Views
{
    /// Oldri Kecaj - 991666875 - April 7th, 2023 - .NET C# - Joseph Jean Charles - Computer Programming
    public partial class MainView : Window
    {
        #region Constructor
        public MainView()
        {
            InitializeComponent();
        }

        #endregion
        //Main Point of Entry
        private void AppWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
