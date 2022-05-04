using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LovesProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DB_128040_lovesEntities DB = new DB_128040_lovesEntities();
        public MainWindow()
        {
            InitializeComponent();

            foreach (var item in DB.Stores)
            {
                StoreIDCB.Items.Add(item.StoreId);
                Store st = DB.Stores.Find(StoreIDCB.SelectedItem);
            }
            StoreIDCB.SelectedIndex = 0;


        }

        private void EnterBtnClick(object sender, RoutedEventArgs e)
        {
            //StorePrice sp = DB.StorePrices.Where(s => s.StoreNumber.ToString() == st.ToString()).OrderByDescending(s => s.TimeStamp).Take(1).FirstOrDefault();
            //int storeId = sp.Store.StoreId;

            decimal unleadedprice;
            string value = PriceTxtBox.Text;
            bool canconvert = Decimal.TryParse(value, out unleadedprice);
            if (canconvert == false)
            {
                MessageBox.Show("Please enter a valid amount");
                return;
            }
            else
            {
                MessageBoxResult response;
                response = MessageBox.Show($" You entered {PriceTxtBox.Text}", "Does this look correct?", MessageBoxButton.YesNo);
            }
            Store st = DB.Stores.Find(StoreIDCB.SelectedItem);
            Console.WriteLine(st.Threshold);

            //StorePrice currentStore = DB.StorePrices.Find(st.StoreId);
            StorePrice currentStore = DB.StorePrices.Where(s => s.StoreNumber.ToString() == st.StoreId.ToString() && (s.Grade == 1)).OrderByDescending(s => s.TimeStamp).Take(1).FirstOrDefault();

            StorePrice new1 = new StorePrice();
            new1.PreviousPrice = currentStore.NewPrice;
            new1.NewPrice = unleadedprice;
            new1.Grade = 1;
            new1.PriceDifference = currentStore.NewPrice - unleadedprice;
            new1.StoreNumber = currentStore.StoreNumber;
            new1.Store = currentStore.Store;
            new1.TimeStamp = DateTime.Now;

            StorePrice c2 = DB.StorePrices.Where(s => s.StoreNumber.ToString() == st.StoreId.ToString() && (s.Grade == 2)).OrderByDescending(s => s.TimeStamp).Take(1).FirstOrDefault();

            if (c2 != null)
                currentStore = c2;

            StorePrice new2 = new StorePrice();
            new2.PreviousPrice = currentStore.NewPrice;
            new2.NewPrice = new1.NewPrice + st.Threshold;
            new2.Grade = 2;
            new2.PriceDifference = currentStore.NewPrice - unleadedprice;
            new2.StoreNumber = currentStore.StoreNumber;
            new2.Store = currentStore.Store;
            new2.TimeStamp = DateTime.Now;

            StorePrice c3 = DB.StorePrices.Where(s => s.StoreNumber.ToString() == st.StoreId.ToString() && (s.Grade == 3)).OrderByDescending(s => s.TimeStamp).Take(1).FirstOrDefault();

            if (c3 != null)
                currentStore = c3;

            StorePrice new3 = new StorePrice();
            new3.PreviousPrice = currentStore.NewPrice;
            new3.NewPrice = new2.NewPrice + st.Threshold;
            new3.Grade = 3;
            new3.PriceDifference = currentStore.NewPrice - unleadedprice;
            new3.StoreNumber = currentStore.StoreNumber;
            new3.Store = currentStore.Store;
            new3.TimeStamp = DateTime.Now;

            ListBoxOutput.Items.Add($"Store # {StoreIDCB.SelectedItem}: \n* Grade: {new1.Grade} - Unleaded: New Price: {new1.NewPrice.ToString("C")}, Previous Price: {new1.PreviousPrice.ToString("C")}, Price Difference: {new1.PriceDifference.ToString("C")} \n* Grade: {new2.Grade} - Plus: New Price: {new2.NewPrice.ToString("C")}, Previous Price: {new2.PreviousPrice.ToString("C")}, Price Difference: {new2.PriceDifference.ToString("C")} \n* Grade: {new3.Grade} - Super: New Price: {new3.NewPrice.ToString("C")}, Previous Price: {new3.PreviousPrice.ToString("C")}, Price Difference: {new3.PriceDifference.ToString("C")} \nTimeStamp: {new3.TimeStamp}");
            DB.StorePrices.Add(new1);
            DB.StorePrices.Add(new2);
            DB.StorePrices.Add(new3);
            DB.SaveChanges();

        }

        private void ExportBtnClick(object sender, RoutedEventArgs e)
        {
            StreamWriter outputcsv = new StreamWriter("MyFile.csv");

            foreach (var item in ListBoxOutput.Items)
            {
                outputcsv.WriteLine(item.ToString());
            }

            outputcsv.Close();

            MessageBox.Show("Upload to CSV complete!");

        }

       
    }
}
