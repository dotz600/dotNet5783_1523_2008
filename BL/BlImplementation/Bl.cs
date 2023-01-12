using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlImplementation;

sealed internal class Bl : IBl
{
    internal Bl()//cnsrt
    {
        Product = new Product();
        Cart = new Cart();
        Order = new Order();
        OrderForList = new OrderForList();
        OrderItem = new OrderItem();
        ProductForList = new ProductForList();
        ProductItem = new ProductItem();
    }
    public IProduct Product { get; }

    public ICart Cart { get; }

    public IOrder Order { get; }

    public IOrderForList OrderForList { get; }

    public IOrderItem OrderItem { get; }

    public IProductForList ProductForList { get; }

    public IProductItem ProductItem { get; }
}
