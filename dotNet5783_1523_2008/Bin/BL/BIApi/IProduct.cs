

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BIApi;
/// <summary>
/// IProduct --- TO DO --- Complete the interface summary
/// </summary>
public interface IProduct 
{
    IEnumerable<ProductForList> ReadAll();
    Product Read(int id); //for manger screen

    ProductItem Read(int id, Cart myCart); //for buyer screen

    void Create(Product p);
    void Delete(int id);

    void Update(Product p);

}
