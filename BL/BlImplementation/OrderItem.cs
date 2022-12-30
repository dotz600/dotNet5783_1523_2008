using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
namespace BlImplementation;

internal class OrderItem : IOrderItem
{
    private readonly DalApi.IDal? Dal = DalApi.Factory.Get();

    public BO.OrderItem Read(int productID)//return BO OrderItem - search by product id 
    {
        DO.OrderItem item =  Dal!.OrderItem.ReadProductId(productID);
        BO.OrderItem res = new();
        res.ProductID = productID;
        res.Price = item.Price;
        res.Amount = item.Amount;
        res.TotalPrice = item.Price * item.Amount;
        res.ID = item.OrderID;
        //get name from dal
        res.Name = Dal.Product.Read(productID).Name;
        return res;
    }
}
