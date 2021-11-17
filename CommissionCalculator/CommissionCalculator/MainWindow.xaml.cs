using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CommissionCalculator
{
    public partial class MainWindow : Window
    {
        ObservableCollection<CommissionItem> items = new ObservableCollection<CommissionItem>();

        public float TotalSold { get; private set; }
        public float TotalCommission { get; private set; }
        public float TotalRP { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            CommissionList.ItemsSource = items;

            CalculateAll();
            Update();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                float price = float.Parse(ItemPrice.Text);
                CommissionItem newItem = new CommissionItem(price, (bool)OodBox.IsChecked, (bool)RpBox.IsChecked, (bool)ReturnBox.IsChecked);

                items.Insert(0, newItem);
                AddNewItem(newItem);

                ItemPrice.Text = string.Empty;
                OodBox.IsChecked = false;
                RpBox.IsChecked = false;
                ReturnBox.IsChecked = false;
                ErrorMessage.Visibility = Visibility.Hidden;

                Update();
            }
            catch (FormatException)
            {
                ErrorMessage.Visibility = Visibility.Visible;
            }
        }

        private void CalculateAll()
        {
            TotalSold = 0;
            TotalCommission = 0;
            TotalRP = 0;

            foreach (CommissionItem i in items)
            {
                AddNewItem(i);
            }
        }

        private void AddNewItem(CommissionItem commissionItem)
        {
            TotalSold += commissionItem.Price + commissionItem.ReplacementPrice;
            TotalCommission += commissionItem.Commission + commissionItem.ReplacementPrice * .1f;
            TotalRP += commissionItem.ReplacementPrice;

        }

        private void Update()
        {
            SoldText.Text = "Total Sold: $" + TotalSold;
            CommText.Text = "Commission Earned: $" + TotalCommission;
            PlanText.Text = "From Plans: $" + TotalRP;
        }
    }
}
