// We did the bonus - we used TryParse for the conversions
namespace DalTest;
using DalApi;
using DO;
public class Program
{
    /// <summary>
    /// MAIN
    /// </summary>
    static void Main()
    {
        IDal dal = new Dal.DalList();
        int chooseMenu, chooseEntity; // function and entity selection
        Console.WriteLine("Choose an entity:");
        Console.WriteLine("0: to exit");
        Console.WriteLine("1: to product");
        Console.WriteLine("2: to order");
        Console.WriteLine("3: to orderItem");
        while (!int.TryParse(Console.ReadLine(), out chooseEntity)) // absorption of an entity
            Console.Write("Please enter a number (your choice):");
        while (chooseEntity != 0) // As long as 0 is not pressed
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
                        while (!int.TryParse(Console.ReadLine(), out chooseMenu)) // We will take a function number
                            Console.Write("Please enter a number (your choice):");
                        switch (chooseMenu)
                        {
                            case 1: // Add
                                Product p = new(); // We will create an object
                                int id, stock; double price;
                                Category category;
                                do { Console.WriteLine("enter id of product"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                p.ID = id;
                                Console.WriteLine("enter name of product");
                                p.Name = Console.ReadLine() ?? "";
                                do { Console.WriteLine("enter price of product"); }
                                while (!double.TryParse(Console.ReadLine(), out price));
                                p.Price = price;
                                do { Console.WriteLine("enter category of product"); }
                                while (!Enum.TryParse(Console.ReadLine(), out category));
                                p.Category = category;
                                do { Console.WriteLine("enter inStock of product"); }
                                while (!int.TryParse(Console.ReadLine(), out stock));
                                p.InStock = stock;
                                dal.Product.Add(p); // We will call the add function
                                break;
                            case 2: // get product
                                int id1;
                                do { Console.WriteLine("enter id of product"); }
                                while (!int.TryParse(Console.ReadLine(), out id1));
                                Console.WriteLine(dal.Product.GetById(id1)); // We will call get
                                break;
                            case 3: // get all products
                                IEnumerable<Product?> list = dal.Product.GetAll();
                                foreach (var pro in list) // We will go through all the products and copy to the new system
                                    Console.WriteLine(pro);
                                break;
                            case 4: //  to update
                                Product p1 = new(); // We will create an object
                                int id2, stock1; double price1;
                                Category category1;
                                do { Console.WriteLine("enter id of product"); }
                                while (int.TryParse(Console.ReadLine(), out id2));
                                p1.ID = id2;
                                Console.WriteLine("enter name of product");
                                p1.Name = Console.ReadLine() ?? "";
                                do { Console.WriteLine("enter price of product"); }
                                while (!double.TryParse(Console.ReadLine(), out price1));
                                p1.Price = price1;
                                do { Console.WriteLine("enter category of product"); }
                                while (!Enum.TryParse(Console.ReadLine(), out category1));
                                p1.Category = category1;
                                do { Console.WriteLine("enter inStock of product"); }
                                while (!int.TryParse(Console.ReadLine(), out stock1));
                                p1.InStock = stock1;
                                if (p1.Name?.Length > 0) // If no blank input is entered
                                    dal.Product.Update(p1); // We will activate the update function
                                break;
                            case 5: // to delete
                                int id3;
                                do { Console.WriteLine("enter id of product"); }
                                while (!int.TryParse(Console.ReadLine(), out id3));
                                dal.Product.Delete(id3); // We will activate the delete function
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
                        while (!int.TryParse(Console.ReadLine(), out chooseMenu)) // We will take a function number
                            Console.Write("Please enter a number (your choice):");
                        switch (chooseMenu)
                        {
                            case 1: // Add
                                Order order = new(); // We will create an object
                                int id;
                                order.ID = 1;
                                Console.WriteLine("enter customer name of order");
                                order.CustomerName = Console.ReadLine() ?? "";
                                Console.WriteLine("enter customer email of order");
                                order.CustomerEmail = Console.ReadLine() ?? "";
                                Console.WriteLine("enter customer adress of order");
                                order.CustomerAdress = Console.ReadLine() ?? "";
                                order.OrderDate = DateTime.Now; // We will enter the date
                                dal.Order.Add(order); // we call to add function
                                break;
                            case 2: // get order
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                Console.WriteLine(dal.Order.GetById(id)); // We will call get
                                break;
                            case 3: // get all orders
                                IEnumerable<Order?> list = dal.Order.GetAll(); // We will create a new array
                                foreach (var o in list) // We will copy the entire array of orders
                                    Console.WriteLine(o);
                                break;
                            case 4: // update
                                Order order1 = new(); //  We will create an object
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                order1.ID = id;
                                Console.WriteLine("enter customer name of order");
                                order1.CustomerName = Console.ReadLine() ?? "";
                                Console.WriteLine("enter customer email of order");
                                order1.CustomerEmail = Console.ReadLine() ?? "";
                                Console.WriteLine("enter customer adress of order");
                                order1.CustomerAdress = Console.ReadLine() ?? "";
                                order1.OrderDate = DateTime.Now;
                                if (order1.CustomerName?.Length > 0) // If no blank input is entered
                                    dal.Order.Update(order1); // We will activate the update function
                                break;
                            case 5: // delete
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                dal.Order.Delete(id); // We will activate the delete function
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
                        while (!int.TryParse(Console.ReadLine(), out chooseMenu)) // We will take a function number
                            Console.Write("Please enter a number (your choice):");
                        switch (chooseMenu)
                        {
                            case 1: // Add
                                OrderItem orderItem = new(); // We will create an object
                                int id, idProduct, idOrder, amount;
                                orderItem.ID = 1;
                                do { Console.WriteLine("enter product ID of orderItem"); }
                                while (!int.TryParse(Console.ReadLine(), out idProduct));
                                orderItem.ProductID = idProduct;
                                do { Console.WriteLine("enter order ID of orderItem"); }
                                while (!int.TryParse(Console.ReadLine(), out idOrder));
                                orderItem.OrderID = idOrder;
                                orderItem.Price = dal.Product.GetById(orderItem.ProductID)?.Price ?? 0;
                                do { Console.WriteLine("enter amount of orderItem"); }
                                while (!int.TryParse(Console.ReadLine(), out amount));
                                orderItem.Amount = amount;
                                dal.OrderItem.Add(orderItem); // We will call the add function
                                break;
                            case 2: // get orderItem
                                do { Console.WriteLine("enter id of orderItem"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                Console.WriteLine(dal.OrderItem.GetById(id)); // We will call get
                                break;
                            case 3: // get all orderItems
                                IEnumerable<OrderItem?> list = dal.OrderItem.GetAll(); // We will create a new array
                                foreach (var o in list) // We will copy all the items
                                    Console.WriteLine(o);
                                break;
                            case 4: // update
                                OrderItem orderItem1 = new(); //  We will create an object
                                do { Console.WriteLine("enter id of orderItem"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                orderItem1.ID = id;
                                do { Console.WriteLine("enter product ID of orderItem"); }
                                while (!int.TryParse(Console.ReadLine(), out idProduct));
                                orderItem1.ProductID = idProduct;
                                do { Console.WriteLine("enter order ID of orderItem"); }
                                while (!int.TryParse(Console.ReadLine(), out idOrder));
                                orderItem1.OrderID = idOrder;
                                orderItem1.Price = dal.Product.GetById(orderItem1.ProductID)?.Price ?? 0;
                                do { Console.WriteLine("enter amount of orderItem"); }
                                while (!int.TryParse(Console.ReadLine(), out amount));
                                orderItem1.Amount = amount;
                                if (orderItem1.ProductID > 0) // If no blank input is entered
                                    dal.OrderItem.Update(orderItem1); // We will activate the update function
                                break;
                            case 5: // delete
                                do { Console.WriteLine("enter id of orderItem"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                dal.OrderItem.Delete(id); //  We will activate the delete function
                                break;
                        }
                        break;
                    case 0:
                        break;
                }
            }
            catch (Exception str) // catches the error
            {
                Console.WriteLine(str);
            }
            // Let's choose again which entity we want
            Console.WriteLine("Choose an entity:");
            Console.WriteLine("0: to exit");
            Console.WriteLine("1: to product");
            Console.WriteLine("2: to order");
            Console.WriteLine("3: to orderItem");
            while (!int.TryParse(Console.ReadLine(), out chooseEntity))
                Console.Write("Please enter a number (your choice):");
        }
    }
}
