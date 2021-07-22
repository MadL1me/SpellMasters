using System;
using Server.Protocol;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running server");

            var registry = new ClientRegistry(50);
            var bus = new ServerPacketBus();
            var server = new ServerListener(registry, bus);
            server.Listen(9669);
        }
    }
}