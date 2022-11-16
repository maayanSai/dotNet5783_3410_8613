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
            int chooseEntity;
            int chooseMenu;
            Console.WriteLine("Choose an entity:");
            Console.WriteLine("0: to exit");
            Console.WriteLine("1: to product");
            Console.WriteLine("2: to order");
            Console.WriteLine("3: to orderItem");
            int.TryParse(Console.ReadLine(),out chooseEntity);
            while (chooseEntity != '0')
            {

                try
                { 
                    switch (chooseEntity)
                    {
                        case 1:
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("1: to Add");
                            Console.WriteLine("2: to get product");
                            Console.WriteLine("3: to get all products");
                            Console.WriteLine("4: to update");
                            Console.WriteLine("5: to delete");
                            int.TryParse(Console.ReadLine(), out chooseMenu);
                            switch (chooseMenu)
                            {
                                case 1:
                                    Product p = new Product();
                                    int id, stock; double price;
                                    Category category;
                                    Console.WriteLine("enter id of product");
                                    int.TryParse(Console.ReadLine(), out id);
                                    p.ID = id;
                                    Console.WriteLine("enter name of product");
                                    p.Name = Console.ReadLine();
                                    Console.WriteLine("enter price of product");
                                    double.TryParse(Console.ReadLine(), out price);
                                    p.Price = price;
                                    Console.WriteLine("enter category of product");
                                    Enum.TryParse(Console.ReadLine(), out category);
                                    p.Category = category;
                                    Console.WriteLine("enter inStock of product");
                                    int.TryParse(Console.ReadLine(), out stock);
                                    p.InStock = stock;
                                    Dal.DalProduct.AddToProduct(p);
                                    break;
                                case 2:
                                    int id1;
                                    Console.WriteLine("enter id of product");
                                    int.TryParse(Console.ReadLine(), out id1);
                                    Console.WriteLine(Dal.DalProduct.getProduct(id1));
                                    break;
                                case 3:
                                    Product[] prod = DalProduct.allProduct();
                                    foreach (Product pro in prod)
                                    {
                                        Console.WriteLine(pro);
                                    }
                                    break;
                                case 4:
                                    Product p1 = new Product();
                                    int id2, stock1; double price1;
                                    Category category1;
                                    Console.WriteLine("enter id of product");
                                    int.TryParse(Console.ReadLine(), out id2);
                                    p1.ID = id2;
                                    Console.WriteLine("enter name of product");
                                    p1.Name = Console.ReadLine();
                                    Console.WriteLine("enter price of product");
                                    double.TryParse(Console.ReadLine(), out price1);
                                    p1.Price = price1;
                                    Console.WriteLine("enter category of product");
                                    Enum.TryParse(Console.ReadLine(), out category1);
                                    p1.Category = category1;
                                    Console.WriteLine("enter inStock of product");
                                    int.TryParse(Console.ReadLine(), out stock1);
                                    p1.InStock = stock1;
                                    if (p1.Name.Length > 0)
                                        Dal.DalProduct.updateProcuct(p1);
                                    break;
                                case 5:
                                    int id3;
                                    Console.WriteLine("enter id of product");
                                    int.TryParse(Console.ReadLine(), out id3);
                                    Dal.DalProduct.deleteProcuct(id3);
                                    break;
                            }
                            break;
                        case 2:
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("1: to Add");
                            Console.WriteLine("2: to get order");
                            Console.WriteLine("3: to get all orders");
                            Console.WriteLine("4: to update");
                            Console.WriteLine("5: to delete");
                            int.TryParse(Console.ReadLine(),out chooseMenu);
                            switch (chooseMenu)
                            {
                                case 1:
                                    Order order = new Order();
                                    int id;
                                    Console.WriteLine("enter id of order");
                                    int.TryParse(Console.ReadLine(),out id);
                                    order.ID = id; 
                                    Console.WriteLine("enter customer name of order");
                                    order.CustomerName = Console.ReadLine();
                                    Console.WriteLine("enter customer email of order");
                                    order.CustomerEmail = Console.ReadLine();
                                    Console.WriteLine("enter customer adress of order");
                                    order.CustomerAdress = Console.ReadLine();
                                    order.OrderDate = DateTime.Now;
                                    Dal.DalOrder.addToOrder(order);
                                    break;
                                case 2:
                                    Console.WriteLine("enter id of order");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(Dal.DalOrder.getOrder(id));
                                    break;
                                case 3:
                                    Order[] ord = DalOrder.allOrder();
                                    foreach (Order o in ord)
                                    {
                                        Console.WriteLine(o);
                                    }
                                    break;
                                case 4:
                                    Order order1 = new Order();
                                    Console.WriteLine("enter id of order");
                                    int.TryParse(Console.ReadLine(),out id);
                                    order1.ID = id;
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
                                case 5:
                                    Console.WriteLine("enter id of order");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Dal.DalOrder.deleteOrder(id);
                                    break;
                            }
                            break;
                        case 3:
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("1: to Add");
                            Console.WriteLine("2: to get orderItem");
                            Console.WriteLine("3: to get all orderItems");
                            Console.WriteLine("4: to update");
                            Console.WriteLine("5: to delete");
                            int.TryParse(Console.ReadLine(), out chooseMenu);
                            switch (chooseMenu)
                            {
                                case 1:
                                    OrderItem orderItem = new OrderItem();
                                    int id, idProduct, idOrder, amount;
                                    double price;
                                    Console.WriteLine("enter id of orderItem");
                                    int.TryParse(Console.ReadLine(), out id);
                                    orderItem.ID = id; 
                                    Console.WriteLine("enter product ID of orderItem");
                                    int.TryParse(Console.ReadLine(), out idProduct);
                                    orderItem.ProductID = idProduct;
                                    Console.WriteLine("enter order ID of orderItem");
                                    int.TryParse(Console.ReadLine(), out idOrder);
                                    orderItem.OrderID = idOrder;
                                    Console.WriteLine("enter price of orderItem");
                                    double.TryParse(Console.ReadLine(), out price); 
                                    orderItem.Price = price;
                                    Console.WriteLine("enter amount of orderItem");
                                    int.TryParse(Console.ReadLine(), out amount);
                                    orderItem.Amount = Convert.ToInt32(Console.ReadLine());
                                    Dal.DalOrderItem.addToOrderItem(orderItem);
                                    break;
                                case 2:
                                    Console.WriteLine("enter id of orderItem");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(Dal.DalOrderItem.getOrderItem(id));
                                    break;
                                case 3:
                                    OrderItem[] ord = DalOrderItem.allOrderItem();
                                    foreach (OrderItem o in ord)
                                    {
                                        Console.WriteLine(o);
                                    }
                                    break;
                                case 4:
                                    OrderItem orderItem1 = new OrderItem(); 
                                    Console.WriteLine("enter id of orderItem");
                                    int.TryParse(Console.ReadLine(), out id);
                                    orderItem1.ID = id;
                                    Console.WriteLine("enter product ID of orderItem");
                                    int.TryParse(Console.ReadLine(), out idProduct);
                                    orderItem1.ProductID = idProduct;
                                    Console.WriteLine("enter order ID of orderItem");
                                    int.TryParse(Console.ReadLine(), out idOrder);
                                    orderItem1.OrderID = idOrder;
                                    Console.WriteLine("enter price of orderItem");
                                    double.TryParse(Console.ReadLine(), out price);
                                    orderItem1.Price = price;
                                    Console.WriteLine("enter amount of orderItem");
                                    int.TryParse(Console.ReadLine(), out amount);
                                    orderItem1.Amount = Convert.ToInt32(Console.ReadLine());
                                    if (orderItem1.ProductID > 0)
                                        Dal.DalOrderItem.updateOrederItem(orderItem1);
                                    break;

                                case 5:
                                    Console.WriteLine("enter id of orderItem");
                                    int.TryParse(Console.ReadLine(), out id);
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
                Console.WriteLine("Choose an entity:");
                Console.WriteLine("0: to exit");
                Console.WriteLine("1: to product");
                Console.WriteLine("2: to order");
                Console.WriteLine("3: to orderItem");
                int.TryParse(Console.ReadLine(), out chooseEntity);

            }
        }
    }
}
