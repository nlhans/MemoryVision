using System;
using System.Collections.Generic;
using MemoryVision.DataGrabber;
using Triton;

namespace MemoryVision
{
    public class MemoryWaveform
    {
        public event Signal Loaded;
        public List<MemoryChannel> Channels;
        public Dictionary<int,List<byte[]>> Data;
        public string File;

        public void Save(string file)
        {
            // LOAD 
            File = file;

            MemoryWaveformWriter wr = new MemoryWaveformWriter(this, file);
        }
        public void Load(string file)
        {
            MemoryWaveformReader rd= new MemoryWaveformReader(this, file);
            rd.Done += TriggerLoaded;
        }

        public void Load(Grabber grabber)
        {
            Channels = new List<MemoryChannel>();
            for (int i = 0; i < grabber.Channels.Count;i++)
                Channels.Add(grabber.Channels[i]);
            Data = new Dictionary<int, List<byte[]>>();

            for(int i = 0 ;i < Channels.Count; i++)
            {
                Data.Add(Channels[i].ID, grabber.Waveform[i]);
            }

            // etc.
            Save("tmp");
            TriggerLoaded(0);
        }

        private void TriggerLoaded(object o)
        {

            if (Loaded != null)
                Loaded(this);
        }

        public object GetData(int channel, int sample)
        {
            byte[] bytes = Data[channel][sample];
            switch (Channels[channel].Type)
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
