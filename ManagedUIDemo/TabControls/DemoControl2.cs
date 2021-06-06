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
using System.ComponentModel.Composition;
using System.Drawing;
using ManagedUI;

namespace ManagedUIDemo
{
    // 1 Tell the core about this control
    [Export(typeof(ITabControl))]
    // 2 Setup control info, control id must be uniqe
    [ControlInfo("Demo control 2", "tc.demo2")]
    // 3 Setup resource info, like display name, icon ...etc
    //   Here we are not going to use resources, we gonna use normal names instead. To use resources you can 
    //   use TabControlResourceInfo
    //   WARNING: TabControlNoResourceInfo and TabControlResourceInfo MUST not be added to the same control.
    [TabControlNoResourceInfo("Demo control 2")]
    // The control is ready to use now, go ahead and start designing !! :)
    class DemoControl2 : ITabControl
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ListBox listBox1;

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 45);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(77, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Item1",
            "Item2",
            "Item3"});
            this.listBox1.Location = new System.Drawing.Point(6, 68);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 3;
            // 
            // DemoControl2
            // 
            this.BackColor = System.Drawing.Color.DarkRed;
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "DemoControl2";
            this.Size = new System.Drawing.Size(415, 377);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        // Since we have chosen TabControlNoResourceInfo, we need to setup icon manualy if we want to use icon.
        // * We can set the Icon property in the ctor.
        // * OR we can override the proeprty as we do here.
        public override Image Icon
        {
            get
            {
                // Return the object of your desire.
                return Properties.Resources.control_eject_blue;
            }

            set
            {
                // Not important since we already overriden the property.
                // base.Icon = value;
            }
        }
    }
}
