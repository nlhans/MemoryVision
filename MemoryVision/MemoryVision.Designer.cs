namespace MemoryVision
{
    partial class MemoryVision
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.split = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pb_control = new System.Windows.Forms.ProgressBar();
            this.lbl_control = new System.Windows.Forms.Label();
            this.bt_control = new System.Windows.Forms.Button();
            this.bt_settings = new System.Windows.Forms.Button();
            this.bt_triggering = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_exe = new System.Windows.Forms.Label();
            this.lbl_table = new System.Windows.Forms.Label();
            this.bt_load_table = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_clear_waveform = new System.Windows.Forms.Button();
            this.bt_view_waveform = new System.Windows.Forms.Button();
            this.bt_store_waveform = new System.Windows.Forms.Button();
            this.bt_load_waveform = new System.Windows.Forms.Button();
            this.lbl_waveform_file = new System.Windows.Forms.Label();
            this.lbl_waveform_data = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.split.IsSplitterFixed = true;
            this.split.Location = new System.Drawing.Point(0, 0);
            this.split.Name = "split";
            this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.groupBox3);
            this.split.Panel1.Controls.Add(this.groupBox2);
            this.split.Panel1.Controls.Add(this.groupBox1);
            this.split.Panel1MinSize = 80;
            this.split.Size = new System.Drawing.Size(844, 494);
            this.split.SplitterDistance = 110;
            this.split.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pb_control);
            this.groupBox3.Controls.Add(this.lbl_control);
            this.groupBox3.Controls.Add(this.bt_control);
            this.groupBox3.Controls.Add(this.bt_settings);
            this.groupBox3.Controls.Add(this.bt_triggering);
            this.groupBox3.Location = new System.Drawing.Point(514, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(305, 85);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // pb_control
            // 
            this.pb_control.Location = new System.Drawing.Point(87, 48);
            this.pb_control.Maximum = 1000;
            this.pb_control.Name = "pb_control";
            this.pb_control.Size = new System.Drawing.Size(212, 23);
            this.pb_control.TabIndex = 4;
            // 
            // lbl_control
            // 
            this.lbl_control.AutoSize = true;
            this.lbl_control.Location = new System.Drawing.Point(168, 24);
            this.lbl_control.Name = "lbl_control";
            this.lbl_control.Size = new System.Drawing.Size(59, 13);
            this.lbl_control.TabIndex = 3;
            this.lbl_control.Text = "Status: idle";
            // 
            // bt_control
            // 
            this.bt_control.Location = new System.Drawing.Point(87, 19);
            this.bt_control.Name = "bt_control";
            this.bt_control.Size = new System.Drawing.Size(75, 23);
            this.bt_control.TabIndex = 2;
            this.bt_control.Text = "START";
            this.bt_control.UseVisualStyleBackColor = true;
            this.bt_control.Click += new System.EventHandler(this.bt_control_Click);
            // 
            // bt_settings
            // 
            this.bt_settings.Location = new System.Drawing.Point(6, 48);
            this.bt_settings.Name = "bt_settings";
            this.bt_settings.Size = new System.Drawing.Size(75, 23);
            this.bt_settings.TabIndex = 1;
            this.bt_settings.Text = "Settings";
            this.bt_settings.UseVisualStyleBackColor = true;
            // 
            // bt_triggering
            // 
            this.bt_triggering.Location = new System.Drawing.Point(6, 19);
            this.bt_triggering.Name = "bt_triggering";
            this.bt_triggering.Size = new System.Drawing.Size(75, 23);
            this.bt_triggering.TabIndex = 0;
            this.bt_triggering.Text = "Triggering";
            this.bt_triggering.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_exe);
            this.groupBox2.Controls.Add(this.lbl_table);
            this.groupBox2.Controls.Add(this.bt_load_table);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(163, 85);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Address Table";
            // 
            // lbl_exe
            // 
            this.lbl_exe.AutoSize = true;
            this.lbl_exe.Location = new System.Drawing.Point(88, 28);
            this.lbl_exe.Name = "lbl_exe";
            this.lbl_exe.Size = new System.Drawing.Size(10, 13);
            this.lbl_exe.TabIndex = 2;
            this.lbl_exe.Text = "-";
            // 
            // lbl_table
            // 
            this.lbl_table.AutoSize = true;
            this.lbl_table.Location = new System.Drawing.Point(6, 53);
            this.lbl_table.Name = "lbl_table";
            this.lbl_table.Size = new System.Drawing.Size(32, 13);
            this.lbl_table.TabIndex = 1;
            this.lbl_table.Text = "File: -";
            // 
            // bt_load_table
            // 
            this.bt_load_table.Location = new System.Drawing.Point(6, 19);
            this.bt_load_table.Name = "bt_load_table";
            this.bt_load_table.Size = new System.Drawing.Size(75, 23);
            this.bt_load_table.TabIndex = 0;
            this.bt_load_table.Text = "Load";
            this.bt_load_table.UseVisualStyleBackColor = true;
            this.bt_load_table.Click += new System.EventHandler(this.bt_load_table_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_waveform_data);
            this.groupBox1.Controls.Add(this.lbl_waveform_file);
            this.groupBox1.Controls.Add(this.bt_clear_waveform);
            this.groupBox1.Controls.Add(this.bt_view_waveform);
            this.groupBox1.Controls.Add(this.bt_store_waveform);
            this.groupBox1.Controls.Add(this.bt_load_waveform);
            this.groupBox1.Location = new System.Drawing.Point(181, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Waveform";
            // 
            // bt_clear_waveform
            // 
            this.bt_clear_waveform.Location = new System.Drawing.Point(87, 48);
            this.bt_clear_waveform.Name = "bt_clear_waveform";
            this.bt_clear_waveform.Size = new System.Drawing.Size(75, 23);
            this.bt_clear_waveform.TabIndex = 3;
            this.bt_clear_waveform.Text = "Clear";
            this.bt_clear_waveform.UseVisualStyleBackColor = true;
            // 
            // bt_view_waveform
            // 
            this.bt_view_waveform.Location = new System.Drawing.Point(87, 19);
            this.bt_view_waveform.Name = "bt_view_waveform";
            this.bt_view_waveform.Size = new System.Drawing.Size(75, 23);
            this.bt_view_waveform.TabIndex = 2;
            this.bt_view_waveform.Text = "View";
            this.bt_view_waveform.UseVisualStyleBackColor = true;
            this.bt_view_waveform.Click += new System.EventHandler(this.bt_view_waveform_Click);
            // 
            // bt_store_waveform
            // 
            this.bt_store_waveform.Location = new System.Drawing.Point(6, 48);
            this.bt_store_waveform.Name = "bt_store_waveform";
            this.bt_store_waveform.Size = new System.Drawing.Size(75, 23);
            this.bt_store_waveform.TabIndex = 1;
            this.bt_store_waveform.Text = "Store";
            this.bt_store_waveform.UseVisualStyleBackColor = true;
            // 
            // bt_load_waveform
            // 
            this.bt_load_waveform.Location = new System.Drawing.Point(6, 19);
            this.bt_load_waveform.Name = "bt_load_waveform";
            this.bt_load_waveform.Size = new System.Drawing.Size(75, 23);
            this.bt_load_waveform.TabIndex = 0;
            this.bt_load_waveform.Text = "Load";
            this.bt_load_waveform.UseVisualStyleBackColor = true;
            this.bt_load_waveform.Click += new System.EventHandler(this.bt_load_waveform_Click);
            // 
            // lbl_waveform_file
            // 
            this.lbl_waveform_file.AutoSize = true;
            this.lbl_waveform_file.Location = new System.Drawing.Point(168, 24);
            this.lbl_waveform_file.Name = "lbl_waveform_file";
            this.lbl_waveform_file.Size = new System.Drawing.Size(26, 13);
            this.lbl_waveform_file.TabIndex = 5;
            this.lbl_waveform_file.Text = "File:";
            // 
            // lbl_waveform_data
            // 
            this.lbl_waveform_data.AutoSize = true;
            this.lbl_waveform_data.Location = new System.Drawing.Point(168, 53);
            this.lbl_waveform_data.Name = "lbl_waveform_data";
            this.lbl_waveform_data.Size = new System.Drawing.Size(51, 13);
            this.lbl_waveform_data.TabIndex = 6;
            this.lbl_waveform_data.Text = "0ch 0ksp";
            // 
            // MemoryVision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 494);
            this.Controls.Add(this.split);
            this.Name = "MemoryVision";
            this.Text = "MemoryVision";
            this.split.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_clear_waveform;
        private System.Windows.Forms.Button bt_view_waveform;
        private System.Windows.Forms.Button bt_store_waveform;
        private System.Windows.Forms.Button bt_load_waveform;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bt_load_table;
        private System.Windows.Forms.Label lbl_table;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bt_triggering;
        private System.Windows.Forms.Button bt_settings;
        private System.Windows.Forms.Button bt_control;
        private System.Windows.Forms.ProgressBar pb_control;
        private System.Windows.Forms.Label lbl_control;
        private System.Windows.Forms.Label lbl_exe;
        private System.Windows.Forms.Label lbl_waveform_data;
        private System.Windows.Forms.Label lbl_waveform_file;
    }
}

