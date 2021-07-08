using Core.Player;
using LiteNetLib.Utils;

namespace Core.Cards
{
    public abstract class ActionCard
    {
        public abstract int CardId { get; }
        public abstract int EnergyCost { get; }
        public abstract void CastCard(INetworkPlayer networkPlayer);

        public static ActionCard CreateCardFromNetwork(NetDataReader dataReader)
        {
            /*
            switch (dataReader.GetByte())
            {
                case 0:
                    return new MoveLeftCard();
                case 1:
                    return new MoveRightCard();
                case 2:
                    return new MoveUpCardClient();
            }
            */

            return null;
        }
    }
}