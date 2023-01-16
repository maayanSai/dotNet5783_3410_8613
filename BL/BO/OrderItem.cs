namespace BO;

/// <summary>
/// A helper entity of an order item
/// </summary>
public class OrderItem
{
    /// <summary>
    /// The ID number of the orderItem
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// The ID product of the orderItem
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// The price of the orderItem
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// The amount of the orderItem
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    ///  Total price
    /// </summary>
    public double Totalprice { get; set; }
    /// <summary>
    /// name of product
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// An action that prints the orderItem
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.ToStringProperty();

}
