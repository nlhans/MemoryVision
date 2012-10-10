using System;
using System.Threading;
using Triton;
using Triton.Memory;

namespace MemoryVision.DataGrabber
{
    public class Grabber
    {
        public event Signal Done; 

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
        public double Progress
        {
            get
            {
                if(Trigger.IsTriggered)
                {
                    return (1000.0*Waveform.SampleIndex + Config.SamplesBeforeTrigger)/Config.Samples;
                }
                else
                {
                    return (1000.0*Math.Min(Config.SamplesBeforeTrigger, Waveform.WriteCount))/Config.Samples;
                }
            }
        }
        private long _mGrabberCaptureTime = 0;
        private long _mGrabberWaitTime = 0;

        public Grabber(CheatEngineReader table, MemoryReader reader)
        {
            Config = new Configuration(this);

            //TEMPORARY configuration!
            Config.SamplesBeforeTrigger = 1000;
            Config.SamplesAfterTrigger = 9000;
            Config.SampleWaitTime = 10000;//ms, 1ms here 

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
                DataGrabber_Timing();
            }

            // Finalize waveform
            Waveform.Finalize();

            // Sent event DONE
            if(Done!=null)
                Done(this);
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
