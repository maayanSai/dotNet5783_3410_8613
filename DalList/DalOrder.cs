using DO;
namespace Dal;

public class DalOrder
{
    public static int addToOrder(Order ord)
    {
        for (int i = 0; i < DataSource.Config.s_NextOrderNumber; i++)
            if(ord.ID == DataSource.orderArr[i].ID)
                throw new Exception("the object is allreay exists");
        int x = DataSource.Config.NextOrderNumber;
        ord.ID = x;
        DataSource.orderArr[x] = ord;
        return ord.ID; 
    }
    public static Order getOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.s_NextOrderNumber; i++)
        {
            if (id == DataSource.orderArr[i].ID)
                return DataSource.orderArr[i];
        }
        throw new Exception("the object was not found");
    }
    public static Order[] allOrder()
    {
        Order[] Arr = new Order[DataSource.Config.s_NextOrderNumber];
        for (int i = 0; i < DataSource.Config.s_NextOrderNumber; i++)
        {
            Arr[i] = DataSource.orderArr[i];
        }
        return Arr;
    }
    public static void deleteOrder(int id)
    {
        bool isFind = false;
        for (int i = 0; i < DataSource.Config.s_NextOrderNumber; i++)
        {
            if (id == DataSource.orderArr[i].ID)
            {
                DataSource.orderArr[i] = DataSource.orderArr[DataSource.Config.s_NextOrderNumber];
                DataSource.Config.s_NextOrderNumber--;
                isFind = true;
            }
        }
        if (!isFind)
            throw new Exception("the object was not found");
    }
    public static void updateOrder(Order ord)
    {
        bool isFind = false;
        for (int i = 0; i < DataSource.Config.s_NextOrderNumber; i++)
        {
            if (ord.ID == DataSource.orderArr[i].ID)
            {
                DataSource.orderArr[i] = ord;
                isFind = true;
            }
        }
        if (!isFind)
            throw new Exception("the object was not found");
    }
}
