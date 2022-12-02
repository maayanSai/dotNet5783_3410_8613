using DalApi;
using DO;
namespace Dal;
internal struct DalOrder : IOrder
{
    /// <summary>
    /// A function that adds a new order to an array of orders
    /// </summary>
    public  int Add(Order ord)
    {
        int id = DataSource.NextOrderNumber; // The ID of the order will be according to the last empty place in the array
        ord.ID = id; //We will insert the ID into the object
        DataSource.OrdersList.Add(ord); // We will insert the order into the last empty place in the array
        return ord.ID;
    }
    public  Order GetById(int id)
    {
        return DataSource.OrdersList.Find(x => x?.ID == id) ?? throw new Exception("product doesnt exist");
    }
    public  IEnumerable<Order?> GetAll()
    {
        List<Order?> list = new List<Order?>();
        foreach (var order in DataSource.OrdersList)
            list.Add(order);
        return list;
    }
    public  void Delete(int id) 
    {
        if (DataSource.OrdersList.Exists(x => x?.ID == id))
            DataSource.OrdersList.RemoveAll(x => x?.ID == id);
        else
            throw new Exception("order doesnt exist");
    }
    public  void Update(Order ord)
    {
        if (DataSource.OrdersList.Exists(x => x?.ID == ord.ID))
        {
            ord.ID = GetById(ord.ID).ID;
            DataSource.OrdersList.RemoveAll(x => x?.ID == ord.ID);
            DataSource.OrdersList.Add(ord);
        }
        else
            throw new Exception("orderItem doesnt exist");
    }
}

