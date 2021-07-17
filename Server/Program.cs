using System;
using Core.Protocol;
using Server.Protocol;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running server");
            Server server = new Server(50, 9669);
        }
    }
}