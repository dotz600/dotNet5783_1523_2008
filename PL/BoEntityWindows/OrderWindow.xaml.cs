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
            orderStatusSelector.SelectedIndex = 3;//choose default value(None)
            ListViewOrders.ItemsSource = bl.Order.ReadAll();//set list of Orders
        }
        private void OrderStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //filter Orders by status, if status = None, will show all orders
        {
            if (orderStatusSelector.SelectedItem.ToString() == BO.OrderStatus.None.ToString())
                ListViewOrders.ItemsSource = bl?.Order.ReadAll();
            else
            {
                ListViewOrders.ItemsSource = from order in bl?.Order.ReadAll()
                                             where (order.Status == (BO.OrderStatus)orderStatusSelector.SelectedItem)
                                             select order;
            }


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

        private void TrackOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewOrders.SelectedItem != null)
            {
                new trackOrderWindow(((BO.OrderForList)ListViewOrders.SelectedItem).ID).Show();
            }
        }
    }
}
