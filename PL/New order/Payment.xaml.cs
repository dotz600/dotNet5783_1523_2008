﻿using PL.BoEntityWindows;
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
/// Interaction logic for Payment.xaml
/// </summary>
public partial class Payment : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();

    BO.Cart cart;
    public Payment(BO.Cart cart)
    {
        InitializeComponent();
        this.cart = cart;
            
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string name = textBoxName.Text, email = textBoxEmail.Text, addres = textBoxAddres.Text;
        try
        {
            bl?.Cart.CartConfirmation(cart, name, email, addres);
            this.Close();
            cart = new BO.Cart();
        }
        catch(Exception ex) 
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
          , MessageBoxResult.Cancel, MessageBoxOptions.RtlReading);
        }

    }
}
