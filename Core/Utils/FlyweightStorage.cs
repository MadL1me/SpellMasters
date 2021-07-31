using System;
using System.Collections.Generic;
using Core.Cards;
using Core.Entities;
using Core.GameLogic;

namespace Core.Utils
{
    /// <summary>
    /// Represents a flyweight object data storage
    /// </summary>
    public class FlyweightStorage<TData>
    {
        public static FlyweightStorage<TData> Instance { get; } = new FlyweightStorage<TData>();
        
        private List<TData> _datas;

        public FlyweightStorage()
        {
            _datas = new List<TData>();
        }

        /// <summary>
        /// This is required to ensure that data holders are initialized
        /// </summary>
        static FlyweightStorage()
        {
            new EntityData();
            new CardData();
            new EntityEffectData();
        }

        /// <summary>
        /// Registers flyweight data and returns its id
        /// </summary>
        public uint RegisterData(TData data)
        {
            _datas.Add(data);
            return (uint)(_datas.Count - 1);
        }

        /// <summary>
        /// Returns flyweight data by its id or null if the id is invalid
        /// </summary>
        public TData GetData(uint id)
        {
            if (id >= _datas.Count)
                return default;

            return _datas[(int) id];
        }
    }
}