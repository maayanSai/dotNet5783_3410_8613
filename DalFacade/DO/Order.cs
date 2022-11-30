namespace DO;

/// <summary>
/// struct of order
/// </summary>
public struct Order
{
    public int ID { get; set; } // The ID number of the order
    public string CustomerName { get; set; } // The name of customer of the order
    public string CustomerEmail { get; set; } // The email of customer of the order
    public string CustomerAdress { get; set; } // The adress of customer of the order
    public DateTime OrderDate { get; set; } // Time of placing the order                                     
    public DateTime ShipDate { get; set; } // Delivery time
    public DateTime DeliveryrDate { get; set; } // Shipping arrival time

    // An action that prints the order
    public override string ToString() => $@" 
      Order ID: {ID}
      Customer's Name: {CustomerName}
      Customer's Email: {CustomerEmail}
      Order Date: {OrderDate}
      Ship Date: {ShipDate}
      Delivery Date: {DeliveryrDate}";
}

