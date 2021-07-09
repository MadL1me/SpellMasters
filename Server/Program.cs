using System;
using System.IO;
using System.Linq;
using Core.Protocol;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //var server = new GameSimulationServer(3056);
            //server.Run();
            var writer = new OctetWriter();
            writer.WriteVarFixed(-323323.323F);
            
            var bytes = writer.ToArray();
            Console.WriteLine(string.Join(" ", bytes.Select(x => x.ToString("X2"))));

            var ms = new MemoryStream(bytes);
            var reader = new OctetReader(ms);

            Console.WriteLine(reader.ReadVarFixed32());
        }
    }
}