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

            this.SizeChanged += new EventHandler(WaveformView_SizeChanged);

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        void WaveformView_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float channelsize_f = Convert.ToSingle(e.ClipRectangle.Height/6);
            int channelsize_i = Convert.ToInt32(channelsize_f);

            int sample_start = 0;
            int sample_end = 0;

            Font small_font = new Font("Arial", 8.0f);
            Pen fine_pen = new Pen(Color.Gray, 1.0f);
            Pen fine_pen_white = new Pen(Color.White, 1.0f);
            Pen line_pen = new Pen(Color.LightGreen, 1.0f);
            Graphics g = e.Graphics;

            g.FillRectangle(Brushes.Black, e.ClipRectangle);

            // Sample count:
            int samples = _mWaveform.Data[0].Count;
            // TODO: Fix
            sample_end = samples;

            float x_scale = (e.ClipRectangle.Width - 80.0f) / (sample_end - sample_start);
            for (int i = 0; i < _mWaveform.Channels.Count; i++)
            {
                if (_mWaveform.Channels[i].Continous)
                {
                    List<Point> graph = new List<Point>();
                    double min = double.MaxValue;
                    double max = double.MinValue;
                    // Search min-max
                    for (int sample = sample_start; sample < sample_end; sample++)
                    {
                        min = Math.Min(min, Convert.ToDouble(_mWaveform.GetData(i, sample)));
                        max = Math.Max(max, Convert.ToDouble(_mWaveform.GetData(i, sample)));
                    }

                    for (int sample = sample_start; sample < sample_end; sample++)
                    {
                        double value = Convert.ToDouble(_mWaveform.GetData(i, sample));
                        double y_part = 0.95 - 0.9*(value - min)/(max - min);
                        y_part *= channelsize_i;
                        if (double.IsNaN(y_part) || double.IsInfinity(y_part))
                            y_part = 0;
                        int x = Convert.ToInt32(80.0f + Convert.ToSingle(x_scale*(sample - sample_start)));
                        int y = Convert.ToInt32(channelsize_f*i + y_part);

                        graph.Add(new Point(x, y));
                    }
                    graph.Add(new Point(e.ClipRectangle.Width, i*channelsize_i));
                    graph.Add(new Point(80, i*channelsize_i));
                    g.DrawPolygon(line_pen, graph.ToArray());
                }
            }

            // Draw channels.
            for (int i = 0; i < _mWaveform.Channels.Count; i++)
            {
                g.DrawString(_mWaveform.Channels[i].Name, small_font, Brushes.Green, 10.0f, 10.0f + i * channelsize_f);
                g.DrawLine(fine_pen, 0, i * channelsize_f, e.ClipRectangle.Width, i * channelsize_f);
                g.DrawLine(fine_pen, 0, i * channelsize_f + channelsize_f, e.ClipRectangle.Width, i * channelsize_f + channelsize_f);
                g.DrawLine(fine_pen_white, 0, i * channelsize_f + channelsize_f, 80.0f, i * channelsize_f + channelsize_f);
                g.DrawLine(fine_pen_white, 0, i * channelsize_f, 80.0f, i * channelsize_f);
            }
            g.DrawLine(fine_pen_white, 80.0f, 0, 80.0f, _mWaveform.Channels.Count * channelsize_f);

            base.OnPaint(e);
        }
    }
}
