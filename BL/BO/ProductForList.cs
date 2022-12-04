
namespace BO;

public class ProductForList
{
    public int ID { set; get; }
    public string? Name { set; get; }
    public double Price { set; get; }
    public Category Category { get; set; } // The kind of the product
                                           // An action that prints the orderItem
    public override string ToString() => this.ToStringProperty();
}

