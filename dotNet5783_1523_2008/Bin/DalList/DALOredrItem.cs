
using DalApi;
using DO;

namespace Dal;
//search by order ID
internal class DalOredrItem :IOrderItem
{
    public int Create(OrderItem Ot)//add new obj to the data base
    {
        var y = DataSource.s_ordersItemArr.Find(obj => obj.OrderID == Ot.OrderID);//search if the obj allready in data base
        if (y.OrderID != 0)
            throw new ObjExist();

        //substruct thr amount of the order from the product, and then update the product 
        DalProduct p = new DalProduct();
        Product product = p.Read(Ot.ProductID);
        product.InStock = product.InStock - Ot.Amount;
        p.Update(product);
        
        // add the new orderItem in the data base
        DataSource.s_ordersItemArr.Add(Ot);
        return Ot.OrderID;
    }

    public OrderItem Read(int orderId)//serch the order and return it
    {
        var res = DataSource.s_ordersItemArr.Find(obj => obj.OrderID == orderId);
        if (res.OrderID == 0)
            throw new ObjNotFound();
        return res;
    }

    public IEnumerable<OrderItem> ReadAll()//return all the array
    {
        return DataSource.s_ordersItemArr.ToArray();
    }

    public void Delete(int id)//search the obj and delete it from the array
    {
        if (DataSource.s_ordersItemArr.Where(obj => obj.OrderID == id) == null)
            throw new ObjNotFound();

        DataSource.s_ordersItemArr.RemoveAll(obj => obj.OrderID == id);
    }

    public void Update(OrderItem ot)//ovveride the exist obj with the new one
    {
        var y = DataSource.s_ordersItemArr.FirstOrDefault(obj => obj.OrderID == ot.OrderID);
        if (y.OrderID == 0)//------------------------check again if working!!-------------------------
            throw new ObjNotFound();
        y = ot;
    }

    public void Print(OrderItem ot1)
    {
        Console.WriteLine(ot1.ToString());
    }
}
