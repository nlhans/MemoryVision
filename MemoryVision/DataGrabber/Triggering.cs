using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryVision.DataGrabber
{
    public class Triggering
    {
        public bool IsTriggered;
        private Grabber _mGrabber;

        public Triggering(Grabber m)
        {
            IsTriggered = false;
            _mGrabber = m;
        }

        public void Check()
        {
            if (_mGrabber.Waveform.WriteCount > _mGrabber.Config.SamplesBeforeTrigger)
            {
                if ((int) _mGrabber.Waveform.GetRingValue(0, 0) == 5)
                    IsTriggered = true;
            }
        }
    }
}
