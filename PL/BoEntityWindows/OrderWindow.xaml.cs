using PL.BoEntityWindows.Admin;
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

namespace PL.BoEntityWindows
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public OrderWindow()
        {
            InitializeComponent();
            orderStatusSelector.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));//set list of statuses
            orderStatusSelector.SelectedIndex = 4;//choose default value(None)
            ListViewOrders.ItemsSource = bl.Order.ReadAll();//set list of Orders
        }
        private void orderStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //filter products by category, if category = None, will show all products
        {
            if (orderStatusSelector.SelectedItem.ToString() == BO.Categories.None.ToString())
                ListViewOrders.ItemsSource = bl?.Order.ReadAll();
            
        }
        private void ListViewOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListViewOrders.SelectedItem != null)
            {
                BO.OrderForList order = (BO.OrderForList)ListViewOrders.SelectedItem;
                new UpdateOrderWindow(order.ID).Show();
            }
        }

        private void Product_page_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new ProductForListWindow().Show();
        }
    }
}
