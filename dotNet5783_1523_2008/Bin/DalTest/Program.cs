//תכנית ראשית

using DO;
using Dal;
using System.Data.Common;

class myClass
{

    static int Main(string[] args)
    {
        DalProduct p = new DalProduct();
        p.addProduct(new Product { category = Categories.Dairy, ID = 1211, InStock = 0, Name = "dooo", Price = 5});
        
        return 0;
    }
}
