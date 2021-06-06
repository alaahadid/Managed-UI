// ManagedUI (Managed User Interface)
// A managed user interface framework for .net desktop applications.
// 
// Copyright © Alaa Ibrahim Hadid 2021
//
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU Lesser General Public License as published 
// by the Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but 
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
using System.IO;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using System.Drawing;

namespace ManagedUI
{
    [Export(typeof(ITabControl))]
    [ControlInfo("Log Box", "tc.log")]
    [TabControlResourceInfo("TC_Name_Log", "book_open")]
    [SurpressHotKeys]
    class TabControlLogs : ITabControl
    {
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_log;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabControlLogs));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip_log = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip_log.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.ContextMenuStrip = this.contextMenuStrip_log;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            // 
            // contextMenuStrip_log
            // 
            resources.ApplyResources(this.contextMenuStrip_log, "contextMenuStrip_log");
            this.contextMenuStrip_log.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.clearToolStripMenuItem});
            this.contextMenuStrip_log.Name = "contextMenuStrip_log";
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // clearToolStripMenuItem
            // 
            resources.ApplyResources(this.clearToolStripMenuItem, "clearToolStripMenuItem");
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripButton1});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton2
            // 
            resources.ApplyResources(this.toolStripButton2, "toolStripButton2");
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripButton1
            // 
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // TabControlLogs
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TabControlLogs";
            this.contextMenuStrip_log.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private delegate void WriteLineDelegate(string message, string category);

        public override void Initialize()
        {
            base.Initialize();
        }
        public void WriteLine(string message, string category)
        {
            if (IsDisposed)
                return;
            if (this.InvokeRequired)
                this.Invoke(new WriteLineDelegate(WriteLineNormal), message, category);
            else
                WriteLineNormal(message, category);
        }

        private void WriteLineNormal(string message, string category)
        {
            if (IsDisposed)
                return;
            try
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                switch (category)
                {
                    case StatusMode.Error:
                        {
                            richTextBox1.SelectionColor = System.Drawing.Color.Red;
                            break;
                        }
                    case StatusMode.Information:
                        {
                            richTextBox1.SelectionColor = System.Drawing.Color.Green;
                            break;
                        }
                    case StatusMode.Warning:
                        {
                            richTextBox1.SelectionColor = System.Drawing.Color.Gold;
                            break;
                        }
                    case StatusMode.Normal:
                        {
                            richTextBox1.SelectionColor = System.Drawing.Color.Black;
                            break;
                        }
                }
                richTextBox1.SelectedText = message + "\n";

                // This throws an exception when a log is written and user active this control.
                richTextBox1.ScrollToCaret();
            }
            catch { }
        }

        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Title = Properties.Resources.Title_SaveLogIntoFile;
            sav.Filter = Properties.Resources.Filter_TextFiles;
            sav.FileName = Properties.Resources.TC_Name_Log + ".rtf";
            if (sav.ShowDialog(this) == DialogResult.OK)
            {
                if (Path.GetExtension(sav.FileName) == ".rtf")
                {
                    richTextBox1.SaveFile(sav.FileName, RichTextBoxStreamType.RichText);
                }
                else
                {
                    richTextBox1.SaveFile(sav.FileName, RichTextBoxStreamType.PlainText);
                }
            }
        }
        private void clearToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Text = "";
        }
        private void cutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Cut();
        }
        private void copyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Copy();
        }
        private void pasteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Paste();
        }
        private void removeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }
    }
}
