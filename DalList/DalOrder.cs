namespace Dal;
using DalApi;
using DO;

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
    /// Returns a collection of orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter)
    {
        if (DataSource.s_ordersList.Count == 0)
            throw new UnFoundException("the list is empty"); // לשנות את השגיאה
        return filter is null ? DataSource.s_ordersList.Select(order => order) : DataSource.s_ordersList.Where(filter);
    }
    /// <summary>
    /// Deleting an order
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalMissingIdException"></exception>
    public void Delete(int id)
    {
        if (DataSource.s_ordersList.RemoveAll(x => x?.ID == id) == 0)
            throw new UnFoundException("the order for the id: "+id+" does not exsist");
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
    /// <summary>
    /// Returns an order by terms of
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalMissingIdException"></exception>
    public Order? GetById(Func<Order?, bool>? filter) 
    {
      return filter is null ? throw new UnFoundException("there is no func") : DataSource.s_ordersList.FirstOrDefault(x => filter(x));
    }
    public Order? GetById(int id)=> DataSource.s_ordersList.FirstOrDefault(x => x?.ID == id);
}

