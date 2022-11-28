


namespace DO;

/// <summary>
/// OrderItem is an object that represent order of one item
/// fill the order and the product details
/// </summary>
public struct OrderItem
{
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

    public override string ToString() => $@"
        Product ID={ProductID}: 
        Order ID - {OrderID}
        Price: {Price}
        Order Amount: {Amount}
        ";
}
