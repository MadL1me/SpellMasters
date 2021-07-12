using System;
using System.Collections;
using System.Collections.Generic;
using MagicCardGame.Assets.Scripts.Protocol;
using UnityEngine;

namespace MagicCardGame
{
    public class NetworkManager : MonoBehaviour
    {
        private double _lastUpdate;

        private void Update()
        {
            if (Time.timeSinceLevelLoadAsDouble - 0.015 > _lastUpdate)
            {
                _lastUpdate = Time.timeSinceLevelLoadAsDouble;
                NetworkProvider.Poll();
            }
        }
    }
}
