namespace BO;

/// <summary>
/// A product reference entity in the list
/// </summary>
public class ProductForList
{
    /// <summary>
    /// ID (product)
    /// </summary>
    public int ID { set; get; }
    /// <summary>
    /// product name
    /// </summary>
    public string ?Name { set; get; }
    /// <summary>
    /// product price
    /// </summary>
    public double Price { set; get; }
    /// <summary>
    /// The kind of the product
    /// </summary>
    public Category Category { get; set; } 
    /// <summary>
    /// An action that prints the orderItem
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.ToStringProperty();
}

