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
using System.Windows.Shapes;

namespace PL.New_order;

/// <summary>
/// this window show all the product that in the catlog - 
/// by duoble click user can add product to cart
/// </summary>
public partial class Catalog : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();

    public ObservableCollection<BO.ProductItem> ProductToShow { get; }//will update automatic to show the product on the catalog
    public Array Categories { get { return Enum.GetValues(typeof(BO.Categories)); } }//show all the categories in the combobox

    public Catalog()
    {
        ProductToShow = new();
        Refresh();
        InitializeComponent();
    }



    private void CategorySort_SelectionChanged(object sender, SelectionChangedEventArgs e)//show  only the product of the same category
    {
        try
        {
            if (CategorySort.SelectedItem.ToString() == BO.Categories.None.ToString())//show all products
            {
                Refresh();
            }
            else//read with predicate, and show only the chossen category 
            {
                ProductToShow.Clear();
                foreach (var x in bl!.Product.GetCatalog(MainWindow.cart,
                    x => x?.Category.ToString() == CategorySort.SelectedItem.ToString()))
                    ProductToShow.Add(x);
              
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
            , MessageBoxResult.Cancel);
        }

    }
    
    private void Product_item_list_MouseDoubleClick(object sender, MouseButtonEventArgs e)//duoble click will add product to cart
    {
        try
        {
            var tmp = (BO.ProductItem)Product_item_list_view.SelectedItem;//extract the product ID
            int id = tmp.ID;
            new ProductItemForUser(id).ShowDialog();
            Refresh();
        }
        catch (Exception ex) 
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
            , MessageBoxResult.Cancel);
        }
    }

    private void WatchCartButton_Click(object sender, RoutedEventArgs e)//go to cart
    {
        this.Close();
        new CartView().Show();
    }


    private void CheckBox_Checked(object sender, RoutedEventArgs e)//group/sort all the product by category
    {
        var temp = from x in bl?.Product.GetCatalog(MainWindow.cart)
                        orderby x.Category
                        select x;

        ProductToShow.Clear();
        foreach (var x in temp)
            ProductToShow.Add(x);
    }

    private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
       Refresh();
    }

    private void Refresh()//clear productItem to show and replace them with the updated products
    {
        ProductToShow.Clear();
        foreach (var x in bl!.Product.GetCatalog(MainWindow.cart))
            ProductToShow.Add(x);
    }
}
