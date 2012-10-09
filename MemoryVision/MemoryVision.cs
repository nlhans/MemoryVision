﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MemoryVision.DataGrabber;
using Triton;
using Triton.Controls;
using Triton.Memory;

namespace MemoryVision
{
    public partial class MemoryVision : Form
    {
        private VisualListDetails _mTable;
        private MemoryReader _mMemory;
        private Process _mProcess;
        private Timer _mLiveWatch;
        private CheatEngineReader _mMemoryList;
        private Grabber _mGrabber;

        public MemoryVision()
        {
            InitializeComponent();

            FormClosing += new FormClosingEventHandler(MemoryVision_FormClosing);

            _mTable = new VisualListDetails(true);
            _mTable.Columns.Add("ID", "ID", 70);
            _mTable.Columns.Add("Description", "Description", 300);
            _mTable.Columns.Add("Address", "Address", 200);
            _mTable.Columns.Add("Type", "Type", 70);
            _mTable.Columns.Add("Live", "Live Watch", 100);

            _mLiveWatch = new Timer{Interval=50};
            _mLiveWatch.Tick += new EventHandler(_mLiveWatch_Tick);
            _mLiveWatch.Start();

            this.split.Panel2.Controls.Add(_mTable);
        }

        void MemoryVision_FormClosing(object sender, FormClosingEventArgs e)
        {
            TritonBase.TriggerExit();
        }

        private void _mLiveWatch_Tick(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i < _mTable.Items.Count; i++ )
            {
                ListViewItem lvi = _mTable.Items[i];
                string s = _mMemory.ReadAsString(_mMemoryList.GetByUIID(i));
                if (_mTable.Items[i].SubItems[4].Text != s)
                    _mTable.Items[i].ForeColor = Color.Red;
                else
                    _mTable.Items[i].ForeColor = Color.Black;
                _mTable.Items[i].SubItems[4].Text = s;
            }
        }

        private void bt_load_table_Click(object sender, System.EventArgs e)
        {
            if (_mMemory != null)
            {
                _mMemory.CloseHandle();
            }
            string file = "Test.CT";

            _mMemoryList = new CheatEngineReader();
            _mMemoryList.Read(file);

            lbl_table.Text = "Table:" + file;

            // Update UI.
            _mTable.Items.Clear();

            // Search for the exe
            Process[] p = Process.GetProcessesByName(_mMemoryList.ProcessExe.Substring(0, _mMemoryList.ProcessExe.Length - 4));
            if (p.Length == 1)
            {
                _mProcess = p[0];
                _mMemory = new MemoryReader();
                _mMemory.ReadProcess = p[0];
                _mMemory.OpenProcess();

                lbl_exe.Text = "PID:" + p[0].Id + "\r\n[" + p[0].ProcessName + "]";
            }
            else if (p.Length == 0)
            {
                lbl_exe.Text = "Failed";
                MessageBox.Show("Please load only if the process is already running.");
                // TODO: Wait for process.
                return;
            }
            else
            {
                lbl_exe.Text = "Failed";
                MessageBox.Show("Found multiple exe's running! Cannot figure out what to-do.");
                //TODO: Add dialog.
                return;
            }

            IntPtr base_addr = p[0].MainModule.BaseAddress;

            for (int i = 0; i < _mMemoryList.Channels.Count; i++)
            {
                _mMemoryList.Channels[i].UI_ID = i;
                _mMemoryList.Channels[i].Address = new IntPtr(_mMemoryList.Channels[i].Address.ToInt32() + base_addr.ToInt32());
                MemoryChannel ch = _mMemoryList.Channels[i];
                _mTable.Items.Add(new ListViewItem(new string[5]
                                                       {
                                                           ch.ID.ToString(),
                                                           ch.Name,
                                                           "0x"+String.Format("{0,10:X}", ch.Address.ToInt32()),
                                                           ch.TypeToString(),
                                                           "0.000"
                                                       }));
            }


            _mGrabber = new Grabber(_mMemoryList, _mMemory);
        }

        private void bt_control_Click(object sender, EventArgs e)
        {
            if (_mGrabber != null)
            {
                if (_mGrabber.Running)
                    _mGrabber.Stop();
                else
                    _mGrabber.Start();
            }
        }
    }
}