using System.IO;
using System.IO.Compression;
using System.Threading;

namespace MemoryVision
{
    public class MemoryWaveformWriter
    {
        private MemoryWaveform _mWaveform;
        public MemoryWaveformWriter(MemoryWaveform wfm, string file)
        {
            _mWaveform = wfm;
            ThreadPool.QueueUserWorkItem(Write, file);
        }

        private void StoreData(string file, byte[] data)
        {

            // Create the compressed file.
            using (FileStream GzFile = File.Create(file))
            using (GZipStream Compress = new GZipStream(GzFile, CompressionMode.Compress))
            {
                // Compress this data:
                Compress.Write(data, 0, data.Length);
                // Done.
            }

        }

        private void Write(object f)
        {
            string file = (string)f;
            // Use CheatEngineWriter() to write new CT file.

            // Dump all waveforms.
            foreach(MemoryChannel mc in _mWaveform.Channels)
            {
                int sample = 0;
                byte[] data = new byte[_mWaveform.Data[mc.ID].Count*mc.DataSize];
                for (long d = 0; d < data.Length; d+=mc.DataSize)
                {
                    for (int b = 0; b < mc.DataSize; b++)
                        data[b + d] = _mWaveform.Data[mc.ID][sample][b];
                    sample++;
                }

                StoreData(file + ".ch" + mc.ID.ToString(), data);
            }
        }
    }
}