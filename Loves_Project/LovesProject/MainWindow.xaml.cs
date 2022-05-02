using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        List<GradeClass> grade = new List<GradeClass>();
        List<StorePriceClass> storePrice = new List<StorePriceClass>();
        List<StoreClass> store = new List<StoreClass>();

        public MainWindow()
        {

            InitializeComponent();
            string connectionString = "Data Source=tcp:s24.winhost.com;Initial Catalog=DB_128040_loves;User ID=user;Password=Khalil3033;Integrated Security=False;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("select * from Grade", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["GradeID"].ToString() + " " + reader["Name"].ToString());
                GradeClass temp = new GradeClass(); 
                temp.GradeName = reader["Name"].ToString();
                temp.GradeID= reader["GradeID"].ToString();
                grade.Add(temp);
               
            }
            reader.Close();

            SqlCommand command2 = new SqlCommand("select * from Store", connection);
            SqlDataReader reader2 = command2.ExecuteReader();

            while (reader2.Read())
            {
               Console.WriteLine(reader2["StoreID"].ToString() + " " + reader2["Threshold"].ToString());
                StoreClass temp = new StoreClass();
                temp.StoreID= Convert.ToInt32(reader2["StoreID"]);
                temp.Threshold = Convert.ToInt32(reader2["Threshold"]);
                store.Add(temp);
            }
            reader2.Close();

            SqlCommand command3 = new SqlCommand("select * from StorePrice order by Timestamp desc", connection);
            SqlDataReader reader3 = command3.ExecuteReader();

            while (reader3.Read())
            {
                Console.WriteLine(reader3["StoreNumber"].ToString() + " " + reader3["Grade"].ToString() + " " + reader3["PreviousPrice"] + " " + reader3["Timestamp"]);
                StorePriceClass temp= new StorePriceClass();
                temp.StoreNumber = Convert.ToInt32(reader3["StoreNumber"]);
                temp.StoreGrade = reader3["Grade"].ToString();
                temp.PreviousPrice = Convert.ToDouble(reader3["PreviousPrice"]);
                temp.TimeStamp = reader3["Timestamp"].ToString();
                storePrice.Add(temp);
            }
            reader3.Close();
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
                   
                   //DB_128040_lovesEntities mydb = new DB_128040_lovesEntities();
                    //mydb.StorePrices.Find(plusprice);
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
