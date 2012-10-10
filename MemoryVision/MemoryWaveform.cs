using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryVision.DataGrabber;

namespace MemoryVision
{
    public class MemoryWaveform
    {
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
        }
    }
}
