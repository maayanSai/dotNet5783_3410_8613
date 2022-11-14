
namespace DO;

public struct Order
{
    /// <summary>
    /// The ID number of the order
    /// </summary>
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryrDate { get; set; }
    public override string ToString() => $@"
      Order ID: {ID}
      Customer's Name: {CustomerName}
      Customer's Email: {CustomerEmail}
      Order Date: {OrderDate}
      Ship Date: {ShipDate}
      Delivery Date: {DeliveryrDate}";
}

