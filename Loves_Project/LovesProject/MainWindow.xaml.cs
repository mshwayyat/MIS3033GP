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

namespace LovesProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnterBtnClick(object sender, RoutedEventArgs e)
        {
            double unleadedprice;
            string value = PriceTxtBox.Text;
            bool canconvert = double.TryParse(value, out unleadedprice);
            double plusprice;
            double superprice;

            if (canconvert)
            {
                plusprice = unleadedprice + 0.30;
                superprice = plusprice + 0.30;
                //Show the grade window 
                MessageBoxResult response;
                response = MessageBox.Show($" You entered {unleadedprice}", "Does this look correct?", MessageBoxButton.YesNo);

                if (response == MessageBoxResult.Yes)
                {

                }
                

                //Calculate the other prices and output in list box 
            }
            else
            {
                MessageBox.Show("Error, please enter a valid gas price.");
            }
        }
    }
}
