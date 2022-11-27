using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///for mange product details, and action on product
namespace BO;

public class Product
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Categories Category { get; set; }
    public int InStock { get; set; }
    public override string ToString() => $@"
    BO.Product,
    ID : {ID},
    Name : {Name},
    Price : {Price} 
    Category : {Category}
    InStock : {InStock}
    ";
}


    


