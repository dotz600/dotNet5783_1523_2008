using BlApi;
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
    /// from admin window- can update order status and watch all orders
    /// </summary>
    public partial class OrderWindow : Window
    {
        readonly BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<OrderForList?> orderForLists { get; set; }
        public Array OrderStat { get { return Enum.GetValues(typeof(BO.OrderStatus)); } }
        public OrderWindow()
        {
            orderForLists ??= new();
            foreach (var x in bl!.Order.ReadAll())
                orderForLists.Add(x);

            InitializeComponent();
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
            }


        }
        private void ListViewOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)//open update order window
        {
            if (ListViewOrders.SelectedItem != null)
            {
                BO.OrderForList order = (BO.OrderForList)ListViewOrders.SelectedItem;
                UpdateOrderWindow win = new(order.ID, true);
                win.ShowDialog();
                Refresh();
            }
        }

        private void Product_page_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new ProductForListWindow().Show();
        }


        public void Refresh()//update the Observable Collection order for list
        {

            orderForLists.Clear();
            foreach (var x in bl!.Order.ReadAll()) 
                orderForLists.Add(x);

            orderStatusSelector.SelectedIndex = 3;
        }
    }
}
