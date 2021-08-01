using System;
using Core.Utils;
using Server.GameLogic;
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
            
            var lobbiesBus = new LobbiesServerPacketBus();
            var lobbiesController = new LobbiesController(server, lobbiesBus);
            
            server.Listen(9669);
        }
    }
}
