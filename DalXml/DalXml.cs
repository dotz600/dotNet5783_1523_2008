namespace Dal;
using DalApi;
using DO;
using System.Diagnostics;

sealed internal class DalXml : IDal
{
    public IOrder Order { get; } = new Dal.Order();

    public IProduct Product { get; } = new Dal.Product();

    public IOrderItem OrderItem { get; } = new Dal.OrderItem();

    public static IDal Instance { get; } = new DalXml();

}