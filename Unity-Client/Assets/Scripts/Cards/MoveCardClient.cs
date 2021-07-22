using Core.Cards;
using Core.Utils;

namespace MagicCardGame
{
    public class MoveLeftCardClient : MoveLeftCard
    {
        public MoveLeftCardClient(ActionCardConfig config, NetVector2 moveVector) : base(config, moveVector) { }
    }
    
    public class MoveRightCardClient : MoveRightCard
    {
        public MoveRightCardClient(ActionCardConfig config, NetVector2 moveVector) : base(config, moveVector) { }
    }
    
    public class MoveUpCardClient : MoveUpCard
    {
        public MoveUpCardClient(ActionCardConfig config, NetVector2 moveVector) : base(config, moveVector) { }
    }
}