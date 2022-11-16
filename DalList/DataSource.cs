//using System.Diagnostics;
//using System.Xml.Linq;
using DO;
namespace Dal;

internal static class DataSource
{
    static DataSource() { s_Initialize(); }
    private static readonly Random rnd = new();
    internal static Order[] orderArr = new Order[100];
    internal static OrderItem[] orderItemArr = new OrderItem[200];
    internal static Product[] productArr = new Product[50];
    internal class Config
    {
        // order

        // internal const int s_startOrderNumber = 0;
        internal static int s_NextOrderNumber = 0; //s_startOrderNumber;
        internal static int NextOrderNumber { get=>s_NextOrderNumber++; }
        // orderItem

        // internal const int s_startOrderItemNumber = 0;
        internal static int s_NextOrderItemNumber = 0; //s_startOrderItemNumber;
        internal static int NextOrderItemArr { get => s_NextOrderItemNumber++; }
        // Product
        internal static int ArrProductIndex = 0;

    }
    private static string[,] productNames = new string[5, 5]
        {{"diamond ring", "wedding ring", "pearl ring","double ring","climbing ring" }, // rings
        {"pearl necklace", "long necklace", "tight necklace","diamond necklace","diamond necklace" }, // necklaces
        {"Pearl bracelet","Diamond bracelet","double bracelet","Hard bracelet","Link bracelet"}, // bracelets
        {"Pearl anklet", "Diamond anklet","Double ankle","Stiff ankle","Snake ankle"}, // foot bracelets
        {"falling earings", "hoop earings", "tight earings", "climbing earings", "clip earings" }}; // earings
    private static string[,] orderNameEmailAdress = new string[3, 3]
    {
        { "Avigail Haim","avi@gmail.com","Hadasim 3 Bney-Brak " },
        {"Ayala Coen","aya@gmail.com","Hpalmach 15 Tel-Aviv" },
        {"Maayan Levi","maayan@gmail.com","Hashikma 12 Or-Yehuda" }
    };
    private static void createAndInitProducts()
    {
        for (int i = 0; i < 20; i++)
        {
            Product p = new Product();

            bool isExist;
            do
            {
                p.ID = rnd.Next(100000, 1000000);
                isExist = false;

                for (int j = 0; j <= Config.ArrProductIndex; j++)
                    if (productArr[j].ID == p.ID)
                        isExist = true;
            }
            while (isExist);

            p.Category = (Category)rnd.Next(5);
            p.Name = productNames[(int)p.Category, rnd.Next(5)];
            p.Price = rnd.Next(500, 5000);
            if (i == 1) p.InStock = 0;//חמש אחוז אפס
            else
               p.InStock= rnd.Next(4);
            try
            {
                DalProduct.AddToProduct(p);

            }
            catch (Exception str)
            {
                Console.WriteLine(str);
                i--;
            }


        }
    }
    private static void createAndInitOrders()
    {

        for (int i = 0; i < 20; i++)
        {
            int days = rnd.Next(21, 200);
            Order ord = new Order();
            ord.ID = 1;
            int x = rnd.Next(3);
            ord.CustomerName = orderNameEmailAdress[x, 0];
            ord.CustomerEmail = orderNameEmailAdress[x, 1];
            ord.CustomerAdress = orderNameEmailAdress[x, 2];
            ord.OrderDate = DateTime.Now.AddDays(-days);
            ord.ShipDate = DateTime.MinValue;
            ord.DeliveryrDate = DateTime.MinValue;
            if (i < 0.8 * 20)
            {
                days = rnd.Next(10, 20);
                TimeSpan shipTime = new TimeSpan(days, 0, 0, 0);
                ord.ShipDate = ord.OrderDate + shipTime;
            }
            if (i < 0.8 * 0.6 * 20)
            {
                days = rnd.Next(1, 10);
                TimeSpan deliverTime = new TimeSpan(days, 0, 0, 0);
                ord.DeliveryrDate = ord.ShipDate + deliverTime;
            }
            orderArr[i] = ord;

        }
    }
    private static void createAndInitOrderItem()
    {
        int y;
        int x;
        for (int i = 0; i < 20; i++)
        {
            x = rnd.Next(1, 5);
            for (int j = 0; j < x; j++)
            {
                OrderItem oi = new OrderItem();
                oi.ID = 1;
                oi.Amount = rnd.Next(1, 4);
                y = rnd.Next(0, Config.ArrProductIndex);
                oi.ProductID = DataSource.productArr[y].ID;
                oi.OrderID = DataSource.orderArr[i].ID;
                oi.Price = DataSource.productArr[y].Price;
                orderItemArr[i + j] = oi;
            }
        }

    }
    private static void s_Initialize()
    {

        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItem();

    }
}



