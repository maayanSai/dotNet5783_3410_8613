

using DO;

namespace Dal;

public class DalOrder
{
    public static int addToOrder(Order ord)
    {
        int x = DataSource.Config.NextOrderNumber;
        ord.ID = x;
        DataSource.orderArr[x] = ord;
        return ord.ID; 
    }
}
