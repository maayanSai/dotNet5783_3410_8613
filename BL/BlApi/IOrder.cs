namespace BlApi;
using BO;

/// <summary>
/// interface IOrder
/// </summary>
public interface IOrder
{
    /// <summary>
    /// Order list request
    /// </summary>
    /// <returns></returns>
    IEnumerable<OrderForList?> GetOrders();
    /// <summary>
    /// Order details request
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Order ItemOrder(int id);
    /// <summary>
    /// Order shipping update
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Order? Updateshipping(int id);
    /// <summary>
    /// Order supply update
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Order? Updatesupply(int id);
    /// <summary>
    /// Order tracking 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    OrderTracking? Tracking(int id);

    public IEnumerable<StatisticksOrderByMonth> GetStatisticksOrderByMonths();
    OrderForList GetOrderForList (int id);

}
