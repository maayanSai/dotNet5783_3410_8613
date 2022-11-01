using System;
partial class Program
{
    static void Main(string[] args)
    {
        Welcome3410();
        Welcome8613();
        Console.ReadKey();
    }
    static partial void Welcome8613();
    private static void Welcome3410()
    {
        Console.WriteLine("Enter your name: ");
        string userName = Console.ReadLine();
        Console.WriteLine("{0}, welcome to my first console application", userName);
    }
}