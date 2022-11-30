using DO;
namespace Dal;
internal static class DataSource
{   
    static DataSource() { _sInitialize(); }
    private static readonly Random _rnd = new();
    internal static List<Product?> ProductsList { get; } = new List<Product?>();
    internal static List<Order?> OrdersList { get; } = new List<Order?>();
    internal static List<OrderItem?> OrderItemsList { get; } = new List<OrderItem?>();

    // order
    // internal const int s_startOrderNumber = 0;
    internal static int _sNextOrderNumber = 0; //s_startOrderNumber;
    internal static int _nextOrderNumber { get=>_sNextOrderNumber++; }
    // orderItem
    // internal const int s_startOrderItemNumber = 0;
    internal static int _sNextOrderItemNumber = 0; //s_startOrderItemNumber;
    internal static int _nextOrderItem { get => _sNextOrderItemNumber++; }
        // Product
    internal static int _ProductIndex = 0;

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
    private static void _createAndInitProducts()
    {
        for (int i = 0; i < 20; i++)
        {
            Product p = new Product();
            p.ID = i + 100000;
            p.Category = (Category)_rnd.Next(5);
            p.Name = _productNames[(int)p.Category, _rnd.Next(5)];
            p.Price = _rnd.Next(500, 5000);
            if (i == 1) p.InStock = 0; //חמש אחוז אפס
            else
               p.InStock= _rnd.Next(4);
            ProductsList.Add(p);
        }
    }
    private static void _createAndInitOrders()
    {
        for (int i = 0; i < 20; i++)
        {
            int days = _rnd.Next(21, 200);
            Order ord = new Order();
            ord.ID = _nextOrderNumber;
            int x = _rnd.Next(3);
            ord.CustomerName = _orderNameEmailAdress[x, 0];
            ord.CustomerEmail =_orderNameEmailAdress[x, 1];
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
            DalOrder.Add(ord);
        }
    }
    private static void _createAndInitOrderItem()
    {
        int x, y;
        for (int i = 0; i < 20; i++)
        {
            x = _rnd.Next(1, 5);
            OrderItem oi = new OrderItem();
            for (int j = 0; j < x; j++)
            {
                oi.ID = _nextOrderItem;
                oi.Amount = _rnd.Next(1, 4);
                oi.ProductID = 100000 + _rnd.Next(0, 20);
                oi.OrderID = 100000 + i;
                oi.Price = _rnd.Next(500, 5000);
            }
            DalOrderItem.Add(oi);
        }
    }
    private static void _sInitialize()
    {
        _createAndInitProducts();
        _createAndInitOrders();
        _createAndInitOrderItem();
    }
}



