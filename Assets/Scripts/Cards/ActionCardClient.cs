using LiteNetLib.Utils;
using MagicCardGame.Network;

namespace MagicCardGame
{
    public abstract class ActionCardClient
    {
        public abstract int CardId { get; }
        public abstract int EnergyCost { get; }
        public abstract void CastCard(INetworkPlayer networkPlayer);

        public static ActionCardClient CreateCardFromNetwork(NetDataReader dataReader)
        {
            switch (dataReader.GetByte())
            {
                case 0:
                    return new MoveLeftCardClient();
                case 1:
                    return new MoveRightCardClient();
                case 2:
                    return new MoveUpCardClient();
            }

            return null;
        }
    }
}