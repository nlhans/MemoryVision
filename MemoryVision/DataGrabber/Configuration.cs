namespace MemoryVision.DataGrabber
{
    public enum TriggerCondition
    {
        ALWAYS,

        CHANGED,

        EDGE_RISING,
        EDGE_FALLING,

        EDGE_RISING_VALUE,
        EDGE_FALLING_VALUE,

        IN_RANGE,
        OUT_RANGE,

        EQUALS,
        NOT_EQUALS,

        GREATER_THAN,
        SMALLER_THAN,

        GREATER_THAN_OR_EQUALS,
        SMALLER_THAN_OR_EQUALS
    }

    public class Configuration
    {
        public int SamplesAfterTrigger;
        public int SamplesBeforeTrigger;
        public int Samples{get { return SamplesAfterTrigger + SamplesBeforeTrigger; }}

        public bool Trigger_Simple = true;

        // Simple single-condition single-state triggering variables.
        public int Trigger_Simple_Channel = 0;
        public TriggerCondition Trigger_Simple_Condition;
        public MemoryChannelType Trigger_Simple_ValueType;
        public byte[] Trigger_Simple_Value;
        public byte[] Trigger_Simple_Value2;

        private Grabber _mGrabber;
        public int SampleWaitTime;

        public Configuration(Grabber m)
        {
            _mGrabber = m;
        }
    }
}