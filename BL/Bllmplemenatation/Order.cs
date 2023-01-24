namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Text;

/// <summary>
/// The order-logical entity
/// </summary>
internal class Order : IOrder
{
    private static readonly DalApi.IDal dal = DalApi.Factory.Get()!;

    /// <summary>
        /// order list request (admin screen)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.OrderForList> GetOrders()
    {
        lock (dal)
        {
            return dal!.Order.GetAll().Select(order =>

        {
            lock (dal)
            {
                var items = dal.OrderItem.GetAll(delegate (DO.OrderItem? x) { return x?.OrderID == order?.ID; });

                return new BO.OrderForList
                {
                    ID = order?.ID ?? throw new BO.BlNullPropertyException("missing order id"),
                    CustomerName = order?.CustomerName ?? " ",
                    Status = GetStatus(order),
                    TotalPrice = items.Sum(x => x?.Price * x?.Amount ?? 0),//alredy cheked id isnot null
                    Amount = items.Count(),
                };
            }
        });
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public BO.OrderForList GetOrderForList(int id)
    {
        lock (dal)
        {
            DO.Order? order = dal.Order.GetById(id);
            var items = dal.OrderItem.GetAll(delegate (DO.OrderItem? x) { return x?.OrderID == order?.ID; });

            return new BO.OrderForList
            {
                ID = order?.ID ?? throw new BO.BlNullPropertyException("missing order id"),
                CustomerName = order?.CustomerName ?? " ",
                Status = GetStatus(order),
                TotalPrice = items.Sum(x => x?.Price * x?.Amount ?? 0),//alredy cheked id isnot null
                Amount = items.Count(),
            };
        }
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    /// 
    public BO.Order ItemOrder(int id)
    {
        DO.Order? orderD;
        if (id < 0)
            throw new BO.BlInCorrectException("id is negative");
        try
        {
            lock (dal)
            {
                orderD = dal?.Order.GetById(delegate (DO.Order? x) { return x?.ID == id; });
            }
        }
        catch (DO.UnFoundException exp)
        {
            throw new BO.BlMissingEntityException("the order does not exsists", exp);
        }
        //  Create a collection of order items
        lock (dal)
        {
            var items = dal?.OrderItem?.GetAll((delegate (DO.OrderItem? x) { return x?.OrderID == orderD?.ID; })).Select(x => new BO.OrderItem

            {
                ID = x?.ID ?? throw new BO.BlNullPropertyException("missing order item id"),
                ProductID = x?.ProductID ?? throw new BO.BlNullPropertyException("missing product id"),
                Price = x?.Price ?? 0,
                Name = dal?.Product.GetById(x?.ProductID ?? throw new BO.BlNullPropertyException("missing product id"))?.Name,//האיי די תמיד יהיה תקין
                Amount = x?.Amount ?? 0,
                Totalprice = x?.Price * x?.Amount ?? 0

            }).ToList();

            return new BO.Order
            {
                ID = orderD?.ID ?? throw new BO.BlNullPropertyException("missing order id"),
                CustomerName = orderD?.CustomerName,
                CustomerAdress = orderD?.CustomerAdress,
                CustomerEmail = orderD?.CustomerEmail,
                OrderDate = orderD?.OrderDate,
                ShipDate = orderD?.ShipDate,
                DeliveryrDate = orderD?.DeliveryrDate,
                Status = GetStatus(orderD),
                Items = items!,
                TotalPrice = items!.Sum(x => x.Totalprice),
            };
        }
    }
    /// <summary>
    /// Order shipping update (Admin order management screen)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="BO.Exceptions.BODoesNotExistException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order? Updateshipping(int id)
    {
        DO.Order order;
        if (id < 0)
            throw new BO.BlInCorrectException("id is negative");
        try
        {
            lock (dal)
            {
                order = dal?.Order.GetById(id) ?? throw new BO.BlNullPropertyException("missing product id");
            }
            if (order.OrderDate != null && order.ShipDate == null) // Check if an order exists, and has not yet been sent
            {
                dal?.Order.Update(new DO.Order
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
        catch (DO.UnFoundException exp)
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    /// 
    public BO.Order? Updatesupply(int id)
    {
        DO.Order order;
        if (id < 0)
            throw new BO.BlInCorrectException("id is negative");
        try
        {
            lock (dal)
            {
                order = dal?.Order.GetById(id) ?? throw new BO.BlNullPropertyException("missing product id");
            }
            // Check if an order exists, already sent but not yet delivered
            if (order.ShipDate != null && order.DeliveryrDate == null)
            {
                lock (dal)
                {
                    dal?.Order.Update(new DO.Order

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
            }
            return this.ItemOrder(id);
        }
        catch (DO.UnFoundException exp)
        {
            throw new BO.BlMissingEntityException("The order does not exist", exp);
        }
    }
    /// <summary>
        /// Order Tracking (Manager Order Management Screen)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="BO.BlInCorrectException"></exception>
        /// <exception cref="BO.BlMissingEntityException"></exception>
        /// <exception cref="BO.BlNullPropertyException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.OrderTracking? Tracking(int id)
    {
        DO.Order order;
        if (id < 0)
            throw new BO.BlInCorrectException("id is negative");
        try
        {
            lock (dal)
            {
                order = dal?.Order.GetById(id) ?? throw new BO.BlNullPropertyException("missing product id");
            }
        }
        catch (DO.UnFoundException exp)
        {
            throw new BO.BlMissingEntityException("missing order", exp);
        }
        List<Tuple<DateTime?, string?>> tracking = new();
        if (order.OrderDate != null)
            tracking.Add(new Tuple<DateTime?, string?>(order.OrderDate, "the order allready exist"));
        else throw new BO.BlIncorrectDatesException("order date is missing");
        if (order.ShipDate != null)
            tracking.Add(new Tuple<DateTime?, string?>(order.ShipDate, " the order has been sent"));
        if (order.DeliveryrDate != null)
            tracking.Add(new Tuple<DateTime?, string?>(order.DeliveryrDate, " the order has been delivered"));
        BO.OrderTracking ortk = new()
        {
            ID = id,
            Status = GetStatus(order),
            Tracking = tracking,
        };
        return ortk;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.StatisticksOrderByMonth> GetStatisticksOrderByMonths()
    {
        lock (dal)
        {
            return from order in dal!.Order.GetAll()

                   let _order = order.GetValueOrDefault()
                   let orderDate = _order.OrderDate.GetValueOrDefault()
                   group order by CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(orderDate.Month) into newGroup
                   select new BO.StatisticksOrderByMonth
                   {
                       MonthName = newGroup.Key,
                       CountOrders = newGroup.Count(),
                       OrdersTotalPrice = (from order in newGroup
                                           let totalPriceOfOrder = dal.OrderItem?.GetAll(orderItem => orderItem?.ID == order?.ID)
                                           .Sum(orderItem => orderItem?.Price)
                                           select totalPriceOfOrder).Sum()
                   };
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order? OrderEldest()
    {
        lock (dal)
        {
            var orders = dal.Order.GetAll(x => x?.DeliveryrDate == null);

            if (orders.Any())
            {
                lock (dal)
                {
                    orders = dal.Order.GetAll(x => x?.ShipDate == null);
                }

                orders =   (from order in orders
                            orderby order?.OrderDate
                            select order);


                DO.Order? eldestDoOrder_Ordered = orders.FirstOrDefault();
                
                
                
                lock (dal)
                {
                    orders = dal.Order.GetAll(x => (x?.DeliveryrDate == null && (x?.ShipDate != null)));
                }
                orders =   (from order in orders
                            orderby order?.ShipDate
                            select order);
                DO.Order? eldestDoOrder_Shipted = orders.FirstOrDefault();
                if (eldestDoOrder_Ordered?.OrderDate<eldestDoOrder_Shipted?.ShipDate)
                    return this.ItemOrder(eldestDoOrder_Ordered?.ID??throw new BO.BlNullPropertyException("missing id"));
                return this.ItemOrder(eldestDoOrder_Shipted?.ID??throw new BO.BlNullPropertyException("missing id"));
            }

            else
            {
                return null;
            }
        }
    }
}


