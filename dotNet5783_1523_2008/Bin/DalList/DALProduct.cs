using DalApi;
using DO;

namespace Dal;

using DalApi;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;



internal class DalProduct : IProduct
{
   public int Create(Product p1)
   {
        var y = DataSource.s_productsArr.Find(obj => obj.ID == p1.ID);//search if the obj allready in data base
        if (y.ID != 0)
            throw new ObjExist();

        DataSource.s_productsArr.Add(p1);
        return p1.ID;
   }

    public Product Read(int id)
    {
        var res = DataSource.s_productsArr.Find(obj => obj.ID == id);
        if (res.ID == 0)
            throw new ObjNotFound();
        return res;
    }
    
    public IEnumerable<Product> ReadAll()
    {
        return DataSource.s_productsArr.ToArray();
    }

    public void Delete(int id)
    {
        if(DataSource.s_productsArr.Where(obj => obj.ID == id) == null)
            throw new ObjNotFound();
        
        DataSource.s_productsArr.RemoveAll(obj => obj.ID == id);
    }
    public void Update(Product p1)
    {
        var y = DataSource.s_productsArr.FirstOrDefault(obj => obj.ID == p1.ID);
        if (y.ID == 0)//------------------------check again if working!!-------------------------
            throw new ObjNotFound();
        y = p1;
    }

    public void Print(Product p1)
    {
        Console.WriteLine(p1.ToString());
    }
    public void reset()
    {
        int n = DataSource.s_randomNum;
    }

}
