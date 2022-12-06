namespace BO;
using DO;


public class OrderItem
{
    public int ID { get; set; } // The ID number of the orderItem
    public int ProductID { get; set; } // The ID product of the orderItem
    public double Price { get; set; } // The price of the orderItem
    public int Amount { get; set; } // The amount of the orderItem
    public double Totalprice { get; set; } // מחיר סופי
    public string? Name { get; set; } // שם מוצר

    // An action that prints the orderItem
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
