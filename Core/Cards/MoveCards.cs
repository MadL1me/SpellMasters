using System.Threading.Tasks;
using Core.Player;
using Core.Utils;

namespace Core.Cards
{
    public abstract class MoveCard : ActionCard
    {
        protected virtual NetVector2 MoveVector { get; set; }

        public async override Task CastCard(NetworkPlayer networkPlayer, BattleEnvironment environment)
        {
            networkPlayer.Move(MoveVector);
            base.CastCard(networkPlayer, environment);
        }

        protected MoveCard(ActionCardConfig config, NetVector2 moveVector) : base(config)
        {
            MoveVector = moveVector;
        }
    }

    public class MoveLeftCard : MoveCard
    {
        protected override NetVector2 MoveVector => NetVector2.Left;
        public MoveLeftCard(ActionCardConfig config, NetVector2 moveVector) : base(config, moveVector) { }
    }
    
    public class MoveRightCard : MoveCard
    {
        protected override NetVector2 MoveVector => NetVector2.Right;
        public MoveRightCard(ActionCardConfig config , NetVector2 moveVector) : base(config, moveVector) { }
    }
    
    public class MoveUpCard : MoveCard
    {
        protected override NetVector2 MoveVector => NetVector2.Up;
        public MoveUpCard(ActionCardConfig config, NetVector2 moveVector) : base(config, moveVector) { }
    }
}