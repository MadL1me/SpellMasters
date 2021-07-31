using System.Collections.Generic;
using Core.Cards;

namespace Core.Entities
{
    /// <summary>
    /// Represents network player character, his cards deck, health, name, etc.
    /// </summary>
    public class NetworkedPlayer : MobNetworkedEntity
    {
        // These properties override ones in the flyweight entity data.
        // We need to do this in order to apply unique player stats
        public long MaxHealth { get; set; }
        public int MaxEnergy { get; set; }
        public string DisplayedName { get; set; }
        
        public ActionCardsQueueController CardsQueueController { get; set; }

        public NetworkedPlayer(uint networkId) : base(0, networkId)
        {
            // TODO This is weird and we should change this to not show the full queue
            CardsQueueController = new ActionCardsQueueController(this, 5);
        }

        public override void ResetStats()
        {
            Health = MaxHealth;
            Energy = MaxEnergy;
        }

        public virtual void Update()
        {
        }
    }
}