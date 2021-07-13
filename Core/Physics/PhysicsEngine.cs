using System.Collections.Generic;

namespace Core.Collision
{
    public class PhysicsEngine
    {
        public List<GravityComponent> GravityObjects { get; protected set; }
        
        public List<BoxCollider> Colliders { get; protected set;}
        
        public void Update(float deltaTime)
        {
            ApplyGravity(deltaTime);
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            for (var i = 0; i < Colliders.Count; i++)
            {
                for (int j = 0; j < Colliders.Count; j++)
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