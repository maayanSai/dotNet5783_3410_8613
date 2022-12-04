
namespace BO;

public class Cart
{
    public string? CustomerName { get; set; } // The name of customer of the order
    public string? CustomerEmail { get; set; } // The email of customer of the order
    public string? CustomerAdress { get; set; } // The adress of customer of the order
    public IEnumerable<OrderItem>? Items { set; get; }
    public double TotalPrice { set; get; }
    // An action that prints the order
    public override string ToString() => this.ToStringProperty();

}
