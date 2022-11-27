using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

/// <summary>
/// Cart
/// Functions:
/// * Add product to the cart
/// * Confirm cart(create new order)
/// * Update amount of exist product
/// You can create a cart, and when you confirm the cart, order will be create in the data base
/// Fields:
///* CustomerName, Email, Address
///* Items
///* Total price
/// </summary>
public interface ICart 
{
    Cart Add(Cart cart, int productId);//Add product to the cart
    void CartConfirmation(Cart cart, string name, string email, string adress);//Confirm the order
    Cart Update(Cart cart, int productId, int amount);//Update sigle product
}
