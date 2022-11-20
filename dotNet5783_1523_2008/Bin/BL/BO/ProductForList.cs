using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///For the product list screen and catalog screen
namespace BO;

public class ProductForList
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Categories Category { get; set; } 

    public override string ToString() => $@"
    BO.ProductForList,
    ID : {ID},
    Name : {Name}, Price : {Price} , Category : {Category},
    ";
}
