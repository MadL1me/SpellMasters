using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running server");
            var server = new Server(50, 9669);
        }
    }
}