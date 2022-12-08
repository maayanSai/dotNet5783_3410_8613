namespace BO;

/// <summary>
/// A helper entity of a list order
/// </summary>
public class OrderForList
{
    /// <summary>
    /// ID (order)
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// The name of customer of the order
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// Order status
    /// </summary>
    public OrderStatus Status { set; get; }
    /// <summary>
    /// total price
    /// </summary>
    public double? TotalPrice { set; get; }
    /// <summary>
    /// amount of items
    /// </summary>
    public int Amount { get; set; } 
    /// <summary>
    /// An action that prints the orderItem
    /// </summary>
    /// <returns></returns>
    public override string ToString()=> this.ToStringProperty();
}
