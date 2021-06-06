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
    [ControlInfo("Files", "tc.files")]
    [TabControlResourceInfo("TC_Name_Files", "")]
    class TCFiles : ITabControl
    {
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        [Import]
        private MainService service;
        public override void Initialize()
        {
            base.Initialize();
            service.FolderChanged += Service_FolderChanged;
        }

        private void Service_FolderChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            string[] files = Directory.GetFiles(service.Folder);

            foreach (string file in files)
            {
                if (Path.GetExtension(file).ToLower() == ".txt")
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = Path.GetFileName(file);
                    item.SubItems.Add(file);
                    item.Tag = file;

                    listView1.Items.Add(item);
                }
            }
        }

        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(401, 441);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 181;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Path";
            this.columnHeader2.Width = 201;
            // 
            // TCFiles
            // 
            this.Controls.Add(this.listView1);
            this.Name = "TCFiles";
            this.Size = new System.Drawing.Size(401, 441);
            this.ResumeLayout(false);

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)// Only one file accepted
                service.File = listView1.SelectedItems[0].Tag.ToString();// Path is stored in the tag
            else
                service.File = "";// Clear
        }
    }
}
