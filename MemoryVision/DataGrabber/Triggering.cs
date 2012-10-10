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
            double data_double, data_n_minus_1_double, value_double, value2_double;
            int data_int, data_n_minus_1_int, value_int, value2_int;
            
            if (_mGrabber.Config.Trigger_Simple && !IsTriggered && _mGrabber.Waveform.WriteCount > _mGrabber.Config.SamplesBeforeTrigger)
            {
                byte[] value = _mGrabber.Config.Trigger_Simple_Value;
                byte[] value2 = _mGrabber.Config.Trigger_Simple_Value2;

                byte[] data = _mGrabber.Waveform.GetRingData(_mGrabber.Config.Trigger_Simple_Channel, 0);// Get this value;
                byte[] data_n_minus_1 = _mGrabber.Waveform.GetRingData(_mGrabber.Config.Trigger_Simple_Channel, 1);

                switch(_mGrabber.Config.Trigger_Simple_Condition)
                {
                    case TriggerCondition.ALWAYS:
                        IsTriggered = true;
                        break;

                    case TriggerCondition.CHANGED:
                        for (int i = 0; i < data.Length; i++)
                            if (data[i] != data_n_minus_1[i])
                                IsTriggered = true;
                        break;

                    case TriggerCondition.EQUALS:
                        if (data.Equals(value))
                            IsTriggered = true;
                        break;
                    case TriggerCondition.NOT_EQUALS:
                        if (!data.Equals(value))
                            IsTriggered = true;
                        break;


                    case TriggerCondition.IN_RANGE:
                        data_double = Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, data));
                        value_double = Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, value));
                        value2_double = Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, value2));
                        if (data_double >= value_double && data_double <= value2_double)
                            IsTriggered = true;
                            

                            break;

                    case TriggerCondition.OUT_RANGE:
                        data_double = Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, data));
                        value_double = Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, value));
                        value2_double = Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, value2));
                        if (!(data_double > value_double && data_double < value2_double))
                            IsTriggered = true;
                        break;

                    case TriggerCondition.GREATER_THAN:
                        data_double = Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, data));
                        value_double = Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, value));
                        if (data_double > value_double)
                            IsTriggered = true;
                        break;

                    case TriggerCondition.GREATER_THAN_OR_EQUALS:
                        data_double = Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, data));
                        value_double = Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, value));
                        if (data_double >= value_double)
                            IsTriggered = true;
                        break;

                    case TriggerCondition.SMALLER_THAN:
                        data_double =  Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, data));
                        value_double =  Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, value));
                        if (data_double < value_double)
                            IsTriggered = true;
                        break;

                    case TriggerCondition.SMALLER_THAN_OR_EQUALS:
                        data_double =  Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, data));
                        value_double =  Convert.ToDouble(_mGrabber.Channels.ConvertToObject(
                            _mGrabber.Config.Trigger_Simple_Channel, value));
                        if (data_double <= value_double)
                            IsTriggered = true;
                        break;

                    case TriggerCondition.EDGE_RISING:
                        data_int =
                            (int)_mGrabber.Channels.ConvertToObject(_mGrabber.Config.Trigger_Simple_Channel, data);
                        data_n_minus_1_int =
                            (int)_mGrabber.Channels.ConvertToObject(_mGrabber.Config.Trigger_Simple_Channel, data_n_minus_1);

                        if (data_int != 0 && data_n_minus_1_int == 0)
                            IsTriggered = true;
                        break;

                    case TriggerCondition.EDGE_FALLING:
                        data_int =
                            (int)_mGrabber.Channels.ConvertToObject(_mGrabber.Config.Trigger_Simple_Channel, data);
                        data_n_minus_1_int =
                            (int)_mGrabber.Channels.ConvertToObject(_mGrabber.Config.Trigger_Simple_Channel, data_n_minus_1);

                        if (data_int == 0 && data_n_minus_1_int != 0)
                            IsTriggered = true;
                        break;

                    case TriggerCondition.EDGE_RISING_VALUE:
                        data_int =
                            (int)_mGrabber.Channels.ConvertToObject(_mGrabber.Config.Trigger_Simple_Channel, data);
                        data_n_minus_1_int =
                            (int)_mGrabber.Channels.ConvertToObject(_mGrabber.Config.Trigger_Simple_Channel, data_n_minus_1);
                        value_int =
                            (int)_mGrabber.Channels.ConvertToObject(_mGrabber.Config.Trigger_Simple_Channel, value);

                        if (data_int == value_int && data_n_minus_1_int != value_int)
                            IsTriggered = true;
                        break;

                    case TriggerCondition.EDGE_FALLING_VALUE:
                        data_int =
                            (int)_mGrabber.Channels.ConvertToObject(_mGrabber.Config.Trigger_Simple_Channel, data);
                        data_n_minus_1_int =
                            (int)_mGrabber.Channels.ConvertToObject(_mGrabber.Config.Trigger_Simple_Channel, data_n_minus_1);
                        value_int =
                            (int)_mGrabber.Channels.ConvertToObject(_mGrabber.Config.Trigger_Simple_Channel, value);

                        if (data_int != value_int && data_n_minus_1_int == value_int)
                            IsTriggered = true;
                        break;

                    default:
                        throw new Exception("");
                        break;
                }
            }
        }
    }
}
