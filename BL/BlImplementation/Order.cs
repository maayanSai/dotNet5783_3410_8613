namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;

internal class Order: IOrder
{
    DalApi.IDal Dal = new Dal.DalList();

    public IEnumerable<OrderForList?> GetOrders()
    {
        IEnumerable<DO.Order?> orders = Dal.Order.GetAll();

        return orders.Select(ord => new BO.OrderForList
        {
            ID = ord?.ID ?? throw new Exception("missing id"),
            CustomerName = ord?.CustomerName ?? throw new Exception("missing name"),
            Status = (BO.OrderStatus?)ord.Status ?? throw new Exception("wrong status"),
            TotalPrice =  ?? throw new Exception("missing price"),
            Amount = ord.Amount ?? throw new Exception("missing amount")
        });
    }

    public BO.Order ItemOrder(int id)
    {
        DO.Order order;
        if (id > 0)
        {
            try
            {
                order = Dal.Order.GetById(id);
            }
            catch (DO.DalDoesNotExistException exp)
            {
                throw new BO.Exceptions.BODoesNotExistException(exp.Message);
            }
        }
        return new BO.Order()
        {
            ID = order.ID,
            CustomerName = order.CustomerName,
            CustomerAdress = order.CustomerAdress,
            CustomerEmail = order.CustomerEmail,
            OrderDate = order.OrderDate,
            ShipDate = order.ShipDate,
            DeliveryrDate = order.DeliveryrDate,
            TotalPrice = order.TotalPrice,
            Status = (BO.OrderStatus)order.Status,
            Items = order.Items
        };
    }

    public OrderTracking? Tracking(int id)
    {
        DO.Order order;
        try
        {
            order = Dal.Order.GetById(id);
        }
        catch (DO.DalDoesNotExistException exp)
        {
            throw new BO.Exceptions.BODoesNotExistException(exp.Message);
        }
    }

    public BO.Order? Updateshipping(int id)
    {
        DO.Order order;
        try
        {
            order = Dal.Order.GetById(id);
        }
        catch (DO.DalDoesNotExistException exp)
        {
            throw new BO.Exceptions.BODoesNotExistException(exp.Message);
        }
        if ((BO.OrderStatus)order.Status == "confirmed")
        {
            return new BO.Order()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                CustomerAdress = order.CustomerAdress,
                CustomerEmail = order.CustomerEmail,
                OrderDate = order.OrderDate,
                ShipDate = DateTime.Now,
                DeliveryrDate = order.DeliveryrDate,
                TotalPrice = order.TotalPrice,
                Status = (BO.OrderStatus)order.Status,
                Items = order.Items
                ShipDate = order.ShipDate,
            };
        }
    }

    public BO.Order? Updatesupply(int id)
    {
        DO.Order order;
        try
        {
            order = Dal.Order.GetById(id);
        }
        catch (DO.DalDoesNotExistException exp)
        {
            throw new BO.Exceptions.BODoesNotExistException(exp.Message);
        }
        if ((BO.OrderStatus)order.Status != "Provided")
        {
            return new BO.Order()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                CustomerAdress = order.CustomerAdress,
                CustomerEmail = order.CustomerEmail,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryrDate = DateTime.Now,
                TotalPrice = order.TotalPrice,
                Status = (BO.OrderStatus)order.Status,
                Items = order.Items
                ShipDate = order.ShipDate,
            };
        }
    }
}
