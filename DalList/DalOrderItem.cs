namespace Dal;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Implementing the functions of order details
/// </summary>
internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// Adding an item
    /// </summary>
    /// <param name="oi"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem oi)
    {
        oi.ID = DataSource.s_NextOrderItemNumber; //We will insert the ID into the object
        //DataSource.nextOrderItemId();
        DataSource.s_orderItemsList.Add(oi); // We will insert the order into the last empty place in the array
        return oi.ID;
    }
    /// <summary>
    /// Returns a collection of items
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]    /// 
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter)
    {
        if (DataSource.s_orderItemsList.Count == 0)
            throw new UnFoundException("the list is empty"); 
        return filter is null ? DataSource.s_orderItemsList.Select(orderItem => orderItem) : DataSource.s_orderItemsList.Where(filter);
    }
    /// <summary>
    /// Deleting an item
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalMissingIdException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        if (DataSource.s_orderItemsList.RemoveAll(x => x?.ID == id)==0)
            throw new UnFoundException( "the order item for the id: "+id+" does not exsist -do that cant be erased");
    }
    /// <summary>
    /// Item update
    /// </summary>
    /// <param name="oi"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem oi)
    {
        Delete(oi.ID); // if not found - exception is thrown from this method
        DataSource.s_orderItemsList.Add(oi);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem? GetById(int id)=> DataSource.s_orderItemsList.FirstOrDefault(x => x?.ID == id);
    /// <summary>
    /// Item return by terms of
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalMissingIdException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem? GetById(Func<OrderItem?, bool>? filter)=> filter is null ? throw new UnFoundException("there is no func") : DataSource.s_orderItemsList.First(x => filter(x));
}
