using DalApi;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    string s_orderItem = "orderItem";

    static DO.OrderItem? createOrderItemfromXElement(XElement s)
    {
        return new DO.OrderItem()
        {
            ID = s.ToIntNullable("ID") ?? throw new DO.UnFoundException("missing id"),
            ProductID = s.ToIntNullable("ProductID") ?? throw new DO.UnFoundException("missing product id"),
            OrderID = s.ToIntNullable("OrderID") ?? throw new DO.UnFoundException("missing order id"),
            Amount = s.ToIntNullable("Amount") ?? throw new DO.UnFoundException("missing amount"),
            Price = (double)(s?.ToDoubleNullable("Price")??0),
        };
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public int Add(DO.OrderItem oi)
    {
        XElement? OrderItemsRootElement = XMLTools.LoadListFromXMLElement(s_orderItem);
        oi.ID = config.nextOrderItemId();
        XElement? orderItemElement = new XElement("OrderItem",
            new XElement("ID",oi.ID),
            new XElement("ProductID",oi.ProductID),
            new XElement("OrderID", oi.OrderID),
            new XElement("Price", oi.Price),
            new XElement("Amount", oi.Amount)
        );
        OrderItemsRootElement.Add(orderItemElement);
        XMLTools.SaveListToXMLElement(OrderItemsRootElement, s_orderItem);
        return oi.ID;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Delete(int id)
    {
        XElement OrderItemsRootElement = XMLTools.LoadListFromXMLElement(s_orderItem);
        XElement? orderItemElement = (from o1 in OrderItemsRootElement.Elements()
                                      where (int?)o1.Element("ID") == id
                                      select o1).FirstOrDefault() ?? throw new DO.UnFoundException("id is not exist");
        orderItemElement.Remove();
        XMLTools.SaveListToXMLElement(OrderItemsRootElement, s_orderItem);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        XElement OrderItemsRootElement = XMLTools.LoadListFromXMLElement(s_orderItem);
        if(filter==null)
        {
            return OrderItemsRootElement.Elements().Select(s => createOrderItemfromXElement(s));
        }
        else
        {
            return from s in OrderItemsRootElement.Elements()
                   let orderItem = createOrderItemfromXElement(s)
                   where filter(orderItem)
                   select (DO.OrderItem?)orderItem;
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public DO.OrderItem? GetById(Func<DO.OrderItem?, bool>? filter)
    {
        XElement OrderItemsRootElement = XMLTools.LoadListFromXMLElement(s_orderItem);
        if (filter == null)
        {
            throw new DO.UnFoundException("missing id"); 
        }
        else
        {
            var help= from s in OrderItemsRootElement.Elements()
                   let orderItem = createOrderItemfromXElement(s)
                   where filter(orderItem)
                   select (DO.OrderItem?)orderItem;
            return help.First();    
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public DO.OrderItem? GetById(int id)
    {
        XElement OrderItemsRootElement = XMLTools.LoadListFromXMLElement(s_orderItem);
        return (from o in OrderItemsRootElement?.Elements()
                where o.ToIntNullable("ID") == id
                select (DO.OrderItem?)createOrderItemfromXElement(o)).FirstOrDefault()
                ?? throw new DO.UnFoundException("missing id");
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Update(DO.OrderItem item)
    {
        Delete(item.ID);
        Add(item);
    }
}