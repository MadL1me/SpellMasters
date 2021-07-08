using Core.Cards;
using Core.Player;

namespace Core.Player
{
    public abstract class BattleEnvironment
    {
        public INetworkPlayer[] NetworkPlayers { get; protected set; }

        public INetworkPlayer GetClosestCharacter(NetVector2 position)
        {
            var minDistance = float.MaxValue;
            INetworkPlayer closestPlayer = null;
            
            foreach (var networkPlayer in NetworkPlayers)
            {
                var distance = NetVector2.Distance(position, networkPlayer.PlayerCharacter.CharacterPosition);
                
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    closestPlayer = networkPlayer;
                }
            }

            return closestPlayer;
        }
        
        public INetworkPlayer GetClosestCharacterExcept(NetVector2 position, INetworkPlayer player)
        {
            var minDistance = float.MaxValue;
            INetworkPlayer closestPlayer = null;
            
            foreach (var networkPlayer in NetworkPlayers)
            {
                if (networkPlayer == player)
                    continue;

                var distance = NetVector2.Distance(position, networkPlayer.PlayerCharacter.CharacterPosition);
                
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    closestPlayer = networkPlayer;
                }
            }

            return closestPlayer;
        }
    }

    public class TwoPlayersBattleEnvironment : BattleEnvironment
    {
        public TwoPlayersBattleEnvironment(INetworkPlayer first, INetworkPlayer second)
        {
            NetworkPlayers = new[]
            {
                first,
                second
            };
        }
    }
}