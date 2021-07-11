using System;
using System.IO;
using System.Linq;
using System.Text;
using Core.Protocol;
using Server.Protocol;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running server");

            var registry = new ClientRegistry(50);
            var bus = new PacketHandlerBus<ClientWrapper>();

            var server = new ServerListener(registry, bus);
            server.Listen(9669);
        }
    }
}