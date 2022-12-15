﻿namespace Dal;
using DalApi;
using DO;
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
    public int Add(OrderItem oi)
    {
        oi.ID = DataSource.s_NextOrderItemNumber; //We will insert the ID into the object
        DataSource.s_orderItemsList.Add(oi); // We will insert the order into the last empty place in the array
        return oi.ID;
    }


    /// <summary>
    /// Returns a collection of items
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter)
    {
        if (DataSource.s_orderItemsList.Count == 0)
            throw new Exception("the list is empty"); // לשנות את השגיאה
        return filter is null ? DataSource.s_orderItemsList.Select(orderItem => orderItem) : DataSource.s_orderItemsList.Where(filter);
    }

    /// <summary>
    /// Deleting an item
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalMissingIdException"></exception>
    public void Delete(int id)
    {
        if (DataSource.s_orderItemsList.RemoveAll(x => x?.ID == id)==0)
            throw new DalMissingIdException(id, "order item");
    }
    /// <summary>
    /// Item update
    /// </summary>
    /// <param name="oi"></param>
    public void Update(OrderItem oi)
    {
        Delete(oi.ID); // if not found - exception is thrown from this method
        DataSource.s_orderItemsList.Add(oi);
    }
    public OrderItem? GetById(int id)
    {
        return DataSource.s_orderItemsList.FirstOrDefault(x => x?.ID == id);
    }
    /// <summary>
    /// Returns an item by 2 ID numbers
    /// </summary>
    /// <param name="idProduct"></param>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="DO.DalMissingIdException"></exception>
    public OrderItem? GetByTwoId(int idProduct, int idOrder)
    {
        IEnumerable<OrderItem?> ordreit = DataSource.s_orderItemsList.Where(x => x?.OrderID == idOrder && x?.ProductID == idProduct);
        ordreit.ToList();
        if (!ordreit.Any())
            throw new DO.DalMissingIdException(idProduct, idOrder, "Order item", "the order item which has the profuct id and the order id that you asked for does not exsist");
        return ordreit.First();


    }
    /// <summary>
    /// Item return by terms of
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalMissingIdException"></exception>
    public OrderItem? GetById(Func<OrderItem?, bool>? filter)
    {
        // לשנות את השגיאה
        return filter is null ? throw new Exception("there is no func") : DataSource.s_orderItemsList.First(x => filter(x));
    }

    /// <summary>
    /// Returns an item by order ID number
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetByOrderId(int idOrder)
    {
        return GetAll(delegate (OrderItem? x) { return x?.OrderID == idOrder; });
    }


}
