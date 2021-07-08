using Core.Player;

namespace Core.Cards
{
    public abstract class MoveCard : ActionCard
    {
        public override int EnergyCost => 0;
        protected abstract NetVector2 MoveVector { get; }

        public abstract override void CastCard(INetworkPlayer networkPlayer);
    }

    public abstract class MoveLeftCard : MoveCard
    {
        public override int CardId => 0;
        public override int EnergyCost => 0;
        public abstract override void CastCard(INetworkPlayer networkPlayer);
        protected override NetVector2 MoveVector => NetVector2.Left;
    }
    
    public abstract class MoveRightCard : MoveCard
    {
        public override int CardId => 1;
        public override int EnergyCost => 0;
        public abstract override void CastCard(INetworkPlayer networkPlayer);
        protected override NetVector2 MoveVector => NetVector2.Right;
    }
    
    public abstract class MoveUpCard : MoveCard
    {
        public override int CardId => 2;
        public override int EnergyCost => 0;
        public abstract override void CastCard(INetworkPlayer networkPlayer);
        protected override NetVector2 MoveVector => NetVector2.Up;
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