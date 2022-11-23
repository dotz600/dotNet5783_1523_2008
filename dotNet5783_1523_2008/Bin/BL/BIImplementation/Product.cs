using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BIApi;

namespace BIImplementation;

internal class Product : IProduct
{
  
    private DalApi.IDal dal => new Dal.DalList();
    public void Create(BO.Product p)
    {
        CheckNameIdPriceStock(p);

        try
        {
            dal.Product.Create(new DO.Product { ID = p.ID, Name = p.Name, InStock = p.InStock,
                Price = p.Price, category = (DO.Categories)p.Category });
        }
        catch (Exception ex)
        {
        }
    }



    public void Delete(int id)
    {
        if (id < 0)
            throw new Exception("negetive id");
        try
        {
            DO.Product p = dal.Product.Read(id);
        }
        catch(Exception e)
        {
            throw new Exception("product dont exist");
        }
        try
        {
            DO.OrderItem ot = dal.OrderItem.ReadProductId(id);//if product dont found wiil throw exp and wiil catch it next, 
            throw new Exception("product found and cant be deleted");
        }
        catch(Exception e)//that mean product not found in order item and we can delete it
        {
           dal.Product.Delete(id);
        }
    }

    public BO.Product Read(int id) //for manger screen
    {
        if(id < 0)
            throw new Exception("negetive id"); 

        try
        {

            DO.Product DOpro = dal.Product.Read(id);
            BO.Product BOpro = new BO.Product { Name = DOpro.Name , ID = id, InStock = DOpro.InStock ,
                Price = DOpro.Price, Category = (BO.Categories)DOpro.category };
            return BOpro;
        }
        catch(Exception ex)
        {
            throw new Exception("cant read product");
        }
        
    }

    public BO.ProductItem Read(int id, BO.Cart myCart) //for buyer screen
    {
        if (id < 0)
            throw new Exception("negetive id");
        try
        {
            DO.Product DOproduct = dal.Product.Read(id);
            BO.ProductItem BOproductItem = new BO.ProductItem { Name = DOproduct.Name , Price = DOproduct.Price ,
                Amount = DOproduct.InStock, Category = (BO.Categories)DOproduct.category, InStock = false, ID = DOproduct.ID };

            if (DOproduct.InStock > 0)
                BOproductItem.InStock = true;

            return BOproductItem;
        }
        catch (Exception ex)
        {
            throw new Exception("cant read product");
        }
       
    }

    public IEnumerable<BO.ProductForList> ReadAll()
    {
        List<BO.ProductForList> res = new List<BO.ProductForList>();
        IBl bl = new Bl();//for operait the convertion function from ProductForList 

        foreach(var DOproduct in dal.Product.ReadAll())
            res.Add(bl.ProductForList.DOproductToBOproductForList(DOproduct));//convert the DOproduct to BoProduct, then add it to list

        return res;
    }

    public void Update(BO.Product p)
    {
        CheckNameIdPriceStock(p);
        try
        {
            dal.Product.Create(new DO.Product { ID = p.ID, InStock = p.InStock, Name = p.Name, Price = p.Price, category = (DO.Categories)p.Category });
        }
        catch (Exception ex)
        {

        }
        throw new NotImplementedException();
    }

    private static void CheckNameIdPriceStock(BO.Product p)
    {
        if (p.ID < 0) throw new Exception();
        if (p.Name.Length == 0) throw new Exception();
        if (p.Price <= 0) throw new Exception();
        if (p.InStock < 0) throw new Exception();
    }
}
