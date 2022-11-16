using DO;
namespace Dal;
public class DalOrder
{
    /// <summary>
    /// A function that adds a new order to the order array
    /// </summary>
    public static int AddToOrder(Order ord) 
    {
        int id = DataSource._nextOrderNumber; // The ID will be equal to the last empty place in the array
        ord.ID = id; // Enter the ID
        DataSource._orderArr[id] = ord; // We will insert the new order into the order array
        return ord.ID;
    }
    /// <summary>
    /// A function that receives an ID and returns the order
    /// </summary>
    public static Order GetOrder(int id)
    {
        for (int i = 0; i < DataSource._sNextOrderNumber; i++) // A loop that goes through the orders array
            if (id == DataSource._orderArr[i].ID) // If the order is found
                return DataSource._orderArr[i]; // Return the order
        throw new Exception("the object was not found"); // If the order is not found an error will be sent
    }
    /// <summary>
    /// A function that returns the entire array of orders
    /// </summary>
    public static Order[] AllOrder()
    {
        Order[] Arr = new Order[DataSource._sNextOrderNumber]; // We will create a new array with the largest number of existing orders
        for (int i = 0; i < DataSource._sNextOrderNumber; i++) // A loop that goes through the orders array
            Arr[i] = DataSource._orderArr[i]; // We will copy the orders
        return Arr; // We will return a copy array
    }
    /// <summary>
    /// A function that receives an order number and deletes it
    /// </summary>
    public static void DeleteOrder(int id)
    {
        bool isFind = false; // Checks if the order is found
        for (int i = 0; i < DataSource._sNextOrderNumber; i++) // A loop that goes through the orders array
        {
            if (id == DataSource._orderArr[i].ID) // If the order is found
            {
                //We will insert the last order in the array, in the place of the order that is deleted
                DataSource._orderArr[i] = DataSource._orderArr[DataSource._sNextOrderNumber];
                DataSource._sNextOrderNumber--; // We will reduce the number of existing orders
                isFind = true; // Order found
            }
        }
        if (!isFind) // Order not found
            throw new Exception("the object was not found"); // throws an error
    }
    /// <summary>
    /// A function that updates an order
    /// </summary>
    public static void UpdateOrder(Order ord)
    {
        bool isFind = false; // Checks if the order is found
        for (int i = 0; i < DataSource._sNextOrderNumber; i++) // A loop that goes through the orders array
        {
            if (ord.ID == DataSource._orderArr[i].ID) // If the order is found
            {
                DataSource._orderArr[i] = ord; // We will enter the new order instead
                isFind = true; // Order found
            }
        }
        if (!isFind) // Order not found
            throw new Exception("the object was not found"); // throws an error
    }
}

