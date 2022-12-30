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

namespace PL.New_order
{
    /// <summary>
    /// this window show all the product that in the catlog - 
    /// by duoble click user can add product to cart
    /// </summary>
    public partial class Catalog : Window
    {
        readonly BlApi.IBl? bl = BlApi.Factory.Get();

        private List<BO.ProductItem> ProductToShow { get { return bl!.ProductItem.ReadAll().ToList(); } }
        public Array Categories { get { return Enum.GetValues(typeof(BO.Categories)); } }

        CartView cartView;

        public Catalog()
        {
            InitializeComponent();
            cartView = new CartView();
        }

        private void CategorySort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CategorySort.SelectedItem.ToString() == BO.Categories.None.ToString())
                    Product_item_list_view.ItemsSource = bl?.ProductItem.ReadAll();
                else//read with predicate
                    Product_item_list_view.ItemsSource = bl?.ProductItem.ReadAll(x => x?.Category.ToString() == CategorySort.SelectedItem.ToString());
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
                var tmp = (BO.ProductItem)Product_item_list_view.SelectedItem;
                int id = tmp.ID;
                var win = new UpdateProductWindow(id);
                win.textBoxUpdateProductAmount.IsEnabled= false;
                win.textBoxUpdateProductPrice.IsEnabled= false;
                win.ConfirmButton.IsEnabled= false;
                win.cartPrevWin = cartView;
                win.Show();
    
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
                , MessageBoxResult.Cancel);
            }
        }

        private void WatchCartButton_Click(object sender, RoutedEventArgs e)//go to cart
        {
            cartView.Show();
            //this.Close();
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)//sort all the product by category
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
