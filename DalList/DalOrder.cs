﻿using DO;
namespace Dal;
public class DalOrder
{
    /// <summary>
    /// A function that adds a new order to an array of orders
    /// </summary>
    public static int Add(Order ord)
    {
        int id = DataSource._nextOrderNumber; // The ID of the order will be according to the last empty place in the array
        ord.ID = id; //We will insert the ID into the object
        DataSource.OrdersList.Add(ord); // We will insert the order into the last empty place in the array
        //        throw new Exception("the object is allreay exists");
        int id = DataSource.Config.NextOrderNumber;
    /// <summary>
    /// A function that returns an order by ID
    /// </summary>
    public static Order GetOrder(int id)
        DataSource.orderArr[id] = ord;
        return DataSource.OrdersList.Find(x => x?.ID == id) ?? throw new Exception("product doesnt exist");
        int x = DataSource.orderArr.Length;
    /// <summary>
    /// A function that returns all the orders that exist in the array
    /// </summary>
    public static IEnumerable<Order?> AllOrder()
    {
        List<Order?> list = new List<Order?>();
        foreach (var order in DataSource.OrdersList)
            list.Add(order);
        return list;
    }
    /// <summary>
    /// A function that deletes an order by order number
    /// </summary>
    public static void DeleteOrder(int id)
        for (int i = 0; i < DataSource.Config.s_NextOrderNumber; i++)
        if (DataSource.OrdersList.Exists(x => x?.ID == id))
            DataSource.OrdersList.RemoveAll(x => x?.ID == id);
        else
            throw new Exception("product doesnt exist");
    }
    public static void UpdateOrder(Order ord)
    {
        if (DataSource.OrdersList.Exists(x => x?.ID == ord.ID))
        {
            ord.ID = GetOrder(ord.ID).ID;
            DataSource.OrdersList.RemoveAll(x => x?.ID == ord.ID);
            DataSource.OrdersList.Add(ord);
        for (int i = 0; i < DataSource.Config.s_NextOrderNumber; i++)
        else
            throw new Exception("product doesnt exist");
            {
                DataSource.orderArr[i] = ord;
                isFind = true;
            }
        }
        if (!isFind)
            throw new Exception("the object was not found");
    }
}

