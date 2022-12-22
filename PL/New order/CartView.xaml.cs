﻿using BO;
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
    //need to add update function
    private void confirmCartButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
        new Payment(tmpCart).Show();

    }
}