using DalApi;
namespace Dal;

/// <summary>
/// Dal List
/// </summary>
sealed public class DalList : IDal
{
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
