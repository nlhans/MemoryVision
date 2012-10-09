using System;

namespace MemoryVision
{
    public class MemoryChannel
    {
        public int ID;
        public int UI_ID;
        public string Name;
        public MemoryChannelType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                switch (value)
                {
                    default:
                        DataSize = 4;
                        break;
                }
            }
        }

        private MemoryChannelType _Type;
        public uint DataSize;
        public string Parameters;
        public IntPtr Address;

        public string TypeToString()
        {
            switch(Type)
            {
                case MemoryChannelType.FLOAT:
                    return "float";
                    break;
                case MemoryChannelType.INT32:
                    return "Int32";
                    break;
                default:
                    return "??";
                    break;
            }
        }
    }
}