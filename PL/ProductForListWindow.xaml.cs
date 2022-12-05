using PL.BoEntityWindows;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ProducrForListWindow.xaml
    /// </summary>
    public partial class ProductForListWindow : Window
    {
        BlApi.IBl bl = new BlImplementation.Bl();
        public ProductForListWindow()
        {
            InitializeComponent();
            ListViewProductForList.ItemsSource = bl.Product.ReadAll();
            ProductSelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.Bl = bl;//pass the data
            addProductWindow.Show();
        }

        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateProductWindow updateProductWindow = new UpdateProductWindow();
            updateProductWindow.Bl = bl;
            updateProductWindow.Show();
        }

        private void ProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewProductForList.ItemsSource = bl.Product.ReadAll();//--TO DO -- finish!!
        }
    }
}
