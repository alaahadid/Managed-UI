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
using System.ComponentModel.Composition;
using ManagedUI;
using System.IO;
using System.Windows.Forms;

namespace MyProject
{
    [Export(typeof(ITabControl))]
    [ControlInfo("Text Edit", "tc.textedit")]
    [TabControlResourceInfo("TC_Name_TextEdit", "")]
    class TCTextEdit : ITabControl
    {
        [Import]
        private MainService service;

        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(625, 586);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // TCTextEdit
            // 
            this.Controls.Add(this.richTextBox1);
            this.Name = "TCTextEdit";
            this.Size = new System.Drawing.Size(625, 586);
            this.ResumeLayout(false);

        }

        private RichTextBox richTextBox1;

        public override void Initialize()
        {
            base.Initialize();
            service.FileChanged += Service_FileChanged;
        }
        /// <summary>
        /// Save the content of the rich text box control into Service.File.
        /// </summary>
        public void SaveChanges()
        {
            richTextBox1.SaveFile(service.File, RichTextBoxStreamType.PlainText);
        }

        private void Service_FileChanged(object sender, EventArgs e)
        {
            if (File.Exists(service.File))
                richTextBox1.Lines = File.ReadAllLines(service.File);
            else
                richTextBox1.Text = "";
        }
    }
}
