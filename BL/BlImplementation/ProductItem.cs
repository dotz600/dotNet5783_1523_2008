using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

internal class ProductItem : IProductItem
{
    private readonly DalApi.IDal? Dal = DalApi.Factory.Get();

    public IEnumerable<BO.ProductItem> ReadAll(Func<BO.ProductItem, bool>? predicate = null)
    {
        IEnumerable<BO.ProductItem> res ;
        res = from pro in Dal?.Product.ReadAll()
              where pro != null
              let tmp = PruductToProductItem((DO.Product)pro)//convert
              select tmp; 

        if (predicate != null )//sort by predicate
            res = from p in res
                  where(predicate(p))
                  select p;
        
        return res;
    }
    private BO.ProductItem PruductToProductItem(DO.Product p)//convert Pruduct To Product Item
    {
        var res =  new BO.ProductItem()
        {
            ID = p.ID,
            Name = p.Name,
            Category = (BO.Categories)p.Category,
            Price = p.Price,
            Amount = p.InStock,
            InStock = false
        };

        if(p.InStock > 0)
            res.InStock = true;

        return res;
    }
}
