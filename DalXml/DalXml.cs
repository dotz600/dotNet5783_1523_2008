namespace Dal;
using DalApi;
using DO;
using System.Diagnostics;

sealed internal class DalXml : IDal
{
    private DalXml()
    {
        Product = new Product();
        Order = new Order();
        OrderItem = new OrderItem();
    }
    public static IDal Instance { get; } = new DalXml();

    public IOrder Order { get; } = new Dal.Order();

    public IProduct Product { get; } = new Dal.Product();

    public IOrderItem OrderItem { get; } = new Dal.OrderItem();

    public static IDal Instance { get; } = new DalXml();

}