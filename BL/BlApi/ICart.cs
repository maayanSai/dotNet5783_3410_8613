namespace BlApi;
using BO;
public interface ICart
{
    /// <summary>
    /// Adding a product to the shopping cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Cart Add(Cart cart, int id);
    /// <summary>
    /// Update as of a product in the shopping cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    Cart? Update(Cart cart, int id, int amount);
    /// <summary>
    /// Basket confirmation for the order
    /// </summary>
    /// <param name="cart"></param>
    void MakeAnOrder(Cart cart);
}
