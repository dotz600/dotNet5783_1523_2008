﻿using BlApi;
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

    List<BO.ProductForList?> me;
    ObservableCollection<BO.OrderItem> orderItems = new ObservableCollection<BO.OrderItem>();
    public CartView()
    {
        InitializeComponent();
        me = bl!.Product.ReadAll().ToList();
        //Cart_list_view.ItemsSource = MainWindow.cart.Items;
        CartRefresh();
        Cart_list_view.ItemsSource = orderItems;
    }
  
    private void ConfirmCartButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
        new Payment().Show();

    }

    private void Cart_list_view_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var slected = (BO.OrderItem)Cart_list_view.SelectedItem;
        this.Close();
        new UpdateAmount(slected.ProductID).Show();
        
    }
    public void CartRefresh()
    {
        orderItems.Clear();
        for (int i = 0; i < MainWindow.cart.Items?.Count; i++)
        {
            OrderItem? x = MainWindow.cart.Items[i];
            orderItems.Add(x);
        }
    }

}
