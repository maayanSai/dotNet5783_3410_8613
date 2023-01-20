using DalApi;
using System.Security.Cryptography;

namespace Dal;

internal class DalOrder : IOrder
{
    string s_orders = "order";

    public int Add(DO.Order ord)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        // The ID of the order will be according to the last empty place in the array
        int id = config.nextOrderId();
        ord.ID = id;  //We will insert the ID into the object
        listOrders?.Add(ord); // We will insert the order into the last empty place in the array
        XMLTools.SaveListToXMLSerializer(listOrders!, s_orders);
        return ord.ID;
    }

    public void Delete(int id)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (listOrders.RemoveAll(x => x?.ID == id) == 0)
            throw new DO.UnFoundException("the order for the id: " + id + " does not exsist");
        XMLTools.SaveListToXMLSerializer(listOrders!, s_orders);
    }

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (listOrders.Count == 0)
            throw new DO.UnFoundException("the list is empty"); 
        return filter is null ? listOrders.Select(order => order) : listOrders.Where(filter);
    }

    public DO.Order? GetById(Func<DO.Order?, bool>? filter)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        return filter is null ? throw new DO.UnFoundException("there is no func") : listOrders.FirstOrDefault(x => filter(x));
    }

    public DO.Order? GetById(int id)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        return listOrders.FirstOrDefault(x => x?.ID == id);
    }

    public void Update(DO.Order ord)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        Delete(ord.ID); // if not found - exception is thrown from this method
        listOrders.Add(ord);
        XMLTools.SaveListToXMLSerializer(listOrders!, s_orders);
    }
}
