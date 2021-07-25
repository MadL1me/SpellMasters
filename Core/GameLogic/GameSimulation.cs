using Core.GameLogic;

namespace Server
{
    public class GameSimulation
    {
        public BattleEnvironment Environment { get; }

        public bool IsRunning { get; private set; }
        
        public GameSimulation(BattleEnvironment environment)
        {
            Environment = environment;
        }

        public void Start() => IsRunning = true;

        public void Stop() => IsRunning = false;
        
        public void Update(float deltaTime)
        {
            if (!IsRunning)
                return;
            
            Environment.Update(deltaTime);
        }
    }
}