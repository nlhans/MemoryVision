using System;

namespace MemoryVision
{
    public class MemoryChannel
    {
        public int ID;
        public int UI_ID;
        public string Name;
        public MemoryChannelType Type;
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