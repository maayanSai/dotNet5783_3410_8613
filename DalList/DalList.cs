namespace Dal;
using DalApi;

/// <summary>
/// Dal List
/// </summary>
sealed internal class DalList : IDal
{
    /// <summary>
    /// A static property named Instance and initialized to an object of the class itself
    /// </summary>
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    /// <summary>
    /// order
    /// </summary>
    public IOrder Order { get; } = new DalOrder();
    /// <summary>
    /// product
    /// </summary>
    public IProduct Product { get; } = new DalProduct();
    /// <summary>
    /// ordetItem
    /// </summary>
    public IOrderItem OrderItem { get; } = new DalOrderItem();
}
