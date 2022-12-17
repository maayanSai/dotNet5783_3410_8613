namespace BO;

/// <summary>
/// Features of an order
/// </summary>
public class Order
{
    /// <summary>
    /// The ID number of the order
    /// </summary>
    public int ID { get; set; }
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
    /// Order status
    /// </summary>
    public OrderStatus? Status { set; get; }
    /// <summary>
    /// Time of placing the order   
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// Shipping arrival time
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// Delivery time
    /// </summary>
    public DateTime? DeliveryrDate { get; set; }
    /// <summary>
    /// Final price of an order
    /// </summary>
    public double TotalPrice { set; get; }
    /// <summary>
    /// A collection of items
    /// </summary>
    public List<OrderItem>? Items { set; get; }
    /// <summary>
    /// To String Property
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.ToStringProperty();
}
