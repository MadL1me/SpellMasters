using System.Threading;

namespace Core.Protocol.Packets
{
    /// <summary>
    /// Server-to-client packet base that also expects a callback.
    /// Warning!!! In ReadDataOctets and WriteDataOctets you MUST also
    /// call the base methods!
    /// </summary>
    public abstract class S2CCallbackPacketBase : S2CPacketBase, IPacketSequenceProvider
    {
        private static int _nextSequenceNumber;
        
        private uint _sequenceNumber;

        /// <summary>
        /// Initializes the current callback packet instance, assigning
        /// it a unique sequence number
        /// </summary>
        protected S2CCallbackPacketBase()
        {
            _sequenceNumber = GetNextAvailableSequenceNumber();
        }
        
        protected override void ReadDataOctets(OctetReader reader)
        {
            _sequenceNumber = reader.ReadUVarInt32();
        }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteUVarInt(_sequenceNumber);
        }

        /// <summary>
        /// Inherits the sequence number from a provider
        /// </summary>
        public void InheritSequenceId(IPacketSequenceProvider provider)
        {
            _sequenceNumber = provider.GetSequenceId();
        }
        
        /// <summary>
        /// Creates a response packet of a specified type
        /// </summary>
        public T CreateResponse<T>() where T : S2CCallbackPacketBase, new()
        {
            return new T {_sequenceNumber = _sequenceNumber};
        }

        private static uint GetNextAvailableSequenceNumber()
        {
            return unchecked((uint) Interlocked.Increment(ref _nextSequenceNumber));
        }

        public uint GetSequenceId()
        {
            return _sequenceNumber;
        }
        
        public virtual bool IsErrorPacket()
        {
            return false;
        }
    }
}