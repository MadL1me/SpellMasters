﻿using Core.Utils;

namespace Core.Entities
{
    public interface INetworkObject
    {
        int TypeId { get; }
        NetVector2 Position { get; }
    }
}