namespace DO;

/// <summary>
/// struct of orderItem
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// The ID number of the orderItem
    /// </summary>
    public int ID { get; set; } 
    /// <summary>
    /// The ID product of the orderItem
    /// </summary>
    public int ProductID { get; set; }  
    /// <summary>
    /// The ID order of the orderItem
    /// </summary>
    public int OrderID { get; set; }  
    /// <summary>
    /// The price of the orderItem
    /// </summary>
    public double Price { get; set; } 
    /// <summary>
    /// The amounr of the orderItem
    /// </summary>
    public int Amount { get; set; } 
    /// <summary>
    /// An action that prints the orderItem
    /// </summary>
    /// <returns></returns>
    public override string ToString()=> this.ToStringProperty();
}
