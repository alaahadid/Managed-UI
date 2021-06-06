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
using ManagedUI;

namespace ManagedUIDemo
{
    // 1 Tell the core about this control
    [Export(typeof(ITabControl))]
    // 2 Setup control info, control id must be uniqe
    [ControlInfo("Demo control 3", "tc.demo3")]
    // 3 Setup resource info, like display name, icon ...etc
    //   Here we are not going to use resources, we gonna use normal names instead. To use resources you can 
    //   use TabControlResourceInfo
    //   WARNING: TabControlNoResourceInfo and TabControlResourceInfo MUST not be added to the same control.
    [TabControlNoResourceInfo("Demo control 3")]
    // The control is ready to use now, go ahead and start designing !! :)
    class DemoControl3 : ITabControl
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;

        // Don't worry about this method, the core will take care of it.
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox2);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 411);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Just another controls... here we are going to test the docked controls.";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox1.Location = new System.Drawing.Point(3, 16);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(457, 96);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "Dock in the top ....";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Location = new System.Drawing.Point(3, 112);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(457, 296);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "Dock is full !!";
            // 
            // DemoControl3
            // 
            this.Controls.Add(this.groupBox1);
            this.Name = "DemoControl3";
            this.Size = new System.Drawing.Size(463, 411);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
