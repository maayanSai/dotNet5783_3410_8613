using BO;

namespace BlApi;

public interface IOrder
{
    IEnumerable<OrderForList?> GetOrders();
    Order? ItemOrder(int id);
    Order? Updateshipping(int id);
    Order? Updatesupply(int id);
    OrderTracking? Tracking(int id);





}
