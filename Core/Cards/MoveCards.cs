using Core.Player;

namespace Core.Cards
{
    public abstract class MoveCard : ActionCard
    {
        protected abstract NetVector2 MoveVector { get; }

        public override void CastCard(INetworkPlayer networkPlayer)
        {
            networkPlayer.Move(MoveVector);
            base.CastCard(networkPlayer);
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

    public struct NetVector2
    {
        public float X;
        public float Y;

        public NetVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static readonly NetVector2 Left = new NetVector2(-1,0);
        public static readonly NetVector2 Up = new NetVector2(0,1);
        public static readonly NetVector2 Right = new NetVector2(1,0);
        public static readonly NetVector2 Down = new NetVector2(0,-1);
        public static readonly NetVector2 Zero = new NetVector2(0,0);
    }
}