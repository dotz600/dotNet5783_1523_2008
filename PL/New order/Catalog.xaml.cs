using BO;
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

namespace PL.New_order
{
    /// <summary>
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        readonly BlApi.IBl? bl = BlApi.Factory.Get();
        Cart cart = new();

        public Catalog()
        {
            InitializeComponent();
            try
            {
                Product_item_list_view.ItemsSource = bl.ProductItem.ReadAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
                , MessageBoxResult.Cancel, MessageBoxOptions.RtlReading);
            }
            CategorySort.ItemsSource = Enum.GetValues(typeof(BO.Categories));//set list of categories
            CategorySort.SelectedIndex = 9;//choose default value(None)
        }

        private void CategorySort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CategorySort.SelectedItem.ToString() == BO.Categories.None.ToString())
                    Product_item_list_view.ItemsSource = bl?.ProductItem.ReadAll();
                else
                    Product_item_list_view.ItemsSource = bl?.ProductItem.ReadAll(x => x?.Category.ToString() == CategorySort.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
                , MessageBoxResult.Cancel, MessageBoxOptions.RtlReading);
            }

        }
        
        private void Product_item_list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var tmp = (BO.ProductItem)Product_item_list_view.SelectedItem;
            try
            {
                bl?.Cart.Add(cart, tmp.ID);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
                , MessageBoxResult.Cancel, MessageBoxOptions.RtlReading);
            }
        }

        private void WatchCartButton_Click(object sender, RoutedEventArgs e)
        {
            new CartView(cart).Show();
            this.Close();
        }


        private void CategoryColumn_Selected(object sender, RoutedEventArgs e)
        {
            Product_item_list_view.ItemsSource = from x in bl?.ProductItem.ReadAll() group x by x.Category;
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Product_item_list_view.ItemsSource = from x in bl?.ProductItem.ReadAll()
                                                 orderby x.Category 
                                                 select x;

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Product_item_list_view.ItemsSource = bl?.ProductItem.ReadAll();
        }
    }
}
