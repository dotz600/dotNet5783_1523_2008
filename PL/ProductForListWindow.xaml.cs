
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
/// Interaction logic for ProducrForListWindow.xaml
/// </summary>
public partial class ProductForListWindow : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();
    ObservableCollection<ProductForList> productForLists = new ObservableCollection<ProductForList>();
    public ProductForListWindow()
    {
        InitializeComponent();
        foreach (var x in bl.Product.ReadAll()) productForLists.Add(x);
        EventArgs args = new();
        ProductSelector.SelectedIndex = 9;//choose default value(None)
        ListViewProductForList.ItemsSource = productForLists;//set list of products
        ProductSelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));//set list of categories
    }

    private void AddProductButton_Click(object sender, RoutedEventArgs e)
    {
        AddProductWindow addProductWindow = new();
        addProductWindow.Bl = bl;//pass the data
        addProductWindow.ShowDialog();
        this.Refresh();
        ListViewProductForList.ItemsSource = productForLists;//update the list of products

    }



    private void ProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //filter products by category, if category = None, will show all products
    {
        if (ProductSelector.SelectedItem.ToString() == BO.Categories.None.ToString())
        {
            productForLists.Clear();
            foreach (var x in bl.Product.ReadAll()) productForLists.Add(x);
        }
        else
        {
            productForLists.Clear();
            foreach (var x in (bl?.Product.ReadAll(x => x?.Category.ToString() == ProductSelector.SelectedItem.ToString())) ) productForLists.Add(x);
        }
       
    }

    private void ListViewProductForList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //double click on product takes to update win
    {
        if (ListViewProductForList.SelectedItem != null)
        {
            BO.ProductForList p = (BO.ProductForList)ListViewProductForList.SelectedItem;
            UpdateProductWindow updateProductWindow = new(p){ bl = bl, prevWin = this};
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
        foreach (var x in bl.Product.ReadAll()) productForLists.Add(x);
        ProductSelector.SelectedIndex = 9;
    }
}
