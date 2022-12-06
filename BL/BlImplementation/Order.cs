namespace BlImplementation;

using BlApi;
using BO;
using System.Collections.Generic;

internal class Order: IOrder
{
     DalApi.IDal Dal = new Dal.DalList();
    public IEnumerable<BO.OrderForList?> GetOrders()
    {
        return Dal.Order.GetAll().Select(order => new BO.OrderForList
        {
            ID = order?.ID ?? throw new Exception("missing id"),
            CustomerName = order?.CustomerName ?? " ",
            Status = GetStatus(order),
            TotalPrice = Dal.OrderItem.GetByOrderId(order?.ID?? throw new Exception("missing id")).Sum(x => x?.Price * x?.Amount),
            Amount = Dal.OrderItem.GetByOrderId(order?.ID ?? throw new Exception("missing id")).Count(),
        }) ;
    }
    private BO.OrderStatus GetStatus(DO.Order? order)
    {
        if(order?.OrderDate== DateTime.MinValue)
            return BO.OrderStatus.Initiated;
        if(order?.ShipDate== DateTime.MinValue)
            return OrderStatus.Ordered;
        if (order?.DeliveryrDate == DateTime.MinValue)
            return OrderStatus.Shipped;
        return OrderStatus.Delivered;
    }
    public BO.Order ItemOrder(int id)
    {
        DO.Order orderD;
        if (id < 0)
            throw new Exception("id is negative");
        try
        {
            orderD = Dal.Order.GetById(id);
        }
        catch(DO.DalDoesNotExistException exp)
        {
            throw new BO.Exceptions.BODoesNotExistException(exp.Message);
        }
        var items = Dal.OrderItem?.GetByOrderId(orderD.ID).Select(x => new BO.OrderItem
        {
            ID = x?.ID ?? throw new Exception("missing id"),
            ProductID = x?.ProductID ?? throw new Exception("missing id"),
            Price = x?.Price ?? 0,
            Name = Dal.Product.GetById(x?.ProductID ?? throw new Exception("missing id")).Name,
            Amount = x?.Amount ?? 0,
            Totalprice = x?.Price * x?.Amount ?? 0
        }).ToList();
        try
        {
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
        catch( DO.DalDoesNotExistException exp)
        {
            throw new BO.Exceptions.BODoesNotExistException(exp.Message);
        }
    }

    public OrderTracking? Tracking(int id)
    {
        DO.Order order;

        if (id < 0)
            throw new Exception("id is negative");
        try
        {
            order = Dal.Order.GetById(id);
            List<Tuple<DateTime, string>> tracking = new();
            if (order.OrderDate != DateTime.MinValue)
                tracking.Add(new Tuple<DateTime, string>(order.OrderDate, "the order allready exist"));
            if (order.ShipDate != DateTime.MinValue)
                tracking.Add(new Tuple<DateTime, string>(order.ShipDate, " the order has been sent"));
            if (order.DeliveryrDate != DateTime.MinValue)
                tracking.Add(new Tuple<DateTime, string>(order.ShipDate, " the order has been delivered"));
            return new OrderTracking
            {
                ID = id,
                Status = GetStatus(order),
                Tracking = tracking,
            };
        }
        catch (DO.DalDoesNotExistException exp)
        {
            throw new Exceptions.BODoesNotExistException(exp.Message);
        }
    }

    public BO.Order? Updateshipping(int id)
    {
        DO.Order order;
        if (id < 0)
            throw new Exception("id is negative");
        try
        {
            order = Dal.Order.GetById(id);
            if(order.ShipDate==DateTime.MinValue)
            {
                Dal.Order.Update(new DO.Order
                {
                    ID=id,
                    CustomerAdress=order.CustomerAdress,
                    CustomerEmail=order.CustomerEmail,
                    CustomerName= order.CustomerName,
                    OrderDate=order.OrderDate,
                    ShipDate=DateTime.Now,
                    DeliveryrDate=order.DeliveryrDate,
                });
            }
            return this.ItemOrder(id);
        }
        catch (DO.DalDoesNotExistException exp)
        {
            throw new BO.Exceptions.BODoesNotExistException(exp.Message);
        }
    }
    public BO.Order? Updatesupply(int id)
    {
        DO.Order order;
        if (id < 0)
            throw new Exception("id is negative");
        try
        {
            order = Dal.Order.GetById(id);
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
        catch (DO.DalDoesNotExistException exp)
        {
            throw new BO.Exceptions.BODoesNotExistException(exp.Message);
        }
    }
}
