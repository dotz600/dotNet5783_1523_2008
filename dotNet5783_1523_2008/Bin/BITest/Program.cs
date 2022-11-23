using BIApi;
using BIImplementation;
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
            }

        }
        while (X != 0);
    }

    static int getObjType(ref int X)//done
    {
        Console.WriteLine("For Product enter 1\n" + "For Order enter 2\n" + "For Cart enter 3");
        bool checkInput = int.TryParse(Console.ReadLine(), out int classType);

        if (classType > 3 || classType < 1 || !checkInput)
        {
            if (checkInput)///that mean we get good int but not in the range 1 - 3
                X = 0;
            else ///its just error input
                Console.WriteLine("Try again...");
        }
        return classType;
    }

    public static void productFunctions(ref BO.Cart c)
    {
        IBl bl = new Bl();
        Console.WriteLine("Select the desired test:\n" +
            "To add object enter 1\n" +
           "To delete one object enter 2\n" +
           "To read a one object as a manger enter 3\n" +
           "To read a one object as a buyer enter 4\n" +
            "To read all list of object 5\n" +
           "To update object enter 6\n");

        bool checkInput = int.TryParse(Console.ReadLine(), out int x);
        if (!checkInput)
            x = -1;

        switch (x)
        {
            case 1:
                BO.Product p = new BO.Product();
                inputProduct(ref p);
                bl.Product.Create(p);
                break;
            case 2:
                Console.WriteLine("please enter a product id to delete (onley for manger)");
                int productId = Convert.ToInt32(Console.ReadLine());
                bl.Product.Delete(productId);
                break;
            case 3:
                Console.WriteLine("please enter a product id to read");
                int Id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(bl.Product.Read(Id).ToString()); //as a manger
                break;
            case 4:
                Console.WriteLine("please enter a product id to read from cart");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(bl.Product.Read(id, c).ToString()); //as a buyer
                break;
            case 5://print all product as product for list
                foreach (var pl in bl.Product.ReadAll())
                    Console.WriteLine(pl.ToString());
                break;
            case 6:
                BO.Product p1 = new BO.Product();
                inputProduct(ref p1);
                bl.Product.Update(p1);
                break;

        }
    }
    public static void orderFunctions()
    {
        IBl bl = new Bl();
        Console.WriteLine("Select the desired test:\n" +
         "To read all list of (for manger) object enter 1\n" +
        "To read a one object enter 2\n" +
        "To update shipping date enter 3\n" +
         "To update delivery date enter 4\n" +
           "To track an order enter 5\n");

        bool checkInput = int.TryParse(Console.ReadLine(), out int x);
        if (!checkInput)
            x = -1;

        switch (x)
        {
            case 1: //for manger screen
                foreach (var ol in bl.Order.ReadAll())
                    Console.WriteLine(ol.ToString());
                break;
            case 2: //for buyer and manger
                Console.WriteLine("please enter a order id to read");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(bl.Order.Read(id).ToString());
                break;
            case 3:
                Console.WriteLine("please enter a order id to update shipping");
                int orderId = Convert.ToInt32(Console.ReadLine());
                //Console.WriteLine(bl.Order.UpdateShipping(orderId).ToString());
                break;
            case 4:
                Console.WriteLine("please enter a order id to update delivery");
                int orId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(bl.Order.UpdateDelivery(orId).ToString());
                break;
            case 5:
                Console.WriteLine("please enter a order id to update delivery");
                int idO = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(bl.Order.TrackingOrder(idO).ToString());
                break;

        }

    }
    public static void cartFunctions(ref BO.Cart c)//done
    {
        IBl bl = new Bl();
        int productId, amount;
        string name, email, adress;
        Console.WriteLine("Select the desired test:\n" +
         "To add object enter 1\n" +
        "To confirm cart enter 2\n" +
         "To update object enter 3\n");

        bool checkInput = int.TryParse(Console.ReadLine(), out int x);
        if (!checkInput)
            x = -1;

        switch (x)
        {
            case 1: //add product to cart
                Console.WriteLine("please enter a product id to add to cart");
                productId = Convert.ToInt32(Console.ReadLine());
                bl.Cart.Add(c, productId);
                break;
            case 2:
                Console.WriteLine("please enter a your name, email, adress");
                var line1 = Console.ReadLine();
                var data1 = line1.Split(' ');/// add all the input data to array, split each elemnt by space
                name = data1[0];
                email = data1[1];
                adress = data1[2];
                bl.Cart.CartConfirmation(c, name, email, adress);
                break;
            case 3:
                Console.WriteLine("please enter a product id and the amount you wish to update");
                var line = Console.ReadLine();
                var data = line.Split(' ');/// add all the input data to array, split each elemnt by space
                productId = int.Parse(data[0]);
                amount = int.Parse(data[1]);
                bl.Cart.Update(c, productId, amount);
                break;

        }
    }


    // input fuctions 

    public static void inputCart(ref BO.Cart c1)///get input for all OrderItem details 
    {
        Console.WriteLine("Please Enter Cart details: CustomerName, CustomerEmail,CustomerAdress,Total Price");
        var line = Console.ReadLine();
        var data = line.Split(' ');/// add all the input data to array, split each elemnt by space
        c1.CustomerName = data[0];
        c1.CustomerEmail = data[1];
        c1.CustomerAddress = data[2];
        c1.TotalPrice = double.Parse(data[3]);
    }

    static void InputOrder(ref BO.Order o1)///get input for all Order details
    {
        Console.WriteLine("Please Enter Order details: OrderID, CustomerName, CustomerEmail,CustomerAdress" +
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
        Console.WriteLine("Please Enter Product details: ProductID, Name, Price, Category, InStock");
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
