namespace Core.GameLogic
{
    public class Stamina
    {
        public float MinLimit;
        public float MaxLimit;
        public float RegenerationSpeed;
        private float _available;
        
        
        public float Available
        {
            get => _available;
            set => _available = value > MaxLimit ? MaxLimit : value < MinLimit ? MinLimit : value;
        }


        public Stamina(float minLimit = 0, float maxLimit = 100, float regenerationSpeed = 10)
        {
            MinLimit = minLimit;
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