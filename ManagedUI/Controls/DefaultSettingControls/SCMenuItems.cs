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
    [ControlInfo("Menu Items", "sc.menuitems")]
    [SettingsControlResourceInfo("SC_Name_MenuItems", "Category_Enviroment", "application_side_tree", "SC_Description_MenuItems")]
    class SCMenuItems : ISettingsControl
    {
        private Button button16;
        private Button button15;
        private Button button14;
        private Button button13;
        private PropertyGrid propertyGrid1;
        private Label label4;
        private TextBox textBox_MapNane;
        private Label label3;
        private TextBox textBox_file;
        private Label label2;
        private Button button8;
        private Button button7;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private TreeView treeView_menusMapTree;
        private Label label1;
        private Label label5;
        private MenuItemsMap menuItemsMap;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SCMenuItems));
            this.button16 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_MapNane = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_file = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.treeView_menusMapTree = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button16
            // 
            resources.ApplyResources(this.button16, "button16");
            this.button16.Name = "button16";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button15
            // 
            resources.ApplyResources(this.button15, "button15");
            this.button15.Name = "button15";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button14
            // 
            resources.ApplyResources(this.button14, "button14");
            this.button14.Name = "button14";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button13
            // 
            resources.ApplyResources(this.button13, "button13");
            this.button13.Name = "button13";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // propertyGrid1
            // 
            resources.ApplyResources(this.propertyGrid1, "propertyGrid1");
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBox_MapNane
            // 
            resources.ApplyResources(this.textBox_MapNane, "textBox_MapNane");
            this.textBox_MapNane.Name = "textBox_MapNane";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBox_file
            // 
            resources.ApplyResources(this.textBox_file, "textBox_file");
            this.textBox_file.Name = "textBox_file";
            this.textBox_file.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button8
            // 
            resources.ApplyResources(this.button8, "button8");
            this.button8.Name = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button9_Click);
            // 
            // button7
            // 
            resources.ApplyResources(this.button7, "button7");
            this.button7.Name = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
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
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
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
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeView_menusMapTree
            // 
            resources.ApplyResources(this.treeView_menusMapTree, "treeView_menusMapTree");
            this.treeView_menusMapTree.FullRowSelect = true;
            this.treeView_menusMapTree.HideSelection = false;
            this.treeView_menusMapTree.Name = "treeView_menusMapTree";
            this.treeView_menusMapTree.ShowNodeToolTips = true;
            this.treeView_menusMapTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_menusMapTree_AfterSelect);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // SCMenuItems
            // 
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_MapNane);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_file);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView_menusMapTree);
            this.Controls.Add(this.label1);
            this.Name = "SCMenuItems";
            resources.ApplyResources(this, "$this");
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public override void LoadSettings()
        {
            base.LoadSettings();
            // 1 load the map file at documents if found.
            bool success = false;
            menuItemsMap = MenuItemsMap.LoadMenuItemsMap(Path.Combine(MUI.Documentsfolder, "menu.mim"), out success);
            textBox_file.Text = Path.Combine(MUI.Documentsfolder, "menu.mim");
            if (success)
            {
                RefreshTree();
            }
        }
        public override void SaveSettings()
        {
            base.SaveSettings();
            if (treeView_menusMapTree.Nodes.Count == 0)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_ThereIsNoMenuItemToSave);
                return;
            }
            menuItemsMap.Name = textBox_MapNane.Text;
            menuItemsMap.RootItems.Clear();
            for (int i = 0; i < treeView_menusMapTree.Nodes.Count; i++)
            {
                menuItemsMap.RootItems.Add((MenuItemsMapElement)treeView_menusMapTree.Nodes[i].Tag);
                ApplyNodeToElement((MenuItemsMapElement)treeView_menusMapTree.Nodes[i].Tag, treeView_menusMapTree.Nodes[i]);
            }
            // Save
            if (!MenuItemsMap.SaveMenuItemsMap(textBox_file.Text, menuItemsMap))
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToSaveTheMapFile);
            }
            else
            {
                if (Path.Combine(MUI.Documentsfolder, "menu.mim") == textBox_file.Text)
                {
                    // Apply the map to the current !!
                    GUIService.GUI.CurrentMenuItemsMap = menuItemsMap;
                }
               // ManagedMessageBox.ShowMessage(Properties.Resources.Message_MenuItemsMapSavedSuccessfully);
            }
        }
        public override void DefaultSettings()
        {
            base.DefaultSettings();
            menuItemsMap = MenuItemsMap.DefaultMenuItemsMap();
            RefreshTree();
        }
        private void RefreshTree()
        {
            textBox_MapNane.Text = menuItemsMap.Name;
            treeView_menusMapTree.Nodes.Clear();
            foreach (MenuItemsMapElement element in menuItemsMap.RootItems)
            {
                TreeNode tr = new TreeNode();

                switch (element.Type)
                {
                    case MIRType.SMI:
                        {
                            tr.Text = "---------------------";
                            tr.ToolTipText = "SPLITER";
                            break;
                        }
                    default:
                        {
                            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> mir = GUIService.GUI.GetMenuItem(element.ID);
                            if (mir != null)
                            {
                                tr.Text = mir.Value.DisplayName;
                                if (mir.Value is RMI)
                                    tr.ToolTipText = ((RMI)mir.Value).ToolTip;
                                else if (mir.Value is PMI)
                                    tr.ToolTipText = ((PMI)mir.Value).ToolTip;
                                else if (mir.Value is CMI)
                                    tr.ToolTipText = ((CMI)mir.Value).ToolTip;
                                else if (mir.Value is TMI)
                                    tr.ToolTipText = ((TMI)mir.Value).ToolTip;
                                else if (mir.Value is CBMI)
                                    tr.ToolTipText = ((CBMI)mir.Value).ToolTip;
                            }
                            break;
                        }
                }
                tr.Tag = element;

                treeView_menusMapTree.Nodes.Add(tr);
                // Apply children
                ApplyTreeNodeChildren(tr, element);
            }
        }
        private void ApplyTreeNodeChildren(TreeNode node, MenuItemsMapElement element)
        {
            foreach (MenuItemsMapElement celement in element.Items)
            {
                TreeNode tr = new TreeNode();

                switch (celement.Type)
                {
                    case MIRType.SMI:
                        {
                            tr.Text = "---------------------";
                            tr.ToolTipText = "SPLITER";
                            break;
                        }
                    default:
                        {
                            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> mir = GUIService.GUI.GetMenuItem(celement.ID);
                            if (mir != null)
                            {
                                tr.Text = mir.Value.DisplayName;
                                if (mir.Value is RMI)
                                    tr.ToolTipText = ((RMI)mir.Value).ToolTip;
                                else if (mir.Value is PMI)
                                    tr.ToolTipText = ((PMI)mir.Value).ToolTip;
                                else if (mir.Value is CMI)
                                    tr.ToolTipText = ((CMI)mir.Value).ToolTip;
                                else if (mir.Value is TMI)
                                    tr.ToolTipText = ((TMI)mir.Value).ToolTip;
                                else if (mir.Value is CBMI)
                                    tr.ToolTipText = ((CBMI)mir.Value).ToolTip;
                            }
                            break;
                        }
                }
                tr.Tag = celement;

                node.Nodes.Add(tr);
                // Apply children
                ApplyTreeNodeChildren(tr, celement);
            }
        }
        private void ApplyNodeToElement(MenuItemsMapElement element, TreeNode node)
        {
            element.Items.Clear();
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                element.Items.Add((MenuItemsMapElement)node.Nodes[i].Tag);
                ApplyNodeToElement((MenuItemsMapElement)node.Nodes[i].Tag, node.Nodes[i]);
            }
        }
        private void ReloadFile()
        {
            bool success = false;
            MenuItemsMap map = MenuItemsMap.LoadMenuItemsMap(textBox_file.Text, out success);
            if (success)
            {
                menuItemsMap = map;
                this.Text = menuItemsMap.Name + " - " + Properties.Resources.Title_MIREditor;
                RefreshTree();
            }
            else
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToLoadMIRFileAt + " " + textBox_file.Text);
            }
        }
        public override bool CanSave
        {
            get
            {
                if (!File.Exists(textBox_file.Text))
                    ManagedMessageBox.ShowErrorMessage(Properties.Resources.Message_PleaseSpecifyTheMIRFile);
                return File.Exists(textBox_file.Text);
            }
        }
        // Reload
        private void button9_Click(object sender, EventArgs e)
        {
            ReloadFile();
        }
        // Clear all
        private void button6_Click(object sender, EventArgs e)
        {
            treeView_menusMapTree.Nodes.Clear();
        }
        // Remove selected
        private void button7_Click(object sender, EventArgs e)
        {
            if (treeView_menusMapTree.SelectedNode != null)
            {
                if (treeView_menusMapTree.SelectedNode.Parent == null)
                    treeView_menusMapTree.Nodes.Remove(treeView_menusMapTree.SelectedNode);
                else
                    treeView_menusMapTree.SelectedNode.Parent.Nodes.Remove(treeView_menusMapTree.SelectedNode);
            }
        }
        private void treeView_menusMapTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = null;
            if (treeView_menusMapTree.SelectedNode != null)
            {
                string id = ((MenuItemsMapElement)treeView_menusMapTree.SelectedNode.Tag).ID;
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> mir =
                    GUIService.GUI.GetMenuItem(id);
                if (mir != null)
                {
                    propertyGrid1.SelectedObject = mir.Value;
                }
            }
        }
        // Add RMI
        private void button1_Click(object sender, EventArgs e)
        {
            FormAddMIR frm = new FormAddMIR(MIRType.ROOT);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                MenuItemsMapElement element = new MenuItemsMapElement(frm.SelectedMIRID, MIRType.ROOT);
                TreeNode node = new TreeNode();
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> mir = GUIService.GUI.GetMenuItem(frm.SelectedMIRID);
                if (mir != null)
                {
                    node.Text = mir.Value.DisplayName;
                    if (mir.Value is RMI)
                        node.ToolTipText = ((RMI)mir.Value).ToolTip;
                }
                node.Tag = element;
                treeView_menusMapTree.Nodes.Add(node);
            }
        }
        // Add PMI
        private void button3_Click(object sender, EventArgs e)
        {
            if (treeView_menusMapTree.SelectedNode == null)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_PleaseSelectAnItemFirst);
                return;
            }
            if (((MenuItemsMapElement)treeView_menusMapTree.SelectedNode.Tag).Type == MIRType.SMI)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_SplitterCannotHaveSubitems);
                return;
            }
            FormAddMIR frm = new FormAddMIR(MIRType.PMI);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                MenuItemsMapElement element = new MenuItemsMapElement(frm.SelectedMIRID, MIRType.PMI);
                TreeNode node = new TreeNode();
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> mir = GUIService.GUI.GetMenuItem(frm.SelectedMIRID);
                if (mir != null)
                {
                    node.Text = mir.Value.DisplayName;
                    if (mir.Value is PMI)
                        node.ToolTipText = ((PMI)mir.Value).ToolTip;
                }
                node.Tag = element;
                treeView_menusMapTree.SelectedNode.Nodes.Add(node);
                treeView_menusMapTree.SelectedNode.Expand();
            }
        }
        // Add CMI
        private void button2_Click(object sender, EventArgs e)
        {
            if (treeView_menusMapTree.SelectedNode == null)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_PleaseSelectAnItemFirst);
                return;
            }
            if (((MenuItemsMapElement)treeView_menusMapTree.SelectedNode.Tag).Type == MIRType.SMI)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_SplitterCannotHaveSubitems);
                return;
            }
            FormAddMIR frm = new FormAddMIR(MIRType.CMI);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                MenuItemsMapElement element = new MenuItemsMapElement(frm.SelectedMIRID, MIRType.CMI);
                TreeNode node = new TreeNode();
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> mir = GUIService.GUI.GetMenuItem(frm.SelectedMIRID);
                if (mir != null)
                {
                    node.Text = mir.Value.DisplayName;
                    if (mir.Value is CMI)
                        node.ToolTipText = ((CMI)mir.Value).ToolTip;
                }
                node.Tag = element;
                treeView_menusMapTree.SelectedNode.Nodes.Add(node);
                treeView_menusMapTree.SelectedNode.Expand();
            }
        }
        // Add SMI
        private void button4_Click(object sender, EventArgs e)
        {
            if (treeView_menusMapTree.SelectedNode == null)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_PleaseSelectAnItemFirst);
                return;
            }
            if (((MenuItemsMapElement)treeView_menusMapTree.SelectedNode.Tag).Type == MIRType.SMI)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_SplitterCannotHaveSubitems);
                return;
            }
            MenuItemsMapElement element = new MenuItemsMapElement("", MIRType.SMI);
            TreeNode node = new TreeNode();
            node.Text = "---------------------";
            node.ToolTipText = "SPLITER";
            node.Tag = element;
            treeView_menusMapTree.SelectedNode.Nodes.Add(node);
            treeView_menusMapTree.SelectedNode.Expand();
        }
        // Add DMI
        private void button5_Click(object sender, EventArgs e)
        {
            if (treeView_menusMapTree.SelectedNode == null)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_PleaseSelectAnItemFirst);
                return;
            }
            if (((MenuItemsMapElement)treeView_menusMapTree.SelectedNode.Tag).Type == MIRType.SMI)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_SplitterCannotHaveSubitems);
                return;
            }
            FormAddMIR frm = new FormAddMIR(MIRType.DMI);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                MenuItemsMapElement element = new MenuItemsMapElement(frm.SelectedMIRID, MIRType.DMI);
                TreeNode node = new TreeNode();
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> mir = GUIService.GUI.GetMenuItem(frm.SelectedMIRID);
                if (mir != null)
                {
                    node.Text = mir.Value.DisplayName;
                }
                node.Tag = element;
                treeView_menusMapTree.SelectedNode.Nodes.Add(node);
                treeView_menusMapTree.SelectedNode.Expand();
            }
        }
        // Move up
        private void button13_Click(object sender, EventArgs e)
        {
            if (treeView_menusMapTree.SelectedNode == null)
                return;
            // Get the selected item
            if (treeView_menusMapTree.SelectedNode.Parent == null)
            {
                int currentIndex = treeView_menusMapTree.Nodes.IndexOf(treeView_menusMapTree.SelectedNode);
                if (currentIndex == 0)
                    return;
                // Store the node in other object
                TreeNode node = treeView_menusMapTree.SelectedNode;
                // Remove it !
                treeView_menusMapTree.Nodes.Remove(treeView_menusMapTree.SelectedNode);
                // Add new one in the new index
                currentIndex--;
                treeView_menusMapTree.Nodes.Insert(currentIndex, node);

                treeView_menusMapTree.SelectedNode = treeView_menusMapTree.Nodes[currentIndex];
            }
            else
            {
                int currentIndex = treeView_menusMapTree.SelectedNode.Parent.Nodes.IndexOf(treeView_menusMapTree.SelectedNode);
                if (currentIndex == 0)
                    return;
                // Store the node in other object
                TreeNode node = treeView_menusMapTree.SelectedNode;
                // Remove it !
                treeView_menusMapTree.SelectedNode.Parent.Nodes.Remove(treeView_menusMapTree.SelectedNode);
                // Add new one in the new index
                currentIndex--;
                treeView_menusMapTree.SelectedNode.Parent.Nodes.Insert(currentIndex, node);
                treeView_menusMapTree.SelectedNode = treeView_menusMapTree.SelectedNode.Parent.Nodes[currentIndex];
            }
        }
        // Move down
        private void button14_Click(object sender, EventArgs e)
        {
            if (treeView_menusMapTree.SelectedNode == null)
                return;
            // Get the selected item
            if (treeView_menusMapTree.SelectedNode.Parent == null)
            {
                int currentIndex = treeView_menusMapTree.Nodes.IndexOf(treeView_menusMapTree.SelectedNode);
                if (currentIndex == treeView_menusMapTree.Nodes.Count - 1)
                    return;
                // Store the node in other object
                TreeNode node = treeView_menusMapTree.SelectedNode;
                // Remove it !
                treeView_menusMapTree.Nodes.Remove(treeView_menusMapTree.SelectedNode);
                // Add new one in the new index
                currentIndex++;
                treeView_menusMapTree.Nodes.Insert(currentIndex, node);
                treeView_menusMapTree.SelectedNode = treeView_menusMapTree.Nodes[currentIndex];
            }
            else
            {
                int currentIndex = treeView_menusMapTree.SelectedNode.Parent.Nodes.IndexOf(treeView_menusMapTree.SelectedNode);
                if (currentIndex == treeView_menusMapTree.SelectedNode.Parent.Nodes.Count - 1)
                    return;
                // Store the node in other object
                TreeNode node = treeView_menusMapTree.SelectedNode;
                // Remove it !
                treeView_menusMapTree.SelectedNode.Parent.Nodes.Remove(treeView_menusMapTree.SelectedNode);
                // Add new one in the new index
                currentIndex++;
                treeView_menusMapTree.SelectedNode.Parent.Nodes.Insert(currentIndex, node);
                treeView_menusMapTree.SelectedNode = treeView_menusMapTree.SelectedNode.Parent.Nodes[currentIndex];
            }
        }
        // Add TMI
        private void button15_Click(object sender, EventArgs e)
        {
            if (treeView_menusMapTree.SelectedNode == null)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_PleaseSelectAnItemFirst);
                return;
            }
            if (((MenuItemsMapElement)treeView_menusMapTree.SelectedNode.Tag).Type == MIRType.SMI)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_SplitterCannotHaveSubitems);
                return;
            }
            FormAddMIR frm = new FormAddMIR(MIRType.TMI);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                MenuItemsMapElement element = new MenuItemsMapElement(frm.SelectedMIRID, MIRType.TMI);
                TreeNode node = new TreeNode();
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> mir = GUIService.GUI.GetMenuItem(frm.SelectedMIRID);
                if (mir != null)
                {
                    node.Text = mir.Value.DisplayName;
                    if (mir.Value is TMI)
                        node.ToolTipText = ((TMI)mir.Value).ToolTip;
                }
                node.Tag = element;
                treeView_menusMapTree.SelectedNode.Nodes.Add(node);
                treeView_menusMapTree.SelectedNode.Expand();
            }
        }
        // Add CBMI
        private void button16_Click(object sender, EventArgs e)
        {
            if (treeView_menusMapTree.SelectedNode == null)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_PleaseSelectAnItemFirst);
                return;
            }
            if (((MenuItemsMapElement)treeView_menusMapTree.SelectedNode.Tag).Type == MIRType.SMI)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_SplitterCannotHaveSubitems);
                return;
            }
            FormAddMIR frm = new FormAddMIR(MIRType.CBMI);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                MenuItemsMapElement element = new MenuItemsMapElement(frm.SelectedMIRID, MIRType.CBMI);
                TreeNode node = new TreeNode();
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> mir = GUIService.GUI.GetMenuItem(frm.SelectedMIRID);
                if (mir != null)
                {
                    node.Text = mir.Value.DisplayName;
                    if (mir.Value is CBMI)
                        node.ToolTipText = ((CBMI)mir.Value).ToolTip;
                }
                node.Tag = element;
                treeView_menusMapTree.SelectedNode.Nodes.Add(node);
                treeView_menusMapTree.SelectedNode.Expand();
            }
        }
    }
}
