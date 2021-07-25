using Core.Utils;

namespace Core.Entities
{
    /// <summary>
    /// Represents a type of entity
    /// </summary>
    public class EntityData
    {
        public long MaxHealth { get; set; }
        public int MaxEnergy { get; set; }
        public string DisplayedName { get; set; }
        public NetVector2 Bounds { get; set; }

        static EntityData()
        {
            var storage = FlyweightStorage<EntityData>.Instance;

            // 0: Player
            storage.RegisterData(new EntityData
            {
                MaxHealth = 100,
                MaxEnergy = 100,
                DisplayedName = "",
                Bounds = new NetVector2(5, 10)
            });
        }
    }
}