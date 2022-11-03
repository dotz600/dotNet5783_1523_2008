namespace DO;

public struct Product
{
    int ID { get; set; }
    string name { get; set; }
    double price { get; set; }
    Category category { get; set; }
    int inStock { get; set; }
}
