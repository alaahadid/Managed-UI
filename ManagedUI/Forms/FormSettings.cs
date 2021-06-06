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
using System.IO;
using System.Windows.Forms;

namespace ManagedUI
{
    /// <summary>
    /// Settings form
    /// </summary>
    public partial class FormSettings : Form
    {
        /// <summary>
        /// The settings form
        /// </summary>
        public FormSettings()
        {
            InitializeComponent();
            foreach (Lazy<ISettingsControl, IControlInfo> con in GUIService.GUI.AvailableSettingControls)
            {
                con.Value.LoadSettings();
            }
            imageList1.Images.Add(Properties.Resources.bullet_black);
            imageList1.Images.Add(Properties.Resources.bullet_white);
            GUIService.GUI.OnSettingsOpened();
            ReloadSettingControls();
        }

        private ISettingsControl settingsControl;

        private void ReloadSettingControls()
        {
            treeView1.Nodes.Clear();
            panel_settingsPanel.Controls.Clear();
            label_settingsInfo.Text = "";
            label_title.Text = "";
            if (GUIService.GUI.AvailableSettingControls == null)
                return;
            // 2 Add controls without category
            foreach (Lazy<ISettingsControl, IControlInfo> con in GUIService.GUI.AvailableSettingControls)
            {
                // Check out the category
                if (con.Value.Category == null || con.Value.Category == "")
                {
                    // No category, just add the control
                    treeView1.Nodes.Add(GetSettingsControlNode(con));
                }
            }
            foreach (Lazy<ISettingsControl, IControlInfo> con in GUIService.GUI.AvailableSettingControls)
            {
                // Check out the category
                if (con.Value.Category != null && con.Value.Category != "")
                {
                    // Search for the category node
                    bool found = false;
                    foreach (TreeNode node in treeView1.Nodes)
                    {
                        if (node.Text == con.Value.Category)
                        {
                            node.Nodes.Add(GetSettingsControlNode(con));
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        TreeNode node = new TreeNode();
                        node.Text = con.Value.Category;
                        node.ImageIndex = node.SelectedImageIndex = 0;
                        treeView1.Nodes.Add(node);

                        node.Nodes.Add(GetSettingsControlNode(con));
                    }
                }
            }
         
            treeView1.ExpandAll();
        }
        private TreeNode GetSettingsControlNode(Lazy<ISettingsControl, IControlInfo> con)
        {
            TreeNode node = new TreeNode();
            node.Text = con.Value.DisplayName;
            if (con.Value.Icon != null)
            {
                imageList1.Images.Add(con.Value.Icon);
                node.SelectedImageIndex = node.ImageIndex = imageList1.Images.Count - 1;
            }
            else
            {
                node.ImageIndex = node.SelectedImageIndex = 1;
            }
            node.Tag = con.Metadata.ID;
            return node;
        }

        // Cancel
        private void button1_Click(object sender, EventArgs e)
        {
            GUIService.GUI.OnSettingsClosedDiscarded();
            Close();
        }
        // Save
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Lazy<ISettingsControl, IControlInfo> con in GUIService.GUI.AvailableSettingControls)
            {
                if (con.Value.CanSave)
                {
                    con.Value.SaveSettings();
                }
                else
                {
                    // Select the control and abort
                    panel_settingsPanel.Controls.Clear();
                    settingsControl = con.Value;
                    settingsControl.Location = new System.Drawing.Point(0, 0);
                    panel_settingsPanel.Controls.Add(settingsControl);
                    return;
                }
            }
            GUIService.GUI.OnSettingsClosedSaving();
            Close();
        }
        // Default
        private void button3_Click(object sender, EventArgs e)
        {
            if (settingsControl != null)
            {
                settingsControl.DefaultSettings();
                GUIService.GUI.OnSettingsResetDefault(settingsControl.ID);
            }
        }
        // Default all
        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Lazy<ISettingsControl, IControlInfo> con in GUIService.GUI.AvailableSettingControls)
            {
                con.Value.DefaultSettings();
            }
            GUIService.GUI.OnSettingsResetDefaultAll();
        }
        // Import
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = Properties.Resources.Title_ImportSettings;
            op.Filter = Properties.Resources.FilterName_SettingsBackup + "(*.st)|*.st;*.ST;";
            if (op.ShowDialog(this) == DialogResult.OK)
            {
                // Do the import
                try
                {
                    Stream str = new FileStream(op.FileName, FileMode.Open, FileAccess.Read);
                    BinaryReader reader = new BinaryReader(str);
                    foreach (Lazy<ISettingsControl, IControlInfo> con in GUIService.GUI.AvailableSettingControls)
                        con.Value.ImportSettings(ref reader);
                    str.Flush();
                    str.Close();
                    reader.Close();

                    ManagedMessageBox.ShowMessage(Properties.Resources.Message_SettingsImportedSuccessfully);
                }
                catch (Exception ex)
                {
                    ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToImportTheSettings + "\n" + ex.Message);
                }
            }
        }
        // Export
        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Title = Properties.Resources.Title_ExportSettings;
            sav.Filter = Properties.Resources.FilterName_SettingsBackup + "(*.st)|*.st;*.ST;";
            if (sav.ShowDialog(this) == DialogResult.OK)
            {
                ManagedMessageBoxResult res = ManagedMessageBox.ShowQuestionMessage(Properties.Resources.Message_ExportSettingsRequiresSaveSettings, Properties.Resources.Title_ExportSettings);
                if (res.ClickedButtonIndex == 0)
                {
                    // Save
                    foreach (Lazy<ISettingsControl, IControlInfo> con in GUIService.GUI.AvailableSettingControls)
                    {
                        if (con.Value.CanSave)
                        {
                            con.Value.SaveSettings();
                        }
                        else
                        {
                            // Select the control and abort
                            panel_settingsPanel.Controls.Clear();
                            settingsControl = con.Value;
                            panel_settingsPanel.Controls.Add(settingsControl);
                            settingsControl.Location = new System.Drawing.Point(0, 0);
                            return;
                        }
                    }
                    // Do the export
                    try
                    {
                        Stream str = new FileStream(sav.FileName, FileMode.Create, FileAccess.Write);
                        BinaryWriter writer = new BinaryWriter(str);
                        foreach (Lazy<ISettingsControl, IControlInfo> con in GUIService.GUI.AvailableSettingControls)
                            con.Value.ExportSettings(ref writer);
                        str.Flush();
                        str.Close();
                        writer.Close();

                        ManagedMessageBox.ShowMessage(Properties.Resources.Message_SettingsExportedSuccessfully);
                    }
                    catch (Exception ex)
                    {
                        ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToExportTheSettings + "\n" + ex.Message);
                    }
                }
            }
        }
        // After select
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Clear the panel
            panel_settingsPanel.Controls.Clear();
            label_settingsInfo.Text = "";
            label_title.Text = "";
            settingsControl = null;
            // Load the current control
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Tag == null)
                    return;
                string id = treeView1.SelectedNode.Tag.ToString();
                Lazy<ISettingsControl, IControlInfo> con = GUIService.GUI.GetSettingsControl(id);
                if (con != null)
                {
                    settingsControl = con.Value;
                    label_title.Text = con.Value.DisplayName;
                    label_settingsInfo.Text = con.Value.Description;
                    panel_settingsPanel.Controls.Add(con.Value);
                    settingsControl.Location = new System.Drawing.Point(10, 10);
                }
            }
        }
    }
}
