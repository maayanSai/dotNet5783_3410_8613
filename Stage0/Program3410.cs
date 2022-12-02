using System;

namespace Stage0
{
    partial class Program
    {
        static void Main()
        {
            Welcome3410();
            Welcome8613();
            Console.ReadKey();
        }
        static partial void Welcome8613();
        private static void Welcome3410()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine() ?? "";
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
    }
}
