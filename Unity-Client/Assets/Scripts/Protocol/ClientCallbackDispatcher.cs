using Core.Protocol;
using Core.Protocol.Packets;

namespace MagicCardGame.Assets.Scripts.Protocol
{
    public class ClientCallbackDispatcher : PacketCallbackDispatcher<ServerConnection, S2CCallbackPacketBase, S2CErrorPacket>
    {
        
    }
}