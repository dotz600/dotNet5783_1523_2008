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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
            
        }

        private void Add_Product_Confirmation_Click(object sender, RoutedEventArgs e)
        {
            BlApi.IBl Bl = new BlImplementation.Bl();
            try
            {
                BO.Product p = create_product();
                Bl.Product.Create(p);
                new ProducrForListWindow().Show();
            }
            catch(Exception ex)
            { MessageBox.Show("Exception!"); }
        }

        private BO.Product create_product()
        {
            BO.Product p = new();
            string s = textBoxAddProductID.Text;
            p.ID = int.Parse(s);
            s = textBoxAddProductName.Text;
            p.Name = s;
            s = textBoxAddProductCategory.Text;
            BO.Categories category;
            Enum.TryParse(s, out category);
            p.Category = category;
            s = textBoxAddProductPrice.Text;
            p.Price = double.Parse(s);
            s = textBoxAddProductAmount.Text;
            p.InStock = int.Parse(s);
            return p;
        }
    }
}
