﻿
using BO;
using PL.BoEntityWindows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// admin window - can watch all the products in store - as productForList Entity
/// can update/add products, also can watch all order and update it shipping details
/// </summary>
public partial class ProductForListWindow : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();
    public ObservableCollection<ProductForList?> productForLists { get; }
    public Array Categories { get { return Enum.GetValues(typeof(BO.Categories)); } }

    public ProductForListWindow()
    {
        productForLists ??= new();
        InitializeComponent();
        Refresh();
    }

    private void AddProductButton_Click(object sender, RoutedEventArgs e)
    {
        AddProductWindow addProductWindow = new()
        {
            Bl = bl//pass the data
        };
        addProductWindow.ShowDialog();
        Refresh();
    }



    private void ProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //filter products by category, if category = None, will show all products
    {

        if (ProductSelector.SelectedItem.ToString() == BO.Categories.None.ToString())
        {
            Refresh();
        }
        else
        {
            productForLists.Clear();
            foreach (var x in (bl!.Product.ReadAll(x => x?.Category.ToString() == ProductSelector.SelectedItem.ToString())))
                productForLists.Add(x);
        }

    }

    private void ListViewProductForList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //double click on product takes to update win
    {
        
        if (ListViewProductForList.SelectedItem != null)
        {
            BO.ProductForList p = (BO.ProductForList)ListViewProductForList.SelectedItem;
            UpdateProductWindow updateProductWindow = new(p.ID){ bl = bl, prevWin = this};
            updateProductWindow.addToCartButton.IsEnabled = false;
            updateProductWindow.Show();
        }
    }

    private void Order_page_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
        new OrderWindow().Show();
       
    }
    public void Refresh()
    {
        productForLists.Clear();
        foreach (var x in bl!.Product.ReadAll()) 
            productForLists.Add(x);
        ProductSelector.SelectedIndex = 9;
    }
}
