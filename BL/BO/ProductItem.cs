
namespace BO;

public class ProductItem
{
    public int ID { get; set; } // The ID number of the orderItem
    public string? Name { get; set; } // The name of the product
    public double Price { get; set; } // The price of the product
    public int Amount { get; set; } // 
    public Category Category { get; set; } // The kind of the product
    public bool isStock { get; set; }
    // An action that prints the order
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
