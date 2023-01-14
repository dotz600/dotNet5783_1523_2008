using BO;
using PL.BoEntityWindows.Admin;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.BoEntityWindows;

/// <summary>
/// from admin window- can update order status and watch all orders
/// </summary>
public partial class OrderWindow : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();
    public ObservableCollection<OrderForList?> OrderForLists { get; set; } = new();//hold the order list to show on screen
    public Array OrderStat { get { return Enum.GetValues(typeof(BO.OrderStatus)); } }//hold the category list to show on combobox

    public BO.OrderStatus SelectedStatus { get; set; } = BO.OrderStatus.None;

    public BO.OrderForList SelectedOrder { get; set; } = new();
    public OrderWindow()
    {
        Refresh();//fill orderForList collction to show on screen
        InitializeComponent();
    }

    private void OrderStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //filter Orders by status, if status = None, will show all orders
    {
        if (SelectedStatus == BO.OrderStatus.None)
            Refresh();
        else
            Refresh(o => o?.Status == SelectedStatus);
    }

    private void ListViewOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)//open update order window, can update shipping
    {

        if(SelectedOrder != null)
        {
            new UpdateOrderWindow(SelectedOrder.ID, true).ShowDialog();
            Refresh();//update the order collction to show on screen
        }
    }

    private void Product_page_Click(object sender, RoutedEventArgs e)//go to product page
    {
        this.Close();
        new ProductForListWindow().Show();
    }


    public void Refresh(Func<BO.OrderForList?, bool>? predicate = null)//update the Observable Collection order for list
    {
        OrderForLists.Clear();
        foreach (var x in bl!.Order.ReadAll(predicate))
            OrderForLists.Add(x);
    }
}
