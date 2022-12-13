namespace Dal;
using DalApi;
using DO;
using System.Linq.Expressions;

/// <summary>
/// Fulfillment of an order
/// </summary>
internal class DalOrder : IOrder
{
    /// <summary>
    /// Adds an order
    /// </summary>
    /// <param name="ord"></param>
    /// <returns></returns>
    public int Add(Order ord)
    {
        // The ID of the order will be according to the last empty place in the array
        ord.ID = DataSource.s_NextOrderNumber; ; //We will insert the ID into the object
        DataSource.s_ordersList.Add(ord); // We will insert the order into the last empty place in the array
        return ord.ID;
    }
    /// <summary>
    /// Returns an order by ID number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalMissingIdException"></exception>
    public Order GetById(int id) => DataSource.s_ordersList.Find(x => x?.ID == id)
        ?? throw new DalMissingIdException(id, "order");
    /// <summary>
    /// Returns a collection of orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? f)
    {
        IEnumerable<Order?> ord;
        ord = DataSource.s_ordersList;
        if (f != null)
        {
            var o= from Order? item in ord
                   where f(item)
                   select item;
        }
        return ord;
    }

    /// <summary>
    /// Deleting an order
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalMissingIdException"></exception>
    public void Delete(int id)
    {
        if (DataSource.s_ordersList.RemoveAll(x => x?.ID == id) == 0)
            throw new DalMissingIdException(id, "order");
    }
    /// <summary>
    /// Update Invitation
    /// </summary>
    /// <param name="ord"></param>
    public void Update(Order ord)
    {
        Delete(ord.ID); // if not found - exception is thrown from this method
        DataSource.s_ordersList.Add(ord);
    }
}

