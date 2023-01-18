
using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace Dal;
//search by order ID
internal class DalOredrItem : IOrderItem
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(OrderItem Ot)//add new obj to the data base
    {
        var res = DataSource.s_ordersItemArr.Find(obj => obj?.OrderID == Ot.OrderID && obj?.ProductID == Ot.ProductID);//search if the obj allready in data base
        if (res != null)
            throw new ObjExistException("Order item allready found");

        // add the new orderItem in the data base
        DataSource.s_ordersItemArr.Add(Ot);
        return Ot.OrderID;
    }
    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem Read(int orderId)//serch the order and return it
    {
        var res = DataSource.s_ordersItemArr.Find(obj => obj?.OrderID == orderId);
        if (res == null)
            throw new ObjNotFoundException("Order item doesn't found");
        return (OrderItem)res;
    }
    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem ReadProductId(int productId)//serch the order by product id and return it
    {
        var res = DataSource.s_ordersItemArr.Find(obj => obj?.ProductID == productId);
        if (res == null)
            throw new ObjNotFoundException("Order item doesn't found");
        return (OrderItem)res;
    }
    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem?> ReadAll(Func<OrderItem?, bool>? predicate = null)//return all the array
    {
        if (predicate == null)
            return DataSource.s_ordersItemArr.ToList();
        else
            return DataSource.s_ordersItemArr.FindAll(x => predicate(x));
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)//search the obj and delete it from the array
    {
        if (DataSource.s_ordersItemArr.Where(obj => obj?.OrderID == id) == null)
            throw new ObjNotFoundException("Order item doesn't found");

        DataSource.s_ordersItemArr.RemoveAll(obj => obj?.OrderID == id);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem ot)//ovveride the exist obj with the new one
    {
        int t = DataSource.s_ordersItemArr.FindIndex(x => x?.OrderID == ot.OrderID);
        if (t != -1)
            DataSource.s_ordersItemArr[t] = ot;
        else
            throw new ObjNotFoundException("cant update order item");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem ReadIf(Func<OrderItem?, bool> predicate)
    {
        OrderItem? orderItem = DataSource.s_ordersItemArr.FindLast(x => predicate(x));
        if (orderItem != null)
            return (OrderItem)orderItem;
        else
            throw new ObjNotFoundException();

    }
}
