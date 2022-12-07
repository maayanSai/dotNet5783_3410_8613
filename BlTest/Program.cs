using DalApi;
using BlApi;
using BO;
internal class Program
{
    private static void Main(string[] args)
    {
        IBl bl = new Bllmplementaion.Bl();
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
                                bl.Order.GetOrders();
                                break;
                            case 2:
                                int id;
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                bl.Order.ItemOrder(id);
                                break;
                            case 3:
                                int id1;
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id1));
                                bl.Order.Updateshipping(id1);
                                break;
                            case 4:
                                int id2;
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id2));
                                bl.Order.Updatesupply(id2);
                                break;
                            case 5:
                                int id3;
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id3));
                                bl.Order.Updatesupply(id3);
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
                                Cart c = new();
                                Console.WriteLine("enter name of customer ");
                                c.CustomerName = Console.ReadLine() ?? "";
                                Console.WriteLine("enter email of customer");
                                c.CustomerEmail = Console.ReadLine() ?? "";
                                Console.WriteLine("enter adress of customer");
                                c.CustomerAdress = Console.ReadLine() ?? " ";
                                c.TotalPrice=
                                bl.Cart.Add();

                                break;
                        }
                        break;
                }