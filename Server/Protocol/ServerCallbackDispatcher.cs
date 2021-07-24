using Core.Protocol;
using Core.Protocol.Packets;

namespace Server.Protocol
{
    public class ServerCallbackDispatcher : PacketCallbackDispatcher<ClientWrapper, C2SCallbackPacketBase, C2SErrorPacket>
    {
        
    }
}