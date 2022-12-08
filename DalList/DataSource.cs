using DO;
namespace Dal;
using System;
internal static class DataSource
{
    static DataSource() { s_Initialize(); } // A constructive operation, which initializes the entity arrays
    private static readonly Random _rnd = new(); // lottery variable

    internal static List<Product?> ProductsList = new(); // An array of orders
    internal static List<Order?> OrdersList = new(); // An array of items
    internal static List<OrderItem?> OrderItemsList = new(); // array of products

    internal const int s_startOrderNumber = 1000;
    private static int s_nextOrderNumber = s_startOrderNumber;
    internal static int NextOrderNumber { get => s_nextOrderNumber++; }

    internal const int s_startOrderItemNumber = 1000;
    private static int s_nextOrderItemNumber = s_startOrderItemNumber;
    internal static int NextOrderItemNumber { get => s_nextOrderItemNumber++; }

    // Matrix of product names
    private static readonly string[,] _productNames = new string[5, 5]
        {{"diamond ring", "wedding ring", "pearl ring","double ring","climbing ring" }, // rings
        {"pearl necklace", "long necklace", "tight necklace","diamond necklace","diamond necklace" }, // necklaces
        {"Pearl bracelet","Diamond bracelet","double bracelet","Hard bracelet","Link bracelet"}, // bracelets
        {"Pearl anklet", "Diamond anklet","Double ankle","Stiff ankle","Snake ankle"}, // foot bracelets
        {"falling earings", "hoop earings", "tight earings", "climbing earings", "clip earings" }}; // earings

    private static readonly string[,] _orderNameEmailAdress = new string[3, 3]
    {
        { "Avigail Haim","avi@gmail.com","Hadasim 3 Bney-Brak " },
        {"Ayala Coen","aya@gmail.com","Hpalmach 15 Tel-Aviv" },
        {"Maayan Levi","maayan@gmail.com","Hashikma 12 Or-Yehuda" }
    };
    

    /// <summary>
    /// A function that creates and inserts a new product
    /// </summary>
    private static void createAndInitProducts()
    {
        for (int i = 0; i < 20; i++) // We will initialize 20 products
        {
            int r1 = _rnd.Next(5);
            ProductsList.Add(new()
            {
                ID = i + 100000,
                Category = (Category)r1,
                Name = _productNames[r1, _rnd.Next(5)],
                Price = _rnd.Next(500, 5000),
                InStock = i == 1 ? 0 : _rnd.Next(4),
            });
        }
    }

    /// <summary>
    /// A function that creates and adds the orders
    /// </summary>
    private static void createAndInitOrders()
    {
        Order order;
        for (int i = 0; i < 20; i++)
        {
            int days = _rnd.Next(21, 200);
            int x = _rnd.Next(3);
            order = new()
            {
                ID = NextOrderNumber,
                CustomerName = _orderNameEmailAdress[x, 0],
                CustomerEmail = _orderNameEmailAdress[x, 1],
                CustomerAdress = _orderNameEmailAdress[x, 2],
                OrderDate = DateTime.Now.AddDays(-days),
                ShipDate = null,//DateTime.MinValue,
                DeliveryrDate = null,//DateTime.MinValue,
            };
            if (i < 0.8 * 20)
                order.ShipDate = order.OrderDate + new TimeSpan(_rnd.Next(10, 20), 0, 0, 0);
            if (i < 0.8 * 0.6 * 20)
                order.DeliveryrDate = order.ShipDate + new TimeSpan(_rnd.Next(1, 10), 0, 0, 0);
            OrdersList.Add(order);
        }
    }
    /// <summary>
    /// A function that creates and adds the order details
    /// </summary>
    private static void createAndInitOrderItem()
    {
        int x;
        for (int i = 0; i < 20; i++)
        {
            x = _rnd.Next(1, 5);
            OrderItem oi = new ();
            for (int j = 0; j < x; j++)
            {
                oi.ID = NextOrderItemNumber;
                oi.Amount = _rnd.Next(1, 4);
                oi.ProductID = 100000 + _rnd.Next(0, 20);
                oi.OrderID = 100000 + i;
                oi.Price = _rnd.Next(500, 5000);
            }
            OrderItemsList.Add(oi);
        }
    }
    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItem();
    }
}



