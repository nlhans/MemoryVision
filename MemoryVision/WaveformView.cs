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
            //float channelsize_
            float channelsize_analogue_f = 44.0f;
            int channelsize_analogue_i = Convert.ToInt32(channelsize_analogue_f);
            float channelsize_digital_f = 22.0f;
            int channelsize_digital_i = Convert.ToInt32(channelsize_digital_f);
            
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

            float y_graph = 0;
            float x_scale = (e.ClipRectangle.Width - 80.0f) / (sample_end - sample_start);
            for (int i = 0; i < _mWaveform.Channels.Count; i++)
            {
                if (_mWaveform.Channels[i].Analogue)
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
                        y_part *= channelsize_analogue_i;
                        if (double.IsNaN(y_part) || double.IsInfinity(y_part))
                            y_part = 0;
                        int x = Convert.ToInt32(80.0f + Convert.ToSingle(x_scale*(sample - sample_start)));
                        int y = Convert.ToInt32(y_graph + y_part);

                        graph.Add(new Point(x, y));
                    }
                    graph.Add(new Point(e.ClipRectangle.Width, Convert.ToInt32(y_graph)));
                    graph.Add(new Point(80, Convert.ToInt32(y_graph)));
                    g.DrawPolygon(line_pen, graph.ToArray());
                    y_graph += channelsize_analogue_f;
                }
                else
                {
                    y_graph += channelsize_digital_f;

                    int sample_last_draw = sample_start;
                    byte[] last_value = _mWaveform.GetBytes(i, sample_start);
                    for (int sample = sample_start; sample < sample_end; sample++)
                    {
                        byte[] value = _mWaveform.GetBytes(i, sample);
                        bool eq = true;
                        for (int b = 0; b < value.Length; b++)
                            if (value[b] != last_value[b])
                                eq = false;

                        if (sample == sample_end-1 || !eq)
                        {
                            // Draw last value(!)
                            int sample_width = sample - sample_last_draw;
                            float draw_width = 8.0f;
                            float draw_height = 13.0f;
                            float x_size = x_scale*sample_width+2;
                            object last_value_object = _mWaveform.GetData(i, sample-1);

                            float x = 80.0f + sample*x_scale -x_size  / 2.0f-draw_width/2.0f;
                            float y = y_graph - channelsize_digital_f / 2.0f - draw_height / 2.0f;
                            
                            if(x_size > draw_width)
                            {
                                g.DrawString(last_value_object.ToString(), small_font, Brushes.White, x,y);
                            }
                            
                            float x_box_0 = 80.0f+sample_last_draw*x_scale;
                            float x_box_1 = 80.0f + sample * x_scale - 3;
                            float x_box_2 = 80.0f + sample * x_scale;

                            float y_box_0 = y_graph-2;
                            float y_box_1 = y_graph - channelsize_digital_f+2;

                            g.DrawLine(line_pen, x_box_0, y_box_0, x_box_1, y_box_0); // top
                            g.DrawLine(line_pen, x_box_0, y_box_1, x_box_1, y_box_1); // bot
                            g.DrawLine(line_pen, x_box_1, y_box_0, x_box_2, y_box_1); // top->bot
                            g.DrawLine(line_pen, x_box_1, y_box_1, x_box_2, y_box_0); // bot->top


                            last_value = value;
                            sample_last_draw = sample;
                        }
                    }
                }
            }

            // Draw channels.
            y_graph = 0;
            for (int i = 0; i < _mWaveform.Channels.Count; i++)
            {
                float channelsize = ((_mWaveform.Channels[i].Analogue)
                                         ? channelsize_analogue_f
                                         : channelsize_digital_f);
                g.DrawString(_mWaveform.Channels[i].Name, small_font, Brushes.LightGreen, 5.0f, 5.0f + y_graph);
                g.DrawLine(fine_pen, 0, y_graph, e.ClipRectangle.Width, y_graph);
                g.DrawLine(fine_pen, 0, y_graph + channelsize, e.ClipRectangle.Width, y_graph + channelsize);
                g.DrawLine(fine_pen_white, 0, y_graph + channelsize, 80.0f, y_graph+channelsize);
                g.DrawLine(fine_pen_white, 0, y_graph, 80.0f, y_graph);
            
                y_graph+=channelsize;
            }
            g.DrawLine(fine_pen_white, 80.0f, 0, 80.0f, y_graph);

            base.OnPaint(e);
        }
    }
}
