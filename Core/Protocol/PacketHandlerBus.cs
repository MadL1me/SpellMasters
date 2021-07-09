using System.Collections.Generic;

namespace Core.Protocol
{
    public class PacketHandlerBus
    {
        private List<IPacketHandler> _handlers;

        public PacketHandlerBus()
        {
            _handlers = new List<IPacketHandler>();
        }

        public void RegisterHandler(IPacketHandler handler)
        {
            _handlers.Add(handler);
        }

        public void HandlePacket(IPacket packet)
        {
            foreach (var handler in _handlers)
            {
                if (!handler.HandledPacketIds.Contains(packet.PacketId))
                    continue;
                
                handler.HandlePacket(packet);
                break;
            }
        }
    }
}