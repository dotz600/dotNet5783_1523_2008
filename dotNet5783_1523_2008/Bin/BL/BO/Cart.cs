using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///For the shopping cart management and order confirmation screen
namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public List<OrderItem> Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    BO.Cart
    CustomerName, Email, Address : {CustomerName}, {CustomerEmail}, {CustomerAddress}
    Items : {Items}
    Total price : {TotalPrice:f}";
}
