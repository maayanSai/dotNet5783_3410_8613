using DO;
using DalApi;

namespace Dal;

internal class OrderItem : IOrderItem
{
    string s_orderItem = "OrderItem";
    public int Add(DO.OrderItem oi)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem? GetById(Func<DO.OrderItem?, bool>? filter)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.OrderItem item)
    {
        throw new NotImplementedException();
    }
}