using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;
using Triton;

namespace MemoryVision
{
    public class MemoryWaveformReader
    {
        public event Signal Done;
        private MemoryWaveform _mWaveform;
        public MemoryWaveformReader(MemoryWaveform mwf, string file)
        {
            _mWaveform = mwf;
            ThreadPool.QueueUserWorkItem(Load, file);
        }

        private byte[] GetData(string file)
        {
            byte[] data = new byte[0];

            // Uncompress
            if (File.Exists(file))
            {
                using (MemoryStream DatFile = new MemoryStream())
                using (FileStream GzFile = File.OpenRead(file))
                using (GZipStream Decompress = new GZipStream(GzFile, CompressionMode.Decompress))
                {
                    Decompress.CopyTo(DatFile);

                    data = new byte[DatFile.Length];
                    DatFile.Seek(0, SeekOrigin.Begin);
                    DatFile.Read(data, 0, (int) DatFile.Length);
                }
            }

            return data;
        }

        private void Load(object n)
        {
            string file = (string)n;

            CheatEngineReader channels = new CheatEngineReader();
            channels.Read(file + ".ct"); // Channel File

            _mWaveform.Data = new Dictionary<int, List<byte[]>>();
            _mWaveform.File = file;
            _mWaveform.Channels = new List<MemoryChannel>(channels.Channels);

            // Read all channel ID's
            foreach(MemoryChannel mc in _mWaveform.Channels)
            {
                byte[] data = this.GetData(file + ".ch" + mc.ID.ToString());
                _mWaveform.Data.Add(mc.ID,new List<byte[]>());

                int sample = 0;
                for (long b = 0; b < data.Length; b+=mc.DataSize)
                {
                    // Memcpy() from data to sampleData
                    byte[] sampleData = new byte[mc.DataSize];
                    for (int i = 0; i < mc.DataSize; i++)
                        sampleData[i] = data[b + i];

                    // Store
                    _mWaveform.Data[mc.ID].Add(sampleData);
                    sample++;
                }
            }

            //DONE
            if (Done != null)
                Done(_mWaveform);
        }

    }
}