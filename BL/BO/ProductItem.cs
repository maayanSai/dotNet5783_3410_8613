namespace BO;

/// <summary>
/// A product item reference entity
/// </summary>
public class ProductItem
{
    /// <summary>
    /// The ID number of the orderItem
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// The name of the product
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// The price of the product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// The kind of the product
    /// </summary>
    public Category? Category { get; set; }
    /// <summary>
    /// Availability (Is it in stock)
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// An action that prints the order
    /// </summary>
    /// <returns></returns>
      public bool isStock { get; set; }
    public string? ImageRelativeName { get; set; }
    public override string ToString() => this.ToStringProperty();
}
