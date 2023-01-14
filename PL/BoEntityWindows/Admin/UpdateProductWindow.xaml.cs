
using BO;
using PL.New_order;
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

namespace PL.BoEntityWindows;

/// <summary>
/// admin can update the amount and price of the product
/// buyer can only watch
/// </summary>
public partial class UpdateProductWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public BO.Product ProductDetails { get; set; } //hold the product details

    public Array CategoriesToShow { get { return Enum.GetValues(typeof(BO.Categories)); } } //return array with all the categories
    public UpdateProductWindow(int id)
    {
        ProductDetails = bl.Product.Read(id);
        InitializeComponent();
    }
    
    private void Update_Product_Confirmation_Click(object sender, RoutedEventArgs e)//Update product and return to PFL win
    {
        try
        {
            Update_product(ProductDetails);
            bl!.Product.Update(ProductDetails);
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
            , MessageBoxResult.Cancel, MessageBoxOptions.RtlReading);
        }
    }
    

    private void Update_product(BO.Product p)//take data from fields, and returns new product
    {
        string s = textBoxUpdateProductPrice.Text;
        p.Price = double.Parse(s);
        s = textBoxUpdateProductAmount.Text;
        p.InStock = int.Parse(s);
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            this.Close();
            bl!.Product.Delete(ProductDetails.ID);
            MessageBox.Show("Successfully deleted from store", "Success", MessageBoxButton.OK, MessageBoxImage.Information
          , MessageBoxResult.Cancel);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
            , MessageBoxResult.Cancel, MessageBoxOptions.RtlReading);
        }
    }
}
