
namespace DO;

/// <summary>
/// order is an object that represent customer order
/// fill the order and the customer details
/// </summary>
public struct Order
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }

    
    public override string ToString() => $@"
        Order ID={ID},
        Customer Details: - {CustomerName} : {CustomerEmail} : {CustomerAdress}
        OrderDate: {OrderDate}
        ShipDate: {ShipDate}
        DeliveryDate: {DeliveryDate}
        ";


}
