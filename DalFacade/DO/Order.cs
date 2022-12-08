namespace DO;

/// <summary>
/// struct of order
/// </summary>
public struct Order
{
    /// <summary>
    /// The ID number of the order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// The name of customer of the order
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// The email of customer of the order
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    /// The adress of customer of the order
    /// </summary>
    public string CustomerAdress { get; set; }
    /// <summary>
    /// Time of placing the order 
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// Delivery time
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// Shipping arrival time
    /// </summary>
    public DateTime? DeliveryrDate { get; set; } 
    /// <summary>
    /// amount of product
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// An action that prints the order
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.ToStringProperty();
}

