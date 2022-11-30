using DO;
namespace Dal;
public class DalOrderItem
{
    public static int Add(OrderItem oi)
    {
        int id = DataSource._nextOrderItem; // The ID of the order will be according to the last empty place in the array
        oi.ID = id; //We will insert the ID into the object
        DataSource.OrderItemsList.Add(oi); // We will insert the order into the last empty place in the array
        return oi.ID;
    }
    public static OrderItem GetOrderItem(int id)
    {
        return DataSource.OrderItemsList.Find(x => x?.ID == id) ?? throw new Exception("product doesnt exist");

    }
    public static IEnumerable<OrderItem?> AllOrderItem()
    {
        List<OrderItem?> list = new List<OrderItem?>();
        foreach (var orderItem in DataSource.OrderItemsList)
            list.Add(orderItem);
        return list;
    }
    public static void DeleteOrederItem(int id)
    {
        if (DataSource.OrderItemsList.Exists(x => x?.ID == id))
            DataSource.OrderItemsList.RemoveAll(x => x?.ID == id);
        else
            throw new Exception("product doesnt exist");
    }
    public static void UpdateOrederItem(OrderItem oi)
    {
        if (DataSource.OrderItemsList.Exists(x => x?.ID == oi.ID))
        {
            oi.ID = GetOrderItem(oi.ID).ID;
            DataSource.OrderItemsList.RemoveAll(x => x?.ID == oi.ID);
            DataSource.OrderItemsList.Add(oi);
        }
        else
            throw new Exception("product doesnt exist");
    }
}
