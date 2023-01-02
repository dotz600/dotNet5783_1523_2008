﻿using BlApi;
using BlImplementation;

namespace BlImplementation;

internal class Product : IProduct
{

    private readonly DalApi.IDal? Dal = DalApi.Factory.Get();
    public void Create(BO.Product p)
    {
        if (int.TryParse(p.Name, out int x))
            throw new BO.EmptyNameException("Name must be a string!");
        if (p?.Name?.Length == 0)
            throw new BO.EmptyNameException("Name cat be a empty!");
        if (p?.InStock < 0)
            throw new BO.NegativeAmountException("Amount in stock cant be negative!");
        if (p?.ID < 0)
            throw new BO.NegativeIDException("ID cant be negative!");
        if (p?.Price < 0)
            throw new BO.NegativePriceException("price cant be negative!");
        try
        {

            CheckNameIdPriceStock(p);
            Dal?.Product.Create(new DO.Product
            {
                ID = p.ID,
                Name = p.Name,
                InStock = p.InStock,
                Price = p.Price,
                Category = (DO.Categories)p.Category!
            });
        }
        catch (DalApi.ObjExistException ex)
        {
            throw new BO.CreateObjectFailedException("product ID already exist", ex);
        }
        //all the other main will catch
    }



    public void Delete(int id)//check if the product not found in any order item, if so delete it
    {
        try
        {
            if (id < 0)
                throw new BO.NegativeIDException("negetive id");
            DO.Product p = Dal?.Product.Read(id) ?? throw new NullReferenceException();
        }
        catch(BO.NegativeIDException ex) 
        { 
            throw ex;
        }
        catch(DalApi.ObjNotFoundException ex) 
        { 
            throw new BO.ReadObjectFailedException("there is no product to delete", ex); 
        }
        try
        {
            //search the product id in all order item, if product dont found wiil throw exp and will catch it next,
            //and then in the catch delete the product
            DO.OrderItem ot = Dal?.OrderItem.ReadProductId(id) ?? throw new NullReferenceException();
            
            throw new DalApi.ObjExistException();//if reachd here that mean product found in order
        }
        
        catch (DalApi.ObjNotFoundException)//that mean product not found in order item and we can delete it
        {
            try//just for safety// by now we now the product found and can be delete, but just for safety
            {
                Dal?.Product.Delete(id);
            }
            catch(DalApi.ObjNotFoundException ex) 
            { 
                throw new BO.ObjectNotExistException("cant delete product", ex); 
            }
        }
        catch (DalApi.ObjExistException ex)
        {
            throw new BO.ProductFoundInOrderException("product found and cant be deleted", ex);
        }
    }

    public BO.Product Read(int id) //for manger screen
    {
        if (id < 0)
            throw new Exception("negetive id");//will catch in main

        try
        {
            DO.Product DOpro = Dal?.Product.Read(id) ?? throw new NullReferenceException();
            BO.Product BOpro = new()
            {
                Name = DOpro.Name,
                ID = id,
                InStock = DOpro.InStock,
                Price = DOpro.Price,
                Category = (BO.Categories)DOpro.Category
            };
            return BOpro;
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new BO.ObjectNotExistException("cant read product", ex);
        }
    }

    public BO.ProductItem Read(int id, BO.Cart myCart) //for buyer screen
    {
        if (id < 0)
            throw new Exception("negetive id");
        try
        {
            DO.Product DOproduct = Dal?.Product.Read(id) ?? throw new NullReferenceException();
            BO.ProductItem BOproductItem = new()
            {
                Name = DOproduct.Name,
                Price = DOproduct.Price,
                Amount = DOproduct.InStock,
                Category = (BO.Categories)DOproduct.Category,
                InStock = false,
                ID = DOproduct.ID
            };

            if (DOproduct.InStock > 0)
                BOproductItem.InStock = true;

            return BOproductItem;
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new BO.ObjectNotExistException("cant read product", ex);
        }

    }
    public IEnumerable<BO.ProductForList?> ReadAll(Func<BO.ProductForList?, bool>? predicate = null)
    {
        try
        {
            List<BO.ProductForList?> res = new();
            IBl bl = new Bl(); //for operait the convertion function from ProductForList 

            var listReturn = from DOproduct in Dal?.Product.ReadAll()
                             where (DOproduct != null)
                             select bl.ProductForList.DOproductToBOproductForList((DO.Product)DOproduct);
           
            if (predicate != null)//if the user ask to read with predicate, will remove from res all the !predcate 
            {
               listReturn = from x in listReturn
                            where predicate(x)
                            select x;
            }

            return listReturn.ToList();

        }
        catch (Exception ex)
        {
            throw new BO.ReadObjectFailedException("unkenow problom...", ex);
        }
    }


    public void Update(BO.Product p)
    {
        try
        {
            CheckNameIdPriceStock(p);
            Dal?.Product.Update(new DO.Product
            {
                ID = p.ID,
                InStock = p.InStock,
                Name = p.Name,
                Price = p.Price,
                Category = (DO.Categories)p.Category!
            });
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new BO.UpdateObjectFailedException("cant update the product becouse product dosnt found", ex);
        }
        //all the other main will catch
    }

    private static void CheckNameIdPriceStock(BO.Product p)
    {
        if (p.ID < 0)
            throw new BO.NegativeIDException("id is negative");
        if (p.Name== null || p.Name.Length == 0)
            throw new BO.EmptyNameException("your name is empty");
        if (p.Price <= 0)
            throw new BO.NegativePriceException("price is negative");
        if (p.InStock < 0)
            throw new BO.NegativeAmountException("in stock is negative");
    }

 
}
