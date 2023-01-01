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

namespace PL.New_order;

/// <summary>
/// this window represent the checkout process, 
/// take name, email and address from user and crate new order
/// </summary>
public partial class Payment : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();


    public Payment()
    {
        InitializeComponent();
            
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int x;
            string name = textBoxName.Text, email = textBoxEmail.Text, addres = textBoxAddres.Text;
            if (int.TryParse(name, out x) || int.TryParse(addres, out x))
                throw new BO.EmptyNameException("Name or adress must be a string!");
            bl?.Cart.CartConfirmation(MainWindow.cart, name, email, addres);//confirm cart
            //reset cart 
            MainWindow.cart = new BO.Cart();
            Page page = new ThankYouPage();
            this.Content = page;
        }
        catch(Exception ex) 
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
          , MessageBoxResult.Cancel);
        }

    }
}
