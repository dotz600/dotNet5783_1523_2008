

using DO;
using Dal;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;

class MyMain
{
    public static void test()
    {

    }
    public static void Main(string[] args)
    {
        int X = -1, classType = 0;
        Console.WriteLine("Hello!");
        do
        {
            try
            {
                 classType = mainInput(ref X);
                 mainSwitch(X, classType);
            }
            catch(InvalidCastException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        while (X != 0);

    }

    private static void mainSwitch(int X, int classType)
    {
        int id;
        DalProduct dalProduct = new DalProduct();
        Product product = new Product();
        DalOrder dalOrder = new DalOrder();
        Order order = new Order();
        DalOredrItem dalOrderItem = new DalOredrItem();
        OrderItem orderItem = new OrderItem();
       
        switch (X)
        {
            case 0:                                                //exit
                Console.WriteLine("Bye!");
                break;
            case 1:                                                //add obj to list
                if (classType == 1)
                {
                    inputProduct(ref product);
                    dalProduct.Create(product);
                }
                if (classType == 2)
                {
                    inputOrder(ref order);
                    dalOrder.Create(order);
                }
                if (classType == 3)
                {
                    inputOrderItem(ref orderItem);
                    dalOrderItem.Create(orderItem);
                }
                break;
            case 2:                                                  //read obj by ID
                Console.WriteLine("Enter the object ID number");
                id = Convert.ToInt32(Console.ReadLine());
                if (classType == 1)
                {
                    dalProduct.Print(dalProduct.Read(id));
                }
                if (classType == 2)
                {
                    dalOrder.Print(dalOrder.Read(id));
                }
                if (classType == 3)
                {
                    dalOrderItem.Print(dalOrderItem.Read(id));
                }
                break;
            case 3:                                                   //read obj list
                if (classType == 1)
                {
                    Product[] pr = dalProduct.ReadAll();
                    for (int i = 0; i < dalProduct.ReadAll().Length; i++)
                    {
                        dalProduct.Print(pr[i]);
                    }
                }
                if (classType == 2)
                {
                    foreach (Order or in dalOrder.ReadAll())
                    {
                        dalOrder.Print(or);
                        Console.WriteLine(' ');
                    }
                }
                if (classType == 3)
                {
                    OrderItem[] allOrderItems = dalOrderItem.ReadAll();
                    for (int i = 0; i < dalOrderItem.ReadAll().Length; i++)
                    {
                        dalOrderItem.Print(allOrderItems[i]);
                    }
                }
                break;
            case 4:                                                    //update obj
                Console.WriteLine("Enter the object details you wish to update");
                if (classType == 1)
                {
                    inputProduct(ref product);
                    dalProduct.Update(product);
                }
                if (classType == 2)
                {
                    inputOrder(ref order);
                    dalOrder.Update(order);
                }
                if (classType == 3)
                {
                    inputOrderItem(ref orderItem);
                    dalOrderItem.Update(orderItem);
                }
                break;
            case 5:                                                    //delete from list
                Console.WriteLine("Enter the object ID number");
                id = Convert.ToInt32(Console.ReadLine());
                if (classType == 1)
                {
                    dalProduct.Delete(id);
                }
                if (classType == 2)
                {
                    dalOrder.Delete(id);
                }
                if (classType == 3)
                {
                    dalOrderItem.Delete(id);
                }
                break;

        }
    }



    private static int mainInput(ref int X)
    {
        int classType;
        bool checkInput;
        Console.WriteLine("For Product enter 1\n" +
                                      "For Order enter 2\n" +
                                      "For OrderItem enter 3");
        checkInput = int.TryParse(Console.ReadLine(), out classType);
        if (classType > 3 || classType < 1 || !checkInput)
            Console.WriteLine("Try again...");
        else
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

        return classType;
    }


    public static void inputOrderItem(ref OrderItem o)
    {
        var line = Console.ReadLine();
        var data = line.Split(' ');
        o.ProductID = int.Parse(data[0]);
        o.OrderID = int.Parse(data[1]);
        o.Price = double.Parse(data[2]);
        o.Amount = int.Parse(data[3]);
    }

    public static void inputOrder(ref Order o)
    {
        var line = Console.ReadLine();
        var data = line.Split(' ');
        o.ID = int.Parse(data[0]);
        o.CustomerAdress = data[1];
        o.CustomerEmail = data[2];
        o.CustomerName = data[3];
        o.DeliveryDate = DateTime.Parse(data[4]);
        o.OrderDate = DateTime.Parse(data[5]);
        o.ShipDate = DateTime.Parse(data[6]);

    }
    public static void inputProduct(ref Product p)
    {
        var line = Console.ReadLine();
        var data = line.Split(' ');
        p.ID = int.Parse(data[0]);
        p.Name = data[1];
        p.Price = double.Parse(data[2]);
        Categories category;
        Enum.TryParse(data[3], out category);
        p.category = category;
        p.InStock = int.Parse(data[4]);
    }

}

