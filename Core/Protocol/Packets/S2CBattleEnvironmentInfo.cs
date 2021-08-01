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


        public uint LocalPlayerNetworkId { get; set; }
        public uint LobbyId { get; set; }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteUVarInt(LobbyId);
            writer.WriteUVarInt((uint) BattleEnvironment.NetworkPlayers.Length);

            foreach (var networkPlayer in BattleEnvironment.NetworkPlayers)
            {
                writer.WriteUVarInt(networkPlayer.NetworkId);
                writer.WriteString(networkPlayer.DisplayedName);
                writer.WriteVarInt(networkPlayer.MaxHealth);
                writer.WriteVarInt(networkPlayer.MaxEnergy);

                if (LocalPlayerNetworkId == networkPlayer.NumericId)
                {
                    writer.WriteUVarInt((uint) networkPlayer.CardsQueueController.CardsInHand.Length);
                    foreach (var actionCard in networkPlayer.CardsQueueController.CardsInHand)
                        writer.WriteUVarInt(actionCard.NumericId);
                    writer.WriteUVarInt((uint) networkPlayer.CardsQueueController.NextDropCards.Count);
                    foreach (var actionCard in networkPlayer.CardsQueueController.NextDropCards)
                        writer.WriteUVarInt(actionCard.NumericId);
                }
            }
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
            LobbyId = reader.ReadUVarInt32();
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

                if (LocalPlayerNetworkId == networkPlayer.NumericId)
                {
                    var cardsInHandCount = reader.ReadUVarInt32();
                    var cardsQueueController = new ActionCardsQueueController(networkPlayer, cardsInHandCount);

                    for (var j = 0; j < cardsInHandCount; ++j)
                        cardsQueueController.CardsInHand[j] = new ActionCard(reader.ReadUVarInt32());

                    var cardsQueueCount = reader.ReadUVarInt32();
                    for (var j = 0; j < cardsQueueCount; ++j)
                        cardsQueueController.NextDropCards.Enqueue(new ActionCard(reader.ReadUVarInt32()));
                    networkPlayer.CardsQueueController = cardsQueueController;
                }


                BattleEnvironment.NetworkPlayers[i] = networkPlayer;
            }
        }
    }
}