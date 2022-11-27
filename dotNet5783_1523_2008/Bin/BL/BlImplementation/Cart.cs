using BlApi;
using BO;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BlImplementation;

internal class Cart : ICart
{
    private DalApi.IDal dal => new Dal.DalList();

    public BO.Cart Add(BO.Cart cart, int productId)
    {
        if (cart.Items == null)
            cart.Items = new List<BO.OrderItem>();
        BO.OrderItem ot = searchInCart(cart, productId);
        try
        {
            DO.Product p = dal.Product.Read(productId);

            if (p.InStock <= 0)
                throw new NegativeAmountException("the product not in stock");

            if (ot.ID != 0) ///the product found in the cart
            {
                ot.Amount++;
                ot.TotalPrice += ot.Price;
                cart.TotalPrice += ot.Price;
                return cart;
            }
            else//not found in cart - will careate new one and add it to cart
            {
                ot.Price = p.Price;
                ot.TotalPrice = p.Price;
                ot.ProductID = productId;
                ot.Amount = 1;
                cart.Items.Add(ot);
                return cart;
            }
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new ReadObjectFailedException("cant read product from data source, product not exist", ex);
        }
        catch (NegativeAmountException ex)
        {
            throw new NegativeAmountException("the product not in stock", ex);
        }


    }

    public BO.Cart Update(BO.Cart cart, int productId, int amount)
    {
        BO.OrderItem ot = searchInCart(cart, productId);
        if (ot.ProductID == 0)
        {
            throw new ObjectNotExistException("the product not in cart, can't update");
        }
        else
        {
            try
            {
                DO.Product p = dal.Product.Read(productId);
                if (ot.Amount < amount)                         //if want to increase the amount
                {
                    if (p.InStock >= amount)
                    {
                        ot.Amount = amount;
                        cart.TotalPrice -= ot.TotalPrice;
                        ot.TotalPrice = ot.Price * amount;
                        cart.TotalPrice += ot.TotalPrice;
                        return cart;
                    }
                    else
                    {
                        throw new NegativeAmountException("not enough in stock");
                    }
                }
                else if (amount == 0)                        //if want to not order at all
                {
                    cart.TotalPrice -= ot.TotalPrice;
                    cart.Items.Remove(ot);
                }
                else if (ot.Amount > amount)                //if want to decrease the amount
                {
                    ot.Amount = amount;
                    cart.TotalPrice -= ot.TotalPrice;
                    ot.TotalPrice = ot.Price * amount;
                    cart.TotalPrice += ot.TotalPrice;
                }
                return cart;
            }
            catch (DalApi.ObjNotFoundException ex)
            {
                throw new ReadObjectFailedException("cant read product from data source, product not exist", ex);
            }

        }

    }

    public void CartConfirmation(BO.Cart cart, string name, string email, string adress)
    {
        //check all the data is good
        try
        {
            if (!email.Contains("@"))
                throw new EmptyNameException("email dont contain @");
            if (name.Length == 0)
                throw new EmptyNameException("coustomer name is empty");
            if (adress.Length == 0)
                throw new EmptyNameException("addres is empty");
            if (cart.Items != null)
            {
                foreach (var ot in cart.Items)
                {
                    if (dal.Product.Read(ot.ProductID).InStock < ot.Amount)
                        throw new NegativeAmountException("product in cart dont have enough in stock");
                }
            }
            else
                throw new ObjectNotExistException("Cart is empty, try again...");
            //create new order and add it to data base
            int orderId = dal.Order.Create(new DO.Order
            {
                ShipDate = null,
                CustomerAdress = adress,
                CustomerEmail = email,
                CustomerName = name,
                 DeliveryDate = null,
                 OrderDate = DateTime.Now
            });

            foreach (var ot in cart.Items)
            {
                DO.OrderItem DOot = new DO.OrderItem();
                DOot.OrderID = orderId;
                DOot.ProductID = ot.ProductID;
                DOot.Amount = ot.Amount;
                DOot.Price = ot.TotalPrice;
                dal.OrderItem.Create(DOot);

                DO.Product p = dal.Product.Read(ot.ProductID);
                p.InStock -= ot.Amount;

                if (p.InStock < 0)//just for safety
                    p.InStock = 0;
                dal.Product.Update(p);
            }
              
            
        }
        catch (EmptyNameException ex)
        {
            throw new EmptyNameException(ex.Message);
        }
        catch (ObjectNotExistException ex)
        {
            throw new ObjectNotExistException("cant read product from cart", ex);
        }
        catch (NegativeAmountException ex)
        {
            throw new NegativeAmountException(ex.Message);
        }
        catch(DalApi.ObjNotFoundException ex)
        {
            throw new ObjectNotExistException(ex.Message, ex);
        }
        catch(DalApi.ObjExistException ex)
        {
            throw new CreateObjectFailedException(ex.Message, ex);
        }

    }



    private BO.OrderItem searchInCart(BO.Cart cart, int productId)//help function
    {
        if (cart.Items != null)
        {
            BO.OrderItem res = cart.Items.Find(ot => ot.ProductID == productId);
            if (res != null)
                return res;
        }
        return new BO.OrderItem();
     }
}
