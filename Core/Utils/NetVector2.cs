using System;

namespace Core.Utils
{
    public struct NetVector2
    {
        public float X;
        public float Y;

        public NetVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static float Distance(NetVector2 vector, NetVector2 secondVector)
        {
            return (float)Math.Sqrt(Math.Pow(vector.X - secondVector.X, 2) + Math.Pow(vector.Y - secondVector.Y, 2));
        }

        public static bool operator ==(NetVector2 first, NetVector2 second) =>
            first.X == second.X && first.Y == second.Y;

        public static bool operator !=(NetVector2 first, NetVector2 second) => !(first == second);

        public static NetVector2 operator +(NetVector2 first, NetVector2 second)
            => new NetVector2(first.X + second.X, first.Y + second.Y);

        public static NetVector2 operator *(NetVector2 first, float second)
            => new NetVector2(first.X * second, first.Y * second);
        
        public static readonly NetVector2 Left = new NetVector2(-1,0);
        public static readonly NetVector2 Up = new NetVector2(0,1);
        public static readonly NetVector2 Right = new NetVector2(1,0);
        public static readonly NetVector2 Down = new NetVector2(0,-1);
        public static readonly NetVector2 Zero = new NetVector2(0,0);
    }
}