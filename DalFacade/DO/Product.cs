namespace DO;

/// <summary>
/// struct of product
/// </summary>
public class Product
{
    public int ID { get; set; } // The ID number of the product
    public string Name { get; set; } // The name of the product
    public double Price { get; set; } // The price of the product
    public Category Category { get; set; } // The kind of the product
    public int InStock { get; set; } // The amount of the product in stock

    // An action that prints the order
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
