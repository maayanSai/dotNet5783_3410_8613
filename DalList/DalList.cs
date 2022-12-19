namespace Dal;
using DalApi;

/// <summary>
/// Dal List
/// </summary>
sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    /// <summary>
    /// order
    /// </summary>
    public IOrder Order => new DalOrder();
    /// <summary>
    /// product
    /// </summary>
    public IProduct Product => new DalProduct();
    /// <summary>
    /// ordetItem
    /// </summary>
    public IOrderItem OrderItem => new DalOrderItem();
}
