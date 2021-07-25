namespace Core.Protocol.Packets
{
    /// <summary>
    /// Represents a server-to-client arbitrary error packet
    /// </summary>
    public class S2CErrorPacket : S2CCallbackPacketBase
    {
        public override ushort PacketId => 0x00E0;
        public override bool UseEncryption => true;
        
        /// <summary>
        /// The numeric error id. If this id is 0, it means that the operation
        /// was a success
        /// </summary>
        public int ErrorId { get; set; }
        
        public S2CErrorPacket() {}

        public S2CErrorPacket(int errorId) => ErrorId = errorId;

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteVarInt(ErrorId);
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
            ErrorId = reader.ReadVarInt32();
        }

        public override bool IsErrorPacket()
        {
            return ErrorId != 0;
        }
    }
}