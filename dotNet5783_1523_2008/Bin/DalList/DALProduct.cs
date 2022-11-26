using DalApi;
using DO;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Dal;

public class DalProduct : IProduct
{
   public int Create(Product p1)
   {
        foreach (Product myPro in DataSource.s_productsArr)
        {
            if (myPro.ID == p1.ID)//if the ID exist in the array make new ID and check agian
                throw new ObjExistException("product already exist");
        }
        DataSource.s_productsArr.Add(p1);
        return p1.ID;

   }

    public Product Read(int id)
    {
        foreach(Product myPro in DataSource.s_productsArr)
        {
            if (myPro.ID == id)
                return myPro;
        }
        throw new ObjNotFoundException("Product doesn't found");
    }
    
    public IEnumerable<Product> ReadAll()
    {
        return DataSource.s_productsArr.ToList();
    }

    public void Delete(int id)
    {
        if(DataSource.s_productsArr.Where(p => p.ID == id) == null)
            throw new ObjNotFoundException("Product doesn't found");

        DataSource.s_productsArr.Remove(Read(id));
    }
    public void Update(Product p1)
    {

        int t = DataSource.s_productsArr.FindIndex(p => p.ID == p1.ID);
        if(t != -1)
            DataSource.s_productsArr[t] = p1;

        

        x = p1;
    }

    public void Print(Product p1)
    {
        Console.WriteLine(p1.ToString());
    }
 
}
