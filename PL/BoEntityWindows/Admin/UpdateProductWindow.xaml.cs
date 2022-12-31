
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

    public UpdateProductWindow(int id)
    {
        InitializeComponent();
        UpdateProductGrid.DataContext = bl.Product.Read(id);
        CategoryComboBoxUpdate.ItemsSource = Enum.GetValues(typeof(BO.Categories)); //set list of categories
    }
    
    private void Update_Product_Confirmation_Click(object sender, RoutedEventArgs e)//Update product and return to PFL win
    {
        try
        {
            BO.Product p = Update_product();
            bl!.Product.Update(p);
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
            , MessageBoxResult.Cancel, MessageBoxOptions.RtlReading);
        }
    }
    

    private BO.Product Update_product()//take data from fields, and returns new product
    {
        BO.Product p = new();
        string s = textBoxUpdateProductID.Text;
        p.ID = int.Parse(s);
        s = textBoxUpdateProductName.Text;
        p.Name = s;
        p.Category = (BO.Categories)CategoryComboBoxUpdate.SelectedItem;
        s = textBoxUpdateProductPrice.Text;
        p.Price = double.Parse(s);
        s = textBoxUpdateProductAmount.Text;
        p.InStock = int.Parse(s);
        return p;
    }

    private void AddToCartButton_Click(object sender, RoutedEventArgs e)//for buyer only, can add the product to cart
    {
        try
        {
            bl?.Cart.Add(MainWindow.cart,int.Parse(textBoxUpdateProductID.Text));
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
