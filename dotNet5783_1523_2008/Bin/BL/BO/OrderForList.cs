using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
///For order list screen
namespace BO;

public class OrderForList
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public OrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    BO.OrderForList,
    ID : {ID},
    Customer Name : 
    {CustomerName}, OrderStatus : {Status} , Amount Of Items : {AmountOfItems}, Total Price : {TotalPrice}. 
    ";
}
