// We did the bonus - we used TryParse for the conversions
using Dal;
using DO;
namespace DalTest
{
    public class Program
    {
        // We will create the entity objects
        private static DalProduct _dalProduct = new DalProduct(); // product
        private static DalOrder _dalOrder = new DalOrder(); // Order
        private static DalOrderItem _dalOrderItem = new DalOrderItem(); // Order item

        //MAIN
        static void Main(String[] args)
        {
            int chooseEntity; // entity selection
            int chooseMenu; // function selection
            Console.WriteLine("Choose an entity:");
            Console.WriteLine("0: to exit");
            Console.WriteLine("1: to product");
            Console.WriteLine("2: to order");
            Console.WriteLine("3: to orderItem");
            int.TryParse(Console.ReadLine(), out chooseEntity); // absorption of an entity
            while (chooseEntity != '0') // As long as 0 is not pressed
            {
                try
                {
                    switch (chooseEntity)
                    {
                        case 1: // product
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("1: to Add");
                            Console.WriteLine("2: to get product");
                            Console.WriteLine("3: to get all products");
                            Console.WriteLine("4: to update");
                            Console.WriteLine("5: to delete");
                            int.TryParse(Console.ReadLine(), out chooseMenu); // We will take a function number
                            switch (chooseMenu)
                            {
                                case 1: // Add
                                    Product p = new Product(); // We will create an object
                                    int id, stock; double price;
                                    Category category;
                                    // We will collect the ID
                                    Console.WriteLine("enter id of product");
                                    int.TryParse(Console.ReadLine(), out id);
                                    p.ID = id;
                                    // We will collect the name
                                    Console.WriteLine("enter name of product");
                                    p.Name = Console.ReadLine();
                                    // We will collect the price
                                    Console.WriteLine("enter price of product");
                                    double.TryParse(Console.ReadLine(), out price);
                                    p.Price = price;
                                    // We will collect the category
                                    Console.WriteLine("enter category of product");
                                    Enum.TryParse(Console.ReadLine(), out category);
                                    p.Category = category;
                                    // We will collect the stock
                                    Console.WriteLine("enter inStock of product");
                                    int.TryParse(Console.ReadLine(), out stock);
                                    p.InStock = stock;
                                    Dal.DalProduct.Add(p); // We will call the add function
                                    break;
                                case 2: // get product
                                    int id1;
                                    // We will collect the ID
                                    Console.WriteLine("enter id of product");
                                    int.TryParse(Console.ReadLine(), out id1);
                                    Console.WriteLine(Dal.DalProduct.GetProduct(id1)); // We will call get
                                    break;
                                case 3: // get all products
                                    IEnumerable<Product> list = Dal.DalProduct.AllProduct();
                                    foreach (Product pro in list) // We will go through all the products and copy to the new system
                                    {
                                        Console.WriteLine(pro);
                                    }
                                    break;
                                case 4: //  to update
                                    Product p1 = new Product(); // We will create an object
                                    int id2, stock1; double price1;
                                    Category category1;
                                    // We will collect the ID
                                    Console.WriteLine("enter id of product");
                                    int.TryParse(Console.ReadLine(), out id2);
                                    p1.ID = id2;
                                    // We will collect the name
                                    Console.WriteLine("enter name of product");
                                    p1.Name = Console.ReadLine();
                                    // We will collect the price
                                    Console.WriteLine("enter price of product");
                                    double.TryParse(Console.ReadLine(), out price1);
                                    p1.Price = price1;
                                    // We will collect the category
                                    Console.WriteLine("enter category of product");
                                    Enum.TryParse(Console.ReadLine(), out category1);
                                    p1.Category = category1;
                                    // We will collect the stock
                                    Console.WriteLine("enter inStock of product");
                                    int.TryParse(Console.ReadLine(), out stock1);
                                    p1.InStock = stock1;
                                    if (p1.Name.Length > 0) // If no blank input is entered
                                        Dal.DalProduct.UpdateProcuct(p1); // We will activate the update function
                                    break;
                                case 5: // to delete
                                    int id3;
                                    // We will collect the ID
                                    Console.WriteLine("enter id of product");
                                    int.TryParse(Console.ReadLine(), out id3);
                                    Dal.DalProduct.DeleteProcuct(id3); // We will activate the delete function
                                    break;
                            }
                            break;
                        case 2: // order
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("1: to Add");
                            Console.WriteLine("2: to get order");
                            Console.WriteLine("3: to get all orders");
                            Console.WriteLine("4: to update");
                            Console.WriteLine("5: to delete");
                            int.TryParse(Console.ReadLine(), out chooseMenu); // We will take a function number
                            switch (chooseMenu)
                            {
                                case 1: // Add
                                    Order order = new Order(); // We will create an object
                                    int id;
                                    order.ID = 1;
                                    // We will collect the name
                                    Console.WriteLine("enter customer name of order");
                                    order.CustomerName = Console.ReadLine();
                                    // We will collect the Email
                                    Console.WriteLine("enter customer email of order");
                                    order.CustomerEmail = Console.ReadLine();
                                    // We will collect the adress
                                    Console.WriteLine("enter customer adress of order");
                                    order.CustomerAdress = Console.ReadLine();
                                    order.OrderDate = DateTime.Now; // We will enter the date
                                    Dal.DalOrder.Add(order); // we call to add function
                                    break;
                                case 2: // get order
                                    Console.WriteLine("enter id of order");
                                    int.TryParse(Console.ReadLine(), out id); // We will collect the ID
                                    Console.WriteLine(Dal.DalOrder.GetOrder(id)); // We will call get
                                    break;
                                case 3: // get all orders
                                    Order[] ord = DalOrder.AllOrder(); // We will create a new array
                                    foreach (Order o in ord) // We will copy the entire array of orders
                                    {
                                        Console.WriteLine(o);
                                    }
                                    break;
                                case 4: // update
                                    Order order1 = new Order(); //  We will create an object
                                    // We will collect the ID
                                    Console.WriteLine("enter id of order");
                                    int.TryParse(Console.ReadLine(), out id);
                                    order1.ID = id;
                                    // We will collect the name
                                    Console.WriteLine("enter customer name of order");
                                    order1.CustomerName = Console.ReadLine();
                                    // We will collect the Email
                                    Console.WriteLine("enter customer email of order");
                                    order1.CustomerEmail = Console.ReadLine();
                                    // We will collect the adress
                                    Console.WriteLine("enter customer adress of order");
                                    order1.CustomerAdress = Console.ReadLine();
                                    order1.OrderDate = DateTime.Now; // We will enter the date
                                    if (order1.CustomerName.Length > 0) // If no blank input is entered
                                        Dal.DalOrder.UpdateOrder(order1); // We will activate the update function
                                    break;
                                case 5: // delete
                                    // We will collect the ID
                                    Console.WriteLine("enter id of order");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Dal.DalOrder.DeleteOrder(id); // We will activate the delete function
                                    break;
                            }
                            break;
                        case 3: //OrderItem
                            Console.WriteLine("Choose one of the following:");
                            Console.WriteLine("1: to Add");
                            Console.WriteLine("2: to get orderItem");
                            Console.WriteLine("3: to get all orderItems");
                            Console.WriteLine("4: to update");
                            Console.WriteLine("5: to delete");
                            int.TryParse(Console.ReadLine(), out chooseMenu); // We will take a function number
                            switch (chooseMenu)
                            {
                                case 1: // Add
                                    OrderItem orderItem = new OrderItem(); // We will create an object
                                    int id, idProduct, idOrder, amount;
                                    double price;
                                    orderItem.ID = 1;
                                    // We will collect the ID
                                    Console.WriteLine("enter product ID of orderItem");
                                    int.TryParse(Console.ReadLine(), out idProduct);
                                    // We will take the ID of the product
                                    orderItem.ProductID = idProduct;
                                    // We will receive the ID of the order
                                    Console.WriteLine("enter order ID of orderItem");
                                    int.TryParse(Console.ReadLine(), out idOrder);
                                    orderItem.OrderID = idOrder;
                                    // We will take the price of the item
                                    orderItem.Price = Dal.DalProduct.GetProduct(orderItem.ProductID).Price;
                                    // We will take the quantity of the item
                                    Console.WriteLine("enter amount of orderItem");
                                    int.TryParse(Console.ReadLine(), out amount);
                                    orderItem.Amount = amount;
                                    Dal.DalOrderItem.Add(orderItem); // We will call the add function
                                    break;
                                case 2: // get orderItem
                                    Console.WriteLine("enter id of orderItem");
                                    int.TryParse(Console.ReadLine(), out id); // We will collect the ID
                                    Console.WriteLine(Dal.DalOrderItem.GetOrderItem(id)); // We will call get
                                    break;
                                case 3: // get all orderItems
                                    OrderItem[] ord = DalOrderItem.AllOrderItem(); // We will create a new array
                                    foreach (OrderItem o in ord) // We will copy all the items
                                    {
                                        Console.WriteLine(o);
                                    }
                                    break;
                                case 4: // update
                                    OrderItem orderItem1 = new OrderItem(); //  We will create an object
                                    // We will collect the ID
                                    Console.WriteLine("enter id of orderItem");
                                    int.TryParse(Console.ReadLine(), out id);
                                    orderItem1.ID = id;
                                    // We will take the ID of the product
                                    Console.WriteLine("enter product ID of orderItem");
                                    int.TryParse(Console.ReadLine(), out idProduct);
                                    orderItem1.ProductID = idProduct;
                                    // We will receive the ID of the order
                                    Console.WriteLine("enter order ID of orderItem");
                                    int.TryParse(Console.ReadLine(), out idOrder);
                                    orderItem1.OrderID = idOrder;
                                    // We will take the price of the item
                                    orderItem1.Price = Dal.DalProduct.GetProduct(orderItem1.ProductID).Price;
                                    // We will take the quantity of the item
                                    Console.WriteLine("enter amount of orderItem");
                                    int.TryParse(Console.ReadLine(), out amount);
                                    orderItem1.Amount = amount;
                                    if (orderItem1.ProductID > 0) // If no blank input is entered
                                        Dal.DalOrderItem.UpdateOrederItem(orderItem1); // We will activate the update function
                                    break;
                                case 5: // delete
                                    Console.WriteLine("enter id of orderItem");
                                    int.TryParse(Console.ReadLine(), out id); // We will collect the ID
                                    Dal.DalOrderItem.DeleteOrederItem(id); //  We will activate the delete function
                                    break;
                            }
                            break;
                    }
                }
                catch (Exception str) // catches the error
                {
                    Console.WriteLine(str); // prints the error
                }
                // Let's choose again which entity we want
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