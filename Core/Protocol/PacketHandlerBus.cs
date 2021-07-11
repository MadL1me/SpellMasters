using System.Collections.Generic;

namespace Core.Protocol
{
    public class PacketHandlerBus<TSender>
    {
        private List<IPacketHandler<TSender>> _handlers;

        public PacketHandlerBus()
        {
            _handlers = new List<IPacketHandler<TSender>>();
        }

        public void RegisterHandler(IPacketHandler<TSender> handler)
        {
            _handlers.Add(handler);
        }

        public void HandlePacket(TSender sender, IPacket packet)
        {
            foreach (var handler in _handlers)
            {
                if (!handler.HandledPacketIds.Contains(packet.PacketId))
                    continue;
                
                handler.HandlePacket(sender, packet);
                break;
            }
        }
    }
}