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
using System;
using System.Windows.Forms;

namespace ManagedUI
{
    /// <summary>
    /// Add new menu item represetator form
    /// </summary>
    public partial class FormAddMIR : Form
    {
        /// <summary>
        /// Add new menu item represetator form
        /// </summary>
        /// <param name="type">The menu item type</param>
        public FormAddMIR(MIRType type)
        {
            InitializeComponent();
            switch (type)
            {
                case MIRType.ROOT:
                    {
                        // Load all available rmi's ...
                        foreach (Lazy<RMI, IMenuItemRepresentatorInfo> mir in
                            GUIService.GUI.AvailableRMIs)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = mir.Value.DisplayName;
                            item.SubItems.Add(mir.Value.ToolTip);
                            item.ImageIndex = -1;
                            item.Tag = mir.Metadata.ID;
                            listView1.Items.Add(item);
                        }
                        break;
                    }
                case MIRType.CMI:
                    {
                        // Load all available cmi's ...
                        foreach (Lazy<CMI, IMenuItemRepresentatorInfo> mir in
                            GUIService.GUI.AvailableCMIs)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = mir.Value.DisplayName;
                            item.SubItems.Add(mir.Value.ToolTip);
                            item.Tag = mir.Metadata.ID;
                            if (mir.Value.Icon != null)
                            {
                                imageList1.Images.Add(mir.Value.Icon);
                                item.ImageIndex = imageList1.Images.Count - 1;
                            }
                            else
                                item.ImageIndex = -1;
                            listView1.Items.Add(item);
                        }
                        break;
                    }
                case MIRType.DMI:
                    {
                        // Load all available dmi's ...
                        foreach (Lazy<DMI, IMenuItemRepresentatorInfo> mir in
                            GUIService.GUI.AvailableDMIs)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = mir.Value.DisplayName;
                            item.SubItems.Add("");
                            item.Tag = mir.Metadata.ID;
                            listView1.Items.Add(item);
                        }
                        break;
                    }
                case MIRType.CBMI:
                    {
                        // Load all available dmi's ...
                        foreach (Lazy<CBMI, IMenuItemRepresentatorInfo> mir in
                            GUIService.GUI.AvailableCBMIs)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = mir.Value.DisplayName;
                            item.SubItems.Add(mir.Value.ToolTip);
                            item.Tag = mir.Metadata.ID;
                            listView1.Items.Add(item);
                        }
                        break;
                    }
                case MIRType.TMI:
                    {
                        // Load all available dmi's ...
                        foreach (Lazy<TMI, IMenuItemRepresentatorInfo> mir in
                            GUIService.GUI.AvailableTMIs)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = mir.Value.DisplayName;
                            item.SubItems.Add(mir.Value.ToolTip);
                            item.Tag = mir.Metadata.ID;
                            listView1.Items.Add(item);
                        }
                        break;
                    }
                case MIRType.PMI:
                    {
                        // Load all available pmi's ...
                        foreach (Lazy<PMI, IMenuItemRepresentatorInfo> mir in
                            GUIService.GUI.AvailablePMIs)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = mir.Value.DisplayName;
                            item.SubItems.Add(mir.Value.ToolTip);
                            item.Tag = mir.Metadata.ID;
                            if (mir.Value.Icon != null)
                            {
                                imageList1.Images.Add(mir.Value.Icon);
                                item.ImageIndex = imageList1.Images.Count - 1;
                            }
                            else
                                item.ImageIndex = -1;
                            listView1.Items.Add(item);
                        }
                        break;
                    }
            }

        }
        /// <summary>
        /// Selected menu item id
        /// </summary>
        public string SelectedMIRID
        {
            get
            {
                if (listView1.SelectedItems.Count > 0)
                    return listView1.SelectedItems[0].Tag.ToString();
                else
                    return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = listView1.SelectedItems.Count == 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
