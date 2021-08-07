using System.Collections.Generic;

namespace Core.Collision
{
    /// <summary>
    /// Custom self-made simple physics engine. Supports only box colliders with AABB checks.
    /// </summary>
    public class PhysicsEngine
    {
        public List<GravityComponent> GravityObjects { get; protected set; } = new List<GravityComponent>();
        public List<BoxCollider> Colliders { get; protected set; } = new List<BoxCollider>();
        
        public void Update(float deltaTime)
        {
            ApplyGravity(deltaTime);
            CheckCollisions();
        }

        public void AddObjectToWorld(GravityComponent component)
        {
            GravityObjects.Add(component);
        }

        public void AddObjectToWorld(BoxCollider component)
        {
            Colliders.Add(component);
        }


        private void CheckCollisions()
        {
            for (var i = 0; i < Colliders.Count; i++)
            {
                for (var j = 0; j < Colliders.Count; j++)
                {
                    if (i == j)
                        continue;
                    
                    Colliders[i].IsColliding(Colliders[j]);
                }                
            }
        }

        private void ApplyGravity(float deltaTime)
        {
            foreach (var gravityObject in GravityObjects)
                gravityObject.Update(deltaTime);
        }
    }
}