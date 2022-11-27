using BlApi;
using BlImplementation;
using BO;

namespace BlImplementation;

internal class Product : IProduct
{

    private DalApi.IDal dal => new Dal.DalList();
    public void Create(BO.Product p)
    {

        try
        {
            CheckNameIdPriceStock(p);
            dal.Product.Create(new DO.Product
            {
                ID = p.ID,
                Name = p.Name,
                InStock = p.InStock,
                Price = p.Price,
                category = (DO.Categories)p.Category
            });
        }
        catch (DalApi.ObjExistException ex)
        {
            throw new CreateObjectFailedException("cant create new product", ex);
        }
        catch (NegativeIDException ex)
        {
            throw new NegativeIDException();
        }
        catch (NegativePriceException ex)
        {
            throw new NegativePriceException();
        }
        catch (EmptyNameException ex)
        {
            throw new EmptyNameException();
        }
        catch (NegativeAmountException ex)
        {
            throw new NegativeAmountException();
        }

    }



    public void Delete(int id)//check if the product not found in any order item, if so delete it
    {
        try
        {
            if (id < 0)
                throw new NegativeIDException("negetive id");
            DO.Product p = dal.Product.Read(id);
        }
        catch(NegativeIDException ex) { throw ex; }
        catch(DalApi.ObjNotFoundException ex) { throw new ReadObjectFailedException("there is no priduct to delete", ex); }
        try
        {
            //search the product id in all order item, if product dont found wiil throw exp and will catch it next,
            //and then in the catch delete the product
            DO.OrderItem ot = dal.OrderItem.ReadProductId(id);
            
            throw new Exception("product found and cant be deleted");//if reachd here that mean product found in order
        }
        
        catch (DalApi.ObjNotFoundException)//that mean product not found in order item and we can delete it
        {
            try//just for safety// by now we now the product found and can be delete, but just for safety
            {
                dal.Product.Delete(id);
            }
            catch(DalApi.ObjNotFoundException ex) 
            { 
                throw new ObjectNotExistException("cant delete product", ex); 
            }
        }
    }

    public BO.Product Read(int id) //for manger screen
    {
        if (id < 0)
            throw new Exception("negetive id");//will catch in main

        try
        {
            DO.Product DOpro = dal.Product.Read(id);
            BO.Product BOpro = new BO.Product
            {
                Name = DOpro.Name,
                ID = id,
                InStock = DOpro.InStock,
                Price = DOpro.Price,
                Category = (BO.Categories)DOpro.category
            };
            return BOpro;
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new ObjectNotExistException("cant read product", ex);
        }
    }

    public BO.ProductItem Read(int id, BO.Cart myCart) //for buyer screen
    {
        if (id < 0)
            throw new Exception("negetive id");
        try
        {
            DO.Product DOproduct = dal.Product.Read(id);
            BO.ProductItem BOproductItem = new BO.ProductItem
            {
                Name = DOproduct.Name,
                Price = DOproduct.Price,
                Amount = DOproduct.InStock,
                Category = (BO.Categories)DOproduct.category,
                InStock = false,
                ID = DOproduct.ID
            };

            if (DOproduct.InStock > 0)
                BOproductItem.InStock = true;

            return BOproductItem;
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new ObjectNotExistException("cant read product", ex);
        }

    }

    public IEnumerable<BO.ProductForList> ReadAll()
    {
        try
        {
            List<BO.ProductForList> res = new List<BO.ProductForList>();
            IBl bl = new Bl(); //for operait the convertion function from ProductForList 

            foreach (var DOproduct in dal.Product.ReadAll())
                res.Add(bl.ProductForList.DOproductToBOproductForList(DOproduct));//convert the DOproduct to BoProduct, then add it to list

            return res;
        }
        catch(Exception ex)
        { 
            throw new Exception("unkenow problom..."); 
        }
    }

    public void Update(BO.Product p)
    {
        try
        {
            CheckNameIdPriceStock(p);
            dal.Product.Update(new DO.Product
            {
                ID = p.ID,
                InStock = p.InStock,
                Name = p.Name,
                Price = p.Price,
                category = (DO.Categories)p.Category
            });
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new UpdateObjectFailedException("cant update the product becouse product dosnt found", ex);
        }
        catch (NegativeIDException ex)
        {
            throw ex;
        }
        catch (NegativePriceException ex)
        {
            throw ex;
        }
        catch (EmptyNameException ex)
        {
            throw ex;
        }
        catch (NegativeAmountException ex)
        {
            throw ex;
        }
    }

    private static void CheckNameIdPriceStock(BO.Product p)
    {
        if (p.ID < 0)
            throw new NegativeIDException("id is negative");
        if (p.Name.Length == 0)
            throw new EmptyNameException("your name is empty");
        if (p.Price <= 0)
            throw new NegativePriceException("price is negative");
        if (p.InStock < 0)
            throw new NegativeAmountException("in stock is negative");
    }
}
