using Core.Player;

namespace Server
{
    public class GameSimulationServer
    {
        public BattleEnvironment Environment { get; }

        public bool IsRunning { get; private set; }
        
        public GameSimulationServer(BattleEnvironment environment)
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