using BlApi;
using BO;
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

namespace PL.New_order;

/// <summary>
/// Interaction logic for CartView.xaml
/// </summary>
public partial class CartView : Window
{
   
    readonly BlApi.IBl? bl = BlApi.Factory.Get();

    Cart tmpCart;
    public CartView(Cart cart)
    {
        InitializeComponent();
        Cart_list_view.ItemsSource = cart.Items;
        tmpCart= cart;
    }
  
    private void ConfirmCartButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
        new Payment(tmpCart).Show();

    }

    private void Cart_list_view_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var slected = (BO.OrderItem)Cart_list_view.SelectedItem;
        this.Close();
        new UpdateAmount(tmpCart, slected.ProductID).Show();
        
    }
}
