using System.ComponentModel;

namespace DO;

/// <summary>
/// Product is an object that represent product in the store
/// fill the product details such as category, name, price, ID and more.
/// </summary>
public struct Product
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Category? category { get; set; }
    public int InStock { get; set; }
   

    public override string ToString() => $@"
        Product ID={ID}: {Name},
        category - {category}
        Price: {Price}
        Amount in stock: {InStock}
        "; 


}

