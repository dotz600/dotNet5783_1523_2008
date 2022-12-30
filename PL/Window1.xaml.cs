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

        public Window1()
        {
            InitializeComponent();
            //this.DataContext = this;
           // me = bl!.Product.ReadAll().ToList();
        }
    }
}
