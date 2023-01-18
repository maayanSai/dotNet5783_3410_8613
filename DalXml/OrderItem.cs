
using DO;
using DalApi;

namespace Dal;

internal class OrderItem : IOrderItem
{
    string s_orderItem = "orderItem";
    public int Add(DO.OrderItem oi)
    {
        oi.ID = s_orderItem.s_NextOrderItemNumber; //We will insert the ID into the object
        s_orderItem.Add(oi); // We will insert the order into the last empty place in the array
        return oi.ID;
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
