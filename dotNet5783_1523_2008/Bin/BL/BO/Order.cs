using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///for screen order details, and order operations 
namespace BO;

public class Order
{

    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryrDate { get; set; }
    public OrderItem? Items { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
    BO.Order: ,
    ID : {ID}, 
    Customer Name, Email, Adress : {CustomerName} : {CustomerEmail} : {CustomerAdress},
    Order Date & status : {OrderDate} {Status}, Payment Date : {PaymentDate}, ShipDate : {ShipDate},
    DeliveryrDate : {DeliveryrDate}, 
    Items : {Items},
    TotalPrice {TotalPrice}
    ";
}
