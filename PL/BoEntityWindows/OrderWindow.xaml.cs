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

namespace PL.BoEntityWindows;

/// <summary>
/// from admin window- can update order status and watch all orders
/// </summary>
public partial class OrderWindow : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();
    public ObservableCollection<OrderForList?> orderForLists { get; set; }//hold the order list to show on screen
    public Array OrderStat { get { return Enum.GetValues(typeof(BO.OrderStatus)); } }//hold the category list to show on combobox

    public BO.OrderStatus SelectedStatus { get; set; } = BO.OrderStatus.None;
    public OrderWindow()
    {
        orderForLists ??= new();
        foreach (var x in bl!.Order.ReadAll())//fill orderForList collction to show on screen
            orderForLists.Add(x);

        InitializeComponent();
    }
    private void OrderStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //filter Orders by status, if status = None, will show all orders
    {
        if (SelectedStatus == BO.OrderStatus.None)
        {
            Refresh();
        }
        else
        {
            orderForLists.Clear();
            foreach (var order in bl!.Order.ReadAll())
                if (order?.Status == SelectedStatus)
                    orderForLists.Add(order);
        }


    }
    private void ListViewOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)//open update order window, can update shipping
    {
        if (ListViewOrders.SelectedItem != null)
        {
            BO.OrderForList order = (BO.OrderForList)ListViewOrders.SelectedItem;
            UpdateOrderWindow win = new(order.ID, true);
            win.ShowDialog();
            Refresh();//update the order collction to show on screen
        }
    }

    private void Product_page_Click(object sender, RoutedEventArgs e)//go to product page
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
