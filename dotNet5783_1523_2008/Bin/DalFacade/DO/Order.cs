
namespace DO;

public struct Order
{
    int ID { get; set; }
    string CustomerName { get; set; }
    string CustomerEmail { get; set; }
    string CustomerAdress { get; set; }
    DateTime orderDate { get; set; }
    DateTime deliveryDate { get; set; }
    DateTime shippingDate { get; set; }

}

