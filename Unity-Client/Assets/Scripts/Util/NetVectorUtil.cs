using Core.Utils;
using UnityEngine;

namespace MagicCardGame.Assets.Scripts.Util
{
    public static class NetVectorUtil
    {
        public static Vector2 AsVector2(this NetVector2 vector) => new Vector2(vector.X, vector.Y);
    }
}