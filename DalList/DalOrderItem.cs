namespace Dal;
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
    /// Item return by ID number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalMissingIdException"></exception>
    public OrderItem GetById(int id)=> DataSource.s_orderItemsList.Find(x => x?.ID == id)
        ?? throw new DalMissingIdException(id, "order item");
    /// <summary>
    /// Returns a collection of items
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll()
    {
     IEnumerable<OrderItem?> orderitemcolleption;
         orderitemcolleption=DataSource.s_orderItemsList;
        return orderitemcolleption;
    }
    /// <summary>
    /// Deleting an item
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalMissingIdException"></exception>
    public void Delete(int id)
    {
        if (DataSource.s_orderItemsList.RemoveAll(x => x?.ID == id)==0)
            throw new DalMissingIdException(id,"order item");
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
    /// <summary>
    /// Returns an item by 2 ID numbers
    /// </summary>
    /// <param name="idProduct"></param>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="DO.DalMissingIdException"></exception>
    public OrderItem? GetByTwoId(int idProduct, int idOrder)
    {
        IEnumerable<OrderItem?> ordreit =DataSource.OrderItemsList.Where(x => x?.OrderID == idOrder && x?.ProductID == idProduct);
        ordreit.ToList();
        if (!ordreit.Any())
            throw new DO.DalMissingIdException(idProduct, idOrder, "Order item", "the order item which has the profuct id and the order id that you asked for does not exsist");   
        return ordreit.First();
       
        
    }
    /// <summary>
    /// Returns an item by order ID number
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetByOrderId (int idOrder)
    {
        IEnumerable<OrderItem?> ordi;
        ordi=DataSource.s_orderItemsList.Where(x => x?.ID == idOrder);
        return ordi;
    }
}
