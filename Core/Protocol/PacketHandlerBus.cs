using System.Collections.Generic;

namespace Core.Protocol
{
    public class PacketHandlerBus<TSender>
    {
        private List<IPacketHandler<TSender>> _handlers;
        
        public PacketCallbackDriver<TSender> CallbackDriver { get; protected set; }

        public PacketHandlerBus()
        {
            _handlers = new List<IPacketHandler<TSender>>();
        }

        /// <summary>
        /// Creates a callback driver for this bus instance using a specified callback dispatcher
        /// </summary>
        public void CreateCallbackDriver(int timeToLive, IPacketCallbackDispatcher dispatcher)
        {
            CallbackDriver = new PacketCallbackDriver<TSender>(timeToLive, dispatcher);
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
            
            if (packet is IPacketSequenceProvider seqProv)
                CallbackDriver?.DispatchCallback(sender, seqProv.GetSequenceId(), packet, seqProv.IsErrorPacket());
        }

        /// <summary>
        /// Performs generic packet handler bus updating
        /// </summary>
        public void Update()
        {
            CallbackDriver?.InvalidateCallbackList();
        }
    }
}