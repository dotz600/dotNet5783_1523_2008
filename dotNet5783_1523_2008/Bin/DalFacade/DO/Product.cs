using System.ComponentModel;

namespace DO;

public struct Product
{
    int ID { get; set; }
    string Name { get; set; }
    double Price { get; set; }
    Category category { get; set; }
    int InStock { get; set; }
}
