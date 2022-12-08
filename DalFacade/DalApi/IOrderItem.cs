namespace DalApi;
using DO;

/// <summary>
/// Creating an interface of products
/// </summary>
public interface IOrderItem : ICrud<OrderItem>
{
    /// <summary>
    /// A function that returns a product by 2 ID numbers
    /// </summary>
    /// <param name="idProduct"></param>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    OrderItem? GetByTwoId(int idProduct, int idOrder);
    /// <summary>
    /// A function that returns a product by order ID number
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    IEnumerable<OrderItem?> GetByOrderId(int idOrder);
}
