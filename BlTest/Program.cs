﻿namespace BlTest;
using BO;
using BlApi;
using BlImplementation;

 public class Program
{
    static IBl bl = new Bl();
    static void Main()
    {
        int id, amount, stock;
        double price;
        int chooseMenu, chooseEntity; // function and entity selection
        Console.WriteLine("Choose an entity:");
        Console.WriteLine("0: to exit");
        Console.WriteLine("1: to order");
        Console.WriteLine("2: to cart");
        Console.WriteLine("3: to product");
        while (!int.TryParse(Console.ReadLine(), out chooseEntity)) // absorption of an entity
            Console.Write("Please enter a number (your choice):");
        Console.WriteLine();
        while (chooseEntity != 0) // As long as 0 is not pressed
        {
            try
            {
                switch (chooseEntity)
                {
                    case 1: // order
                        Console.WriteLine("Choose one of the following:");
                        Console.WriteLine("1: to get all orders");
                        Console.WriteLine("2: to get items order");
                        Console.WriteLine("3: to Update shipping");
                        Console.WriteLine("4: to Update supply");
                        Console.WriteLine("5: to tracking");
                        Console.WriteLine("6: to exit");
                        while (!int.TryParse(Console.ReadLine(), out chooseMenu)) // We will take a function number
                            Console.Write("Please enter a number (your choice):");
                        Console.WriteLine();
                        switch (chooseMenu)
                        {
                            case 1: // get all orders
                                var list= bl.Order.GetOrders();
                                foreach(var item in list)
                                    Console.WriteLine(item);
                                Console.WriteLine();
                                break;
                            case 2: // get items order
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                BO.Order o = bl.Order.ItemOrder(id);
                                Console.WriteLine(o);
                                Console.WriteLine();
                                break;
                            case 3: // Update shipping
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                Console.WriteLine(bl.Order.Updateshipping(id));
                                Console.WriteLine();
                                break;
                            case 4: // Update supply
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                Console.WriteLine(bl.Order.Updatesupply(id));
                                Console.WriteLine();
                                break;
                            case 5: // tracking
                                do { Console.WriteLine("enter id of order"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                Console.WriteLine(bl.Order.Tracking(id));
                                Console.WriteLine();
                                break;
                            case 6: // exit
                                break;
                            default:
                                throw new Exception("the choice has not correct");
                        }
                        break;
                    case 2: // cart
                        Console.WriteLine("Choose one of the following:");
                        Console.WriteLine("1: to Add");
                        Console.WriteLine("2: Update");
                        Console.WriteLine("3: Make an order");
                        Console.WriteLine("4: to exit");
                        while (!int.TryParse(Console.ReadLine(), out chooseMenu)) // We will take a function number
                            Console.Write("Please enter a number (your choice):");
                        Console.WriteLine();
                        switch (chooseMenu)
                        {
                            case 1: // Add
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
                                Console.WriteLine("enter price of product");
                                while (!double.TryParse(Console.ReadLine(), out price)) ;
                                Console.WriteLine("enter amount of product");
                                while (!int.TryParse(Console.ReadLine(), out amount)) ;
                                c.TotalPrice = price * amount;
                                c.Items = new List<BO.OrderItem>();
                                Console.WriteLine(bl.Cart.Add(c, idProduct));
                                Console.WriteLine();
                                break;
                            case 2: // Update
                                int idProduct1;
                                Cart c1 = new();
                                do { Console.WriteLine("enter id of product"); }
                                while (!int.TryParse(Console.ReadLine(), out idProduct1));
                                Console.WriteLine("enter name of customer ");
                                c1.CustomerName = Console.ReadLine() ?? "";
                                Console.WriteLine("enter email of customer");
                                c1.CustomerEmail = Console.ReadLine() ?? "";
                                Console.WriteLine("enter adress of customer");
                                c1.CustomerAdress = Console.ReadLine() ?? " ";
                                Console.WriteLine("enter price of product");
                                while (!double.TryParse(Console.ReadLine(), out price)) ;
                                Console.WriteLine("enter amount of product");
                                while (!int.TryParse(Console.ReadLine(), out amount)) ;
                                c1.TotalPrice = price * amount;
                                c1.Items = new List<BO.OrderItem>();
                                Console.WriteLine(bl.Cart.Update(c1, idProduct1, amount));
                                Console.WriteLine();
                                break;
                            case 3: // Make an order
                                Cart c2 = new();
                                Console.WriteLine("enter name of customer ");
                                c2.CustomerName = Console.ReadLine() ?? "";
                                Console.WriteLine("enter email of customer");
                                c2.CustomerEmail = Console.ReadLine() ?? "";
                                Console.WriteLine("enter adress of customer");
                                c2.CustomerAdress = Console.ReadLine() ?? " ";
                                Console.WriteLine("enter price of product");
                                while (!double.TryParse(Console.ReadLine(), out price)) ;
                                Console.WriteLine("enter amount of product");
                                while (!int.TryParse(Console.ReadLine(), out amount)) ;
                                c2.TotalPrice = price * amount;
                                c2.Items = new List<BO.OrderItem>();
                                bl.Cart.MakeAnOrder(c2);
                                Console.WriteLine();
                                break;
                            case 4: // exit
                                break;
                            default:
                                throw new Exception("the choice has not correct");
                        }
                        break;
                    case 3: // product
                        Console.WriteLine("Choose one of the following:");
                        Console.WriteLine("1: to Get products");
                        Console.WriteLine("2: to get item product by id");
                        Console.WriteLine("3: to get item product by id and cart");
                        Console.WriteLine("4: to add");
                        Console.WriteLine("5: to delete");
                        Console.WriteLine("6: to update");
                        Console.WriteLine("7: to exit");
                        while (!int.TryParse(Console.ReadLine(), out chooseMenu)) // We will take a function number
                            Console.Write("Please enter a number (your choice):");
                        Console.WriteLine();
                        switch (chooseMenu)
                        {
                            case 1: // Get products
                                var list = bl.Product.GetProducts();
                                foreach (var item in list)
                                    Console.WriteLine(item);
                                Console.WriteLine();
                                break;
                            case 2: // get item product by id
                                do { Console.WriteLine("enter id of product"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                BO.Product p = bl.Product.ItemProduct(id);
                                Console.WriteLine(p);
                                Console.WriteLine();
                                break;
                            case 3: // get item product by id and cart
                                Cart cart = new();
                                do { Console.WriteLine("enter id of product"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                Console.WriteLine(bl.Product.ItemProduct(id, cart));
                                Console.WriteLine();
                                break;
                            case 4: // add
                                BO.Category category;
                                BO.Product p1 = new();
                                do { Console.WriteLine("enter id of product"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                p1.ID = id;
                                Console.WriteLine("enter name of product ");
                                p1.Name = Console.ReadLine() ?? "";
                                do { Console.WriteLine("enter stock of product"); }
                                while (!int.TryParse(Console.ReadLine(), out stock));
                                p1.InStock = stock;
                                do { Console.WriteLine("enter price of product"); }
                                while (!double.TryParse(Console.ReadLine(), out price));
                                p1.Price = price;
                                do { Console.WriteLine("enter category of product"); }
                                while (!Enum.TryParse(Console.ReadLine(), out category));
                                p1.Category = category;
                                bl.Product.Add(p1);
                                Console.WriteLine();
                                break;
                            case 5: // delete
                                do { Console.WriteLine("enter id of product"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                bl.Product.Delete(id);
                                Console.WriteLine();
                                break;
                            case 6: // update
                                BO.Category category1;
                                BO.Product p2 = new();
                                do { Console.WriteLine("enter id of product"); }
                                while (!int.TryParse(Console.ReadLine(), out id));
                                p2.ID = id;
                                Console.WriteLine("enter name of product ");
                                p2.Name = Console.ReadLine() ?? "";
                                do { Console.WriteLine("enter stock of product"); }
                                while (!int.TryParse(Console.ReadLine(), out stock));
                                p2.InStock = stock;
                                do { Console.WriteLine("enter price of product"); }
                                while (!double.TryParse(Console.ReadLine(), out price));
                                p2.Price = price;
                                do { Console.WriteLine("enter category of product"); }
                                while (!Enum.TryParse(Console.ReadLine(), out category1));
                                p2.Category = category1;
                                bl.Product.Update(p2);
                                Console.WriteLine();
                                break;
                            case 7: // exit
                                break;
                            default:
                                throw new Exception("the choice has not correct");
                        }
                        break;
                    case 0:
                        break;
                    default:
                        throw new Exception("the choice has not correct");
                }
            }
            catch (Exception str) // catches the error
            {
                Console.WriteLine(str); // prints the error
            }
            Console.WriteLine("Choose an entity:");
            Console.WriteLine("0: to exit");
            Console.WriteLine("1: to order");
            Console.WriteLine("2: to cart");
            Console.WriteLine("3: to product");
            while (!int.TryParse(Console.ReadLine(), out chooseEntity)) // absorption of an entity
                Console.Write("Please enter a number (your choice):");
            Console.WriteLine();
        }
    }

}
