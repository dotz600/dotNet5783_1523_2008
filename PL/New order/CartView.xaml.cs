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
/// 
/// </summary>
public partial class CartView : Window
{
   
    readonly BlApi.IBl? bl = BlApi.Factory.Get();

    public ObservableCollection<BO.OrderItem?> OrderItems 
    {
        get; set;
    }
    public CartView()
    {
        OrderItems ??= new();
        MainWindow.cart.Items ??= new();
        Refresh();
        InitializeComponent();
    }
  
    private void ConfirmCartButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
        new Payment().Show();

    }

    private void Refresh()
    {
        OrderItems.Clear();
        foreach (var item in MainWindow.cart.Items!)
        {
            OrderItems.Add(item);
        }
    }

    private void Cart_list_view_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var slected = (BO.OrderItem)Cart_list_view.SelectedItem;
        new UpdateAmount(slected.ProductID).ShowDialog();
        Refresh();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
        new Catalog().Show();
    }
}
