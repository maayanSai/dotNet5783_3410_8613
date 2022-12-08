namespace BlImplementation;
using BlApi;
using System.Collections.Generic;

/// <summary>
/// The order-logical entity
/// </summary>
internal class Order: IOrder
{
     DalApi.IDal Dal = new Dal.DalList();
    /// <summary>
    /// order list request (admin screen)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<BO.OrderForList?> GetOrders()
    {
        return Dal.Order.GetAll().Select(order => new BO.OrderForList
        {
            ID = order?.ID ?? throw new BO.BlNullPropertyException("missing order id"),
            CustomerName = order?.CustomerName ?? " ",
            Status = GetStatus(order),
            TotalPrice = Dal.OrderItem?.GetByOrderId(order?.ID??1).Sum(x => x?.Price * x?.Amount)??0,//alredy cheked id isnot null
            Amount = Dal.OrderItem?.GetByOrderId(order?.ID ?? 1).Count()??0,
        }) ;
    }

    /// <summary>
    /// A private helper function, that returns the state of the order 
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private static BO.OrderStatus GetStatus(DO.Order? order)
    {
        if (order?.ShipDate != null)
        {
            if (order?.DeliveryrDate != null && order?.DeliveryrDate < DateTime.Now)
                return BO.OrderStatus.Delivered;
            return BO.OrderStatus.Shipped;
        }
        else
            return BO.OrderStatus.Ordered;
    }
    /// <summary>
    /// Order details request (for manager screen and buyer screen)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    public BO.Order ItemOrder(int id)
    {
        DO.Order orderD;
        if (id < 0)
            throw new Exception("id is negative");
        try
        {
            orderD = Dal.Order.GetById(id);
        }
        catch(DO.DalMissingIdException exp)
        {
            throw new BO.BlMissingEntityException("the order does not exsists",exp);
        }
        //  Create a collection of order items
        var items = Dal.OrderItem?.GetByOrderId(orderD.ID).Select(x => new BO.OrderItem
        {
            ID = x?.ID ?? throw new BO.BlNullPropertyException("missing order item id"),
            ProductID = x?.ProductID ?? throw new  BO.BlNullPropertyException("missing product id"),
            Price = x?.Price ?? 0,
            Name = Dal.Product.GetById(x?.ProductID ?? 1).Name,//האיי די תמיד יהיה תקין
            Amount = x?.Amount ?? 0,
            Totalprice = x?.Price * x?.Amount ?? 0
            
        }).ToList();
        return new BO.Order
        {
            ID = orderD.ID,
            CustomerName = orderD.CustomerName,
            CustomerAdress = orderD.CustomerAdress,
            CustomerEmail = orderD.CustomerEmail,
            OrderDate = orderD.OrderDate,
            ShipDate = orderD.ShipDate,
            DeliveryrDate = orderD.DeliveryrDate,
            Status = GetStatus(orderD),
            Items = items,
            TotalPrice= items!.Sum(x=> x.Totalprice),
        };
    }
    /// <summary>
    /// Order Tracking (Manager Order Management Screen)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlInCorrectException"></exception>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    public BO.OrderTracking? Tracking(int id)
    {
        DO.Order order;
        if (id < 0)
            throw new BO.BlInCorrectException("id is negative");
        try
        {
            order = Dal.Order.GetById(id);
        }
        catch (DO.DalMissingIdException exp)
        {
            throw new BO.BlMissingEntityException("missing order", exp);
        }  
            List<Tuple<DateTime?, string?>> tracking = new();
            if (order.OrderDate != null)
                tracking.Add(new Tuple<DateTime?, string?>(order.OrderDate, "the order allready exist"));
            else throw new BO.BlIncorrectDatesException("order date is miising");
            if (order.ShipDate != null)
                tracking.Add(new Tuple<DateTime?, string?>(order.ShipDate, " the order has been sent"));
           // else throw new BO.BlNullPropertyException("Ship date is missing");
            if (order.DeliveryrDate != null)
                tracking.Add(new Tuple<DateTime?, string?>(order.ShipDate, " the order has been delivered"));
            //else throw new BO.BlNullPropertyException("Delivery Date is missing");
            BO.OrderTracking ortk = new()
            {
                ID = id,
                Status = GetStatus(order),
                Tracking = tracking,
            };
            return ortk;
        }
  
    /// <summary>
    /// Order shipping update (Admin order management screen)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="BO.Exceptions.BODoesNotExistException"></exception>
    public BO.Order? Updateshipping(int id)
    {
        DO.Order order;
        if (id < 0)
            throw new BO.BlInCorrectException("id is negative");
        try
        {
            order = Dal.Order.GetById(id);
            if (order.ShipDate != null) // Check if an order exists, and has not yet been sent
            {
                Dal.Order.Update(new DO.Order
                {
                    ID = id,
                    CustomerAdress = order.CustomerAdress,
                    CustomerEmail = order.CustomerEmail,
                    CustomerName = order.CustomerName,
                    OrderDate = order.OrderDate,
                    ShipDate = DateTime.Now,
                    DeliveryrDate = order.DeliveryrDate,
                });
            }
            return this.ItemOrder(id);
        }
        catch (DO.DalMissingIdException exp)
        {
            throw new BO.BlMissingEntityException("The order does not exist", exp);
        }
    }

    /// <summary>
    /// Order Delivery Update (Admin Order Management Screen)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="BO.Exceptions.BODoesNotExistException"></exception>
    public BO.Order? Updatesupply(int id)
    {
        DO.Order order;
        if (id < 0)
            throw new BO.BlInCorrectException("id is negative");
        try
        {
            order = Dal.Order.GetById(id);
            // Check if an order exists, already sent but not yet delivered
            if (order.ShipDate != DateTime.MinValue && order.DeliveryrDate == DateTime.MinValue)
            {
                Dal.Order.Update(new DO.Order
                {
                    ID = id,
                    CustomerAdress = order.CustomerAdress,
                    CustomerEmail = order.CustomerEmail,
                    CustomerName = order.CustomerName,
                    OrderDate = order.OrderDate,
                    ShipDate = order.ShipDate,
                    DeliveryrDate = DateTime.Now,
                });
            }
            return this.ItemOrder(id);
        }
        catch (DO.DalMissingIdException exp)
        {
            throw new BO.BlMissingEntityException("The order does not exist", exp);
        }
    }
}
