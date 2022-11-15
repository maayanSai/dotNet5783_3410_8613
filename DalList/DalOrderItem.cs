using DO;
using System.Security.Cryptography;

namespace Dal;

public class DalOrderItem
{
    public static int addToOrderItem(OrderItem oi) 
    {
        for (int i = 0; i < DataSource.Config.s_NextOrderItemNumber; i++)
            if (oi.ID == DataSource.orderItemArr[i].ID)
                throw new Exception("the object is allreay exists");
        int x = DataSource.Config.NextOrderItemArr;
        oi.ID = x;
        DataSource.orderItemArr[x] = oi;
        return oi.ID;
    }
    public static OrderItem getOrderItem(int id)
    {
        for (int i = 0; i < DataSource.Config.s_NextOrderItemNumber; i++)
            if (id == DataSource.orderItemArr[i].ID)
                return DataSource.orderItemArr[i];
        throw new Exception("the object was not found");
    }
    public static OrderItem[] allOrderItem()
    {
        OrderItem[] Arr = new OrderItem[DataSource.Config.s_NextOrderItemNumber];
        for (int i = 0; i < DataSource.Config.s_NextOrderItemNumber; i++)
            Arr[i] = DataSource.orderItemArr[i];
        return Arr;
    }
    public static void deleteOrederItem(int id)
    {
        bool isFind = false;
        for (int i = 0; i < DataSource.Config.s_NextOrderItemNumber; i++)
            if(id== DataSource.orderItemArr[i].ID)
            {
                DataSource.orderItemArr[i] = DataSource.orderItemArr[DataSource.Config.s_NextOrderItemNumber];
                DataSource.Config.s_NextOrderItemNumber--;
                isFind = true;
            }
                // האם צריך לשנות Id
       if(!isFind)
            throw new Exception("the object was not found");
    }
    public static void updateOrederItem(OrderItem oi)
    {
        bool isFind = false;
        for (int i = 0; i < DataSource.Config.s_NextOrderItemNumber; i++)
            if (oi.ID == DataSource.orderItemArr[i].ID)
            {
                isFind = true;
                DataSource.orderItemArr[i] = oi;
            }
        if (!isFind)
            throw new Exception("the object was not found");
    }
}
