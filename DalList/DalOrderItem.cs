

using DO;

namespace Dal;

public class DalOrderItem
{
    public static int addToOrderItem(OrderItem oi) 
    {
        int x = DataSource.Config.NextOrderItemArr;
        oi.ID = x;
        DataSource.orderItemArr[x] = oi;
        return oi.ID;
    }

}
