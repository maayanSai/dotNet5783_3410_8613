
namespace BO;

internal class OrderForList
{
    public int ID { get; set; }
    public string? CustomerName { get; set; } // The name of customer of the order
    public OrderStatus Status { set; get; }
    public double TotalPrice { set; get; }
    public int Amount { get; set; } // 

    // An action that prints the orderItem
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
