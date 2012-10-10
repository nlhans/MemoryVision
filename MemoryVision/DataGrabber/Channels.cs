using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MemoryVision.DataGrabber
{
    public class Channels
    {
        private Grabber _mGrabber;
        private List<MemoryChannel> DataChannels;

        public int Count { get { return DataChannels.Count; } }
        public MemoryChannel this[int id] { get { return DataChannels[id]; } }

        public Channels(Grabber m, CheatEngineReader reader)
        {
            _mGrabber = m;

            DataChannels = new List<MemoryChannel>(reader.Channels);
        }

        public object ConvertToObject(int channel, byte[] bytes)
        {
            if(bytes ==null) return 0;
            switch(DataChannels[channel].Type)
            {
                case MemoryChannelType.INT32:
                    return (object)BitConverter.ToInt32(bytes, 0);
                    break;
                case MemoryChannelType.FLOAT:
                    return (object)BitConverter.ToSingle(bytes, 0);
                    break;
                case MemoryChannelType.DOUBLE:
                    return (object)BitConverter.ToDouble(bytes, 0);
                    break;
                default:
                    return (object)0;
                    break;
            }
        }
    }
}