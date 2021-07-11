using Core.Protocol;
using Core.Protocol.Packets;
using UnityEngine;

namespace MagicCardGame.Assets.Scripts.Protocol
{
    public class ClientPacketBus : PacketHandlerBus<ServerConnection>
    {
        public ClientPacketBus()
        {
            // encryption request
            RegisterHandler(new SimplePacketHandler<ServerConnection, S2CEncryptionRequest>((connection, packet) =>
            {
                Debug.Log("Received encryption request " + packet.EncryptionAlgorithm);
            }));
        }
    }
}