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
using System.IO;
using System.Linq;
using System.Text;
using ManagedUI;

namespace ManagedUIDemo
{
    // 1 Export the type of settings control. This class should be a subclass of ISettingsControl of course.
    [Export(typeof(ISettingsControl))]
    // 2 Setup info of this control
    [ControlInfo("Demo Settings Control 1", "settings.demo1")]
    // 3 Setup resource info, like display name, icon ...etc
    //   Here we are going to use resources, If you don't want to use resources you can 
    //   use SettingsControlNoResourceInfo
    //   WARNING: SettingsControlNoResourceInfo and SettingsControlResourceInfo MUST not be added to the same control.
    // This control has no category.
    [SettingsControlResourceInfo("SC_Name_Demo1", "", "wrench", "SC_Desc_Demo1")]
    class SettingsControl1 : ISettingsControl
    {
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox5;

        private void InitializeComponent()
        {
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(3, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(77, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(3, 26);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(77, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(86, 3);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(77, 17);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(86, 26);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(77, 17);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "checkBox4";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(169, 3);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(77, 17);
            this.checkBox5.TabIndex = 4;
            this.checkBox5.Text = "checkBox5";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(243, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "Some value of some settings";
            // 
            // SettingsControl1
            // 
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Name = "SettingsControl1";
            this.Size = new System.Drawing.Size(307, 168);
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
            checkBox1.Checked = true;
            checkBox2.Checked = false;
            checkBox3.Checked = true;
            checkBox4.Checked = false;
            checkBox5.Checked = true;
            textBox1.Text = "Some value for some settings";
        }
        public override void ExportSettings(ref BinaryWriter stream)
        {
            base.ExportSettings(ref stream);
            // We use the binary writer to save settings values. 
            // We use the actual settings for this, here as demo we will use the gui instead.
            // IMPORTANT !! 
            // Let's say we export the settings values chk1, chk2...chk9 
            // then when we import the settings we read chk1, chk2...chk9 (same order) 
            stream.Write(checkBox1.Checked);
            stream.Write(checkBox2.Checked);
            stream.Write(checkBox3.Checked);
            stream.Write(checkBox4.Checked);
            stream.Write(checkBox5.Checked);
            // We need to do some trick here to write a text value
            WriteStringASCII(textBox1.Text, stream);
        }
        public override void ImportSettings(ref BinaryReader stream)
        {
            base.ImportSettings(ref stream);
            // We use the binary reader to import settings values. 
            // We use the actual settings for this, here as demo we will use the gui instead.
            // IMPORTANT !! 
            // We read the settings values as they are exported, for example, we export chk1, chk2...chk9 
            // then when we import the settings we read chk1, chk2...chk9 (same order) 
            checkBox1.Checked = stream.ReadBoolean();
            checkBox2.Checked = stream.ReadBoolean();
            checkBox3.Checked = stream.ReadBoolean();
            checkBox4.Checked = stream.ReadBoolean();
            checkBox5.Checked = stream.ReadBoolean();
            // We need to do some trick here to write a text value
            textBox1.Text = ReadStringASCII(stream);
        }
    }
}
