using System;
using Core.Cards;
using Core.GameLogic;
using Core.Player;

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
                writer.WriteReal32(networkPlayer.PlayerCharacter.PlayerCurrentStats.Stamina.Available);
                writer.WriteReal32(networkPlayer.PlayerCharacter.PlayerCurrentStats.Stamina.MaxLimit);
                writer.WriteReal32(networkPlayer.PlayerCharacter.PlayerCurrentStats.Stamina.MinLimit);
                writer.WriteReal32(networkPlayer.PlayerCharacter.PlayerCurrentStats.Stamina.RegenerationSpeed);
                writer.WriteString(networkPlayer.PlayerCharacter.PlayerCurrentStats.DisplayName);
                writer.WriteReal32(networkPlayer.PlayerCharacter.PlayerCurrentStats.Health);
                writer.WriteBool(networkPlayer.PlayerCharacter.CanMove);
                writer.WriteUVarInt((uint) networkPlayer.PlayerId);
                writer.WriteUVarInt((uint) networkPlayer.CardsQueueController.CardsInHand.Length);
            }
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
            var lobbySize = (int) reader.ReadUVarInt32();
            BattleEnvironment = new BattleEnvironment(lobbySize);
            for (var i = 0; i < lobbySize; ++i)
            {
                var stamina = new Stamina
                {
                    Available = reader.ReadReal32(),
                    MaxLimit = reader.ReadReal32(),
                    MinLimit = reader.ReadReal32(),
                    RegenerationSpeed = reader.ReadReal32()
                };
                var networkPlayerStats = new NetworkPlayerStats
                {
                    DisplayName = reader.ReadString(),
                    Health = reader.ReadReal32(),
                    Stamina = stamina
                };
                var playerCharacter = new NetworkPlayerCharacter(networkPlayerStats)
                {
                    CanMove = reader.ReadBool()
                };
                var networkPlayer = new NetworkPlayer
                {
                    PlayerId = (int) reader.ReadUVarInt32(),
                    PlayerCharacter = playerCharacter
                };
                var cardsQueueController = new ActionCardsQueueController(networkPlayer, (int) reader.ReadUVarInt32());
                networkPlayer.CardsQueueController = cardsQueueController;
                BattleEnvironment.NetworkPlayers[i] = networkPlayer;
            }
        }
    }
}