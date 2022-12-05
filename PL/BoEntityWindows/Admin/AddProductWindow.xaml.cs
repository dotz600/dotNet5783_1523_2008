using BlImplementation;
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

namespace PL.BoEntityWindows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public AddProductWindow()
        {
            InitializeComponent();
            CategoryComboBoxAdd.ItemsSource = Enum.GetValues(typeof(BO.Categories));
        }
        public BlApi.IBl? Bl;
        private void Add_Product_Confirmation_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                BO.Product p = create_product_Add();
                Bl!.Product.Create(p);
                new ProductForListWindow().Show();
            }
            catch (Exception ex)
            { MessageBox.Show("Exception!"); }
        }

        private BO.Product create_product_Add()
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
}
