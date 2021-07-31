using System.Threading.Tasks;
using Core.Entities;
using Core.GameLogic;
using Core.Utils;

namespace Core.Cards
{
    /// <summary>
    /// Class of all ActionCards, which maps data and behaviour
    /// </summary>
    public class ActionCard : FlyweightInstance<CardData>
    {
        public ActionCard(uint cardId) : base(cardId) { }
        
        public CardBehaviour GetCardBehaviour() => 
            FlyweightStorage<CardBehaviour>.Instance.GetData(NumericId);
        
        public Type GetCardBehaviourAs<Type>() where Type : CardBehaviour => 
            (Type) FlyweightStorage<CardBehaviour>.Instance.GetData(NumericId);
        
        /// <summary>
        /// Called when the player is about to cast the card
        /// </summary>
        public void ExecuteCast(BattleEnvironment battle, NetworkedPlayer player) =>
            GetCardBehaviour().ExecuteCast(battle, player, this);
    }
}