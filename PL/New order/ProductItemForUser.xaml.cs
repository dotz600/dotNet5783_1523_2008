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
/// show product details to costumer- 
/// can add product to cart
/// </summary>
public partial class ProductItemForUser : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();

    public BO.ProductItem ProductToShow { get; }//will update automatic to show the product on the catalog
    public Array Categories { get { return Enum.GetValues(typeof(BO.Categories)); } }//show all the categories in the combobox


    public ProductItemForUser(int id)
    {
        ProductToShow = new();
        ProductToShow = bl!.Product.Read(id, MainWindow.cart);
        InitializeComponent();

    }

    private void AddToCartButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl?.Cart.Add(MainWindow.cart, int.Parse(textBoxUpdateProductID.Text));
            this.Close();

            MessageBox.Show("Successfully added to cart", "Success", MessageBoxButton.OK, MessageBoxImage.Information
            , MessageBoxResult.Cancel);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
            , MessageBoxResult.Cancel);
            this.Close();

        }
    }
}
