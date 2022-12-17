namespace Dal;
using DO;
using System;

/// <summary>
/// DataSource
/// </summary>
internal static class DataSource
{
    /// <summary>
    /// A constructive operation, which initializes the entity arrays
    /// </summary>
    static DataSource() { s_Initialize(); }
    private static readonly Random _rnd = new(); // lottery variable

    internal static List<Product?> s_productsList = new(); // An array of orders
    internal static List<Order?> s_ordersList = new(); // An array of items
    internal static List<OrderItem?> s_orderItemsList = new(); // array of products

    internal const int _startOrderNumber = 1000;
    private static int s_nextOrderNumber = _startOrderNumber;
    internal static int s_NextOrderNumber { get => s_nextOrderNumber++; }

    internal const int _startOrderItemNumber = 1000;
    private static int s_nextOrderItemNumber = _startOrderItemNumber;
    internal static int s_NextOrderItemNumber { get => s_nextOrderItemNumber++; }

    /// <summary>
    /// Matrix of product names
    /// </summary>
    private static readonly string[,] s_productNames = new string[5, 5]
        {{"diamond ring", "wedding ring", "pearl ring","double ring","climbing ring" }, // rings
        {"pearl necklace", "long necklace", "tight necklace","diamond necklace","diamond necklace" }, // necklaces
        {"Pearl bracelet","Diamond bracelet","double bracelet","Hard bracelet","Link bracelet"}, // bracelets
        {"Pearl anklet", "Diamond anklet","Double ankle","Stiff ankle","Snake ankle"}, // foot bracelets
        {"falling earings", "hoop earings", "tight earings", "climbing earings", "clip earings" }}; // earings
    /// <summary>
    /// matrix of order Names,Email and Adress
    /// </summary>
    private static readonly string[,] s_orderNameEmailAdress = new string[3, 3]
    {
        { "Avigail Haim","avi@gmail.com","Hadasim 3 Bney-Brak " },
        {"Ayala Coen","aya@gmail.com","Hpalmach 15 Tel-Aviv" },
        {"Maayan Levi","maayan@gmail.com","Hashikma 12 Or-Yehuda" }
    };
    /// <summary>
    /// A function that creates and inserts a new product
    /// </summary>
    private static void s_createAndInitProducts()
    {
        for (int i = 0; i < 20; i++) // We will initialize 20 products
        {
            int r1 = _rnd.Next(5);
            s_productsList.Add(new()
            {
                ID = i + 100000,
                Category = (Category)r1,
                Name = s_productNames[r1, _rnd.Next(5)],
                Price = _rnd.Next(500, 5000),
                InStock = i == 1 ? 0 : _rnd.Next(4),
            });
        }
    }
    /// <summary>
    /// A function that creates and adds the orders
    /// </summary>
    private static void s_createAndInitOrders()
    {
        Order order;
        for (int i = 0; i < 20; i++)
        {
            int days = _rnd.Next(21, 200);
            int x = _rnd.Next(3);
            order = new()
            {
                ID = s_NextOrderNumber,
                CustomerName = s_orderNameEmailAdress[x, 0],
                CustomerEmail = s_orderNameEmailAdress[x, 1],
                CustomerAdress = s_orderNameEmailAdress[x, 2],
                OrderDate = DateTime.Now.AddDays(-days),
                ShipDate = null,
                DeliveryrDate = null,
            };
            if (i < 0.8 * 20)
                order.ShipDate = order.OrderDate + new TimeSpan(_rnd.Next(10, 20), 0, 0, 0);
            if (i < 0.8 * 0.6 * 20)
                order.DeliveryrDate = order.ShipDate + new TimeSpan(_rnd.Next(1, 10), 0, 0, 0);
            s_ordersList.Add(order);
        }
    }
    /// <summary>
    /// A function that creates and adds the order details
    /// </summary>
    private static void s_createAndInitOrderItem()
    {
        int x;
        for (int i = 0; i < 20; i++)
        {
            x = _rnd.Next(1, 5);
            OrderItem oi = new();
            for (int j = 0; j < x; j++)
            {
                oi.ID = s_NextOrderItemNumber;
                oi.Amount = _rnd.Next(1, 4);
                oi.OrderID = 1000 + i;
                do
                {
                    oi.ProductID = 100000 + _rnd.Next(0, 20);
                } while (s_orderItemsList.Exists(p => p?.ProductID == oi.ProductID && p?.OrderID == oi.OrderID));
                oi.Price = _rnd.Next(500, 5000);
                s_orderItemsList.Add(oi);
            }   
        }
    }
    /// <summary>
    /// A static function that creates the order, product, and items
    /// </summary>
    private static void s_Initialize()
    {
        s_createAndInitProducts();
        s_createAndInitOrders();
        s_createAndInitOrderItem();
    }
}



