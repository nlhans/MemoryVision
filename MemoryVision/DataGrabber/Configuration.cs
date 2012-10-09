namespace MemoryVision.DataGrabber
{
    public class Configuration
    {
        public int SamplesAfterTrigger;
        public int SamplesBeforeTrigger;
        public int Samples;

        private Grabber _mGrabber;
        public int SampleWaitTime;

        public Configuration(Grabber m)
        {
            _mGrabber = m;
        }
    }
}