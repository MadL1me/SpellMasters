using Core.Cards;

namespace MagicCardGame
{
    public class MoveLeftCardClient : MoveLeftCard
    {
        public MoveLeftCardClient(ActionCardConfig config) : base(config) { }
    }
    
    public class MoveRightCardClient : MoveRightCard
    {
        public MoveRightCardClient(ActionCardConfig config) : base(config) { }
    }
    
    public class MoveUpCardClient : MoveUpCard
    {
        public MoveUpCardClient(ActionCardConfig config) : base(config) { }
    }
}