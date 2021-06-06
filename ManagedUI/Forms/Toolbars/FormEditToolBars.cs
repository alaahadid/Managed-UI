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
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ManagedUI
{
    /// <summary>
    /// Form for editing toolbars
    /// </summary>
    public partial class FormEditToolBars : Form
    {
        /// <summary>
        /// Form for editing toolbars
        /// </summary>
        /// <param name="isDocumentsFile">Indicates if the file should be loaded from documents or not</param>
        public FormEditToolBars(bool isDocumentsFile)
        {
            InitializeComponent();
            // 1 load the map file at documents if found.
            if (isDocumentsFile)
            {
                bool success = false;
                currentTBRMap = TBRMap.LoadTBRMap(Path.Combine(MUI.Documentsfolder, "toolbars.tbm"), out success);
                if (success)
                {
                    this.Text = currentTBRMap.Name + " - " + Properties.Resources.Title_ToolbarsMapEditor;
                    RefreshList();
                    textBox_file.Text = Path.Combine(MUI.Documentsfolder, "toolbars.tbm");
                }
                else
                {
                    ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToLoadTheCurrentTBRFile);
                }
            }
            else
            {
                bool success = false;
                currentTBRMap = TBRMap.LoadTBRMap(Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "toolbars.tbm"), out success);
                if (success)
                {
                    this.Text = currentTBRMap.Name + " - " + Properties.Resources.Title_ToolbarsMapEditor;
                    RefreshList();
                    textBox_file.Text = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "toolbars.tbm");
                }
                else
                {
                    ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToLoadTheCurrentTBRFile);
                }
            }
        }
        private TBRMap currentTBRMap;
        private TBRElement currentElement;
        private bool isLoading;

        /// <summary>
        /// Refresh toolbars list from loaded file
        /// </summary>
        public void RefreshList()
        {
            propertyGrid1.SelectedObject = null;
            listView1.Items.Clear();
            treeView_menusMapTree.Nodes.Clear();
            textBox_tbrName.Enabled = false;
            checkBox_visible.Enabled = false;
            checkBox_visible.Checked = false;
            textBox_MapNane.Text = currentTBRMap.Name;
            foreach (TBRElement element in currentTBRMap.ToolBars)
            {
                ListViewItem item = new ListViewItem();
                item.Text = element.Name;
                item.SubItems.Add(element.Location.ToString());
                item.SubItems.Add(element.Visible ? Properties.Resources.Word_Yes : Properties.Resources.Word_No);
                item.Tag = element;
                listView1.Items.Add(item);
            }
        }
        private void ReloadFile()
        {
            bool success = false;
            TBRMap map = TBRMap.LoadTBRMap(textBox_file.Text, out success);
            if (success)
            {
                currentTBRMap = map;
                this.Text = currentTBRMap.Name + " - " + Properties.Resources.Title_ToolbarsMapEditor;
                RefreshList();
            }
            else
            {
                listView1.Items.Clear();
                treeView_menusMapTree.Nodes.Clear();
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToLoadTheTBRMapFileAt + " " + textBox_file.Text);
            }
        }
        private void ApplyAndSave()
        {
            ApplyTreeToTBR();// Apply changes
            currentTBRMap.Name = textBox_MapNane.Text;

            currentTBRMap.ToolBars.Clear();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                TBRElement el = (TBRElement)listView1.Items[i].Tag;
                currentTBRMap.ToolBars.Add((TBRElement)listView1.Items[i].Tag);
            }

            // Save
            if (!TBRMap.SaveTBRMap(textBox_file.Text, currentTBRMap))
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToSaveTheMapFile);
            }
            else
            {
                if (Path.Combine(MUI.Documentsfolder, "toolbars.tbm") == textBox_file.Text)
                {
                    // Apply the map to the current !!
                    GUIService.GUI.CurrentToolbarsMap = currentTBRMap;
                }
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_ToolbarsMapSavedSuccessfully);
            }
        }
        private void RefreshTree()
        {
            isLoading = true;
            propertyGrid1.SelectedObject = null;
            treeView_menusMapTree.Nodes.Clear();
            textBox_tbrName.Text = "";
            if (listView1.SelectedItems.Count == 0)
            {
                currentElement = null;
                textBox_tbrName.Enabled = false;
                checkBox_visible.Enabled = false;
                checkBox_visible.Checked = false;
                isLoading = false; return;
            }
            currentElement = (TBRElement)listView1.SelectedItems[0].Tag;
            switch (currentElement.Location)
            {
                case TBRLocation.BOTTOM:
                    comboBox1.SelectedIndex = 3; break;
                case TBRLocation.LEFT:
                    comboBox1.SelectedIndex = 1; break;
                case TBRLocation.RIGHT:
                    comboBox1.SelectedIndex = 2; break;
                case TBRLocation.TOP:
                    comboBox1.SelectedIndex = 0; break;
            }
            textBox_tbrName.Enabled = true;
            textBox_tbrName.Text = currentElement.Name;
            checkBox_visible.Enabled = true;
            comboBox_imageSize.Enabled = checkBox_custom.Checked = currentElement.CustomStyle;
            switch (currentElement.ImageSize.Height)
            {
                case 16: comboBox_imageSize.SelectedIndex = 0; break;
                case 24: comboBox_imageSize.SelectedIndex = 1; break;
                case 32: comboBox_imageSize.SelectedIndex = 2; break;
                case 64: comboBox_imageSize.SelectedIndex = 3; break;
                case 128: comboBox_imageSize.SelectedIndex = 4; break;
            }
            checkBox_visible.Checked = currentElement.Visible;
            foreach (MenuItemsMapElement element in currentElement.RootItems)
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
            isLoading = false;
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
        private void ApplyTreeToTBR()
        {
            if (currentElement == null)
                return;
            currentElement.RootItems.Clear();
            for (int i = 0; i < treeView_menusMapTree.Nodes.Count; i++)
            {
                currentElement.RootItems.Add((MenuItemsMapElement)treeView_menusMapTree.Nodes[i].Tag);
                ApplyNodeToElement((MenuItemsMapElement)treeView_menusMapTree.Nodes[i].Tag, treeView_menusMapTree.Nodes[i]);
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

        // Change
        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = Properties.Resources.Title_OpenToolbarsMapFile;
            op.Filter = Properties.Resources.FilterName_ToolbarsMapFile + " (*.tbm)|*.tbm;*.TBM;";
            op.FileName = textBox_file.Text;

            if (op.ShowDialog(this) == DialogResult.OK)
            {
                textBox_file.Text = op.FileName;
                ReloadFile();
            }
        }
        // Save
        private void button11_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBox_file.Text))
            {
                OpenFileDialog save = new OpenFileDialog();
                save.Title = Properties.Resources.Title_SaveToolbarsMapFile;
                save.Filter = Properties.Resources.FilterName_ToolbarsMapFile + " (*.tbm)|*.tbm;*.TBM;";
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
        // Reload
        private void button9_Click(object sender, EventArgs e)
        {
            ReloadFile();
        }
        // Close
        private void button12_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Add new toolbar
        private void button8_Click(object sender, EventArgs e)
        {
            TBRElement element = new TBRElement();

            string name = Properties.Resources.Word_NewToolsbar;
            int i = 1;
            while (isTBRExist(name, -1))
            {
                name = Properties.Resources.Word_NewToolsbar + i;
                i++;
            }

            element.Name = name;
            element.Location = TBRLocation.TOP;
            element.RootItems = new List<MenuItemsMapElement>();
            element.Visible = true;
            element.CustomStyle = false;
            element.ImageSize = new System.Drawing.Size(16, 16);

            ListViewItem item = new ListViewItem();
            item.Text = element.Name;
            item.SubItems.Add(element.Location.ToString());
            item.SubItems.Add(element.Visible ? Properties.Resources.Word_Yes : Properties.Resources.Word_No);
            item.Tag = element;
            listView1.Items.Add(item);
        }
        // Remove selected toolbar
        private void button15_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            treeView_menusMapTree.Nodes.Clear();
            currentElement = null;
            listView1.Items.Remove(listView1.SelectedItems[0]);
            // Load new tbr
            RefreshTree();
        }
        // Reset map to default
        private void button16_Click(object sender, EventArgs e)
        {
            currentTBRMap = TBRMap.DefaultTBRMap();
            RefreshList();
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
        // Move UP
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
        // Move Down
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
        // Remove menu item
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
        // Clear all (for current toolbar)
        private void button6_Click(object sender, EventArgs e)
        {
            treeView_menusMapTree.Nodes.Clear();
        }
        // When selecting a toolbar
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Save current
            ApplyTreeToTBR();
            // Load new tbr
            RefreshTree();
        }
        // When selecting an item in the tree
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
        // Add root cmi
        private void button1_Click(object sender, EventArgs e)
        {
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
                treeView_menusMapTree.Nodes.Add(node);
            }
        }
        // Add root pmi
        private void button17_Click(object sender, EventArgs e)
        {
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
                treeView_menusMapTree.Nodes.Add(node);
            }
        }
        // Add root smi
        private void button18_Click(object sender, EventArgs e)
        {
            MenuItemsMapElement element = new MenuItemsMapElement("", MIRType.SMI);
            TreeNode node = new TreeNode();
            node.Text = "---------------------";
            node.ToolTipText = "SPLITER";
            node.Tag = element;
            treeView_menusMapTree.Nodes.Add(node);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading)
                return;
            if (currentElement != null)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0: currentElement.Location = TBRLocation.BOTTOM; break;
                    case 1: currentElement.Location = TBRLocation.LEFT; break;
                    case 2: currentElement.Location = TBRLocation.RIGHT; break;
                    case 3: currentElement.Location = TBRLocation.TOP; break;
                }
                listView1.SelectedItems[0].SubItems[1].Text = currentElement.Location.ToString();
                listView1.SelectedItems[0].Tag = currentElement;
            }
        }
        private void textBox_tbrName_TextChanged(object sender, EventArgs e)
        {
            if (isLoading)
                return;
            if (currentElement != null)
            {
                string name = textBox_tbrName.Text;
                int i = 1;
                while (isTBRExist(name, listView1.SelectedItems[0].Index))
                {
                    name = textBox_tbrName.Text + i;
                    i++;
                }
                currentElement.Name = name;
                listView1.SelectedItems[0].Text = currentElement.Name;
                listView1.SelectedItems[0].Tag = currentElement;
            }
        }
        private bool isTBRExist(string name, int index)
        {
            foreach (ListViewItem el in listView1.Items)
            {
                if (name == el.Text && el.Index != index)
                {
                    return true;
                }
            }
            return false;
        }
        private void checkBox_visible_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoading)
                return;
            if (currentElement != null)
            {
                currentElement.Visible = checkBox_visible.Checked;
                if (listView1.SelectedItems.Count == 1)
                {
                    listView1.SelectedItems[0].SubItems[2].Text = currentElement.Visible ? Properties.Resources.Word_Yes : Properties.Resources.Word_No;
                    listView1.SelectedItems[0].Tag = currentElement;
                }
            }
        }
        private void checkBox_custom_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoading)
                return;
            if (currentElement != null)
            {
                comboBox_imageSize.Enabled = checkBox_custom.Checked;

                currentElement.CustomStyle = checkBox_custom.Checked;
            }
        }
        private void comboBox_imageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading)
                return;
            if (currentElement != null)
            {
                switch (comboBox_imageSize.SelectedIndex)
                {
                    case 0: currentElement.ImageSize = new System.Drawing.Size(16, 16); break;
                    case 1: currentElement.ImageSize = new System.Drawing.Size(24, 24); break;
                    case 2: currentElement.ImageSize = new System.Drawing.Size(32, 32); break;
                    case 3: currentElement.ImageSize = new System.Drawing.Size(64, 64); break;
                    case 4: currentElement.ImageSize = new System.Drawing.Size(128, 128); break;
                }
            }
        }
        // Add TMI as root
        private void button19_Click(object sender, EventArgs e)
        {
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
                treeView_menusMapTree.Nodes.Add(node);
            }
        }
        // Add TMI as child
        private void button20_Click(object sender, EventArgs e)
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
        // Add CBMI as root
        private void button21_Click(object sender, EventArgs e)
        {
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
                treeView_menusMapTree.Nodes.Add(node);
            }
        }
        // Add CBMI as child.
        private void button22_Click(object sender, EventArgs e)
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
