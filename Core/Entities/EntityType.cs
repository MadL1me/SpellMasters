using System;

namespace Core.Entities
{
    public class EntityType
    {
        private static EntityType[] _entityTypes = new EntityType[256];
        
        public string Name { get; set; }
        public long MaxHealth { get; set; }

        public static void RegisterEntityType(EntityType type, int id = -1)
        {
            var realId = id;

            if (realId == -1)
            {
                for (var i = 0; i < _entityTypes.Length; i++)
                {
                    if (_entityTypes[i] == null)
                    {
                        realId = i;
                        break;
                    }
                }
            }

            if (realId == -1 || _entityTypes[realId] != null)
                throw new Exception("Could not register entity type " + type.Name);

            _entityTypes[realId] = type;
        }

        public static EntityType GetTypeById(int id) => id > 0 && id < _entityTypes.Length ? _entityTypes[id] : null;
    }
}