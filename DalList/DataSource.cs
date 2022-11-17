using DO;
using System;

namespace Dal;
internal static class DataSource
{   
    static DataSource() { s_Initialize(); } // A constructive operation, which initializes the entity arrays
    private static readonly Random _rnd = new(); // lottery variable
    internal static Order[] _orderArr = new Order[100]; // An array of orders
    internal static OrderItem[] _orderItemArr = new OrderItem[200]; // An array of items
    internal static Product[] _productArr = new Product[50]; // array of products

    //order
    internal static int _sNextOrderNumber = 0; // Saves the first position of the order array
    internal static int _nextOrderNumber { get=>_sNextOrderNumber++; } // Increases the number of orders

    // orderItem
    internal static int _sNextOrderItemNumber = 0; // Saves the first position of the orderItem array
    internal static int _nextOrderItemArr { get => _sNextOrderItemNumber++; } //  Increases the number of orderItems

    //product                                                                     
    internal static int _arrProductIndex = 0; // Index, by number of products

    // Matrix of product names
    private static string[,] _productNames = new string[5, 5]
        {{"diamond ring", "wedding ring", "pearl ring","double ring","climbing ring" }, // rings
        {"pearl necklace", "long necklace", "tight necklace","diamond necklace","diamond necklace" }, // necklaces
        {"Pearl bracelet","Diamond bracelet","double bracelet","Hard bracelet","Link bracelet"}, // bracelets
        {"Pearl anklet", "Diamond anklet","Double ankle","Stiff ankle","Snake ankle"}, // foot bracelets
        {"falling earings", "hoop earings", "tight earings", "climbing earings", "clip earings" }}; // earings

    // A matrix of customer names, email, and addresses
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
            Product p = new Product(); // We will create a new object
            bool isExist;
            do
            {
                p.ID = _rnd.Next(100000, 1000000); // Negril is a 6-digit ID number
                isExist = false;
                for (int j = 0; j <= _arrProductIndex; j++) // A loop that goes through all the products
                    if (_productArr[j].ID == p.ID) // If the product exists
                        isExist = true; // The product exists
            }
            while (isExist); // As long as the product is available
            p.Category = (Category)_rnd.Next(5); // Negril category
            p.Name = _productNames[(int)p.Category, _rnd.Next(5)]; // Negril Name of a product
            p.Price = _rnd.Next(500, 5000); // Negril price
            // 5 percent of the products for which the amount of inventory should be 0
            if (i == 1) p.InStock = 0; // 5 percent 20 products, this is product 1
            else 
               p.InStock= _rnd.Next(4); // For the rest we will grill a stock amount
            try // If there is an error
            {
                DalProduct.AddToProduct(p); // Call the add function
            }
            catch (Exception str)
            {
                Console.WriteLine(str);
                i--; // We will reabsorb and reduce the number of products
            }
        }
    }
    /// <summary>
    /// A function that creates and adds the orders
    /// </summary>
    private static void createAndInitOrders()
    {
        for (int i = 0; i < 20; i++) // We will initialize 20 orders
        {
            int days = _rnd.Next(21, 200); // Nagrill day
            Order ord = new Order(); // We will create a new object
            ord.ID = 1;
            int x = _rnd.Next(3); 
            ord.CustomerName = _orderNameEmailAdress[x, 0]; // Set a customer name
            ord.CustomerEmail =_orderNameEmailAdress[x, 1]; // Set up a customer email
            ord.CustomerAdress = _orderNameEmailAdress[x, 2]; // Set a customer address
            ord.OrderDate = DateTime.Now.AddDays(-days); // Order day-equal to the current day minus the number of days that have passed since the order
            ord.ShipDate = DateTime.MinValue; // delivery date
            ord.DeliveryrDate = DateTime.MinValue; // Delivery arrival date
            if (i < 0.8 * 20) // 80 percent of orders should have a delivery date
            {
                days = _rnd.Next(10, 20); // Nagrill day
                TimeSpan shipTime = new TimeSpan(days, 0, 0, 0); // We will reset the date to 0
                ord.ShipDate = ord.OrderDate + shipTime;
            }
            if (i < 0.8 * 0.6 * 20) // 48 percent of the 80 percent should have an arrival date
            { 
                days = _rnd.Next(1, 10); // Nagrill day
                TimeSpan deliverTime = new TimeSpan(days, 0, 0, 0); // We will reset the date to 0
                ord.DeliveryrDate = ord.ShipDate + deliverTime;
            }
            DalOrder.AddToOrder(ord); // Call the add function
        }
    }
    /// <summary>
    /// A function that creates and adds the order details
    /// </summary>
    private static void createAndInitOrderItem()
    {
        int y;
        int x;
        for (int i = 0; i < 20; i++) // We will initialize 20 items
        {
            x = _rnd.Next(1, 5); // Negril item type
            for (int j = 0; j < x; j++) //
            {
                OrderItem oi = new OrderItem(); // We will create a new object
                oi.ID = 1;
                oi.Amount = _rnd.Next(1, 4); // Grill quantity
                y = _rnd.Next(0, _arrProductIndex); // Grill an item
                // We will add the item to the array
                oi.ProductID = DataSource._productArr[y].ID;
                oi.OrderID = DataSource._orderArr[i].ID;
                oi.Price = DataSource._productArr[y].Price;
                DalOrderItem.AddToOrderItem(oi);  // Call the add function
            }
        }
    }
    /// <summary>
    /// The action calls the actions that initialize the arrays
    /// </summary>
    private static void s_Initialize()  
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItem();
    }
}



