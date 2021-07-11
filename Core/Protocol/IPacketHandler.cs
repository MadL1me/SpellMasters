using System.Collections.Generic;

namespace Core.Protocol
{
    public interface IPacketHandler<TSender>
    {
        ICollection<ushort> HandledPacketIds { get; }

        void HandlePacket(TSender sender, IPacket packet);
    }
}