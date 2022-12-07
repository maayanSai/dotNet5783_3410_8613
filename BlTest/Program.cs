using DalApi;
using BlApi;
using BO;
public class Program
{
    //MAIN
    static void Main()
    {
        IBl bl = new IBl();
        int id;
        int chooseMenu, chooseEntity; // function and entity selection
        Console.WriteLine("Choose an entity:");
        Console.WriteLine("0: to exit");
        Console.WriteLine("1: to order");
        Console.WriteLine("2: to cart");
        Console.WriteLine("3: to product");
        Console.WriteLine("3: to bl");
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
                        Console.WriteLine("1: to get all orders");
                        Console.WriteLine("2: to get items order");
                        Console.WriteLine("3: to Update shipping");
                        Console.WriteLine("4: to Update supply");
                        Console.WriteLine("5: to tracking");
                        Console.WriteLine("6: to exit");
                        while (!int.TryParse(Console.ReadLine(), out chooseMenu)) // We will take a function number
                            Console.Write("Please enter a number (your choice):");
                        switch (chooseMenu)
                        { 
                            case 1:
                                IEnumerable<OrderForList?> list = bl.Order.GetOrders();
                                foreach (var order in list) // We will go through all the products and copy to the new system
                                    Console.WriteLine(order);
                                break;
                            case 2:
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                Console.WriteLine(bl.Order.ItemOrder(id).ToString()); 
                                break;
                            case 3:
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                bl.Order.Updateshipping(id);
                                break;
                            case 4:
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                bl.Order.Updatesupply(id);
                                break;
                            case 5:
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                bl.Order.Updatesupply(id);
                                break;
                            case 6:
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("Choose one of the following:");
                        Console.WriteLine("1: to Add");
                        Console.WriteLine("2: Update");
                        Console.WriteLine("3: MakeAnOrder");
                        Console.WriteLine("4: to exit");
                        while (!int.TryParse(Console.ReadLine(), out chooseMenu)) // We will take a function number
                            Console.Write("Please enter a number (your choice):");
                        switch (chooseMenu)
                        {
                            case 1:
                                int idProduct;
                                Cart c = new();
                                do { Console.WriteLine("enter id of product"); }
                                while (!int.TryParse(Console.ReadLine(), out idProduct));
                                Console.WriteLine("enter name of customer ");
                                c.CustomerName = Console.ReadLine() ?? "";
                                Console.WriteLine("enter email of customer");
                                c.CustomerEmail = Console.ReadLine() ?? "";
                                Console.WriteLine("enter adress of customer");
                                c.CustomerAdress = Console.ReadLine() ?? " ";
                                c.TotalPrice=
                                bl.Cart.Add(c,idProduct);

                                break;
                        }
                        break;
                }