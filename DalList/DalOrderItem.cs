using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Dal;
internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem oi)
    {
        oi.ID = DataSource.NextOrderItemNumber; //We will insert the ID into the object
        DataSource.OrderItemsList.Add(oi); // We will insert the order into the last empty place in the array
        return oi.ID;
    }
    public OrderItem GetById(int id)=> DataSource.OrderItemsList.Find(x => x?.ID == id)
        ?? throw new DalMissingIdException(id, "order item");
    public IEnumerable<OrderItem?> GetAll()
    {
     IEnumerable<OrderItem?> orderitemcolleption;
         orderitemcolleption=DataSource.OrderItemsList;
        return orderitemcolleption;
    }
    public void Delete(int id)
    {
        if (DataSource.OrderItemsList.RemoveAll(x => x?.ID == id)==0)
            throw new DalMissingIdException(id,"order item");
    }
    public void Update(OrderItem oi)
    {
        Delete(oi.ID); // if not found - exception is thrown from this method
        DataSource.OrderItemsList.Add(oi);
    }
    public OrderItem? GetByTwoId(int idProduct, int idOrder)
    {
        var ordreit =DataSource.OrderItemsList.Where(x => x?.OrderID == idOrder && x?.ProductID == idProduct);
        ordreit.ToList();
        if (!ordreit.Any())
            throw new DO.DalMissingIdException(idProduct, idOrder, "Order item", "the order item which has the profuct id and the order id that you asked for does not exsist");   
        return ordreit.First();
       
        
    }
    public IEnumerable<OrderItem?> GetByOrderId (int idOrder)
    {

        IEnumerable<OrderItem?> ordi;
        ordi=DataSource.OrderItemsList.Where(x => x?.ID == idOrder);
        return ordi;

     

    }
}
