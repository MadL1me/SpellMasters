using System;
using Core.Player;
using UnityEngine;

namespace MagicCardGame.Assets.Scripts.GameLogic
{
    public class BattleEnvironmentClient : MonoBehaviour
    {
        private BattleEnvironment _battleEnvironment;

        public void Update()
        {
            _battleEnvironment.Update(Time.deltaTime);
        }
    }
}