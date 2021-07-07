using LiteNetLib.Utils;
using MagicCardGame.Network;

namespace MagicCardGame
{
    public abstract class ActionCardClient
    {
        public abstract int EnergyCost { get; }
       
        public abstract void CastCard(INetworkGamePlayer networkGamePlayer);

        public static ActionCardClient CreateCardFromNetwork(NetDataReader dataReader)
        {
            switch (dataReader.GetByte())
            {
                case 0:
                    return new MoveCardClient();
                case 1:
                    return null;
            }

            return null;
        }
    }
}