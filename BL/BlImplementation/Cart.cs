using BlApi;
using System.Runtime.CompilerServices;

namespace BlImplementation;

internal class Cart : ICart
{
    private readonly DalApi.IDal? Dal = DalApi.Factory.Get();


    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart Add(BO.Cart cart, int productId)
    {
        cart.Items ??= new List<BO.OrderItem?>();//if the list is null make new one

        BO.OrderItem ot = SearchInCart(cart, productId);//return the order item if found or an empty one
        try
        {
            DO.Product p = Dal?.Product.Read(productId) ?? throw new NullReferenceException();

            if (p.InStock <= 0)
                throw new BO.NegativeAmountException("the product not in stock");

            if (ot.ProductID != 0) ///the product found in the cart
            {
                ot.Amount++;
                ot.TotalPrice += ot.Price;
                cart.TotalPrice += ot.Price;
                ot.Name = p.Name;
            }
            else//not found in cart - will careate new one and add it to cart
            {
                ot.Price = p.Price;
                ot.TotalPrice = p.Price;
                ot.ProductID = productId;
                ot.Amount = 1;
                ot.Name = p.Name;
                cart.Items.Add(ot);
                cart.TotalPrice = p.Price;
            }
            return cart;
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new BO.ReadObjectFailedException("cant read product from data source, product not exist", ex);
        }
        catch (BO.NegativeAmountException ex)
        {
            throw new BO.NegativeAmountException("the product not in stock", ex);
        }


    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart Update(BO.Cart cart, int productId, int amount)
    {
        if (amount < 0)
            throw new BO.NegativeAmountException("Cant Order Negative Amount!");
        BO.OrderItem ot = SearchInCart(cart, productId);
        if (ot.ProductID == 0)
            throw new BO.ObjectNotExistException("The product is not in cart, can't update");

        try
        {
            DO.Product p = Dal?.Product.Read(productId) ?? throw new NullReferenceException();
            if (amount == 0)                        //if want to not order at all
            {
                cart.TotalPrice -= ot.TotalPrice;
                cart.Items?.Remove(ot);
            }
            else              //if want to decrease or add the amount
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
            throw new BO.ReadObjectFailedException("cant read product from data source, product not exist", ex);
        }

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int CartConfirmation(BO.Cart cart, string name, string email, string adress)
    {
        try
        {
            //check all the data is good
            CheckData(cart, name, email, adress);

            if (cart?.Items != null)//check all the product have the amount in stock for the order
            {
                foreach (var ot in cart.Items)
                {
                    var p = Dal?.Product.Read(ot!.ProductID);
                    if (p?.InStock < ot!.Amount)
                        throw new BO.NegativeAmountException("product " + p?.Name + " in cart dont have enough in stock");
                }
            }
            else
                throw new BO.CreateObjectFailedException("Your Cart Is Empty! Fill Your Cart First");

            //create new order and add it to data base
            int orderId = Dal?.Order.Create(new DO.Order
            {
                ShipDate = null,
                CustomerAdress = adress,
                CustomerEmail = email,
                CustomerName = name,
                DeliveryDate = null,
                OrderDate = DateTime.Now
            }) ?? throw new NullReferenceException();

            foreach (var ot in cart.Items)
            {
                lock (Dal!)
                {
                    if (ot != null)
                    {
                        //make new order item and push it to data source
                        DO.OrderItem DOot = new()
                        {
                            OrderID = orderId,
                            ProductID = ot.ProductID,
                            Amount = ot.Amount,
                            Price = ot.Price
                        };
                        Dal?.OrderItem.Create(DOot);

                        //update product amount in data source
                        DO.Product p = Dal?.Product.Read(ot.ProductID) ?? throw new NullReferenceException();
                        p.InStock -= ot.Amount;

                        if (p.InStock < 0)//just for safety
                            p.InStock = 0;
                        Dal?.Product.Update(p);

                        ot.ID = orderId;
                    }

                }
            }
            //update cart, just for the comfterbule to debug
            cart.CustomerEmail = email;
            cart.CustomerAddress = adress;
            cart.CustomerName = name;
            return orderId;
        }
        catch (BO.ObjectNotExistException ex)
        {
            throw new BO.ObjectNotExistException(ex.Message);
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new BO.ObjectNotExistException(ex.Message, ex);
        }
        catch (DalApi.ObjExistException ex)//all the other main will catch
        {
            throw new BO.CreateObjectFailedException(ex.Message, ex);
        }
    }

    private static void CheckData(BO.Cart cart, string name, string email, string adress)
    {
        if (!email.Contains('@'))
            throw new BO.EmptyNameException("email dont contain @");
        if (name.Length == 0)
            throw new BO.EmptyNameException("coustomer name is empty");
        if (adress.Length == 0)
            throw new BO.EmptyNameException("adress is empty");
        if (cart?.Items?.Any() == false)
            throw new BO.CreateObjectFailedException("Your Cart Is Empty! Fill Your Cart First");
        if (int.TryParse(name, out _))
            throw new BO.EmptyNameException("Name must be a string!");
        if (int.TryParse(adress, out _))
            throw new BO.EmptyNameException("Adress must be a string!");
    }

    private static BO.OrderItem SearchInCart(BO.Cart cart, int productId)//help function
    {
        if (cart.Items != null)
        {
            BO.OrderItem? res = cart.Items.Find(ot => ot?.ProductID == productId);
            if (res != null)
                return (BO.OrderItem)res;
        }
        return new BO.OrderItem();
    }
}
