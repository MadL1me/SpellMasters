namespace Core.Protocol
{
    /// <summary>
    /// Represents a packet callback dispatcher
    /// </summary>
    public class PacketCallbackDispatcher<TSender, TEventPacket, TErrorPacket> : IPacketCallbackDispatcher
    {
        /// <summary>
        /// Called when a callback successfully arrives
        /// </summary>
        public delegate void CallbackEvent(TSender sender, TEventPacket packet);

        /// <summary>
        /// Called when the callback was either an error packet or was ignored.
        /// If it was ignored, the "error" parameter will be null
        /// </summary>
        public delegate void CallbackError(TSender sender, TErrorPacket error);

        /// <summary>
        /// Dispatches a callback event
        /// </summary>
        public virtual void DispatchEvent(object sender, object callbackEvent, object packet)
        {
            ((CallbackEvent) callbackEvent)((TSender) sender, (TEventPacket) packet);
        }
        
        /// <summary>
        /// Dispatches a callback error
        /// </summary>
        public virtual void DispatchError(object sender, object callbackError, object packet)
        {
            ((CallbackError) callbackError)((TSender) sender, (TErrorPacket) packet);
        }
    }
}