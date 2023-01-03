
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

    public BO.Categories? CurrentCategory { get; set; } //hold the catrgory of the product
    

    public Array CategoriesToShow { get { return Enum.GetValues(typeof(BO.Categories)); } } //return array with all the categories
    public UpdateProductWindow(int id)
    {

        ProductDetails = bl.Product.Read(id);
        CurrentCategory = ProductDetails.Category;
        InitializeComponent();
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

}
