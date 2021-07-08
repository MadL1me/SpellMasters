using Core.Cards;
using Core.Player;

namespace Server
{
    public class ActionCardServer : ActionCard
    {
        public override int CardId { get; }
        public override int EnergyCost { get; }
        public override void CastCard(INetworkPlayer networkPlayer)
        {
            
        }
    }
}