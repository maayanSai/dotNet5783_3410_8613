using DalApi;
using DO;
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
        ?? throw new DalMissingIdException(id,"product");
    public IEnumerable<OrderItem?> GetAll() => new List<OrderItem?>(DataSource.OrderItemsList);

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
        foreach (var item in DataSource.OrderItemsList.Where(x => x?.OrderID == idOrder).Where(x => x?.ProductID == idProduct))
            return item;
      //  throw new DalDoesNotExistException()???!!!!
        return null;
    }
    public IEnumerable<OrderItem?> GetByOrderId (int idOrder)
    {
        List<OrderItem?> ordi = new();
        foreach (var item in DataSource.OrderItemsList.Where(x => x?.ID == idOrder))
            ordi.Add(item);
        return ordi;
    }
}
