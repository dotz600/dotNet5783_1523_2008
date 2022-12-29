using BlApi;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BlImplementation;

internal class Cart : ICart
{
    private readonly DalApi.IDal? Dal = DalApi.Factory.Get();


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
                return cart;
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
                return cart;
            }
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

    public BO.Cart Update(BO.Cart cart, int productId, int amount)
    {
        if (amount < 0)
            throw new BO.NegativeAmountException("Cant Order Negative Amount!");
        BO.OrderItem ot = SearchInCart(cart, productId);
        if (ot.ProductID == 0)
            throw new BO.ObjectNotExistException("the product not in cart, can't update");
        else
        {
            try
            {
                DO.Product p = Dal?.Product.Read(productId) ?? throw new NullReferenceException();
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
                        throw new BO.NegativeAmountException("not enough in stock");
                    }
                }
                else if (amount == 0)                        //if want to not order at all
                {
                    cart.TotalPrice -= ot.TotalPrice;
                    cart.Items?.Remove(ot);
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
                throw new BO.ReadObjectFailedException("cant read product from data source, product not exist", ex);
            }

        }

    }

    public void CartConfirmation(BO.Cart cart, string name, string email, string adress)
    {
        //check all the data is good
        try
        {
            if (!email.Contains('@'))
                throw new BO.EmptyNameException("email dont contain @");
            if (name.Length == 0)
                throw new BO.EmptyNameException("coustomer name is empty");
            if (adress.Length == 0)
                throw new BO.EmptyNameException("addres is empty");
            if (cart?.Items?.Any() == false)
                throw new BO.CreateObjectFailedException("Your Cart Is Empty! Fill Your Cart First");

            if (cart?.Items != null)
            {
                foreach (var ot in cart.Items)
                {
                    if (ot != null)
                    if (Dal?.Product.Read(ot.ProductID).InStock < ot.Amount)
                        throw new BO.NegativeAmountException("product in cart dont have enough in stock");
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
                if (ot != null)
                {
                    DO.OrderItem DOot = new()
                    {
                        OrderID = orderId,
                        ProductID = ot.ProductID,
                        Amount = ot.Amount,
                        Price = ot.TotalPrice
                    };
                    Dal?.OrderItem.Create(DOot);
                } //make new order item and push it to data source
                if (ot != null)
                {
                    DO.Product p = Dal?.Product.Read(ot.ProductID) ?? throw new NullReferenceException();
                    p.InStock -= ot.Amount;


                    if (p.InStock < 0)//just for safety
                        p.InStock = 0;
                    Dal?.Product.Update(p);

                    ot.ID = orderId;
                }
            }
            //update cart, just for the comfterbule to debug
            cart.CustomerEmail = email;
            cart.CustomerAddress = adress;
            cart.CustomerName = name;

        }
        catch (BO.ObjectNotExistException ex)
        {
            throw new BO.ObjectNotExistException(ex.Message);
        }
        catch(DalApi.ObjNotFoundException ex)
        {
            throw new BO.ObjectNotExistException(ex.Message, ex);
        }
        catch(DalApi.ObjExistException ex)//all the other main will catch
        {
            throw new BO.CreateObjectFailedException(ex.Message, ex);
        }
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
