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
    [ControlInfo("Folders", "tc.folders")]
    [TabControlResourceInfo("TC_Name_Folders", "")]
    class TCFolders : ITabControl
    {
        private System.Windows.Forms.TreeView treeView1;
        [Import]
        private MainService service;

        public override void Initialize()
        {
            base.Initialize();
            // Handle the DiskChanged event
            service.DiskChanged += Service_DiskChanged;
        }
        // This called when a disk is selected from TCDisks control
        private void Service_DiskChanged(object sender, EventArgs e)
        {
            // Clear the tree !
            treeView1.Nodes.Clear();
            try// We use try here to handle exceptions when attempting to access a forbidden dir or empty cd/dvd/blue ray drives.
            {
                // Get all directories from selected disk
                string[] folders = Directory.GetDirectories(service.Disk);
                // Add them to the treeview control. We are going to use helper method for this
                foreach (string folder in folders)
                {
                    TreeNode node = new TreeNode();
                    node.Text = Path.GetFileName(folder);// Get the folder name
                    node.Tag = folder;// Keep the full path in the tag

                    // Add it
                    treeView1.Nodes.Add(node);
                    // Add the children of it !
                    AddTreeNode(folder, node);
                }
            }
            catch { }
        }
        /// <summary>
        /// Add a folder's sub-folders into a node
        /// </summary>
        /// <param name="folder">The folder path</param>
        /// <param name="node">The treenode to add into</param>
        private void AddTreeNode(string folder, TreeNode node)
        {
            try// We use try here to handle exceptions when attempting to access a forbidden dir
            {
                // Get all directories from selected folder
                string[] folders = Directory.GetDirectories(folder);
                // Add them to the node control.
                foreach (string fol in folders)
                {
                    TreeNode chnode = new TreeNode();
                    chnode.Text = Path.GetFileName(fol);// Get the folder name
                    chnode.Tag = fol;// Keep the full path in the tag

                    // Add it
                    node.Nodes.Add(chnode);
                    // Add the children of it !
                    AddTreeNode(fol, chnode);
                }
            }
            catch
            {

            }
        }

        private void InitializeComponent()
        {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(418, 407);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // TCFolders
            // 
            this.Controls.Add(this.treeView1);
            this.Name = "TCFolders";
            this.Size = new System.Drawing.Size(418, 407);
            this.ResumeLayout(false);

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Do a simple check ...
            if (treeView1.SelectedNode == null)
                return;

            service.Folder = treeView1.SelectedNode.Tag.ToString();// The folder complete path is stored in the Tag !!
        }
    }
}