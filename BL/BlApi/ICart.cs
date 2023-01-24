namespace BlApi;
/// <summary>
/// interface ICart
/// </summary>
/// 

public interface ICart
{
    /// <summary>
    /// Adding a product to the shopping cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    BO.Cart? Add(BO.Cart cart, int id);
    /// <summary>
    /// Update as of a product in the shopping cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    BO.Cart? Update(BO.Cart cart, int id, int amount);
    /// <summary>
    /// Basket confirmation for the order
    /// </summary>
    /// <param name="cart"></param>
    int MakeAnOrder(BO.Cart cart);
}
