using DO;
namespace Dal;
public class DalOrderItem
{
    /// <summary>
    /// A function that adds an additional item to the order
    /// </summary>
    public static int AddToOrderItem(OrderItem oi)
    {
        int id = DataSource._nextOrderItemArr; // We will set the ID to the last empty place
        oi.ID = id;
        DataSource._orderItemArr[id] = oi; // We will insert the new item into the array
        return oi.ID;
    }
    /// <summary>
    /// A function that returns an item by item number
    /// </summary>
    public static OrderItem GetOrderItem(int id)
    {
        for (int i = 0; i < DataSource._sNextOrderItemNumber; i++) // A loop that goes through all the items
            if (id == DataSource._orderItemArr[i].ID) // If the item exists, it returns it
                return DataSource._orderItemArr[i];
        throw new Exception("the object was not found"); // If the item does not exist, it returns an error
    }
    /// <summary>
    /// A function that returns the entire array of items
    /// </summary>
    public static OrderItem[] AllOrderItem()
    {
        OrderItem[] Arr = new OrderItem[DataSource._sNextOrderItemNumber]; // A new array will be built the size of the number of existing items
        for (int i = 0; i < DataSource._sNextOrderItemNumber; i++) // A loop that goes through all the items
            Arr[i] = DataSource._orderItemArr[i]; // We will copy the items
        return Arr; // We will return the copied array
    }
    /// <summary>
    /// A function that deletes an item by item number
    /// </summary>
    public static void DeleteOrederItem(int id)
    {
        bool isFind = false; // Checks if the item is found
        for (int i = 0; i < DataSource._sNextOrderItemNumber; i++) // A loop that goes through all the items
            if (id == DataSource._orderItemArr[i].ID) // If the item exists
            {
                // We will insert the last item in the array instead of the deleted item
                DataSource._orderItemArr[i] = DataSource._orderItemArr[DataSource._sNextOrderItemNumber];
                DataSource._sNextOrderItemNumber--; // We will reduce the number of items
                isFind = true; // the item exists
            }
        if (!isFind) // if not
            throw new Exception("the object was not found");
    }
    /// <summary>
    /// A function that updates an item
    /// </summary>
    public static void UpdateOrederItem(OrderItem oi)
    {
        bool isFind = false; // Checks if the item is found
        for (int i = 0; i < DataSource._sNextOrderItemNumber; i++) // A loop that goes through all the items
            if (oi.ID == DataSource._orderItemArr[i].ID) // If the item exists
            {
                isFind = true; // the item exists
                DataSource._orderItemArr[i] = oi; // We will insert the new item
            }
        if (!isFind) // if not
            throw new Exception("the object was not found");
    }
}
