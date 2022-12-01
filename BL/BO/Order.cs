namespace BO;
public class Order
{
    public int ID { get; set; } // The ID number of the order
    public string? CustomerName { get; set; } // The name of customer of the order
    public string? CustomerEmail { get; set; } // The email of customer of the order
    public string? CustomerAdress { get; set; } // The adress of customer of the order
    public OrderStatus Status { set; get; }
    public DateTime? OrderDate { get; set; } // Time of placing the order                                     
    public DateTime? ShipDate { get; set; } // Delivery time
    public DateTime? DeliveryrDate { get; set; } // Shipping arrival time
    public double TotalPrice { set; get; }
    public IEnumerable<OrderItem>? Items { set; get; }
    public override string ToString()
    {
        return this.DaLFacade.DO.ToStringProperty();
    }
}
