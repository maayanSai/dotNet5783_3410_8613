using DalApi;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;


namespace Dal;

internal class Order : IOrder
{
    string s_orders = "order";
    [MethodImpl(MethodImplOptions.Synchronized)]

    public int Add(DO.Order ord)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        // The ID of the order will be according to the last empty place in the array
        int? id=(listOrders?.Last()?.ID)!;
        ord.ID = id+1??0;  //We will insert the ID into the object
        listOrders?.Add(ord); // We will insert the order into the last empty place in the array
        XMLTools.SaveListToXMLSerializer(listOrders!, s_orders);
        return ord.ID;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Delete(int id)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (listOrders.RemoveAll(x => x?.ID == id) == 0)
            throw new UnFoundException("the order for the id: " + id + " does not exsist");
        XMLTools.SaveListToXMLSerializer(listOrders!, s_orders);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (listOrders.Count == 0)
            throw new UnFoundException("the list is empty"); 
        return filter is null ? listOrders.Select(order => order) : listOrders.Where(filter);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public DO.Order? GetById(Func<DO.Order?, bool>? filter)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        return filter is null ? throw new UnFoundException("there is no func") : listOrders.FirstOrDefault(x => filter(x));
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public DO.Order? GetById(int id)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        return listOrders.FirstOrDefault(x => x?.ID == id);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Update(DO.Order ord)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        Delete(ord.ID); // if not found - exception is thrown from this method
        listOrders.Add(ord);
        XMLTools.SaveListToXMLSerializer(listOrders!, s_orders);
    }
}
