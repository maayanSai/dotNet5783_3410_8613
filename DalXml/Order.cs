
using DO;
using DalApi;

namespace Dal;

internal class Order : IOrder
{
    public int Add(DO.Order item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public DO.Order? GetById(Func<DO.Order?, bool>? filter)
    {
        throw new NotImplementedException();
    }

    public DO.Order? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order item)
    {
        throw new NotImplementedException();
    }
}
