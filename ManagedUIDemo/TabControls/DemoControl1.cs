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
    [ControlInfo("Demo control 1", "tc.demo1")]
    // 3 Setup resource info, like display name, icon ...etc
    //   Here we are going to use resources, If you don't want to use resources you can 
    //   use TabControlNoResourceInfo
    //   WARNING: TabControlNoResourceInfo and TabControlResourceInfo MUST not be added to the same control.
    [TabControlResourceInfo("TC_Name_Demo1", "book_open")]
    // 4 Here we want to disable the hotkeys when this control become active so we can use shortcuts to
    //   deal with text. Presenting of this attr will be enough to tell the main form this.
    [SurpressHotKeys]
    class DemoControl1 : ITabControl
    {
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox1;
        // Don't mind this auto generated stuff, the base class will take care of it.
        // NEVER call InitializeComponent() at Initialize() or at ctor, this method will be dealt with automatically later.
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(411, 531);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "This is a demo control with text box.\nThis control uses resources for name and ic" +
    "on, thus it can be multilingual.";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 92);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // DemoControl1
            // 
            this.Controls.Add(this.richTextBox1);
            this.Name = "DemoControl1";
            this.Size = new System.Drawing.Size(411, 531);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        // Called by the GUI service when this control first discovered.
        // Can be used to do stuff like you do in ctor.
        public override void Initialize()
        {
            base.Initialize();
        }
        // Called when the control is become visible to user when user selects it in the tabs.
        public override void OnDisplay()
        {
            base.OnDisplay();
        }
        // Called when this control is become hidden from user.
        public override void OnHide()
        {
            base.OnHide();
        }
        // Called when user closes the tab that holds this control.
        public override void OnTabClose()
        {
            base.OnTabClose();
        }

        private void copyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Copy();
        }
        private void cutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Cut();
        }
        private void pasteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Paste();
        }
    }
}
