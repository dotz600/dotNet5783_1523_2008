
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
/// Interaction logic for UpdateProductWindow.xaml
/// </summary>
public partial class UpdateProductWindow : Window
{
    public UpdateProductWindow(BO.ProductForList? data = null)
    {
        InitializeComponent();
        CategoryComboBoxUpdate.ItemsSource = Enum.GetValues(typeof(BO.Categories));//set list of categories
        CategoryComboBoxUpdate.SelectedIndex = 9;//choose default value(None)
        if (data != null)//if there is product to update, set unchangeable values, ID name and category
        {
            textBoxUpdateProductID.Text = data.ID.ToString();
            textBoxUpdateProductName.Text = data.Name;
            CategoryComboBoxUpdate.SelectedItem = data.Category;
        }
    }
    public BlApi.IBl? bl = BlApi.Factory.Get();//pass data
    public ProductForListWindow prevWin;//preview window
    private void Update_Product_Confirmation_Click(object sender, RoutedEventArgs e)//Update product and open PFL win
    {
        try
        {
            BO.Product p = Update_product();
            bl!.Product.Update(p);
            //if (prevWin != null)
            this.Close();
                prevWin.ListViewProductForList.Items.Refresh();
           // new ProductForListWindow().Show();
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
