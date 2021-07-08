using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new GameServer(3056);
            server.Run();
        }
    }
}