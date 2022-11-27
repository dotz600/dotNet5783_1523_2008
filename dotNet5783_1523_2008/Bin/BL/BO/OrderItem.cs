using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///for list item in screen cart and in screen order details
namespace BO;

public class OrderItem
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public int ProductID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    BO.OrderItem,
    ID : {ID},
    ProductID : {ProductID},
    Name : {Name},
    Price : {Price} 
    Amount : {Amount}
    TotalPrice : {TotalPrice} 
    ";
}
