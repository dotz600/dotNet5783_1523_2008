using BlApi;
using BO;
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

namespace PL.New_order;

/// <summary>
/// show the product coustumer have in cart, 
/// can click on each product to update amount
/// can return to catalog and crate new order with confirm cart
/// </summary>
public partial class CartView : Window
{

    public ObservableCollection<BO.OrderItem?> OrderItems { get; set; }//wiil hold all the product item in cart, and show them
    public BO.OrderItem? SelectedOT { get; set; } = new();//hold the delected order item that select from list
    public CartView()
    {
        OrderItems ??= new();
        MainWindow.cart.Items ??= new();
        Refresh();//fill OrderItems with the updated items in cart
        InitializeComponent();
    }
  
    private void ConfirmCartButton_Click(object sender, RoutedEventArgs e)//go to payment window to confirm order
    {
        this.Close();
        new Payment().Show();

    }

    private void Cart_list_view_MouseDoubleClick(object sender, MouseButtonEventArgs e)//go to update amount to order window
    {
        if(SelectedOT != null)
        {
            new UpdateAmount(SelectedOT.ProductID).ShowDialog();
            Refresh();//update the item list
        }

    }

    private void Button_Click(object sender, RoutedEventArgs e)//return to catalog
    {
        this.Close();
        new Catalog().Show();
    }
    private void Refresh()//clear OrderItems to show, and replace them with the updated items in cart
    {
        OrderItems.Clear();
        foreach (var item in MainWindow.cart.Items!)
            OrderItems.Add(item);
    }
}
