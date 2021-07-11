using System;
using System.Collections.Generic;

namespace Core.Protocol
{
    public class SimplePacketHandler<TSender, TPacket> : IPacketHandler<TSender> where TPacket : IPacket, new()
    {
        public ICollection<ushort> HandledPacketIds { get; }

        private Action<TSender, TPacket> _handleAct;

        public SimplePacketHandler(ushort[] handledIds, Action<TSender, TPacket> handleAct)
        {
            HandledPacketIds = new HashSet<ushort>(handledIds);
            
            _handleAct = handleAct;
        }

        public SimplePacketHandler(Action<TSender, TPacket> handleAct)
        {
            HandledPacketIds = new HashSet<ushort>(new[] { new TPacket().PacketId });
            
            _handleAct = handleAct;
        }

        public void HandlePacket(TSender sender, IPacket packet)
        {
            _handleAct(sender, (TPacket) packet);
        }
    }
}