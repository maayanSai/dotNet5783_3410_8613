using Dal;
using DO;
using System;


namespace DalTest
{
    public class Program
    {
        
        private static DalProduct dalProduct = new DalProduct();
        private static DalOrder dalOrder = new DalOrder();
        private static DalOrderItem dalOrderItem = new DalOrderItem();
        static void Main(String[] args)
        {
            char chooseEntity;
            char chooseMenu;
            Console.WriteLine("Choose an entity:");
            Console.WriteLine("0: to exit");
            Console.WriteLine("1: to product");
            Console.WriteLine("2: to order");
            Console.WriteLine("3: to orderItem");
            chooseEntity = Convert.ToChar(Console.ReadLine());
            while (chooseEntity != '0')
            {
                try
                {
                    switch (chooseEntity)
                    {
                        case '1':
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("1: to Add");
                            Console.WriteLine("2: to get product");
                            Console.WriteLine("3: to get all products");
                            Console.WriteLine("4: to update");
                            Console.WriteLine("5: to delete");
                            chooseMenu = Convert.ToChar(Console.ReadLine());

                            switch (chooseMenu)
                            {
                                case '1':
                                    Product p = new Product();
                                    Console.WriteLine("enter id of product");
                                    p.ID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter name of product");
                                    p.Name = Console.ReadLine();
                                    Console.WriteLine("enter price of product");
                                    p.Price = Convert.ToDouble(Console.ReadLine());
                                    Console.WriteLine("enter category of product");
                                    p.Category = (Category)Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter inStock of product");
                                    p.InStock = Convert.ToInt32(Console.ReadLine());
                                    Dal.DalProduct.AddToProduct(p);
                                    break;
                                case '2':
                                    Console.WriteLine("enter id of product");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine(Dal.DalProduct.getProduct(id));
                                    break;
                                case '3':
                                    Product[] prod = DalProduct.allProduct();
                                    foreach (Product pro in prod)
                                    {
                                        Console.WriteLine(pro);
                                    }
                                    break;
                                case '4':
                                    Product p1 = new Product();
                                    Console.WriteLine("enter id of product");
                                    p1.ID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter name of product");
                                    p1.Name = Console.ReadLine();
                                    Console.WriteLine("enter price of product");
                                    p1.Price = Convert.ToDouble(Console.ReadLine());
                                    Console.WriteLine("enter category of product");
                                    p1.Category = (Category)Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter inStock of product");
                                    p1.InStock = Convert.ToInt32(Console.ReadLine());
                                    if (p1.Name.Length > 0)
                                        Dal.DalProduct.updateProcuct(p1);
                                    break;
                                case '5':
                                    Console.WriteLine("enter id of product");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    Dal.DalProduct.deleteProcuct(id);
                                    break;
                            }
                            break;
                        case '2':
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("1: to Add");
                            Console.WriteLine("2: to get order");
                            Console.WriteLine("3: to get all orders");
                            Console.WriteLine("4: to update");
                            Console.WriteLine("5: to delete");
                            chooseMenu = Convert.ToChar(Console.ReadLine());
                            switch (chooseMenu)
                            {
                                case '1':
                                    Order order = new Order();
                                    Console.WriteLine("enter id of order");
                                    order.ID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter customer name of order");
                                    order.CustomerName = Console.ReadLine();
                                    Console.WriteLine("enter customer email of order");
                                    order.CustomerEmail = Console.ReadLine();
                                    Console.WriteLine("enter customer adress of order");
                                    order.CustomerAdress = Console.ReadLine();
                                    order.OrderDate = DateTime.Now;
                                    Dal.DalOrder.addToOrder(order);
                                    break;
                                case '2':
                                    Console.WriteLine("enter id of order");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine(Dal.DalOrder.getOrder(id));
                                    break;
                                case '3':
                                    Order[] ord = DalOrder.allOrder();
                                    foreach (Order o in ord)
                                    {
                                        Console.WriteLine(o);
                                    }
                                    break;
                                case '4':
                                    Order order1 = new Order();
                                    Console.WriteLine("enter id of order");
                                    order1.ID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter customer name of order");
                                    order1.CustomerName = Console.ReadLine();
                                    Console.WriteLine("enter customer email of order");
                                    order1.CustomerEmail = Console.ReadLine();
                                    Console.WriteLine("enter customer adress of order");
                                    order1.CustomerAdress = Console.ReadLine();
                                    order1.OrderDate = DateTime.Now;
                                    if (order1.CustomerName.Length > 0)
                                        Dal.DalOrder.updateOrder(order1);
                                    break;
                                case '5':
                                    Console.WriteLine("enter id of order");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    Dal.DalOrder.deleteOrder(id);
                                    break;
                            }
                            break;
                        case '3':
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("1: to Add");
                            Console.WriteLine("2: to get orderItem");
                            Console.WriteLine("3: to get all orderItems");
                            Console.WriteLine("4: to update");
                            Console.WriteLine("5: to delete");
                            chooseMenu = Convert.ToChar(Console.ReadLine());
                            switch (chooseMenu)
                            {
                                case '1':
                                    OrderItem orderItem = new OrderItem();
                                    Console.WriteLine("enter id of orderItem");
                                    orderItem.ID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter product ID of orderItem");
                                    orderItem.ProductID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter order ID of orderItem");
                                    orderItem.OrderID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter price of orderItem");
                                    orderItem.Price = Convert.ToDouble(Console.ReadLine());
                                    Console.WriteLine("enter amount of orderItem");
                                    orderItem.Amount = Convert.ToInt32(Console.ReadLine());
                                    Dal.DalOrderItem.addToOrderItem(orderItem);
                                    break;
                                case '2':
                                    Console.WriteLine("enter id of orderItem");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine(Dal.DalOrderItem.getOrderItem(id));
                                    break;
                                case '3':
                                    OrderItem[] ord = DalOrderItem.allOrderItem();
                                    foreach (OrderItem o in ord)
                                    {
                                        Console.WriteLine(o);
                                    }
                                    break;
                                case '4':
                                    OrderItem orderItem1 = new OrderItem();
                                    Console.WriteLine("enter id of orderItem");
                                    orderItem1.ID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter product ID of orderItem");
                                    orderItem1.ProductID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter order ID of orderItem");
                                    orderItem1.OrderID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter price of orderItem");
                                    orderItem1.Price = Convert.ToDouble(Console.ReadLine());
                                    Console.WriteLine("enter amount of orderItem");
                                    orderItem1.Amount = Convert.ToInt32(Console.ReadLine());
                                    if (orderItem1.ProductID > 0)
                                        Dal.DalOrderItem.updateOrederItem(orderItem1);
                                    break;

                                case '5':
                                    Console.WriteLine("enter id of orderItem");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    Dal.DalOrderItem.deleteOrederItem(id);
                                    break;
                            }
                            break;
                    }
                }
                catch (Exception str)
                {
                    Console.WriteLine(str);
                }
            }
        }
    }
}
