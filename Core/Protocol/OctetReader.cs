using System;
using System.IO;
using System.Text;

namespace Core.Protocol
{
    public class OctetReader
    {
        private static byte[] _stringXorKey = { 0xAE, 0x05, 0x83, 0x2F, 0x77  };
        
        private Stream _s;

        public OctetReader(Stream stream)
        {
            _s = stream;
        }

        public void ReadBytes(Span<byte> span) => _s.Read(span);

        public byte[] ReadBytes(int count)
        {
            var buf = new byte[count];
            _s.Read(buf, 0, count);
            return buf;
        }

        public byte ReadUInt8() => (byte) _s.ReadByte();

        public ushort ReadUInt16()
        {
            Span<byte> bytes = stackalloc byte[2];
            _s.Read(bytes);
            return BitConverter.ToUInt16(bytes);
        }
        
        public uint ReadUInt32()
        {
            Span<byte> bytes = stackalloc byte[4];
            _s.Read(bytes);
            return BitConverter.ToUInt32(bytes);
        }
        
        public ulong ReadUInt64()
        {
            Span<byte> bytes = stackalloc byte[8];
            _s.Read(bytes);
            return BitConverter.ToUInt64(bytes);
        }

        public sbyte ReadInt8()
        {
            var bt = _s.ReadByte();
            return bt > 127 ? (sbyte)(bt - 256) : (sbyte) bt;
        }

        public short ReadInt16()
        {
            Span<byte> bytes = stackalloc byte[2];
            _s.Read(bytes);
            return BitConverter.ToInt16(bytes);
        }
        
        public int ReadInt32()
        {
            Span<byte> bytes = stackalloc byte[4];
            _s.Read(bytes);
            return BitConverter.ToInt32(bytes);
        }
        
        public long ReadInt64()
        {
            Span<byte> bytes = stackalloc byte[8];
            _s.Read(bytes);
            return BitConverter.ToInt64(bytes);
        }

        public float ReadReal32()
        {
            Span<byte> bytes = stackalloc byte[4];
            _s.Read(bytes);
            return BitConverter.ToSingle(bytes);
        }
        
        public double ReadReal64()
        {
            Span<byte> bytes = stackalloc byte[8];
            _s.Read(bytes);
            return BitConverter.ToDouble(bytes);
        }

        public ulong ReadUVarInt64()
        {
            var value = 0UL;
            var bt = -2;
            var off = 0;

            do
            {
                bt = _s.ReadByte();
                value |= (ulong) ((bt & 0b01111111) << off);
                off += 7;
            } while ((bt & 0b10000000) != 0);
            
            return value;
        }

        public uint ReadUVarInt32() => (uint) ReadUVarInt64();
        
        public ushort ReadUVarInt16() => (ushort) ReadUVarInt64();
        
        public long ReadVarInt64()
        {
            var value = 0L;
            var bt = -2;
            var isFirst = true;
            var off = 0;
            var invert = false;

            do
            {
                bt = _s.ReadByte();
                value |= (bt & (!isFirst ? 0b01111111 : 0b00111111)) << off;

                if (isFirst && (bt & 0b01000000) != 0)
                    invert = true;
                
                off += isFirst ? 6 : 7;
                isFirst = false;
            } while ((bt & 0b10000000) != 0);
            
            return invert ? -value : value;
        }

        public int ReadVarInt32() => (int) ReadVarInt64();
        
        public short ReadVarInt16() => (short) ReadVarInt64();

        public double ReadVarFixed64(int prec = 4) => ReadVarInt64() / Math.Pow(10, prec);
        
        public double ReadUVarFixed64(int prec = 4) => ReadUVarInt64() / Math.Pow(10, prec);
        
        public float ReadVarFixed32(int prec = 4) => ReadVarInt64() / MathF.Pow(10, prec);
        
        public float ReadUVarFixed32(int prec = 4) => ReadUVarInt64() / MathF.Pow(10, prec);

        public bool ReadBool() => _s.ReadByte() == 0xFF;

        public string ReadString()
        {
            var count = ReadUVarInt32();
            var bytes = ReadBytes((int) count);
            
            var newXorKey = _stringXorKey.AsSpan();
            
            for (var i = 0; i < newXorKey.Length; i++)
                newXorKey[i] = unchecked((byte)(newXorKey[i] * count));
            
            for (var i = 0; i < bytes.Length; i++)
                bytes[i] ^= newXorKey[i % newXorKey.Length];

            return Encoding.UTF8.GetString(bytes);
        }
    }
}