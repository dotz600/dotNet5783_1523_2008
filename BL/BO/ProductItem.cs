using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// for catalog screen - with list of products that the buyer can see
namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Categories? Category { get; set; }
    public int AmountInCart { get; set; }
    public bool InStock { get; set; }

    public override string ToString() => $@"
  - BO.ProductItem
    ID : {ID},
    Name : {Name},
    Price : {Price:f} ,
    Category : {Category},
    InStock : {InStock},
    Amount {AmountInCart}";
}
