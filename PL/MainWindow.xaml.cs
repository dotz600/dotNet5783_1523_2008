
using PL.BoEntityWindows;
using PL.BoEntityWindows.Admin;
using PL.New_order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Main window - control the 3 buttons - 
/// admin - mange products and orders
/// order - can add and remove product from cart- and crate new order
/// track - costomer can track order by order id - only watch order details
/// </summary>
public partial class MainWindow : Window
{

    internal static BO.Cart cart = new();//static global cart - all the windows can use
    public MainWindow() => InitializeComponent();
  
    private void Show_Admin_Screen_Click(object sender, RoutedEventArgs e) => new ProductForListWindow().Show();

    private void Show_New_Order_Screen_Click(object sender, RoutedEventArgs e) => new Catalog().Show();

    private void Show_New_Track_Screen_Click(object sender, RoutedEventArgs e) => new trackOrderWindow().Show();

    private void StartSimulationClick(object sender, RoutedEventArgs e) => new StartSimulatorWindow().Show();
}
