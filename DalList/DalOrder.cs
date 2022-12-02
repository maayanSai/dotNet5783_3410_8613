namespace Dal;

using DalApi;
using DO;

internal class DalOrder : IOrder
{
    public int Add(Order ord)
    {
        // The ID of the order will be according to the last empty place in the array
        ord.ID = DataSource.NextOrderNumber; ; //We will insert the ID into the object
        DataSource.OrdersList.Add(ord); // We will insert the order into the last empty place in the array
        return ord.ID;
    }

    public Order GetById(int id) => DataSource.OrdersList.Find(x => x?.ID == id)
        ?? throw new Exception("product doesnt exist");

    public IEnumerable<Order?> GetAll() => new List<Order?>(DataSource.OrdersList);

    public void Delete(int id)
    {
        if (DataSource.OrdersList.RemoveAll(x => x?.ID == id) == 0)
            throw new Exception("order doesnt exist");
    }

    public void Update(Order ord)
    {
        Delete(ord.ID); // if not found - exception is thrown from this method
        DataSource.OrdersList.Add(ord);
    }
}

