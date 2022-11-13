
using DalList;
using DO;

namespace Dal;
//search by order ID
public class DalOredrItem :ICrud<OrderItem>
{
    public int Create(OrderItem Ot)//add new produt to the array
    {
        foreach(var item in DataSource.ordersItemArr)//search if allready exist
        {
            if(Ot.OrderID == item.OrderID)
                throw new Exception("Order Item already exist");
        }
        DataSource.ordersItemArr[DataSource.Config.ordersItemSize++] = Ot;//put the new obj in the array
        //substruct thr amount of the order from the product, and then update the product 
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

        res = DataSource.ordersItemArr.Where(ot => ot.ProductID != 0).ToArray();//search all the obj that have value and put them in to res array 
        return res;
    }

    public void Delete(int id)//search the obj and delete it from the array
    {
        if (DataSource.ordersItemArr.Where(ot => ot.OrderID == id) == null)//if the obj dosent found function "where" return null
            throw new Exception("Order Item doesn't found");

        DataSource.ordersItemArr = DataSource.ordersItemArr.Where(ot => ot.OrderID != id).ToArray();//put in the new array all the obj that dont equal to ot.id
        DataSource.Config.ordersItemSize--;
    }

    public void Update(OrderItem ot)//ovveride the exist obj with the new one
    {
        bool flag = false;//flag to know if obj found and update
        for(int i = 0; i < DataSource.Config.ordersItemSize; i++)
        {
            if(DataSource.ordersItemArr[i].OrderID == ot.OrderID)
            {
                flag = true;
                DataSource.ordersItemArr[i] = ot;
            }
        }
        if (flag == false)//not found
            throw new Exception("Order Item doesn't found");
    }

    public void Print(OrderItem ot1)
    {
        Console.WriteLine(ot1.ToString());
    }
}
