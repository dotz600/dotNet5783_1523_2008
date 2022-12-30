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

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        readonly BlApi.IBl? bl = BlApi.Factory.Get();
        public List<BO.ProductForList?> me
        {
            get
            {
                return bl!.Product.ReadAll().ToList();
            }
        }
        public Array combo
        {
            get
            {
                return Enum.GetValues(typeof(BO.Categories));
            }
        }
        public ObservableCollection<BO.OrderItem?> foo { get; }

        public Window1()
        {
            MainWindow.cart.Items = new();
            foo = new();
            MainWindow.cart.Items.Add(new BO.OrderItem{ Amount = 5, ID = 10, Name = "da", Price = 5, ProductID = 32, TotalPrice = 55 });
            foreach(var x in MainWindow.cart.Items)
                foo.Add(x);
            InitializeComponent();

            //this.DataContext = this;
           // me = bl!.Product.ReadAll().ToList();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foo.Add(new BO.OrderItem { Amount = 5, ID = 10, Name = "fgg", Price = 5, ProductID = 32, TotalPrice = 55 });
            me.Add(new BO.ProductForList { ID = 10, Name = "dotz", Price = 65 });
        }
    }
}
