using System;
using Core.Cards;
using Core.Entities;
using Core.GameLogic;

namespace Core.Protocol.Packets
{
    public class S2CBattleEnvironmentInfo : S2CPacketBase
    {
        public override ushort PacketId => 0x0006;
        public override bool UseEncryption => true;

        public BattleEnvironment BattleEnvironment;

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteUVarInt((uint) BattleEnvironment.NetworkPlayers.Length);
            
            foreach (var networkPlayer in BattleEnvironment.NetworkPlayers)
            {
                writer.WriteUVarInt(networkPlayer.NetworkId);
                writer.WriteString(networkPlayer.DisplayedName);
                writer.WriteVarInt(networkPlayer.MaxHealth);
                writer.WriteVarInt(networkPlayer.MaxEnergy);
                writer.WriteUVarInt((uint) networkPlayer.CardsQueueController.CardsInHand.Length);
            }
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
            var lobbySize = (int) reader.ReadUVarInt32();
            BattleEnvironment = new BattleEnvironment(lobbySize);
            
            for (var i = 0; i < lobbySize; ++i)
            {
                var networkPlayer = new NetworkedPlayer(reader.ReadUVarInt32())
                {
                    DisplayedName = reader.ReadString(),
                    MaxHealth = reader.ReadVarInt64(),
                    MaxEnergy = reader.ReadVarInt32()
                };
                
                var cardsQueueController = new ActionCardsQueueController(networkPlayer, (int) reader.ReadUVarInt32());
                
                networkPlayer.CardsQueueController = cardsQueueController;
                
                BattleEnvironment.NetworkPlayers[i] = networkPlayer;
            }
        }
    }
}