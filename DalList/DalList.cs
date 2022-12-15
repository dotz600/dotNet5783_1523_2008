using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal;

sealed internal class DalList : IDal
{
    private DalList() 
    {
        Product = new DalProduct(); 
        Order= new DalOrder();
        OrderItem = new DalOredrItem();
    }
    public static IDal Instance { get; } = new DalList();

    public IProduct Product { get; }

    public IOrder Order { get; }

    public IOrderItem OrderItem { get; }
}
