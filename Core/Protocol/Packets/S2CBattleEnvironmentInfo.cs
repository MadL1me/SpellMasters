using System;
using Core.Cards;
using Core.GameLogic;
using Core.Player;

namespace Core.Protocol.Packets
{
    public class S2CBattleEnvironmentInfo : S2CPacketBase
    {
        public override ushort PacketId => 0x0006;
        public override bool UseEncryption { get; }

        public BattleEnvironment BattleEnvironment;

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteInt32(BattleEnvironment.NetworkPlayers.Length);
            foreach (var networkPlayer in BattleEnvironment.NetworkPlayers)
            {
                writer.WriteBytes(BitConverter.GetBytes(networkPlayer.PlayerCharacter.PlayerCurrentStats.Stamina.Available));
                writer.WriteBytes(BitConverter.GetBytes(networkPlayer.PlayerCharacter.PlayerCurrentStats.Stamina.MaxLimit));
                writer.WriteBytes(BitConverter.GetBytes(networkPlayer.PlayerCharacter.PlayerCurrentStats.Stamina.MinLimit));
                writer.WriteBytes(BitConverter.GetBytes(networkPlayer.PlayerCharacter.PlayerCurrentStats.Stamina.RegenerationSpeed));
                writer.WriteString(networkPlayer.PlayerCharacter.PlayerCurrentStats.DisplayName);
                writer.WriteBytes(BitConverter.GetBytes(networkPlayer.PlayerCharacter.PlayerCurrentStats.Health));
                writer.WriteBool(networkPlayer.PlayerCharacter.CanMove);
                writer.WriteInt32(networkPlayer.PlayerId);
            }
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
            var lobbySize = reader.ReadInt32();
            BattleEnvironment = new BattleEnvironment(lobbySize);
            for (var i = 0; i < lobbySize; ++i)
            {
                var stamina = new Stamina
                {
                    Available = BitConverter.ToSingle(reader.ReadBytes(4), 0),
                    MaxLimit = BitConverter.ToSingle(reader.ReadBytes(4), 0),
                    MinLimit = BitConverter.ToSingle(reader.ReadBytes(4), 0),
                    RegenerationSpeed = BitConverter.ToSingle(reader.ReadBytes(4), 0)
                };
                var networkPlayerStats = new NetworkPlayerStats
                {
                    DisplayName = reader.ReadString(),
                    Health = BitConverter.ToSingle(reader.ReadBytes(4), 0),
                    Stamina = stamina
                };
                var playerCharacter = new NetworkPlayerCharacter(networkPlayerStats)
                {
                    CanMove = reader.ReadBool()
                };
                var networkPlayer = new NetworkPlayer
                {
                    PlayerId = reader.ReadInt32(),
                    PlayerCharacter = playerCharacter
                };
                var cardsQueueController = new ActionCardsQueueController(networkPlayer, reader.ReadInt32());
                networkPlayer.CardsQueueController = cardsQueueController;
                BattleEnvironment.NetworkPlayers[i] = networkPlayer;
            }
        }
    }
}