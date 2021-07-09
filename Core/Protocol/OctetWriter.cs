using System;
using System.IO;
using System.Text;

namespace Core.Protocol
{
    public class OctetWriter
    {
        private static byte[] _stringXorKey = { 0xAE, 0x05, 0x83, 0x2F, 0x77  };
        
        private MemoryStream _ms;

        public OctetWriter()
        {
            _ms = new MemoryStream();
        }

        public void WriteBytes(byte[] bytes) => _ms.Write(bytes, 0, bytes.Length);
        public void WriteBytes(byte[] bytes, int offset, int length) => _ms.Write(bytes, offset, length);
        public void WriteBytes(ReadOnlySpan<byte> bytes) => _ms.Write(bytes);

        public void WriteUInt8(byte value) => _ms.WriteByte(value);

        public void WriteUInt16(ushort value)
        {
            Span<byte> bytes = stackalloc byte[2];
            BitConverter.TryWriteBytes(bytes, value);
            _ms.Write(bytes);
        }
        
        public void WriteUInt32(uint value)
        {
            Span<byte> bytes = stackalloc byte[4];
            BitConverter.TryWriteBytes(bytes, value);
            _ms.Write(bytes);
        }
        
        public void WriteUInt64(ulong value)
        {
            Span<byte> bytes = stackalloc byte[8];
            BitConverter.TryWriteBytes(bytes, value);
            _ms.Write(bytes);
        }
        
        public void WriteInt8(sbyte value) => _ms.WriteByte(unchecked((byte) value));

        public void WriteInt16(short value)
        {
            Span<byte> bytes = stackalloc byte[2];
            BitConverter.TryWriteBytes(bytes, value);
            _ms.Write(bytes);
        }
        
        public void WriteInt32(int value)
        {
            Span<byte> bytes = stackalloc byte[4];
            BitConverter.TryWriteBytes(bytes, value);
            _ms.Write(bytes);
        }
        
        public void WriteInt64(long value)
        {
            Span<byte> bytes = stackalloc byte[8];
            BitConverter.TryWriteBytes(bytes, value);
            _ms.Write(bytes);
        }

        public void WriteReal32(float value)
        {
            Span<byte> bytes = stackalloc byte[4];
            BitConverter.TryWriteBytes(bytes, value);
            _ms.Write(bytes);
        }
        
        public void WriteReal64(double value)
        {
            Span<byte> bytes = stackalloc byte[8];
            BitConverter.TryWriteBytes(bytes, value);
            _ms.Write(bytes);
        }

        public void WriteUVarInt(ulong value)
        {
            do
            {
                var temp = value & 0b01111111;
                value >>= 7;

                if (value != 0)
                    temp |= 0b10000000;

                WriteUInt8((byte) temp);
            } while (value != 0);
        }
        
        public void WriteVarInt(long value)
        {
            var isFirst = true;
            var inverted = value < 0;
            
            if (inverted)
                value = -value;
            
            do
            {
                var temp = value & (!isFirst ? 0b01111111 : 0b00111111);
                value >>= !isFirst ? 7 : 6;

                if (value != 0)
                    temp |= 0b10000000;

                if (inverted && isFirst)
                    temp |= 0b01000000;

                WriteUInt8((byte) temp);
                isFirst = false;
            } while (value != 0 && value != -1);
        }

        public void WriteVarFixed(double value, int prec = 4)
        {
            WriteVarInt((long) (value * Math.Pow(10, prec)));
        }
        
        public void WriteUVarFixed(double value, int prec = 4)
        {
            WriteUVarInt((ulong) (value * Math.Pow(10, prec)));
        }
        
        public void WriteVarFixed(float value, int prec = 4)
        {
            WriteVarInt((long) (value * MathF.Pow(10, prec)));
        }
        
        public void WriteUVarFixed(float value, int prec = 4)
        {
            WriteUVarInt((ulong) (value * MathF.Pow(10, prec)));
        }

        public void WriteBool(bool value) => _ms.WriteByte(value ? (byte) 0xFF : (byte) 0);

        public void WriteString(string value)
        {
            value ??= "\0";
            
            var bytes = Encoding.UTF8.GetBytes(value);
            WriteUVarInt((uint) bytes.Length);
            
            var newXorKey = _stringXorKey.AsSpan();

            for (var i = 0; i < newXorKey.Length; i++)
                newXorKey[i] = unchecked((byte)(newXorKey[i] * bytes.Length));

            for (var i = 0; i < bytes.Length; i++)
                bytes[i] ^= newXorKey[i % newXorKey.Length];
            
            WriteBytes(bytes);
        }

        public byte[] ToArray()
        {
            return _ms.ToArray();
        }
    }
}