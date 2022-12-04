namespace BlApi;
using BO;
public interface ICart
{
    Cart? Add(Cart cart, int id);
    Cart? Update(Cart cart, int id, int amount);
    void MakeAnOrder(Cart cart);
}
