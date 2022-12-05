namespace DO;

/// <summary>
/// struct of order
/// </summary>
public class Order
{
    /// <summary>
    /// The ID number of the order
    /// </summary>
    public int ID { get; set; }
    public string CustomerName { get; set; } // The name of customer of the order
    public string CustomerEmail { get; set; } // The email of customer of the order
    public string CustomerAdress { get; set; } // The adress of customer of the order
    public DateTime OrderDate { get; set; } // Time of placing the order                                     
    public DateTime ShipDate { get; set; } // Delivery time
    public DateTime DeliveryrDate { get; set; } // Shipping arrival time

    // An action that prints the order
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}

