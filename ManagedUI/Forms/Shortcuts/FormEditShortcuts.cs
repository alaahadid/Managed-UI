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
using System.Collections.Generic;
using System.Windows.Forms;

namespace ManagedUI
{
    /// <summary>
    /// Edit shortcuts form
    /// </summary>
    public partial class FormEditShortcuts : Form
    {
        /// <summary>
        /// Edit shortcuts form
        /// </summary>
        /// <param name="isDocumentsFile">Indicates if the file should be loaded from documents or not</param>
        public FormEditShortcuts(bool isDocumentsFile)
        {
            InitializeComponent();
            button1.Enabled = button2.Enabled = button9.Enabled = false;
            // 1 load the map file at documents if found.
            bool success = false;
            if (isDocumentsFile)
            {
                currentShortcutsMap = ShortcutsMap.LoadShortcutsMap(Path.Combine(MUI.Documentsfolder, "shortcuts.sm"), out success);
                if (success)
                {
                    this.Text = currentShortcutsMap.Name + " - " + Properties.Resources.Title_ShortcutsMapEditor;
                    RefreshHotkeys();
                    textBox_file.Text = Path.Combine(MUI.Documentsfolder, "shortcuts.sm");
                }
                else
                {
                    ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToLoadCurrentShrtMapFile);
                }
            }
            else
            {
                currentShortcutsMap = ShortcutsMap.LoadShortcutsMap(Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "shortcuts.sm"), out success);
                if (success)
                {
                    this.Text = currentShortcutsMap.Name + " - " + Properties.Resources.Title_ShortcutsMapEditor;
                    RefreshHotkeys();
                    textBox_file.Text = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "shortcuts.sm");
                }
                else
                {
                    ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToLoadCurrentShrtMapFile);
                }
            }
        }
        private ShortcutsMap currentShortcutsMap;
        private void ReloadFile()
        {
            bool success = false;
            ShortcutsMap map = ShortcutsMap.LoadShortcutsMap(textBox_file.Text, out success);
            if (success)
            {
                currentShortcutsMap = map;
                this.Text = currentShortcutsMap.Name + " - " + Properties.Resources.Title_ShortcutsMapEditor;
                RefreshHotkeys();
            }
            else
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToLoadTheShortcutsMapFileAt + " " + textBox_file.Text);
            }
        }
        private void RefreshHotkeys()
        {
            textBox_mapName.Text = currentShortcutsMap.Name;
            listView1.Items.Clear();
            // Load all cmi's and see what have shortcut and what's not
            foreach (Lazy<CMI, IMenuItemRepresentatorInfo> cmi in GUIService.GUI.AvailableCMIs)
            {
                // Add it
                ListViewItem item = new ListViewItem();
                item.Text = cmi.Value.DisplayName;
                item.SubItems.Add(Properties.Resources.Word_MenuItem);
                string shrt = currentShortcutsMap.GetShortcut(cmi.Metadata.ID, ShortcutMode.CMI);
                if (shrt != null)
                    item.SubItems.Add(shrt);
                else
                    item.SubItems.Add("");
                bool added = false;
                // Get the shortcut attribute of this cmi
                foreach (Attribute attr in Attribute.GetCustomAttributes(cmi.Value.GetType()))
                {
                    if (attr.GetType() == typeof(ShortcutAttribute))
                    {
                        ShortcutAttribute inf = (ShortcutAttribute)attr;
                        item.SubItems.Add(inf.DefaultShortcut);
                        item.SubItems.Add(inf.ChangableShortcut ? Properties.Resources.Word_Yes : Properties.Resources.Word_No);
                        item.ForeColor = inf.ChangableShortcut ? System.Drawing.Color.Black : System.Drawing.Color.Gray;
                        added = true;
                        break;
                    }
                }
                if (!added)
                {
                    item.SubItems.Add("");
                    item.SubItems.Add(Properties.Resources.Word_Yes);
                }
                if (cmi.Value.Icon != null)
                {
                    imageList1.Images.Add(cmi.Value.Icon);
                    item.ImageIndex = imageList1.Images.Count - 1;
                }
                else
                    item.ImageIndex = -1;

                item.Tag = "CMI" + cmi.Metadata.ID;
                listView1.Items.Add(item);
            }
            // Load all CC's
            foreach (Lazy<ICommandCombination, ICommandCombinationInfo> cc in
                CommandsManager.CMD.AvaialableCommandCombinations)
            {
                // Add it
                ListViewItem item = new ListViewItem();
                item.Text = cc.Value.Name;
                item.SubItems.Add(Properties.Resources.Word_Command);
                string shrt = currentShortcutsMap.GetShortcut(cc.Metadata.ID, ShortcutMode.CC);
                if (shrt != null)
                    item.SubItems.Add(shrt);
                else
                    item.SubItems.Add("");
                bool added = false;
                // Get the shortcut attribute of this cmi
                foreach (Attribute attr in Attribute.GetCustomAttributes(cc.Value.GetType()))
                {
                    if (attr.GetType() == typeof(ShortcutAttribute))
                    {
                        ShortcutAttribute inf = (ShortcutAttribute)attr;
                        item.SubItems.Add(inf.DefaultShortcut);
                        item.SubItems.Add(inf.ChangableShortcut ? Properties.Resources.Word_Yes : Properties.Resources.Word_No);
                        item.ForeColor = inf.ChangableShortcut ? System.Drawing.Color.Black : System.Drawing.Color.Gray;
                        added = true;
                        break;
                    }
                }
                if (!added)
                {
                    item.SubItems.Add("");
                    item.SubItems.Add(Properties.Resources.Word_Yes);
                }
                item.Tag = "CCC" + cc.Metadata.ID;
                listView1.Items.Add(item);
            }
        }
        private string GetShortcutFromList(string id, string mode)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Tag.ToString().Substring(0, 3) == mode &&
                    item.Tag.ToString().Substring(3, item.Tag.ToString().Length - 3) == id)
                {
                    return item.SubItems[2].Text;
                }
            }
            return "";
        }
        private void ApplyAndSave()
        {
            currentShortcutsMap = new ShortcutsMap();
            currentShortcutsMap.Name = textBox_mapName.Text;
            currentShortcutsMap.Shortcuts = new List<ShortCut>();
            // Get the default shortcuts of CMI's
            foreach (Lazy<CMI, IMenuItemRepresentatorInfo> cmi in GUIService.GUI.AvailableCMIs)
            {
                // Get the shortcut from the list
                string shrts = GetShortcutFromList(cmi.Metadata.ID, "CMI");
                if (shrts != "")
                {
                    // Create a new shortcut for this cmi
                    ShortCut shrt = new ShortCut();
                    shrt.Changable = true;
                    shrt.ID = cmi.Metadata.ID + "[shortcut]";
                    shrt.Mode = ShortcutMode.CMI;
                    shrt.TargetID = cmi.Metadata.ID;
                    shrt.TheShortcut = shrts;

                    currentShortcutsMap.Shortcuts.Add(shrt);
                }
            }
            // Get the default shortcuts of CC's
            foreach (Lazy<ICommandCombination, ICommandCombinationInfo> cc in CommandsManager.CMD.AvaialableCommandCombinations)
            {
                // Get the shortcut attribute of this cc
                string shrts = GetShortcutFromList(cc.Metadata.ID, "CCC");
                if (shrts != "")
                {
                    // Create a new shortcut for this cmi
                    ShortCut shrt = new ShortCut();
                    shrt.Changable = true;
                    shrt.ID = cc.Metadata.ID + "[shortcut]";
                    shrt.Mode = ShortcutMode.CMI;
                    shrt.TargetID = cc.Metadata.ID;
                    shrt.TheShortcut = shrts;

                    currentShortcutsMap.Shortcuts.Add(shrt);
                }
            }
            // Save
            if (!ShortcutsMap.SaveShortcutsMap(textBox_file.Text, currentShortcutsMap))
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToSaveTheMapFile);
            }
            else
            {
                if (Path.Combine(MUI.Documentsfolder, "shortcuts.sm") == textBox_file.Text)
                {
                    // Apply the map to the current !!
                    GUIService.GUI.CurrentShortcutsMap = currentShortcutsMap;
                }
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_ShortcutsMapSavedSuccessfully);
            }
        }
        // Change key
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;
            FormChangeKey frm = new FormChangeKey(listView1.SelectedItems[0].Text);
            frm.Location = new System.Drawing.Point(this.Location.X + button1.Location.X, this.Location.Y + button1.Location.Y);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Console.WriteLine(frm.InputName);

                // Check if this one is already taken ...
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.SubItems[2].Text == frm.InputName)
                    {
                        ManagedMessageBox.ShowMessage(Properties.Resources.Message_EnteredHotkeyIsAlreadTaken);
                        item.EnsureVisible();
                        return;
                    }
                }
                listView1.SelectedItems[0].SubItems[2].Text = frm.InputName;
            }
        }
        // Clear key
        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;
            listView1.SelectedItems[0].SubItems[2].Text = "";
        }
        // Clear all
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
                item.SubItems[2].Text = "";
        }
        // Reset all
        private void button5_Click(object sender, EventArgs e)
        {
            currentShortcutsMap = ShortcutsMap.DefaultShortcutsMap();
            RefreshHotkeys();
        }
        // Reload
        private void button6_Click(object sender, EventArgs e)
        {
            ReloadFile();
        }
        // Browse
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = Properties.Resources.Title_OpenShortcutsMapFile;
            op.Filter = Properties.Resources.FilterName_ShortcutsMapFile + " (*.sm)|*.sm;*.SM;";
            op.FileName = textBox_file.Text;

            if (op.ShowDialog(this) == DialogResult.OK)
            {
                textBox_file.Text = op.FileName;
                ReloadFile();
            }
        }
        // Close
        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Save changes
        private void button8_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBox_file.Text))
            {
                OpenFileDialog save = new OpenFileDialog();
                save.Title = Properties.Resources.Title_SaveShortcutsMapFile;
                save.Filter = Properties.Resources.FilterName_ShortcutsMapFile + " (*.sm)|*.sm;*.SM;";
                save.FileName = textBox_file.Text;
                if (save.ShowDialog(this) == DialogResult.OK)
                {
                    textBox_file.Text = save.FileName;
                    ApplyAndSave();
                }
            }
            else
            {
                ApplyAndSave();
            }
        }
        // When selecting a short cut
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = button2.Enabled = button9.Enabled = listView1.SelectedItems.Count == 1;
            if (listView1.SelectedItems.Count != 1)
                return;
            if (listView1.SelectedItems[0].SubItems.Count > 4)
                button1.Enabled = listView1.SelectedItems[0].SubItems[4].Text == Properties.Resources.Word_Yes;
        }
        // Reset current
        private void button9_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;
            bool found = false;
            if (listView1.SelectedItems[0].Tag.ToString().StartsWith("CMI"))
            {
                string cmiid = listView1.SelectedItems[0].Tag.ToString().Replace("CMI", "");
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> mir =
                    GUIService.GUI.GetMenuItem(cmiid);
                if (mir != null)
                {
                    if (mir.Value is CMI)
                    {
                        CMI cmi = (CMI)mir.Value;
                        // Get the shortcut attribute of this cmi
                        foreach (Attribute attr in Attribute.GetCustomAttributes(cmi.GetType()))
                        {
                            if (attr.GetType() == typeof(ShortcutAttribute))
                            {
                                ShortcutAttribute inf = (ShortcutAttribute)attr;

                                listView1.SelectedItems[0].SubItems[2].Text = inf.DefaultShortcut;
                                found = true;
                                break;
                            }
                        }
                    }
                }
            }
            else if (listView1.SelectedItems[0].Tag.ToString().StartsWith("CC"))
            {
                string ccid = listView1.SelectedItems[0].Tag.ToString().Replace("CC", "");
                Lazy<ICommandCombination, ICommandCombinationInfo> cc = CommandsManager.CMD.GetCommandCombination(ccid);
                if (cc != null)
                {
                    // Get the shortcut attribute of this cmi
                    foreach (Attribute attr in Attribute.GetCustomAttributes(cc.Value.GetType()))
                    {
                        if (attr.GetType() == typeof(ShortcutAttribute))
                        {
                            ShortcutAttribute inf = (ShortcutAttribute)attr;

                            listView1.SelectedItems[0].SubItems[2].Text = inf.DefaultShortcut;
                            found = true;
                            break;
                        }
                    }
                }
            }
            if (!found)
                listView1.SelectedItems[0].SubItems[2].Text = "";
        }
        // Change key on double click
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (button1.Enabled)
                button1_Click(this, new EventArgs());
        }
    }
}
