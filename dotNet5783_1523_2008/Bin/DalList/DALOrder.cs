using DalApi;
using DO;

namespace Dal;

internal class DalOrder : IOrder
{
    public void Update(Order o1)//ovveride the exist obj with the new one
    {
        var y = DataSource.s_ordersArr.FirstOrDefault(x => x.ID == o1.ID);
        if(y.ID == 0)//------------------------check again if working!!-------------------------
            throw new ObjNotFound();
        y = o1;
    }
    public int Create(Order o1)//add the new obj to the array, return order ID
    {
        var y = DataSource.s_ordersArr.Find(x => x.ID == o1.ID);//search if the obj allready in data base
        if (y.ID != 0)
            throw new ObjExist();
        
        o1.ID = DataSource.Config.getIdRunNum();
        DataSource.s_ordersArr.Add(o1);
        return o1.ID;
    }
    public void Delete(int id)//delete the obj from the array
    {
        if (DataSource.s_ordersArr.Where(x => x.ID == id) == null)
            throw new ObjNotFound();
        DataSource.s_ordersArr.RemoveAll(x => x.ID == id);
    }

    public Order Read(int id)//return the obj
    {

        var res = DataSource.s_ordersArr.Find(x => x.ID == id);
        if(res.ID == 0)
            throw new ObjNotFound();
        return res;
    }
    public IEnumerable<Order> ReadAll()//return all the obj array
    {
        return DataSource.s_ordersArr.FindAll(x => true);
    }

    public void Print(Order o1)
    {
        Console.WriteLine(o1.ToString());
    }
}
