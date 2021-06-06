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
    /// Form allows to edit the menu items map
    /// </summary>
    public partial class FormEditMenuItemsMap : Form
    {
        /// <summary>
        /// Form allows to edit the menu items map
        /// </summary>
        /// <param name="isDocumentsFile">Indicates if the file should be loaded from documents at initialize</param>
        public FormEditMenuItemsMap(bool isDocumentsFile)
        {
            InitializeComponent();
            // 1 load the map file at documents if found.
            bool success = false;
            if (isDocumentsFile)
            {
                menuItemsMap = MenuItemsMap.LoadMenuItemsMap(Path.Combine(MUI.Documentsfolder, "menu.mim"), out success);
                if (success)
                {
                    this.Text = menuItemsMap.Name + " - " + Properties.Resources.Title_MIREditor;
                    RefreshTree();
                    textBox_file.Text = Path.Combine(MUI.Documentsfolder, "menu.mim");
                }
                else
                {
                    ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToLoadCurrentMIRFile);
                }
            }
            else
            {
                menuItemsMap = MenuItemsMap.LoadMenuItemsMap(Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "menu.mim"), out success);
                if (success)
                {
                    this.Text = menuItemsMap.Name + " - " + Properties.Resources.Title_MIREditor;
                    RefreshTree();
                    textBox_file.Text = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "menu.mim");
                }
                else
                {
                    ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToLoadCurrentMIRFile);
                }
            }
        }

        private MenuItemsMap menuItemsMap;

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
        private void ApplyTreeToMapAndSave()
        {
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
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_MenuItemsMapSavedSuccessfully);
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

        // Change
        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = Properties.Resources.Title_OpenMenuItemsMapFile;
            op.Filter = Properties.Resources.FilterName_MenuItemsMapFile + " (*.mim)|*.mim;*.MIM;";
            op.FileName = textBox_file.Text;

            if (op.ShowDialog(this) == DialogResult.OK)
            {
                textBox_file.Text = op.FileName;
                ReloadFile();
            }
        }
        // Save the menu items map to file !!
        private void button11_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBox_file.Text))
            {
                OpenFileDialog save = new OpenFileDialog();
                save.Title = Properties.Resources.Title_SaveMenuItemsMapFile;
                save.Filter = Properties.Resources.FilterName_MenuItemsMapFile + " (*.mim)|*.mim;*.MIM;";
                save.FileName = textBox_file.Text;
                if (save.ShowDialog(this) == DialogResult.OK)
                {
                    textBox_file.Text = save.FileName;
                    ApplyTreeToMapAndSave();
                }
            }
            else
            {
                ApplyTreeToMapAndSave();
            }
        }
        // Reload
        private void button9_Click(object sender, EventArgs e)
        {
            ReloadFile();
        }
        // Reset
        private void button8_Click(object sender, EventArgs e)
        {
            menuItemsMap = MenuItemsMap.DefaultMenuItemsMap();
            RefreshTree();
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
        // Close
        private void button12_Click(object sender, EventArgs e)
        {
            Close();
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
