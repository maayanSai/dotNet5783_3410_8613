namespace BO;

/// <summary>
/// Order tracking utility
/// </summary>
public class OrderTracking
{
    /// <summary>
    /// ID(order)
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Order status
    /// </summary>
    public OrderStatus Status { set; get; }
    /// <summary>
    /// List of pairs (date, package progress description)
    /// </summary>
    public List<Tuple<DateTime?, string?>>? Tracking { set; get; }
    /// <summary>
    /// An action that prints the orderItem
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.ToStringProperty();
}
