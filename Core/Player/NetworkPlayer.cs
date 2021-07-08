using Core.Cards;
using LiteNetLib;
using LiteNetLib.Utils;

namespace Core.Player
{
    public interface INetworkPlayer
    {
        void Move(NetVector2 vector);
        void SendCardCastToServer(int CardId);
        void MoveCards();
        void Update();
    }
    
    public abstract class NetworkPlayer : INetworkPlayer
    {
        public abstract void Move(NetVector2 vector);
        public abstract void SendCardCastToServer(int CardId);
        public abstract void MoveCards();
        public abstract void Update();
    }

    public class NetworkPlayerStats 
    {
        public int Health;
        public int Energy;
    }
}