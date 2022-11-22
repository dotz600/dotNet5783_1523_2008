using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIApi;
using BO;

namespace BIImplementation;

sealed public class Bl : IBl
{
    public IProduct Product => new Product();

    public ICart Cart => new Cart();

    public IOrder Order => new Order();

    public IOrderForList OrderForList => new OrderForList();

    public IOrderItem OrderItem => new OrderItem();

    public IProductForList ProductForList => new ProductForList();

    public IProductItem productItem => new ProductItem();
}
