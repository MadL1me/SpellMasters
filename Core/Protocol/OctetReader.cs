using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Protocol
{
    public class OctetReader
    {
        private static byte[] _stringXorKey = { 0xAE, 0x05, 0x83, 0x2F, 0x77  };
        
        private Stream _s;
        private byte[] _buf;

        public OctetReader(Stream stream)
        {
            _s = stream;
            _buf = new byte[8];
        }

        public byte[] ReadBytes(int count)
        {
            var buf = new byte[count];
            _s.Read(buf, 0, count);
            return buf;
        }

        public byte ReadUInt8() => (byte) _s.ReadByte();

        public ushort ReadUInt16()
        {
            _s.Read(_buf, 0, 2);
            return BitConverter.ToUInt16(_buf, 0);
        }
        
        public uint ReadUInt32()
        {
            _s.Read(_buf, 0, 4);
            return BitConverter.ToUInt32(_buf, 0);
        }
        
        public ulong ReadUInt64()
        {
            _s.Read(_buf, 0, 8);
            return BitConverter.ToUInt64(_buf, 0);
        }

        public sbyte ReadInt8()
        {
            var bt = _s.ReadByte();
            return bt > 127 ? (sbyte)(bt - 256) : (sbyte) bt;
        }

        public short ReadInt16()
        {
            _s.Read(_buf, 0, 2);
            return BitConverter.ToInt16(_buf, 0);
        }
        
        public int ReadInt32()
        {
            _s.Read(_buf, 0, 4);
            return BitConverter.ToInt32(_buf, 0);
        }
        
        public long ReadInt64()
        {
            _s.Read(_buf, 0, 8);
            return BitConverter.ToInt64(_buf, 0);
        }

        public float ReadReal32()
        {
            _s.Read(_buf, 0, 4);
            return BitConverter.ToSingle(_buf, 0);
        }
        
        public double ReadReal64()
        {
            _s.Read(_buf, 0, 8);
            return BitConverter.ToDouble(_buf, 8);
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
        
        public float ReadVarFixed32(int prec = 4) => (float) (ReadVarInt64() / Math.Pow(10, prec));
        
        public float ReadUVarFixed32(int prec = 4) => (float) (ReadUVarInt64() / Math.Pow(10, prec));

        public bool ReadBool() => _s.ReadByte() == 0xFF;

        public string ReadString()
        {
            var count = ReadUVarInt32();
            var bytes = ReadBytes((int) count);
            
            var newXorKey = _stringXorKey.ToArray();
            
            for (var i = 0; i < newXorKey.Length; i++)
                newXorKey[i] = unchecked((byte)(newXorKey[i] * count));
            
            for (var i = 0; i < bytes.Length; i++)
                bytes[i] ^= newXorKey[i % newXorKey.Length];

            return Encoding.UTF8.GetString(bytes);
        }
    }
}