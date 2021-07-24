namespace Core.Protocol.Packets
{
    /// <summary>
    /// Represents a server-to-client arbitrary error packet
    /// </summary>
    public class C2SErrorPacket : C2SCallbackPacketBase
    {
        public override ushort PacketId => 0xEEE1;
        public override bool UseEncryption => true;
        
        /// <summary>
        /// The numeric error id. If this id is 0, it means that the operation
        /// was a success
        /// </summary>
        public int ErrorId { get; set; }
        
        public C2SErrorPacket() {}

        public C2SErrorPacket(int errorId) => ErrorId = errorId;

        protected override void WriteDataOctets(OctetWriter writer)
        {
            base.WriteDataOctets(writer);
            
            writer.WriteVarInt(ErrorId);
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
            base.ReadDataOctets(reader);
            
            ErrorId = reader.ReadVarInt32();
        }
        
        public override bool IsErrorPacket()
        {
            return ErrorId != 0;
        }
    }
}