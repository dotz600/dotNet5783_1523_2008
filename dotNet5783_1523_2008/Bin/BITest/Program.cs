using BlApi;
using BlImplementation;
using BO;

namespace BITest;

internal class Program
{
    static void Main(string[] args)
    {
        BO.Cart c = new BO.Cart();//the cart is the same for the buyer from start to end 
        int X = -1, classType = 0;
        Console.WriteLine("Hello!");
        do
        {
            try
            {
                classType = getObjType(ref X);
                switch (classType)
                {
                    case 0:                                                ///exit
                        Console.WriteLine("Bye!");
                        break;
                    case 1:
                        productFunctions(ref c);                                  ///product Obj
                        break;
                    case 2:
                        orderFunctions();                                    ///order Obj
                        break;
                    case 3:
                        cartFunctions(ref c);                             //cart Obj
                        break;
                    default:
                        Console.WriteLine("Try agian");
                        X= -1;
                        break;
                }
            }
            catch(NegativeIDException ex) 
            {
                Console.WriteLine(ex);
            }
            catch (EmptyNameException ex)
            {
                Console.WriteLine(ex);

            }
            catch (NegativePriceException ex)
            {
                Console.WriteLine(ex);

            }
            catch (NegativeAmountException ex)
            {
                Console.WriteLine(ex);

            }
            catch (ReadObjectFailedException ex)
            {
                Console.WriteLine(ex);

            }
            catch (CreateObjectFailedException ex)
            {
                Console.WriteLine(ex);
            }
            catch (UpdateObjectFailedException ex)
            {
                Console.WriteLine(ex);
            }
            catch (ObjectNotExistException ex)
            {
                Console.WriteLine(ex);
            }
            catch (ProductFoundInOrderException ex)
            {
                Console.WriteLine(ex);
            }
        }
        while (X != 0);
    }

    public static int getObjType(ref int X)//done
    {
        Console.WriteLine("For Product enter 1\n" + "For Order enter 2\n" + "For Cart enter 3\nPress 0 to exit...");
        bool checkInput = int.TryParse(Console.ReadLine(), out int classType);

        if (classType > 3 || classType < 1 || !checkInput)
        {
            if (checkInput)///that mean we get good int but not in the range 1 - 3
                X = 0;
            else ///its just error input
            {
                Console.WriteLine("Try again...");
            }
        }
        return classType;
    }

    public static void productFunctions(ref BO.Cart c)
    {
        IBl bl = new Bl();
        Console.WriteLine("Select the desired test:\n" +
            "* To add object enter 1\n" +
           "* To delete one object enter 2\n" +
           "* To read a one object as a manger enter 3\n" +
           "* To read a one object as a buyer enter 4\n" +
            "* To read all list of object 5\n" +
           "* To update object enter 6");

        bool checkInput = int.TryParse(Console.ReadLine(), out int x);
        if (!checkInput)
            x = -1;

        switch (x)
        {
            case 1://add product
                BO.Product p = new BO.Product();
                inputProduct(ref p);
                bl.Product.Create(p);
                Console.WriteLine("Operation succeeded.");
                break;
            case 2://delete
                Console.WriteLine("Please enter a product id to delete (only for manager)");
                int productId = Convert.ToInt32(Console.ReadLine());
                bl.Product.Delete(productId);
                Console.WriteLine("Operation succeeded.");
                break;
            case 3://Read manager
                Console.WriteLine("Please enter a product id to read");
                int Id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(bl.Product.Read(Id)); //as a manager
                break;
            case 4://Read buyer
                Console.WriteLine("Please enter a product id to read from cart");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(bl.Product.Read(id, c)); //as a buyer
                break;
            case 5://print all product as product for list
                foreach (var pl in bl.Product.ReadAll())
                    Console.WriteLine(pl);
                break;
            case 6:
                BO.Product p1 = new BO.Product();
                inputProduct(ref p1);
                bl.Product.Update(p1);
                Console.WriteLine(bl.Product.Read(p1.ID));
                Console.WriteLine("Operation succeeded.");
                break;
        }
    }
    public static void orderFunctions()
    {
        IBl bl = new Bl();
        Console.WriteLine("Select the desired test:\n" +
         "* To read all list of (for manger) object enter 1\n" +
        "* To read a one object enter 2\n" +
        "* To update shipping date enter 3\n" +
         "* To update delivery date enter 4\n" +
           "* To track an order enter 5");

        bool checkInput = int.TryParse(Console.ReadLine(), out int x);
        if (!checkInput)
            x = -1;

        switch (x)
        {
            case 1: //Read all for manger screen
                foreach (var ol in bl.Order.ReadAll())
                    Console.WriteLine(ol);
                break;
            case 2: //Read one order for buyer and manger
                Console.WriteLine("Please enter a order id to read");
                int id = Convert.ToInt32(Console.ReadLine());
                var t = bl.Order.Read(id);
                Console.WriteLine(t);
                if (t.Items != null && t.Items.Capacity > 0)
                {
                    Console.WriteLine("    Items:");
                    foreach (var t1 in t.Items)
                        Console.WriteLine(t1);
                }
                break;
            case 3://update shipping 
                Console.WriteLine("Please enter a order id to update shipping");
                int orderId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(bl.Order.UpdateShipping(orderId));
                break;
            case 4://update delivery
                Console.WriteLine("Please enter a order id to update delivery");
                int orId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(bl.Order.UpdateDelivery(orId));
                break;
            case 5://track order
                Console.WriteLine("Please enter a order id track your order");
                int idO = Convert.ToInt32(Console.ReadLine());
                var to = bl.Order.TrackingOrder(idO);
                Console.WriteLine(to);
                if (to.Events != null)
                {
                    Console.WriteLine("    Events:");
                    foreach (var t1 in to.Events)
                        Console.WriteLine(t1);
                }
                break;

        }

    }
    public static void cartFunctions(ref BO.Cart c)
    {
        IBl bl = new Bl();
        int productId, amount;
        string name, email, adress;
        Console.WriteLine("Select the desired test:\n" +
         "* To add object enter 1\n" +
        "* To confirm cart enter 2\n" +
         "* To update object enter 3");

        bool checkInput = int.TryParse(Console.ReadLine(), out int x);
        if (!checkInput)
            x = -1;

        switch (x)
        {
            case 1: //add product to cart
                Console.WriteLine("Please enter a product ID to add to cart");
                productId = Convert.ToInt32(Console.ReadLine());
                bl.Cart.Add(c, productId);
                Console.WriteLine("Operation succeeded.");
                break;
            case 2://confirm order
                Console.WriteLine("Please enter a your -\n name, email, adress");
                var line1 = Console.ReadLine();
                var data1 = line1.Split(' ');/// add all the input data to array, split each elemnt by space
                name = data1[0];
                email = data1[1];
                adress = data1[2];
                bl.Cart.CartConfirmation(c, name, email, adress);
                Console.WriteLine("Operation succeeded.");
                break;
            case 3://update item in cart
                Console.WriteLine("Please enter a product ID and the amount you wish to update");
                var line = Console.ReadLine();
                var data = line.Split(' ');/// add all the input data to array, split each elemnt by space
                productId = int.Parse(data[0]);
                amount = int.Parse(data[1]);
                bl.Cart.Update(c, productId, amount);
                Console.WriteLine(c);
                break;
        }
    }


    // input fuctions 

    public static void inputCart(ref BO.Cart c1)///get input for all OrderItem details 
    {
        Console.WriteLine("Please Enter Cart details: \n" +
            "CustomerName, CustomerEmail, CustomerAdress, Total Price");
        var line = Console.ReadLine();
        var data = line.Split(' ');/// add all the input data to array, split each elemnt by space
        c1.CustomerName = data[0];
        c1.CustomerEmail = data[1];
        c1.CustomerAddress = data[2];
        c1.TotalPrice = double.Parse(data[3]);
    }

    static void InputOrder(ref BO.Order o1)///get input for all Order details
    {
        Console.WriteLine("Please Enter Order details: \n" +
            "OrderID, CustomerName, CustomerEmail, CustomerAdress" +
            " , OrderDate, PaymentDate, ShipDate, DeliveryDate, Order Status,  TotalPrice");

        var line = Console.ReadLine();
        var data = line.Split(' ');
        o1.ID = int.Parse(data[0]);
        o1.CustomerName = data[1];
        o1.CustomerEmail = data[2];
        o1.CustomerAdress = data[3];
        o1.OrderDate = DateTime.Parse(data[4]);
        o1.PaymentDate = DateTime.Parse(data[5]);
        o1.ShipDate = DateTime.Parse(data[6]);
        o1.DeliveryDate = DateTime.Parse(data[7]);
        OrderStatus stat;
        Enum.TryParse(data[8], out stat);
        o1.Status = stat;
        o1.TotalPrice = int.Parse(data[9]);
    }
    public static void inputProduct(ref BO.Product p)///get input for all Product details
    {
        Console.WriteLine("Please Enter Product details:\n" +
            "ProductID, Name, Price, Category, InStock");
        var line = Console.ReadLine();
        var data = line.Split(' ');
        p.ID = int.Parse(data[0]);
        p.Name = data[1];
        p.Price = double.Parse(data[2]);
        BO.Categories category;
        Enum.TryParse(data[3], out category);
        p.Category = category;
        p.InStock = int.Parse(data[4]);
    }
}
