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
using ManagedUI.Properties;
using System;
using System.Windows.Forms;

namespace ManagedUI
{
    /// <summary>
    /// Show all services in the app
    /// </summary>
    public partial class FormServicesBrowser : Form
    {
        /// <summary>
        /// Form that show all services of ManagedUI
        /// </summary>
        public FormServicesBrowser()
        {
            InitializeComponent();
            if (Settings.Default.BlackListedServices == null)
                Settings.Default.BlackListedServices = new System.Collections.Specialized.StringCollection();
            CommandsManager.CMD.Execute("console.show");
            RefreshServices();
        }
        private void RefreshServices()
        {
            listView1.Items.Clear();
            foreach (Lazy<IService, IServiceInfo> ser in MUI.Services)
            {
                ListViewItem item = new ListViewItem();
                item.Text = ser.Metadata.Name;
                item.SubItems.Add(ser.Metadata.ID);
                item.SubItems.Add(ser.Metadata.Description);
                item.SubItems.Add(ser.Metadata.IsDefault ? "Yes" : "No");
                item.SubItems.Add((!Settings.Default.BlackListedServices.Contains(ser.Metadata.ID)) ? "Yes" : "No");

                item.Tag = ser.Metadata.ID;

                listView1.Items.Add(item);
            }
        }
        // Close
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Enable Selected
        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                CommandsManager.CMD.Execute("service.blacklist.remove", new object[] { listView1.SelectedItems[0].Tag.ToString() });
                RefreshServices();
            }
        }
        // Disable Selected
        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                CommandsManager.CMD.Execute("service.blacklist", new object[] { listView1.SelectedItems[0].Tag.ToString() });
                RefreshServices();
            }
        }
    }
}
