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
        public IntPtr Address; // TODO: Support for pointers?
        public bool Analogue // Display as logic or as analogue?
        {
            get
            {
                if (AnalogueAuto == 1)
                {
                    switch (_Type)
                    {
                        case MemoryChannelType.FLOAT:
                            return true;
                            break;
                        case MemoryChannelType.DOUBLE:
                            return true;
                            break;
                        default:
                            return false;
                            break;
                    }
                }
                else if (AnalogueAuto == 0)
                    return false;
                else if (AnalogueAuto == 2)
                    return true;

                return true;
            }
        }

        public int AnalogueAuto = 1; // 1=auto, 2=yes, 0=no///

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