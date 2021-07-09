namespace Core.GameLogic
{
    public class Stamina
    {
        public float Available
        {
            get => _available;
            set => _available = value > MaxLimit ? MaxLimit : value;
        }

        public float MinLimit;
        public float MaxLimit;
        public float RegenerationSpeed; // stamina per second
        private float _available;

        public Stamina(float minLimit = 0, float maxLimit = 100, float regenerationSpeed = 10)
        {
            MinLimit = minLimit;
            MaxLimit = maxLimit;
            Available = maxLimit;
            RegenerationSpeed = regenerationSpeed;
        }

        public Stamina(float maxLimit = 100, float regenerationSpeed = 10)
        {
            MinLimit = 0;
            MaxLimit = maxLimit;
            Available = maxLimit;
            RegenerationSpeed = regenerationSpeed;
        }

        public bool Spend(float amount)
        {
            if (amount <= Available) return false;
            Available -= amount;
            return true;
        }

        public float Update(float deltaTime)
        {
            Available += deltaTime * RegenerationSpeed;
            return Available;
        }
    }
}