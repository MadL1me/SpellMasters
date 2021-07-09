using Core.Player;
using Core.Utils;

namespace Core.Cards
{
    public abstract class MoveCard : ActionCard
    {
        protected abstract NetVector2 MoveVector { get; }

        public override void CastCard(INetworkPlayer networkPlayer, BattleEnvironment environment)
        {
            networkPlayer.Move(MoveVector);
            base.CastCard(networkPlayer, environment);
        }

        protected MoveCard(ActionCardConfig config) : base(config) { }
    }

    public abstract class MoveLeftCard : MoveCard
    {
        protected override NetVector2 MoveVector => NetVector2.Left;
        protected MoveLeftCard(ActionCardConfig config) : base(config) { }
    }
    
    public abstract class MoveRightCard : MoveCard
    {
        protected override NetVector2 MoveVector => NetVector2.Right;
        protected MoveRightCard(ActionCardConfig config) : base(config) { }
    }
    
    public abstract class MoveUpCard : MoveCard
    {
        protected override NetVector2 MoveVector => NetVector2.Up;
        protected MoveUpCard(ActionCardConfig config) : base(config) { }
    }
}