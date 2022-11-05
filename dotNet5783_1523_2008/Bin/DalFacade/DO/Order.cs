
namespace DO;

public struct Order
{
    int ID { get; set; }
    string CustomerName { get; set; }
    string CustomerEmail { get; set; }
    string CustomerAdress { get; set; }
    DateTime OrderDate { get; set; }
    DateTime ShipDate { get; set; }
    DateTime DeliveryDate { get; set; }

}
