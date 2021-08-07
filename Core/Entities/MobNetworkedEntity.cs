using Core.GameLogic;

namespace Core.Entities
{
    /// <summary>
    /// Class that represents all player-like creature entities
    /// </summary>
    public abstract class MobNetworkedEntity : PhysicalNetworkedEntity
    {
        //S2CPlayersRegularData relies on this data to transmit it across the network
        //If you change it,then change in the S2CPlayersRegularData then
        public virtual long Health { get; set; }
        public virtual int Energy { get; set; }
        
        public MobNetworkedEntity(uint typeId, uint networkId) : base(typeId, networkId)
        { }

        /// <summary>
        /// Resets mob stats back to their initial values. This has to be called
        /// when the entity spawns in the world
        /// </summary>
        public virtual void ResetStats()
        {
            Health = SharedData.MaxHealth;
            Energy = SharedData.MaxEnergy;
        }
    }
}