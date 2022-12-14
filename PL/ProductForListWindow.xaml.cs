
using BO;
using PL.BoEntityWindows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for ProducrForListWindow.xaml
/// </summary>
public partial class ProductForListWindow : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();
    public ProductForListWindow()
    {
        InitializeComponent();
        ProductSelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));//set list of categories
        EventArgs args = new();
        ProductSelector.SelectedIndex = 9;//choose default value(None)


        ListViewProductForList.ItemsSource = bl.Product.ReadAll();//set list of products

    }

    private void AddProductButton_Click(object sender, RoutedEventArgs e)
    {
        AddProductWindow addProductWindow = new();
        addProductWindow.Bl = bl;//pass the data
        addProductWindow.Show();
    }

  

    private void ProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //filter products by category, if category = None, will show all products
    {
        if (ProductSelector.SelectedItem.ToString() == BO.Categories.None.ToString())
            ListViewProductForList.ItemsSource = bl?.Product.ReadAll();
        else
            ListViewProductForList.ItemsSource = bl?.Product.ReadAll(x => x?.Category.ToString() != ProductSelector.SelectedItem.ToString() );
    }

    private void ListViewProductForList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //double click on product takes to update win
    {
        if (ListViewProductForList.SelectedItem != null)
        {
            BO.ProductForList p = (BO.ProductForList)ListViewProductForList.SelectedItem;
            UpdateProductWindow updateProductWindow = new(p){ bl = bl};
            updateProductWindow.ShowDialog();
        }
    }
}
