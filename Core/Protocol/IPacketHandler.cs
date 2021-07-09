using System.Collections.Generic;

namespace Core.Protocol
{
    public interface IPacketHandler
    {
        public ICollection<ushort> HandledPacketIds { get; }

        public void HandlePacket(IPacket packet);
    }
}