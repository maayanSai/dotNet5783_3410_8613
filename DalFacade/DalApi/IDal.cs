namespace DalApi;

/// <summary>
/// dal interface
/// </summary>
public interface IDal
{
    /// <summary>
    /// Product
    /// </summary>
    IProduct Product { get; }
    /// <summary>
    /// Order
    /// </summary>
    IOrder Order { get; }
    /// <summary>
    /// OrderItem
    /// </summary>
    IOrderItem OrderItem { get; }
}

