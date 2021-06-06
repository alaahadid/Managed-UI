// This file is part of AHD Subtitles Maker Professional
// A program can create and edit subtitle.
// 
// Copyright © Alaa Ibrahim Hadid 2009 - 2019
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
// 
// Author email: mailto:alaahadidfreeware@gmail.com
//
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace ManagedUI
{
    [Export(typeof(ISettingsControl))]
    [ControlInfo("Hotkeys", "sc.hotkeys")]
    [SettingsControlResourceInfo("SC_Name_Hotkeys", "Category_Enviroment", "keyboard", "SC_Description_Hotkeys")]
    class SCHotkeys : ISettingsControl
    {
        private Button button9;
        private Button button6;
        private Button button5;
        private TextBox textBox_file;
        private Label label3;
        private TextBox textBox_mapName;
        private Label label2;
        private Button button3;
        private Button button2;
        private Label label1;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;
        private Label label4;
        private Button button1;
        private ShortcutsMap currentShortcutsMap;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SCHotkeys));
            this.button9 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox_file = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_mapName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button9
            // 
            resources.ApplyResources(this.button9, "button9");
            this.button9.Name = "button9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button6
            // 
            resources.ApplyResources(this.button6, "button6");
            this.button6.Name = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox_file
            // 
            resources.ApplyResources(this.textBox_file, "textBox_file");
            this.textBox_file.Name = "textBox_file";
            this.textBox_file.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBox_mapName
            // 
            resources.ApplyResources(this.textBox_mapName, "textBox_mapName");
            this.textBox_mapName.Name = "textBox_mapName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // listView1
            // 
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageList1, "imageList1");
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // SCHotkeys
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.textBox_file);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_mapName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Name = "SCHotkeys";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public override void LoadSettings()
        {
            base.LoadSettings();
            button1.Enabled = button2.Enabled = button9.Enabled = false;
            // load the map file at documents if found.
            bool success = false;
            textBox_file.Text = Path.Combine(MUI.Documentsfolder, "shortcuts.sm");
            currentShortcutsMap = ShortcutsMap.LoadShortcutsMap(Path.Combine(MUI.Documentsfolder, "shortcuts.sm"), out success);
            if (success)
            {
                RefreshHotkeys();
            }
        }
        public override void SaveSettings()
        {
            base.SaveSettings();

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
                //  ManagedMessageBox.ShowMessage(Properties.Resources.Message_ShortcutsMapSavedSuccessfully);
            }

        }
        public override bool CanSave
        {
            get
            {
                if (!File.Exists(textBox_file.Text))
                    ManagedMessageBox.ShowErrorMessage(Properties.Resources.Message_PleaseSpecifyTheShortcutsFile);
                return File.Exists(textBox_file.Text);
            }
        }
        public override void DefaultSettings()
        {
            base.DefaultSettings();
            currentShortcutsMap = ShortcutsMap.DefaultShortcutsMap();
            RefreshHotkeys();
        }
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
            foreach (Lazy<ICommandCombination, ICommandCombinationInfo> cc in CommandsManager.CMD.AvaialableCommandCombinations)
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

        // Change key
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;

            FormChangeKey frm = new FormChangeKey(listView1.SelectedItems[0].Text);
            frm.Location = new System.Drawing.Point(this.Parent.Parent.Parent.Parent.Location.X + button1.Location.X, this.Parent.Parent.Parent.Parent.Location.Y + button1.Location.Y);

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
            DefaultSettings();
        }
        // Reload
        private void button6_Click(object sender, EventArgs e)
        {
            ReloadFile();
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
