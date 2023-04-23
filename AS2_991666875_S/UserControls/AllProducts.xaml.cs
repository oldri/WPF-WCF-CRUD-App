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
    /// This is the User Control code for All Products
    public partial class AllProducts : UserControl
    {
        // Constructor for initializing the object
        public AllProducts()
        {
            InitializeComponent();
        }

        //This code only serves as a empty click
        public void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
