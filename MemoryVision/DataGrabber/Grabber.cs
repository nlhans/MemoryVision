using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Triton;
using Triton.Memory;

namespace MemoryVision.DataGrabber
{
    public class Grabber
    {
        public Channels Channels { get; set; }
        public Waveform Waveform { get; set; }
        public Triggering Trigger { get; set; }
        public Configuration Config { get; set; }
        public MemoryReader Reader { get; set; }
        private Thread _mGrabber;

        private MicroStopwatch _mGrabberTiming;
        private bool _mGrabberRunning = false;
        public bool Running { get { return _mGrabberRunning; } }
        public bool Triggered { get { return Trigger.IsTriggered; } }

        private long _mGrabberCaptureTime = 0;
        private long _mGrabberWaitTime = 0;

        public Grabber(CheatEngineReader table, MemoryReader reader)
        {
            Config = new Configuration(this);

            //TEMPORARY configuration!
            Config.SamplesBeforeTrigger = 200;
            Config.SamplesAfterTrigger = 200;
            Config.Samples = 400;
            Config.SampleWaitTime = 5000;//us, 50ms here 



            Channels = new Channels(this,table);
            Waveform = new Waveform(this);
            Trigger = new Triggering(this);
            this.Reader = reader;

            _mGrabberTiming = new MicroStopwatch();

            TritonBase.PreExit += Stop;

        }

        private void DataGrabber_Timing()
        {
            _mGrabberWaitTime += Config.SampleWaitTime;
            while (_mGrabberTiming.ElapsedMicroseconds <= _mGrabberWaitTime)
                Thread.SpinWait(5);
        }

        private void DataGrabber_Thread()
        {
            _mGrabberTiming.Start();
            while(_mGrabberRunning)
            {
                if (Trigger.IsTriggered || Config.SamplesBeforeTrigger > 0)
                    Waveform.Push();

                if (Trigger.IsTriggered)
                {
                    if (Waveform.SampleIndex >= Config.Samples)
                    {
                        long totalTime = _mGrabberTiming.ElapsedMicroseconds - _mGrabberCaptureTime;
                        //totalTime /= Config.SampleWaitTime;
                        _mGrabberRunning = false;
                    }
                }
                else
                {
                    Trigger.Check();
                    _mGrabberCaptureTime = _mGrabberTiming.ElapsedMicroseconds;
                }
                //Debug.WriteLine(_mGrabberTiming.ElapsedMicroseconds.ToString());
                DataGrabber_Timing();
            }

            // Finalize waveform
            Waveform.Finalize();
        }

        public void Start()
        {
            if (_mGrabber == null)
            {
                _mGrabberRunning = true;

                _mGrabber = new Thread(DataGrabber_Thread);
                _mGrabber.Start();
            }
        }

        public void Stop()
        {
            if (_mGrabber != null)
            {
                _mGrabberRunning = false;
                Thread.Sleep(1);
                _mGrabber.Abort();
                _mGrabber = null;
            }
        }

    }
}
