using BIApi;


namespace BIImplementation;

internal class Cart : ICart
{
    private DalApi.IDal dal => new Dal.DalList();
    public BO.Cart Create(BO.Cart cart, int productId)
    {
        BO.OrderItem ot = searchInCart(cart, productId);
        DO.Product p = dal.Product.Read(productId);
        
        if(p.InStock <= 0)
            throw new Exception("the product not in stock");

        if (ot.ID != 0 ) ///the product found in the cart
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

    public BO.Cart Update(BO.Cart cart, int productId, int amount)
    {
        BO.OrderItem ot = searchInCart(cart, productId);
        if (ot.ID == 0)
        {
            throw new Exception("the product not in cart");
        }
        else
        {
            DO.Product p = dal.Product.Read(productId);
            if (ot.Amount < amount)                         //if want to increase the amount
            {
                if (p.InStock >= ot.Amount)
                {
                    ot.Amount = amount;
                    cart.TotalPrice -= ot.TotalPrice;
                    ot.TotalPrice = ot.Price * amount;
                    cart.TotalPrice += ot.TotalPrice;
                    return cart;
                }
                else
                {
                    throw new Exception("not enough in stock");
                }
            }
            else if( amount == 0)                        //if want to not order at all
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
        
    }

    public void CartConfirmation(BO.Cart cart, string name, string email, string adress)
    {
        //check all the data is good
        bool flag = true;
        if (!email.Contains("@") || name.Length == 0 || adress.Length == 0)
            flag = false;
        foreach(var ot in cart.Items) 
        {
            if (dal.Product.Read(ot.ProductID).InStock < ot.Amount)
                flag = false;
        }
        if(flag == false) throw new Exception("ERROR");

        //create new order and add it to data base
        DO.Order res= new DO.Order();
        res.OrderDate = DateTime.Now;
        int orderId = dal.Order.Create(res);

        foreach(var ot in cart.Items) 
        {
            try///make DO - orderItem, and try to add it to data base 
            {
                DO.OrderItem DOot = new DO.OrderItem();
                DOot.OrderID = orderId;
                DOot.ProductID = ot.ProductID;
                DOot.Amount = ot.Amount;
                DOot.Price = ot.TotalPrice;
                dal.OrderItem.Create(DOot);
                
            }
            catch(Exception ex) 
            {

            }
            try//subtruct the amount of order from DO - product, and update product in data base
            {
                DO.Product p = dal.Product.Read(ot.ProductID);
                p.InStock -= ot.Amount;
                dal.Product.Update(p);
            }catch(Exception ex)
            {
            }
        }
    }



    public BO.OrderItem searchInCart(BO.Cart cart , int productId)//help function
    {
        BO.OrderItem res = cart.Items.Find(ot => ot.ProductID == productId);
        if(res == null)
        {
            return res = new BO.OrderItem();
        }
        return res;
    }
}
