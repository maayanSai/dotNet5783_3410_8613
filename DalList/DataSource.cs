 using DO;
namespace Dal;
using System;
internal static class DataSource
{
    static DataSource() { _sInitialize(); } // A constructive operation, which initializes the entity arrays
    private static readonly Random _rnd = new(); // lottery variable

    internal static List<Product?> ProductsList = new List<Product?>(); // An array of orders
    internal static List<Order?> OrdersList = new List<Order?>(); // An array of items
    internal static List<OrderItem?> OrderItemsList = new List<OrderItem?>(); // array of products

    internal const int s_startOrderNumber= 1000;
    private static int s_nextOrderNumber = s_startOrderNumber;
    internal static int NextOrderNumber { get => s_nextOrderNumber++; }

    internal const int s_startOrderItemNumber = 1000;
    private static int s_nextOrderItemNumber = s_startOrderItemNumber;
    internal static int NextOrderItemNumber { get => s_nextOrderItemNumber++; }

    // Matrix of product names
    private static string[,] _productNames = new string[5, 5]
        {{"diamond ring", "wedding ring", "pearl ring","double ring","climbing ring" }, // rings
        {"pearl necklace", "long necklace", "tight necklace","diamond necklace","diamond necklace" }, // necklaces
        {"Pearl bracelet","Diamond bracelet","double bracelet","Hard bracelet","Link bracelet"}, // bracelets
        {"Pearl anklet", "Diamond anklet","Double ankle","Stiff ankle","Snake ankle"}, // foot bracelets
        {"falling earings", "hoop earings", "tight earings", "climbing earings", "clip earings" }}; // earings

    private static string[,] _orderNameEmailAdress = new string[3, 3]
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
            Product p = new Product();
            p.ID = i + 100000;
            p.Category = (Category)_rnd.Next(5);
            p.Name = _productNames[(int)p.Category, _rnd.Next(5)];
            p.Price = _rnd.Next(500, 5000);
            if (i == 1) p.InStock = 0; //חמש אחוז אפס
            else
                p.InStock = _rnd.Next(4);
            ProductsList.Add(p);
        }
    }
    /// <summary>
    /// A function that creates and adds the orders
    /// </summary>
    private static void createAndInitOrders()
    {
        for (int i = 0; i < 20; i++)
        {
            int days = _rnd.Next(21, 200);
            Order ord = new Order();
            ord.ID = NextOrderNumber;
            int x = _rnd.Next(3);
            ord.CustomerName = _orderNameEmailAdress[x, 0];
            ord.CustomerEmail = _orderNameEmailAdress[x, 1];
            ord.CustomerAdress = _orderNameEmailAdress[x, 2];
            ord.OrderDate = DateTime.Now.AddDays(-days);
            ord.ShipDate = DateTime.MinValue;
            ord.DeliveryrDate = DateTime.MinValue;
            if (i < 0.8 * 20)
            {
                days = _rnd.Next(10, 20);
                TimeSpan shipTime = new TimeSpan(days, 0, 0, 0);
                ord.ShipDate = ord.OrderDate + shipTime;
            }
            if (i < 0.8 * 0.6 * 20)
            {
                days = _rnd.Next(1, 10);
                TimeSpan deliverTime = new TimeSpan(days, 0, 0, 0);
                ord.DeliveryrDate = ord.ShipDate + deliverTime;
            }
            OrdersList.Add(ord);
        }
    }
    /// <summary>
    /// A function that creates and adds the order details
    /// </summary>
    private static void createAndInitOrderItem()
    {
        int x, y;
        for (int i = 0; i < 20; i++)
        {
            x = _rnd.Next(1, 5);
            OrderItem oi = new OrderItem();
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
    private static void _sInitialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItem();
    }
}



