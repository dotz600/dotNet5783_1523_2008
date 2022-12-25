
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
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();

    public MainWindow() => InitializeComponent();
    private void Show_Admin_Screen_Click(object sender, RoutedEventArgs e) => new ProductForListWindow().Show();

    private void Show_New_Order_Screen_Click(object sender, RoutedEventArgs e) => new Catalog().Show();

    private void Show_New_Track_Screen_Click(object sender, RoutedEventArgs e) => new trackOrderWindow().Show();
    
    
}
