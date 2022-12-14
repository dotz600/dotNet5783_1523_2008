
//markus tzama 320411523
//david ohev tzion 206672008
//super market store stage 1
using DO;
using Dal;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;
using DalApi;
using System.Diagnostics.Metrics;

class MyMain
{
    public static void Main()
    {
      
        int X = -1, classType;
        Console.WriteLine("Hello!");
        do
        {
            try
            {
                 classType = MainInput(ref X);//get input from the user
                 MainSwitch(X, classType);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        while (X != 0);

    }

    private static void MainSwitch(int X, int classType)
    {
        int id;
        IDal? Obj = Factory.Get(); 
        ///variable for input data from user
        Product product = new();
        Order order = new();
        OrderItem orderItem = new();

        switch (X)
        {
            case 0:                                                ///exit
                Console.WriteLine("Bye!");
                break;
            case 1:                                                ///add obj to list
                if (classType == 1)
                {
                    InputProduct(ref product);
                    Obj?.Product.Create(product);///create new product in the data base 
                }
                if (classType == 2)
                {
                    InputOrder(ref order);
                    Console.WriteLine("your new order ID is: " + Obj?.Order.Create(order));
                }
                if (classType == 3)
                {
                    InputOrderItem(ref orderItem);
                    Obj?.OrderItem.Create(orderItem);
                }
                break;
            case 2:                                                  ///read obj by ID
                Console.WriteLine("Enter the object ID number");
                id = Convert.ToInt32(Console.ReadLine());
                if (classType == 1)
                {
                    Console.WriteLine(Obj?.Product.Read(id));///read the product from DataSource and print it
                }
                if (classType == 2)
                {
                    Console.WriteLine(Obj?.Order.Read(id));
                }
                if (classType == 3)
                {
                    Console.WriteLine(Obj?.OrderItem.Read(id));
                }
                break;
            case 3:                                                   //read obj list an print it all
                if (classType == 1)
                {
                    foreach (Product? p in Obj?.Product.ReadAll()!)
                        if (p != null)
                            Console.WriteLine(p);
                }
                if (classType == 2)
                {
                    foreach (Order? or in Obj?.Order.ReadAll()!)
                        if (or != null)
                            Console.WriteLine(or);
                }
                if (classType == 3)
                {
                    foreach (OrderItem? it in Obj?.OrderItem.ReadAll()!)
                        if (it != null)
                            Console.WriteLine(it);             
                }
                break;
            case 4:                                                    ///update obj
                Console.WriteLine("Enter the object details you wish to update");
                if (classType == 1)
                {
                    InputProduct(ref product);///get the obj data
                    Obj?.Product.Update(product);///update the object, except from the ID
                }
                if (classType == 2)
                {
                    InputOrder(ref order);
                    Obj?.Order.Update(order);
                }
                if (classType == 3)
                {
                    InputOrderItem(ref orderItem);
                    Obj?.OrderItem.Update(orderItem);
                }
                break;
            case 5:                                                    ///delete from list
                Console.WriteLine("Enter the object ID number");
                id = Convert.ToInt32(Console.ReadLine());
                if (classType == 1)
                {
                    Obj?.Product.Delete(id);
                }
                if (classType == 2)
                {
                    Obj?.Order.Delete(id);
                }
                if (classType == 3)
                {
                    Obj?.OrderItem.Delete(id);
                }
                break;

        }
    }



    private static int MainInput(ref int X)
    {
        bool checkInput;
        Console.WriteLine("For Product enter 1\n" +
                                      "For Order enter 2\n" +
                                     "For OrderItem enter 3");
        checkInput = int.TryParse(Console.ReadLine(), out int classType);
        if (classType > 3 || classType < 1 || !checkInput)
           {
            if (checkInput)///that mean we get good int but not in the range 1 - 3
                X = 0;
            else ///its just error input
            Console.WriteLine("Try again...");
            }
        else///we got correct input in range 1-3
        {
            Console.WriteLine("Select the desired test:\n" +
                "To add object enter 1\n" +
               "To read one object enter 2\n" +
               "To read a list of objects enter 3\n" +
               "To update object enter 4\n" +
               "To delete an object from the list enter 5");
            checkInput = int.TryParse(Console.ReadLine(), out X);
            if (!checkInput) X = -1;
        }

        return classType;///and return X by refrence
    }

    public static void InputOrderItem(ref OrderItem o)///get input for all OrderItem details 
    {
        Console.WriteLine("OrderItem: ProductID, OrderID, Price, Amount");
        var line = Console.ReadLine();
        var data = line?.Split(' ');/// add all the input data to array, split each elemnt by space
        o.ProductID = int.Parse(data[0]);
        o.OrderID = int.Parse(data[1]);
        o.Price = double.Parse(data[2]);
        o.Amount = int.Parse(data[3]);
    }

    public static void InputOrder(ref Order o)///get input for all Order details
    {
        Console.WriteLine("Order: OrderID, CustomerAdress, CustomerEmail, CustomerName, DeliveryDate, OrderDate, ShipDate");
        var line = Console.ReadLine();
        var data = line?.Split(' ');
        o.ID = int.Parse(data[0]);
        o.CustomerAdress = data[1];
        o.CustomerEmail = data[2];
        o.CustomerName = data[3];
        o.DeliveryDate = DateTime.Parse(data[4]);
        o.OrderDate = DateTime.Parse(data[5]);
        o.ShipDate = DateTime.Parse(data[6]);

    }
    public static void InputProduct(ref Product p)///get input for all Product details
    {
        Console.WriteLine("Product: ProductID, Name, Price, Category, InStock");
        var line = Console.ReadLine();
        var data = line?.Split(' ');
        p.ID = int.Parse(data[0]);
        p.Name = data[1];
        p.Price = double.Parse(data[2]);
        if(!Enum.TryParse(data[3], out Categories category))
            throw new NullReferenceException();
        p.Category = category;
        p.InStock = int.Parse(data[4]);
    }

}

