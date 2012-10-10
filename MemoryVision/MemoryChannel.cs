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
                    case MemoryChannelType.UINT64:
                        DataSize = 8;
                        break;
                    case MemoryChannelType.INT64:
                        DataSize = 8;
                        break;
                    case MemoryChannelType.DOUBLE:
                        DataSize = 8;
                        break;
                    default:
                        DataSize = 4;
                        break;
                }
            }
        }

        private MemoryChannelType _Type;
        public uint DataSize;
        public string Parameters;
        public IntPtr Address; // Support for pointers?
        public bool Continous=true; // Display as logic or as analogue?

        public string TypeToString()
        {
            switch(Type)
            {
                case MemoryChannelType.FLOAT:
                    return "Float";
                    break;
                case MemoryChannelType.INT32:
                    return "Int32";
                    break;
                case MemoryChannelType.DOUBLE:
                    return "Double";
                default:
                    return "??";
                    break;
            }
        }
    }
}