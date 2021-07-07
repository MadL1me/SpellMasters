namespace MagicCardGame.Network
{
    public interface INetworkGamePlayer
    {
        bool IsLocal { get; }
        
        public void Move(int units);
    }
}