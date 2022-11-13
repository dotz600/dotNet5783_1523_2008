using DalList;
using DO;

namespace Dal;

public class DalOrder : ICrud<Order>
{
    public void Update(Order o1)//ovveride the exist obj with the new one
    {
        bool flag = false;
        for(int i = 0; i < DataSource.Config.ordersSize; i++)
        {
            if (o1.ID == DataSource.ordersArr[i].ID)
            {
                DataSource.ordersArr[i] = o1;
                flag = true;
            }
        }
        if(flag == false)//the obj dont found
            throw new Exception("order doesn't found");
    }
    public int Create(Order o1)//add the new obj to the array, return order ID
    {
        for(int i = 0; i < DataSource.Config.ordersSize; i++)
        {
            if (o1.ID == DataSource.ordersArr[i].ID)
                throw new Exception("order already exist");
        }
        DataSource.ordersArr[DataSource.Config.ordersSize] = o1;
        DataSource.ordersArr[DataSource.Config.ordersSize++].ID = DataSource.Config.getIdRunNum();
        return DataSource.ordersArr[DataSource.Config.ordersSize++].ID;
    }
    public void Delete(int id)//delete the obj from the array
    {
        if (DataSource.ordersArr.Where(o => o.ID == id) == null)
            throw new Exception("order doesn't found");

        DataSource.ordersArr = DataSource.ordersArr.Where(o => o.ID != id).ToArray();
        DataSource.Config.ordersSize--;
    }
    public Order Read(int id)//return the obj
    {
        for (int i = 0; i < DataSource.Config.ordersSize; i++)
        {
            if (id == DataSource.ordersArr[i].ID)
            {
                return DataSource.ordersArr[i];
            }
        }
        throw new Exception("order doesn't found");
    }
    public Order[] ReadAll()//return all the obj array
    {
        Order[] res = new Order[DataSource.Config.ordersSize];
        for(int i = 0; i < DataSource.Config.ordersSize; i++)
        {
            res[i] = DataSource.ordersArr[i];
        }
        return res;
    }

    public void Print(Order o1)
    {
        Console.WriteLine(o1.ToString());
    }
}
