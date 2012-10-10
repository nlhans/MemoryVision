using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MemoryVision
{
    public partial class WaveformView : Form
    {
        private MemoryWaveform _mWaveform;
        public WaveformView(MemoryWaveform waveform)
        {
            InitializeComponent();

            _mWaveform = waveform;
        }
    }
}
