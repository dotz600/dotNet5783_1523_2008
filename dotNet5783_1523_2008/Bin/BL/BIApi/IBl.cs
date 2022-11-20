using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIApi;

public interface IBl
{
    public IProduct Product { get;}
    public ICart Cart { get;}
    public IOrder Order { get;}
    public IOrderForList OrderForList { get;}
    public IOrderItem OrderItem { get;}
    public IProductForList ProductForList { get;}
    public IProductItem productItem { get;}

}
