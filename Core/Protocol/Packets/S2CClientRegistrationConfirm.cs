namespace Core.Protocol.Packets
{
    public class S2CClientRegistrationConfirm : S2CCallbackPacketBase
    {
        public override ushort PacketId => 0x000B;
        
        public uint PlayerNetworkId { get; set; }

        protected override void ReadDataOctets(OctetReader reader)
        {
            base.ReadDataOctets(reader);

            PlayerNetworkId = reader.ReadUVarInt32();
        }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            base.WriteDataOctets(writer);
            
            writer.WriteUVarInt(PlayerNetworkId);
        }
    }
}