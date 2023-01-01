
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
/// from admin page- can add new product to store
/// </summary>
public partial class AddProductWindow : Window   
{
    private BlApi.IBl? Bl = BlApi.Factory.Get();
    public List<BO.Categories> Ctegories //hold the categories to show - exept None
    { 
        get
        {
            var x = Enum.GetValues(typeof(BO.Categories));
            List<BO.Categories> res = new();
            foreach (BO.Categories cat in x)
                if(cat != BO.Categories.None)
                    res.Add(cat);
            
            return res;
        } 
    }
    public AddProductWindow()
    {
        InitializeComponent();
    }
    private void Add_Product_Confirmation_Click(object sender, RoutedEventArgs e)//creates a product, and open PFL win
    {

        try
        {
            int x;
            BO.Product p = Create_product_Add();
            if (int.TryParse(p.Name, out x))
                throw new BO.EmptyNameException("Name must be a string!");
            Bl!.Product.Create(p);
            this.Close();    
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK , MessageBoxImage.Hand
          , MessageBoxResult.Cancel); 
        }
    }

    private BO.Product Create_product_Add()//take data from fields, and returns new product
    {
        BO.Product p = new();
        string s = textBoxAddProductID.Text;
        p.ID = int.Parse(s);
        s = textBoxAddProductName.Text;
        p.Name = s;
        p.Category = (BO.Categories)CategoryComboBoxAdd.SelectedItem;
        s = textBoxAddProductPrice.Text;
        p.Price = double.Parse(s);
        s = textBoxAddProductAmount.Text;
        p.InStock = int.Parse(s);
        return p;
    }

    
}
