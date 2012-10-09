using System;
using System.Collections.Generic;

namespace MemoryVision.DataGrabber
{
    public class Waveform
    {
        public int SampleIndex;
        private Grabber _mGrabber;

        private List<List<byte[]>> DataBuffer = new List<List<byte[]>>();
        private List<List<byte[]>> RingBuffer = new List<List<byte[]>>();
        private int WriteIndex;
        public int WriteCount = 0;

        public Waveform(Grabber m)
        {
            _mGrabber = m;

            // Prepare.
            for (int ch = 0; ch < _mGrabber.Channels.Count; ch++)
            {
                RingBuffer.Add(new List<byte[]>());
                for (int i = 0; i < _mGrabber.Config.SamplesBeforeTrigger; i++)
                    RingBuffer[ch].Add(new byte[_mGrabber.Channels[ch].DataSize]);
            }
            for (int ch = 0; ch < _mGrabber.Channels.Count; ch++)
            {
                DataBuffer.Add(new List<byte[]>());
                for (int i = 0; i < _mGrabber.Config.Samples; i++)
                    DataBuffer[ch].Add(new byte[_mGrabber.Channels[ch].DataSize]);
            }

            SampleIndex = _mGrabber.Config.SamplesBeforeTrigger;
        }

        public void Push()
        {
            if (_mGrabber.Triggered)
            {

                Sample(DataBuffer, SampleIndex);
                SampleIndex++;
            }
            else
            {
                WriteCount++;
                WriteIndex++;
                if (WriteIndex >= _mGrabber.Config.SamplesBeforeTrigger)
                    WriteIndex = 0;
                Sample(RingBuffer, WriteIndex);
            }
        }

        private void Sample(List<List<byte[]>> buffer, int index)
        {
            for (int ch = 0; ch < _mGrabber.Channels.Count; ch++)
            {
                buffer[ch][index] = _mGrabber.Reader.ReadBytes(_mGrabber.Channels[ch].Address,
                                                           _mGrabber.Channels[ch].DataSize);
            }
        }
        
        public void Finalize()
        {
            for (int ch = 0; ch < _mGrabber.Channels.Count; ch++)
            {
                for (int i = 0; i < _mGrabber.Config.SamplesBeforeTrigger; i++)
                {
                    int s = 1+i + WriteIndex;
                    if (s >= _mGrabber.Config.SamplesBeforeTrigger)
                        s -= _mGrabber.Config.SamplesBeforeTrigger;
                    DataBuffer[ch][i] = RingBuffer[ch][s];
                }
            }
        }

        public object GetRingValue(int channel, int offset)
        {
            int index = WriteIndex - offset;
            if (index<0)
                index += _mGrabber.Config.SamplesBeforeTrigger;

            return _mGrabber.Channels.ConvertToObject(channel, RingBuffer[channel][index]);
        }
    }
}
