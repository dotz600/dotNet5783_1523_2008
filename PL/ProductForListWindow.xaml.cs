

using BO;
using PL.BoEntityWindows;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL;

/// <summary>
/// admin window - can watch all the products in store - as productForList Entity
/// can update/add products, also can watch all order and update its shipping details
/// </summary>
public partial class ProductForListWindow : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();
    public ObservableCollection<BO.ProductForList?> ProductForLists { get; } = new();//hold all the product to show for manger
    public Array Categories { get { return Enum.GetValues(typeof(BO.Categories)); } }//hold and show the categoris in combobox

    public BO.Categories SelectedCategory { get; set; } = BO.Categories.None;//hold the category that selected in combobox
    public BO.ProductForList SelectedProduct { get; set; } = new();//hold the selected product that select from list
    public ProductForListWindow()
    {
        InitializeComponent();
        Refresh();//update the product to show
    }

    private void AddProductButton_Click(object sender, RoutedEventArgs e)//go to addProduct window
    {
        new AddProductWindow().ShowDialog();
        Refresh();//update the product list to show manger
    }

    private void ProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //filter products by category, if category = None, will show all products
    {

        if (SelectedCategory == BO.Categories.None)
            Refresh();
        else
            Refresh(x => x?.Category == SelectedCategory);
    }

    private void ListViewProductForList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    //double click on product takes to update win
    {
        new UpdateProductWindow(SelectedProduct.ID).ShowDialog();
        Refresh();
    }

    private void Order_page_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
        new OrderWindow().Show();

    }
    private void Refresh(Func<ProductForList?, bool>? predicate = null)//update the product to show on screen
    {
        ProductForLists.Clear();
        foreach (var x in bl!.Product.ReadAll(predicate))
            ProductForLists.Add(x);
    }
}
