using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal;

sealed internal class DalList : IDal
{
    private DalList() { }
    public static IDal Instance { get; } = new DalList();

    public IProduct Product => new DalProduct();

    public IOrder Order => new DalOrder();

    public IOrderItem OrderItem => new DalOredrItem();
}
