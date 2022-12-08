namespace BO;

/// <summary>
/// Shopping cart
/// </summary>
public class Cart
{
    /// <summary>
    /// The name of customer of the order
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// The email of customer of the order
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// The adress of customer of the order
    /// </summary>
    public string? CustomerAdress { get; set; }
    /// <summary>
    /// A collection of items
    /// </summary>
    public List<BO.OrderItem>?  Items { set; get; }
    /// <summary>
    /// Final price of a shopping basket
    /// </summary>
    public double TotalPrice { set; get; }
    /// <summary>
    /// An action that prints the order
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.ToStringProperty();
}
