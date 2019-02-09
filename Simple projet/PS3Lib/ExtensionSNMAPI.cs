namespace SNMAPINetLib
{
    using System;
    using System.Text;

    public class Extension
    {
        private SNMAPI PS3;

        public Extension(SNMAPI obj)
        {
            PS3 = obj;
        }

        public byte[] GetBytes(uint offset, int length)
        {
            byte[] buffer = new byte[length];
            PS3.GetMemory(offset, buffer);
            return buffer;
        }

        public byte ReadByte(uint offset)
        {
            byte[] buffer = new byte[1];
            PS3.GetMemory(offset, buffer);
            return buffer[0];
        }

        public int ReadInt(uint offset)
        {
            byte[] buffer = new byte[4];
            PS3.GetMemory(offset, buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        public short ReadInt16(uint offset)
        {
            byte[] buffer = new byte[2];
            PS3.GetMemory(offset, buffer);
            return BitConverter.ToInt16(buffer, 0);
        }

        public int ReadInt32(uint offset)
        {
            byte[] buffer = new byte[4];
            PS3.GetMemory(offset, buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        public long ReadInt64(uint offset)
        {
            byte[] buffer = new byte[8];
            PS3.GetMemory(offset, buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        public long ReadLong(uint offset)
        {
            byte[] buffer = new byte[8];
            PS3.GetMemory(offset, buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        public short ReadShort(uint offset)
        {
            byte[] buffer = new byte[2];
            PS3.GetMemory(offset, buffer);
            return BitConverter.ToInt16(buffer, 0);
        }

        public uint ReadUInt(uint offset)
        {
            byte[] buffer = new byte[4];
            PS3.GetMemory(offset, buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        public ushort ReadUInt16(uint offset)
        {
            byte[] buffer = new byte[2];
            PS3.GetMemory(offset, buffer);
            return BitConverter.ToUInt16(buffer, 0);
        }

        public uint ReadUInt32(uint offset)
        {
            byte[] buffer = new byte[4];
            PS3.GetMemory(offset, buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        public ulong ReadUInt64(uint offset)
        {
            byte[] buffer = new byte[8];
            PS3.GetMemory(offset, buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public byte ReadUInt8(uint offset)
        {
            byte[] buffer = new byte[1];
            PS3.GetMemory(offset, buffer);
            return buffer[0];
        }

        public ulong ReadULong(uint offset)
        {
            byte[] buffer = new byte[8];
            PS3.GetMemory(offset, buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public ushort ReadUShort(uint offset)
        {
            byte[] buffer = new byte[2];
            PS3.GetMemory(offset, buffer);
            return BitConverter.ToUInt16(buffer, 0);
        }

        public void WriteByte(uint offset, byte value)
        {
            PS3.SetMemory(offset, new byte[] { value });
        }

        public void WriteInt(uint offset, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            PS3.SetMemory(offset, bytes);
        }

        public void WriteInt16(uint offset, short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            PS3.SetMemory(offset, bytes);
        }

        public void WriteInt32(uint offset, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            PS3.SetMemory(offset, bytes);
        }

        public void WriteInt64(uint offset, long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            PS3.SetMemory(offset, bytes);
        }

        public void WriteLong(uint offset, long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            PS3.SetMemory(offset, bytes);
        }

        public void WriteShort(uint offset, short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            PS3.SetMemory(offset, bytes);
        }

        public void WriteString(uint offset, string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value + "\0");
            PS3.SetMemory(offset, bytes);
        }

        public void WriteUInt(uint offset, uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            PS3.SetMemory(offset, bytes);
        }

        public void WriteUInt16(uint offset, ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            PS3.SetMemory(offset, bytes);
        }

        public void WriteUInt32(uint offset, uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            PS3.SetMemory(offset, bytes);
        }

        public void WriteUInt64(uint offset, ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            PS3.SetMemory(offset, bytes);
        }

        public void WriteUInt8(uint offset, byte value)
        {
            PS3.SetMemory(offset, new byte[] { value });
        }

        public void WriteULong(uint offset, ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            PS3.SetMemory(offset, bytes);
        }

        public void WriteUShort(uint offset, ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            PS3.SetMemory(offset, bytes);
        }
    }
}

