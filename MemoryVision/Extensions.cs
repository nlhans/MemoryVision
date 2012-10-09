using Triton.Memory;

namespace MemoryVision
{
    public static partial class Extensions
    {
        public static string ReadAsString(this MemoryReader reader, MemoryChannel ch)
        {
            switch(ch.Type)
            {
                case MemoryChannelType.FLOAT:
                    return reader.ReadFloat(ch.Address).ToString("0.0000");
                    break;
                case MemoryChannelType.INT32:
                    return reader.ReadInt32(ch.Address).ToString();
                    break;
                default:
                    return "??";
            }
        }
    }
}