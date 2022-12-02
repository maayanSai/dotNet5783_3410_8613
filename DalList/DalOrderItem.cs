using DalApi;
using DO;
namespace Dal;
internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem oi)
    {
        int id = DataSource.NextOrderItemNumber; // The ID of the order will be according to the last empty place in the array
        oi.ID = id; //We will insert the ID into the object
        DataSource.OrderItemsList.Add(oi); // We will insert the order into the last empty place in the array
        return oi.ID;
    }
    public OrderItem GetById(int id)
    {
        return DataSource.OrderItemsList.Find(x => x?.ID == id) ?? throw new Exception("product doesnt exist");

    }
    public IEnumerable<OrderItem?> GetAll()
    {
        List<OrderItem?> list = new List<OrderItem?>();
        foreach (var orderItem in DataSource.OrderItemsList)
            list.Add(orderItem);
        return list;
    }
    public void Delete(int id)
    {
        if (DataSource.OrderItemsList.Exists(x => x?.ID == id))
            DataSource.OrderItemsList.RemoveAll(x => x?.ID == id);
        else
            throw new Exception("orderItem doesnt exist");
    }
    public void Update(OrderItem oi)
    {
        if (DataSource.OrderItemsList.Exists(x => x?.ID == oi.ID))
        {
            oi.ID = GetById(oi.ID).ID;
            DataSource.OrderItemsList.RemoveAll(x => x?.ID == oi.ID);
            DataSource.OrderItemsList.Add(oi);
        }
        else
            throw new Exception("orderItem doesnt exist");
    }
    public OrderItem? GetByTwoId(int idProduct, int idOrder)
    {
        OrderItem? oi;
        if (DataSource.OrderItemsList.Exists(x => x?.ProductID == idProduct) && (DataSource.OrderItemsList.Exists(x => x?.OrderID == idOrder)))
        {
            oi = DataSource.OrderItemsList.Find((x => (x?.ProductID == idProduct) && (x?.OrderID == idOrder)));
            return oi;
        }
        return null;
    }
    public IEnumerable<OrderItem?> GetByOrderId (int idOrder)
    {
        List<OrderItem?> ordi = new List<OrderItem?>();
        foreach (var item in DataSource.OrderItemsList.Where(x => x?.ID == idOrder))
        {
            ordi.Add(item);
        }
        return ordi;
    }
}
