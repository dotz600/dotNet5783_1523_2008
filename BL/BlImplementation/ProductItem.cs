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
        List<BO.ProductItem> res = new();
        foreach (var pro in Dal?.Product.ReadAll() ?? throw new Exception())
        {
            DO.Product p = pro ?? throw new NullReferenceException();
            BO.ProductItem temp = new()
            {
                ID = p.ID,
                Name = p.Name,
                Category = (BO.Categories)p.Category,
                Price = p.Price,
                Amount = p.InStock,
                InStock = false
            } ;

            if (p.InStock > 0)
                temp.InStock = true;

            res.Add(temp);
        }
        if(predicate != null )
            res.RemoveAll(x => !predicate(x));

        return res;
    }

}
