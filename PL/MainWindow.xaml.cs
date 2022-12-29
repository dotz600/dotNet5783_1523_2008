
using BO;
using PL.BoEntityWindows;
using PL.BoEntityWindows.Admin;
using PL.New_order;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Main window - control the 3 buttons - admin, order and track
/// </summary>
public partial class MainWindow : Window
{

    internal static Cart cart = new();//each entry get a brand new cart, the cart will pass bettwen all the relevant window - rest only after payment!
    public MainWindow() => InitializeComponent();
    private void Show_Admin_Screen_Click(object sender, RoutedEventArgs e) => new ProductForListWindow().Show();

    private void Show_New_Order_Screen_Click(object sender, RoutedEventArgs e) => new Catalog().Show();

    private void Show_New_Track_Screen_Click(object sender, RoutedEventArgs e) => new trackOrderWindow().Show();

   
}
