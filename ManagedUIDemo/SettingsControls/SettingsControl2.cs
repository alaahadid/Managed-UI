// ManagedUI (Managed User Interface)
// A managed user interface framework for .net desktop applications.
// 
// Copyright © Alaa Ibrahim Hadid 2021
//
// This library is free software; you can redistribute it and/or modify 
// it under the terms of the GNU Lesser General Public License as published 
// by the Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE.See the GNU Lesser General Public 
// License for more details.
//
// You should have received a copy of the GNU Lesser General Public License 
// along with this library; if not, write to the Free Software Foundation, 
// Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// 
// Author email: mailto:alaahadidfreeware@gmail.com
//
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ManagedUI;

namespace ManagedUIDemo
{
    // 1 Export the type of settings control. This class should be a subclass of ISettingsControl of course.
    [Export(typeof(ISettingsControl))]
    // 2 Setup info of this control
    [ControlInfo("Demo Settings Control 2", "settings.demo2")]
    // 3 Setup resource info, like display name, icon ...etc
    //   Here we are not going to use resources, If you don't want use resources you can 
    //   use SettingsControlResourceInfo
    //   WARNING: SettingsControlNoResourceInfo and SettingsControlResourceInfo MUST not be added to the same control.
    // This control has category.
    [SettingsControlNoResourceInfo("Demo Settings Control 2", "Some Category", "This control is for demo purpose, it uses category.")]
    class SettingsControl2 : ISettingsControl
    {
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        // Since we have chosen SettingsControlNoResourceInfo, we need to setup icon manualy if we want to use icon.
        // * We can set the Icon property in the ctor.
        // * We can override the proeprty as we do here.
        public override Image Icon
        {
            get
            {
                // Return the object of your desire.
                return Properties.Resources.wrench_orange;
            }
            set
            {
                // Not important since we already overriden the property.
                base.Icon = value;
            }
        }

        private void InitializeComponent()
        {
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(87, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(96, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(87, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(189, 3);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(87, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "radioButton3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(3, 26);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(273, 45);
            this.trackBar1.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Somesettings Item 1",
            "Somesettings Item 2",
            "Somesettings Item 3"});
            this.comboBox1.Location = new System.Drawing.Point(3, 77);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(273, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // SettingsControl2
            // 
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Name = "SettingsControl2";
            this.Size = new System.Drawing.Size(319, 156);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public override void LoadSettings()
        {
            base.LoadSettings();
            // Write the code to load your settings here (GUI from actual settings)....
        }
        public override void SaveSettings()
        {
            base.SaveSettings();
            // We apply the values of GUI to actual settings here.
        }
        public override void DefaultSettings()
        {
            base.DefaultSettings();
            // We reset GUI settings to defaults, never apply to actual settings yet.
            radioButton2.Checked = true;
            trackBar1.Value = 1;
            comboBox1.SelectedIndex = 1;
        }
        public override void ExportSettings(ref BinaryWriter stream)
        {
            base.ExportSettings(ref stream);
            // We use the binary writer to save settings values. 
            // We use the actual settings for this, here as demo we will use the gui instead.
            // IMPORTANT !! 
            // Let's say we export the settings values chk1, chk2...chk9 
            // then when we import the settings we read chk1, chk2...chk9 (same order) 
            if (radioButton1.Checked)
                stream.Write(1);
            else if (radioButton2.Checked)
                stream.Write(2);
            else if (radioButton3.Checked)
                stream.Write(3);
            stream.Write(trackBar1.Value);
            stream.Write(comboBox1.SelectedIndex);
        }
        public override void ImportSettings(ref BinaryReader stream)
        {
            base.ImportSettings(ref stream);
            // We use the binary reader to import settings values. 
            // We use the actual settings for this, here as demo we will use the gui instead.
            // IMPORTANT !! 
            // We read the settings values as they are exported, for example, we export chk1, chk2...chk9 
            // then when we import the settings we read chk1, chk2...chk9 (same order) 

            int val = stream.ReadInt32();
            switch (val)
            {
                case 1: radioButton1.Checked = true; break;
                case 2: radioButton2.Checked = true; break;
                case 3: radioButton3.Checked = true; break;
            }
            trackBar1.Value = stream.ReadInt32();
            comboBox1.SelectedIndex = stream.ReadInt32();
        }
    }
}
