using System.Collections.Generic;
using System.Linq;

namespace Core.Protocol
{
    /// <summary>
    /// Manages active callback sequences for packets
    /// </summary>
    public class PacketCallbackDriver<TSender>
    {
        public struct Entry
        {
            public object Event;
            public object ErrorEvent;
            public short TimeToLive;
        }
        
        /// <summary>
        /// Default callback time of life in invalidation cycles
        /// </summary>
        public int DefaultTimeToLive { get; set; }
        
        /// <summary>
        /// The default callback dispatcher for this driver
        /// </summary>
        public IPacketCallbackDispatcher Dispatcher { get; protected set; }

        private Dictionary<uint, Entry> _activeCallbacks;
        private object _callbackLock;

        /// <summary>
        /// Initializes the packet callback driver
        /// </summary>
        /// <param name="timeToLive">Maximum time of life of callbacks when no callback is received (number of invalidation cycles)</param>
        public PacketCallbackDriver(int timeToLive, IPacketCallbackDispatcher dispatcher)
        {
            _activeCallbacks = new Dictionary<uint, Entry>();
            _callbackLock = new object();

            DefaultTimeToLive = timeToLive;
            Dispatcher = dispatcher;
        }

        /// <summary>
        /// Dispatches the callback with the specified id if it is present
        /// </summary>
        public bool DispatchCallback(TSender sender, uint id, IPacket packet, bool isError)
        {
            Entry entry;
            
            lock (_callbackLock)
            {
                if (!_activeCallbacks.TryGetValue(id, out entry))
                    return false;

                _activeCallbacks.Remove(id);
            }

            if (!isError && entry.Event != null)
                Dispatcher.DispatchEvent(sender, entry.Event, packet);
            else if (entry.ErrorEvent != null)
                Dispatcher.DispatchError(sender, entry.ErrorEvent, packet);
            
            return true;
        }

        /// <summary>
        /// Registers a new callback
        /// </summary>
        public void RegisterCallback(uint id, object callbackEvent, object errorEvent)
        {
            lock (_callbackLock)
            {
                if (_activeCallbacks.ContainsKey(id))
                {
                    _activeCallbacks[id] = new Entry
                        {Event = callbackEvent, ErrorEvent = errorEvent, TimeToLive = (short) DefaultTimeToLive};
                }
                else
                {
                    _activeCallbacks.Add(id, new Entry
                        {Event = callbackEvent, ErrorEvent = errorEvent, TimeToLive = (short) DefaultTimeToLive});
                }
            }
        }

        /// <summary>
        /// Invalidates the list of callbacks, removing any dead handlers to free up memory
        /// </summary>
        public void InvalidateCallbackList()
        {
            lock (_callbackLock)
            {
                foreach (var data in _activeCallbacks.ToArray())
                {
                    var newEntry = data.Value;

                    if (--newEntry.TimeToLive == 0)
                    {
                        _activeCallbacks.Remove(data.Key);
                        
                        if (data.Value.ErrorEvent != null)
                            Dispatcher.DispatchError(null, data.Value.ErrorEvent, null);
                    }
                    else
                    {
                        _activeCallbacks[data.Key] = newEntry;
                    }
                }
            }
        }
    }
}