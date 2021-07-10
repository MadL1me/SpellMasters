using System.Collections.Generic;

namespace Core.Protocol
{
    public interface IPacketHandler
    {
        ICollection<ushort> HandledPacketIds { get; }

        void HandlePacket(IPacket packet);
    }
}