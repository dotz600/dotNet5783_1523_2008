using BO;
using PL.BoEntityWindows.Admin;
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
using System.Windows.Shapes;

namespace PL.BoEntityWindows
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        readonly BlApi.IBl? bl = BlApi.Factory.Get();
        ObservableCollection<OrderForList> orderForLists = new();
        public OrderWindow()
        {
            InitializeComponent();
            orderStatusSelector.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));//set list of statuses
            orderStatusSelector.SelectedIndex = 3;//choose default value(None)
            foreach (var x in bl!.Order.ReadAll()) orderForLists.Add(x);
            ListViewOrders.ItemsSource = orderForLists;//set list of Orders
        }
        private void OrderStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //filter Orders by status, if status = None, will show all orders
        {
            if (orderStatusSelector.SelectedItem.ToString() == BO.OrderStatus.None.ToString())
            {
                Refresh();
            }
            else
            {
                orderForLists.Clear();
                foreach (var order in bl!.Order.ReadAll())
                     if (order?.Status == (BO.OrderStatus)orderStatusSelector.SelectedItem)
                           orderForLists.Add(order);
                ListViewOrders.ItemsSource = orderForLists;
            }


        }
        private void ListViewOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListViewOrders.SelectedItem != null)
            {
                BO.OrderForList order = (BO.OrderForList)ListViewOrders.SelectedItem;
                UpdateOrderWindow win = new(order.ID);
                win.prevWin = this;
                win.Show(); 
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
                new trackOrderWindow(((BO.OrderForList)ListViewOrders.SelectedItem).ID).Show();
            else
                new trackOrderWindow().Show();
        }
        public void Refresh()
        {
            orderForLists.Clear();
            foreach (var x in bl!.Order.ReadAll()) orderForLists.Add(x);
                 ListViewOrders.ItemsSource = orderForLists;
            orderStatusSelector.SelectedIndex = 3;
        }
    }
}
