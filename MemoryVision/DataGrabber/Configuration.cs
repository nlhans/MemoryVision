namespace MemoryVision.DataGrabber
{
    public class Configuration
    {
        public int SamplesAfterTrigger;
        public int SamplesBeforeTrigger;
        public int Samples{get { return SamplesAfterTrigger + SamplesBeforeTrigger; }}

        private Grabber _mGrabber;
        public int SampleWaitTime;

        public Configuration(Grabber m)
        {
            _mGrabber = m;
        }
    }
}