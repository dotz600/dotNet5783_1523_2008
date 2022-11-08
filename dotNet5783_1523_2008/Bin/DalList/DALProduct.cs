﻿using DalList;
using DO;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Dal;

public class DalProduct : ICrud<Product>
{
   public int Create(Product p1)
   {
        foreach (Product myPro in DataSource.productsArr)
        {
            if (myPro.ID == p1.ID)//if the ID exist in the array make new ID and check agian
                throw new Exception("product already exist");
        }
        DataSource.productsArr[DataSource.Config.productsSize] = p1;
        return p1.ID;
   }

    public Product Read(int id)
    {
        foreach(Product myPro in DataSource.productsArr)
        {
            if (myPro.ID == id)
                return myPro;
        }
        throw new Exception("Product dont found");
    }
    
    public Product[] ReadAll()
    {
        Product[] res = new Product[DataSource.Config.productsSize];

        for (int i = 0; i < DataSource.Config.productsSize; i++)//copy all the data
            res[i] = DataSource.productsArr[i];
       
        return res;  
    }

    public void Delete(int id)
    {
        DataSource.productsArr = DataSource.productsArr.Where(p => p.ID != id).ToArray();
        //throw new Exception("Product dont found");
    }
    public void Update(Product p1)
    {
        for (int i = 0; i < DataSource.Config.productsSize; i++)
        {
            if (DataSource.productsArr[i].ID == p1.ID)
                DataSource.productsArr[i] = p1;
        }
        throw new Exception("Product dont found");
    }

}