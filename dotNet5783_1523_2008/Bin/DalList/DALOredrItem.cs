using DalList;
using DO;

namespace Dal;
//search by order ID
public class DalOredrItem :ICrud<OrderItem>
{
    public int Create(OrderItem Ot)
    {
        foreach(var item in DataSource.ordersItemArr)
        {
            if(Ot.OrderID == item.OrderID)
                throw new Exception("Order Item already exist");
        }
        DataSource.ordersItemArr[DataSource.Config.ordersItemSize++] = Ot;
        DalProduct p = new DalProduct();
        Product product = p.Read(Ot.ProductID);
        product.InStock = product.InStock - Ot.Amount;
        p.Update(product);
        return Ot.OrderID;
    }

    public OrderItem Read(int orderId)//serch the order and return it
    {
        for (int i = 0; i < DataSource.Config.ordersItemSize; i++)
            if (DataSource.ordersItemArr[i].OrderID == orderId)
                return DataSource.ordersItemArr[i];

        throw new Exception("Order Item doesn't found");
    }

    public OrderItem[] ReadAll()//return all the array
    {
        OrderItem[] res = new OrderItem[DataSource.Config.ordersItemSize];

        res = DataSource.ordersItemArr.Where(ot => ot.ProductID != 0).ToArray();//check if working, else return to for loop 
        return res;
    }

    public void Delete(int id)
    {
        if (DataSource.ordersItemArr.Where(ot => ot.OrderID == id) == null)
            throw new Exception("Order Item doesn't found");

        DataSource.ordersItemArr = DataSource.ordersItemArr.Where(ot => ot.OrderID != id).ToArray();//put in the new array all the product that dont equal to p.id
        DataSource.Config.ordersItemSize--;
    }

    public void Update(OrderItem ot)
    {
        bool flag = false;
        for(int i = 0; i < DataSource.Config.ordersItemSize; i++)
        {
            if(DataSource.ordersItemArr[i].OrderID == ot.OrderID)
            {
                flag = true;
                DataSource.ordersItemArr[i] = ot;
            }
        }
        if (flag == false)
            throw new Exception("Order Item doesn't found");
    }

    public void Print(OrderItem ot1)
    {
        Console.WriteLine(ot1.ToString());
    }
}
