namespace DO;

/// <summary>
/// struct of orderItem
/// </summary>
public struct OrderItem
{
    public int ID { get; set; } // The ID number of the orderItem
    public int ProductID { get; set; } // The ID product of the orderItem
    public int OrderID { get; set; } // The ID order of the orderItem
    public double Price { get; set; } // The price of the orderItem
    public int Amount { get; set; } // The amounr of the orderItem
    

    // An action that prints the orderItem
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
