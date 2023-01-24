namespace BlApi;

/// <summary>
/// interface IOrder
/// </summary>
public interface IOrder
{
    /// <summary>
    /// Order list request
    /// </summary>
    /// <returns></returns>
    IEnumerable<BO.OrderForList?> GetOrders();
    /// <summary>
    /// Order details request
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    BO.Order ItemOrder(int id);
    /// <summary>
    /// Order shipping update
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    BO.Order? Updateshipping(int id);
    /// <summary>
    /// Order supply update
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    BO.Order? Updatesupply(int id);
    /// <summary>
    /// Order tracking 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    BO.OrderTracking? Tracking(int id);
   
    public BO.Order? OrderEldest();

    public IEnumerable<BO.StatisticksOrderByMonth> GetStatisticksOrderByMonths();
    BO.OrderForList GetOrderForList (int id);

}
