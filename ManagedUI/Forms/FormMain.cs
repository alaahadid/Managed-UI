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
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using ManagedUI.Properties;
using System.Diagnostics;

namespace ManagedUI
{
    /// <summary>
    /// The main form of MUI
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// The main form of MUI
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            UpdateTitle();
            InitializeEvents();
            LoadSettings();

            ReloadMenuItems();
            ReloadToolbars();
            // ReloadShortcuts(true);
            LoadShortcuts();
            ReloadTabControls(true);
            ReloadTheme(true);
        }

        #region <Debug and trace>
        private delegate void WriteStatusText(string message, string category);
        /// <summary>
        /// Write a line in the status bar
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="category">The category of the message (see StatusMode)</param>
        public void WriteLine(string message, string category)
        {
            if (!this.InvokeRequired)
                WriteLineNormal(message, category);
            else
                this.Invoke(new WriteStatusText(WriteLineNormal), message, category);
        }
        private void WriteLineNormal(string message, string category)
        {
            Color color = Color.Black;

            switch (category)
            {
                case StatusMode.Error: color = Color.Red; break;
                case StatusMode.Warning: color = Color.Yellow; break;
                case StatusMode.Information: color = Color.LimeGreen; break;
                default: return;
            }

            StatusLabel.ForeColor = color;
            StatusLabel.Text = message;
            statusStrip1.Refresh();
        }
        #endregion
        #region <Initialize>
        private void InitializeEvents()
        {
            MUI.ProjectTitleChanged += MUI_ProjectTitleChanged;
            GUIService.GUI.MenuItemsMapChanged += GUI_MenuItemsMapChanged;
            GUIService.GUI.ToolbarsMapAboutToBeChanged += GUI_ToolbarsMapAboutToBeChanged;
            GUIService.GUI.ToolbarsMapChanged += GUI_ToolbarsMapChanged;
            GUIService.GUI.ShortcutsMapChanged += GUI_ShortcutsMapChanged;
            GUIService.GUI.TabsControlsMapChanged += GUI_TabsControlsMapChanged;
            GUIService.GUI.TabsControlsMapAboutToBeChanged += GUI_TabsControlsMapAboutToBeChanged;
            GUIService.GUI.ThemeAboutToBeChanged += GUI_ThemeAboutToBeChanged;
            GUIService.GUI.ThemeChanged += GUI_ThemeChanged;
            GUIService.GUI.SelectedTabControlIDChanged += GUI_SelectedTabControlIDChanged;

            GUIService.GUI.Progress += GUI_Progress;
            GUIService.GUI.ProgressBegin += GUI_ProgressBegin;
            GUIService.GUI.ProgressFinished += GUI_ProgressFinished;
        }
        private void DisposeEvents()
        {
            MUI.ProjectTitleChanged -= MUI_ProjectTitleChanged;
            GUIService.GUI.MenuItemsMapChanged -= GUI_MenuItemsMapChanged;
            GUIService.GUI.ToolbarsMapAboutToBeChanged -= GUI_ToolbarsMapAboutToBeChanged;
            GUIService.GUI.ToolbarsMapChanged -= GUI_ToolbarsMapChanged;
            GUIService.GUI.ShortcutsMapChanged -= GUI_ShortcutsMapChanged;
            GUIService.GUI.TabsControlsMapChanged -= GUI_TabsControlsMapChanged;
            GUIService.GUI.TabsControlsMapAboutToBeChanged -= GUI_TabsControlsMapAboutToBeChanged;
            GUIService.GUI.ThemeAboutToBeChanged -= GUI_ThemeAboutToBeChanged;
            GUIService.GUI.ThemeChanged -= GUI_ThemeChanged;
            GUIService.GUI.SelectedTabControlIDChanged -= GUI_SelectedTabControlIDChanged;
            GUIService.GUI.Progress -= GUI_Progress;
            GUIService.GUI.ProgressBegin -= GUI_ProgressBegin;
            GUIService.GUI.ProgressFinished -= GUI_ProgressFinished;
        }
        #endregion
        #region <Settings>
        private void LoadSettings()
        {
            this.Size = Properties.Settings.Default.MainWindowSize;
            this.Location = Properties.Settings.Default.MainWindowLocation;
            if (Properties.Settings.Default.MainWindowMaximized)
                this.WindowState = FormWindowState.Maximized;
        }
        private void SaveSettings()
        {
            Properties.Settings.Default.MainWindowMaximized = WindowState == FormWindowState.Maximized;
            this.WindowState = FormWindowState.Normal;
            Properties.Settings.Default.MainWindowSize = this.Size;
            Properties.Settings.Default.MainWindowLocation = this.Location;
        }

        #endregion
        #region <Menu items and toolbars>
        private Dictionary<ToolStripItem, string> MIRControl;
        private void ReloadMenuItems()
        {
            Trace.WriteLine(Properties.Resources.Status_ApplyingMenuItemsMapToTheMainWindow + " ...", StatusMode.Normal);
            menuStrip1.Items.Clear();
            // Load all roots first
            if (GUIService.GUI.CurrentMenuItemsMap == null)
            {
                Trace.TraceError(Properties.Resources.Status_NoMenuItemsMapLoadedtheGUICore);
                return;
            }
            for (int i = 0; i < GUIService.GUI.CurrentMenuItemsMap.RootItems.Count; i++)
            {
                // 1 Get the menu item object from the gui core.
                string id = GUIService.GUI.CurrentMenuItemsMap.RootItems[i].ID;
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                if (item != null)
                {
                    if (item.Value is RMI)
                    {
                        // Add it now !!
                        ToolStripMenuItem rootItem = new ToolStripMenuItem();
                        rootItem.Text = item.Value.DisplayName;
                        rootItem.Tag = id;
                        menuStrip1.Items.Add(rootItem);
                        rootItem.DropDownOpening += RootItem_DropDownOpening;
                        ApplyMIRElement(GUIService.GUI.CurrentMenuItemsMap.RootItems[i], rootItem);
                    }
                }
            }
            Trace.WriteLine(Properties.Resources.Status_MIRMapApplyed, StatusMode.Information);
        }
        private void ReloadToolbars()
        {
            Trace.WriteLine(Properties.Resources.Status_ApplyingToolbarsMapToTheMainWindow + " ...", StatusMode.Normal);
            toolStripContainer1.TopToolStripPanel.Controls.Clear();
            toolStripContainer1.LeftToolStripPanel.Controls.Clear();
            toolStripContainer1.RightToolStripPanel.Controls.Clear();
            toolStripContainer1.BottomToolStripPanel.Controls.Clear();
            // Load the toolbars
            if (GUIService.GUI.CurrentToolbarsMap == null)
            {
                Trace.TraceError(Properties.Resources.Status_NoToolbarsMapLoadedInTheGUICore);
                return;
            }
            // We need to add the toolbars in rebersed priority so that the first toolbar
            // appears first in the list.
            for (int t = GUIService.GUI.CurrentToolbarsMap.ToolBars.Count - 1; t >= 0; t--)
            {
                TBRElement tbr = GUIService.GUI.CurrentToolbarsMap.ToolBars[t];
                ToolStrip toolbar = new ToolStrip();
                toolbar.Visible = tbr.Visible;
                toolbar.Text = tbr.Name;
                toolbar.ParentChanged += Toolbar_ParentChanged;
                tbr.VisibleChanged += Toolbar_VisibleChanged;
                if (tbr.CustomStyle)
                    if (tbr.ImageSize.Height > 0 && tbr.ImageSize.Width > 0)
                        toolbar.ImageScalingSize = tbr.ImageSize;

                // Add the buttons
                for (int i = 0; i < tbr.RootItems.Count; i++)
                {
                    switch (tbr.RootItems[i].Type)
                    {
                        case MIRType.ROOT:
                        case MIRType.DMI:
                            {
                                Trace.TraceError(Properties.Resources.Status_TBRError1);
                                break;
                            }
                        case MIRType.CMI:
                            {
                                string id = tbr.RootItems[i].ID;
                                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                                if (item != null)
                                {
                                    if (item.Value is CMI)
                                    {
                                        CMI cmi = (CMI)item.Value;
                                        if (tbr.RootItems[i].Items.Count == 0)
                                        {
                                            ToolStripButton button = new ToolStripButton();
                                            button.DisplayStyle = ToolStripItemDisplayStyle.Image;
                                            button.Text = cmi.DisplayName;
                                            button.ToolTipText = cmi.ToolTip;

                                            Image icon = cmi.Icon;
                                            if (icon != null)
                                                button.Image = icon;
                                            // Activation status
                                            if (cmi.UseActiveStatus)
                                            {
                                                button.Enabled = cmi.ActiveStatus;
                                                cmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;
                                            }
                                            if (cmi.UseCheckStatus)
                                            {
                                                button.Checked = cmi.CheckStatus;
                                                cmi.CheckStatusChanged += Cmi_CheckStatusChanged;
                                            }

                                            // On execute !
                                            button.Click += Button_ClickCMIExecute;

                                            // Add it !
                                            button.Tag = id;
                                            if (cmi.UseActiveStatus || cmi.UseCheckStatus)
                                                AddMIRControl(button, id);
                                            toolbar.Items.Add(button);
                                        }
                                        else
                                        {
                                            ToolStripSplitButton button = new ToolStripSplitButton();
                                            button.DisplayStyle = ToolStripItemDisplayStyle.Image;
                                            button.Text = cmi.DisplayName;
                                            button.ToolTipText = cmi.ToolTip;

                                            Image icon = cmi.Icon;
                                            if (icon != null)
                                                button.Image = icon;
                                            // Activation status
                                            if (cmi.UseActiveStatus)
                                            {
                                                button.Enabled = cmi.ActiveStatus;
                                                cmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;
                                            }
                                            // On execute !
                                            button.ButtonClick += Button_ClickCMIExecute;
                                            button.DropDownOpening += Button_DropDownOpening;
                                            // Add it !
                                            button.Tag = id;
                                            toolbar.Items.Add(button);
                                            if (cmi.UseActiveStatus || cmi.UseCheckStatus)
                                                AddMIRControl(button, id);
                                            ApplySplitButtonChildren(tbr.RootItems[i], button);
                                        }
                                    }
                                }
                                break;
                            }
                        case MIRType.TMI:
                            {
                                string id = tbr.RootItems[i].ID;
                                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                                if (item != null)
                                {
                                    if (item.Value is TMI)
                                    {
                                        TMI tmi = (TMI)item.Value;
                                        ToolStripTextBox txt = new ToolStripTextBox();
                                        txt.ToolTipText = tmi.ToolTip;
                                        txt.Tag = id;
                                        txt.TextChanged += Txt_TextChanged;
                                        txt.KeyDown += Txt_KeyDown;

                                        // Activation status
                                        if (tmi.UseActiveStatus)
                                        {
                                            txt.Enabled = tmi.ActiveStatus;
                                            tmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;

                                            AddMIRControl(txt, id);
                                        }
                                        toolbar.Items.Add(txt);
                                    }
                                }
                                break;
                            }
                        case MIRType.CBMI:
                            {
                                string id = tbr.RootItems[i].ID;
                                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                                if (item != null)
                                {
                                    if (item.Value is CBMI)
                                    {
                                        CBMI cbmi = (CBMI)item.Value;
                                        cbmi.OnView();
                                        ToolStripComboBox cb = new ToolStripComboBox();
                                        cb.AutoSize = false;
                                        cb.ToolTipText = cbmi.ToolTip;

                                        int max = cbmi.DefaultSize;
                                        foreach (string txt in cbmi.Items)
                                        {
                                            cb.Items.Add(txt);
                                            if (cbmi.SizeChangesAutomatically)
                                            {
                                                int v = TextRenderer.MeasureText(txt, cb.Font).Width;
                                                if (v > max)
                                                    max = v;
                                            }
                                        }
                                        cb.Width = max;
                                        if (cbmi.SelectedItemIndex >= 0 && cbmi.SelectedItemIndex < cb.Items.Count)
                                            cb.SelectedIndex = cbmi.SelectedItemIndex;

                                        if (cbmi.OpenTextMode)
                                            cb.DropDownStyle = ComboBoxStyle.DropDown;
                                        else
                                            cb.DropDownStyle = ComboBoxStyle.DropDownList;

                                        cbmi.ChangeIndexRequest += Cbmi_ChangeIndexRequest;
                                        cbmi.ItemsReloadRequest += Cbmi_ItemsReloadRequest;
                                        cbmi.Tag = cb;

                                        cb.SelectedIndexChanged += Cb_SelectedIndexChanged;
                                        cb.DropDown += Cb_DropDown;
                                        cb.DropDownClosed += Cb_DropDownClosed;
                                        if (cbmi.OpenTextMode)
                                        {
                                            cb.TextChanged += Cb_TextChanged;
                                            cb.KeyDown += Cb_KeyDown;
                                        }
                                        cb.Tag = id;


                                        // Activation status
                                        if (cbmi.UseActiveStatus)
                                        {
                                            cb.Enabled = cbmi.ActiveStatus;
                                            cbmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;

                                            AddMIRControl(cb, id);
                                        }
                                        cbmi.Tag = cb;
                                        toolbar.Items.Add(cb);
                                    }
                                }
                                break;
                            }
                        case MIRType.SMI:
                            {
                                ToolStripSeparator seperator = new ToolStripSeparator();
                                toolbar.Items.Add(seperator);
                                break;
                            }
                        case MIRType.PMI:
                            {
                                string id = tbr.RootItems[i].ID;
                                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                                if (item != null)
                                {
                                    if (item.Value is PMI)
                                    {
                                        PMI pmi = (PMI)item.Value;
                                        if (tbr.RootItems[i].Items.Count == 0)
                                        {
                                            ToolStripButton button = new ToolStripButton();
                                            button.DisplayStyle = ToolStripItemDisplayStyle.Image;
                                            button.Text = pmi.DisplayName;
                                            button.ToolTipText = pmi.ToolTip;

                                            Image icon = pmi.Icon;
                                            if (icon != null)
                                                button.Image = icon;
                                            // Activation status
                                            if (pmi.UseActiveStatus)
                                            {
                                                button.Enabled = pmi.ActiveStatus;
                                                pmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;
                                            }
                                            if (pmi.UseCheckStatus)
                                            {
                                                button.Checked = pmi.CheckStatus;
                                                pmi.CheckStatusChanged += Cmi_CheckStatusChanged;
                                            }
                                            if (pmi.UseActiveStatus || pmi.UseCheckStatus)
                                                AddMIRControl(button, id);
                                            // Add it !
                                            button.Tag = id;
                                            toolbar.Items.Add(button);
                                        }
                                        else
                                        {
                                            ToolStripSplitButton button = new ToolStripSplitButton();
                                            button.DisplayStyle = ToolStripItemDisplayStyle.Image;
                                            button.Text = pmi.DisplayName;
                                            button.ToolTipText = pmi.ToolTip;

                                            Image icon = pmi.Icon;
                                            if (icon != null)
                                                button.Image = icon;
                                            // Activation status
                                            if (pmi.UseActiveStatus)
                                            {
                                                button.Enabled = pmi.ActiveStatus;
                                                pmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;

                                                AddMIRControl(button, id);
                                            }
                                            button.DropDownOpening += Button_DropDownOpening;
                                            // Add it !
                                            button.Tag = id;
                                            toolbar.Items.Add(button);

                                            ApplySplitButtonChildren(tbr.RootItems[i], button);
                                        }
                                    }
                                }
                                break;
                            }
                    }
                }
                toolbar.ShowItemToolTips = true;
                switch (tbr.Location)
                {
                    case TBRLocation.TOP:
                        {
                            toolStripContainer1.TopToolStripPanel.Controls.Add(toolbar);
                            break;
                        }
                    case TBRLocation.LEFT:
                        {
                            toolStripContainer1.LeftToolStripPanel.Controls.Add(toolbar);
                            break;
                        }
                    case TBRLocation.RIGHT:
                        {
                            toolStripContainer1.RightToolStripPanel.Controls.Add(toolbar);
                            break;
                        }
                    case TBRLocation.BOTTOM:
                        {
                            toolStripContainer1.BottomToolStripPanel.Controls.Add(toolbar);
                            break;
                        }
                }
            }
            Trace.WriteLine(Properties.Resources.Status_TBRMapApplyed, StatusMode.Information);
        }
        private void ApplyMIRElement(MenuItemsMapElement element, ToolStripMenuItem rootItem)
        {
            switch (element.Type)
            {
                case MIRType.SMI:
                    {
                        Trace.TraceError(Properties.Resources.Status_MIRError1);
                        return;
                    }
            }
            // Loop through the child items of this element
            for (int i = 0; i < element.Items.Count; i++)
            {
                switch (element.Items[i].Type)
                {
                    case MIRType.ROOT:
                        {
                            // !! ROOT ITEMS CANNOT BE ADDED IN ROOT !!
                            Trace.TraceError(Properties.Resources.Status_MIRError2);
                            break;
                        }
                    case MIRType.TMI:
                        {
                            string id = element.Items[i].ID;
                            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                            if (item != null)
                            {
                                if (item.Value is TMI)
                                {
                                    TMI tmi = (TMI)item.Value;
                                    ToolStripTextBox txt = new ToolStripTextBox();
                                    txt.ToolTipText = tmi.ToolTip;
                                    txt.Tag = id;
                                    txt.TextChanged += Txt_TextChanged;
                                    txt.KeyDown += Txt_KeyDown;
                                    // Activation status
                                    if (tmi.UseActiveStatus)
                                    {
                                        txt.Enabled = tmi.ActiveStatus;
                                        tmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;
                                        AddMIRControl(txt, id);
                                    }

                                    rootItem.DropDownItems.Add(txt);
                                }
                            }
                            break;
                        }
                    case MIRType.CBMI:
                        {
                            string id = element.Items[i].ID;
                            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                            if (item != null)
                            {
                                if (item.Value is CBMI)
                                {
                                    CBMI cbmi = (CBMI)item.Value;
                                    cbmi.OnView();
                                    ToolStripComboBox cb = new ToolStripComboBox();
                                    cb.AutoSize = false;
                                    cb.ToolTipText = cbmi.ToolTip;

                                    int max = cbmi.DefaultSize;
                                    foreach (string txt in cbmi.Items)
                                    {
                                        cb.Items.Add(txt);
                                        if (cbmi.SizeChangesAutomatically)
                                        {
                                            int v = TextRenderer.MeasureText(txt, cb.Font).Width;
                                            if (v > max)
                                                max = v;
                                        }
                                    }
                                    cb.Width = max;
                                    if (cbmi.SelectedItemIndex >= 0 && cbmi.SelectedItemIndex < cb.Items.Count)
                                        cb.SelectedIndex = cbmi.SelectedItemIndex;

                                    if (cbmi.OpenTextMode)
                                        cb.DropDownStyle = ComboBoxStyle.DropDown;
                                    else
                                        cb.DropDownStyle = ComboBoxStyle.DropDownList;

                                    cbmi.ChangeIndexRequest += Cbmi_ChangeIndexRequest;
                                    cbmi.ItemsReloadRequest += Cbmi_ItemsReloadRequest;

                                    cb.SelectedIndexChanged += Cb_SelectedIndexChanged;
                                    cb.DropDown += Cb_DropDown;
                                    cb.DropDownClosed += Cb_DropDownClosed;
                                    if (cbmi.OpenTextMode)
                                    {
                                        cb.TextChanged += Cb_TextChanged;
                                        cb.KeyDown += Cb_KeyDown;
                                    }
                                    // Activation status
                                    if (cbmi.UseActiveStatus)
                                    {
                                        cb.Enabled = cbmi.ActiveStatus;
                                        cbmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;
                                        AddMIRControl(cb, id);
                                    }
                                    cbmi.Tag = cb;
                                    cb.Tag = id;
                                    rootItem.DropDownItems.Add(cb);
                                }
                            }
                            break;
                        }
                    case MIRType.PMI:
                        {
                            // Add a command menu item ! 
                            string id = element.Items[i].ID;
                            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                            if (item != null)
                            {
                                if (item.Value is PMI)
                                {
                                    // Add it now !!
                                    PMI pmi = (PMI)item.Value;
                                    ToolStripMenuItem pmiItem = new ToolStripMenuItem();

                                    pmiItem.Text = pmi.DisplayName;
                                    pmiItem.ToolTipText = pmi.ToolTip;

                                    // Activation status
                                    if (pmi.UseActiveStatus)
                                    {
                                        pmiItem.Enabled = pmi.ActiveStatus;
                                        pmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;
                                    }
                                    if (pmi.UseCheckStatus)
                                    {
                                        pmiItem.Checked = pmi.CheckStatus;
                                        pmi.CheckStatusChanged += Cmi_CheckStatusChanged;
                                    }

                                    Image icon = pmi.Icon;
                                    if (icon != null)
                                        pmiItem.Image = icon;

                                    // Add it !
                                    pmiItem.Tag = id;
                                    rootItem.DropDownItems.Add(pmiItem);
                                    if (pmi.UseActiveStatus || pmi.UseCheckStatus)
                                        AddMIRControl(pmiItem, id);
                                    pmiItem.DropDownOpening += RootItem_DropDownOpening;
                                    // Add children
                                    ApplyMIRElement(element.Items[i], pmiItem);
                                }
                                else
                                {
                                    Trace.TraceError(Properties.Resources.Status_MIRError3);
                                }
                            }
                            break;
                        }
                    case MIRType.DMI:
                        {
                            // Add a command menu item ! 
                            string id = element.Items[i].ID;
                            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                            if (item != null)
                            {
                                if (item.Value is DMI)
                                {
                                    // Add it now !!
                                    DMI dmi = (DMI)item.Value;
                                    ToolStripMenuItem dmiItem = new ToolStripMenuItem();

                                    dmiItem.Text = dmi.DisplayName;

                                    // Activation status
                                    if (dmi.UseActiveStatus)
                                    {
                                        dmiItem.Enabled = dmi.ActiveStatus;
                                        dmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;
                                    }
                                    if (dmi.UseCheckStatus)
                                    {
                                        dmiItem.Checked = dmi.CheckStatus;
                                        dmi.CheckStatusChanged += Cmi_CheckStatusChanged;
                                    }
                                    if (dmi.UseActiveStatus || dmi.UseCheckStatus)
                                        AddMIRControl(dmiItem, id);
                                    // Add it !
                                    dmiItem.Tag = id;
                                    rootItem.DropDownItems.Add(dmiItem);
                                    // We can't add children of this item, the children
                                    // must be updated during run time.
                                    // ApplyMIRElement(element.Items[i], dmiItem);
                                }
                                else
                                {
                                    Trace.TraceError(Properties.Resources.Status_MIRError3);
                                }
                            }
                            break;
                        }
                    case MIRType.SMI:
                        {
                            // Add a splitter !
                            ToolStripSeparator spr = new ToolStripSeparator();
                            rootItem.DropDownItems.Add(spr);
                            // !! SMI CANNOT HAVE CHILDREN !!
                            break;
                        }
                    case MIRType.CMI:
                        {
                            // Add a command menu item ! 
                            string id = element.Items[i].ID;
                            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                            if (item != null)
                            {
                                if (item.Value is CMI)
                                {
                                    // Add it now !!
                                    CMI cmi = (CMI)item.Value;
                                    ToolStripMenuItem cmiItem = new ToolStripMenuItem();

                                    cmiItem.Text = cmi.DisplayName;
                                    cmiItem.ToolTipText = cmi.ToolTip;

                                    Image icon = cmi.Icon;
                                    if (icon != null)
                                        cmiItem.Image = icon;

                                    cmiItem.ShortcutKeyDisplayString =
                                        GUIService.GUI.CurrentShortcutsMap.GetShortcut(cmi.ID, ShortcutMode.CMI);

                                    // Activation status
                                    if (cmi.UseActiveStatus)
                                    {
                                        cmiItem.Enabled = cmi.ActiveStatus;
                                        cmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;
                                    }
                                    if (cmi.UseCheckStatus)
                                    {
                                        cmiItem.Checked = cmi.CheckStatus;
                                        cmi.CheckStatusChanged += Cmi_CheckStatusChanged;
                                    }

                                    // On execute !
                                    cmiItem.Click += CmiItem_Click;
                                    if (cmi.UseActiveStatus || cmi.UseCheckStatus)
                                        AddMIRControl(cmiItem, id);
                                    // Add it !
                                    cmiItem.Tag = id;
                                    rootItem.DropDownItems.Add(cmiItem);
                                    cmiItem.DropDownOpening += RootItem_DropDownOpening;
                                    // Add children
                                    ApplyMIRElement(element.Items[i], cmiItem);
                                }
                                else
                                {
                                    Trace.TraceError(Properties.Resources.Status_MIRError3);
                                }
                            }
                            break;
                        }
                }

            }
        }
        private void ApplySplitButtonChildren(MenuItemsMapElement element, ToolStripSplitButton button)
        {
            switch (element.Type)
            {
                case MIRType.SMI:
                    {
                        Trace.TraceError(Properties.Resources.Status_MIRError1);
                        return;
                    }
            }
            // Loop through the child items of this element
            for (int i = 0; i < element.Items.Count; i++)
            {
                switch (element.Items[i].Type)
                {
                    case MIRType.ROOT:
                        {
                            // !! ROOT ITEMS CANNOT BE ADDED IN ROOT !!
                            Trace.TraceError(Properties.Resources.Status_MIRError2);
                            break;
                        }
                    case MIRType.TMI:
                        {
                            string id = element.Items[i].ID;
                            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                            if (item != null)
                            {
                                if (item.Value is TMI)
                                {
                                    TMI tmi = (TMI)item.Value;
                                    ToolStripTextBox txt = new ToolStripTextBox();
                                    txt.ToolTipText = tmi.ToolTip;
                                    txt.Tag = id;
                                    txt.TextChanged += Txt_TextChanged;
                                    txt.KeyDown += Txt_KeyDown;
                                    // Activation status
                                    if (tmi.UseActiveStatus)
                                    {
                                        txt.Enabled = tmi.ActiveStatus;
                                        tmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;

                                        AddMIRControl(txt, id);
                                    }

                                    button.DropDownItems.Add(txt);
                                }
                            }
                            break;
                        }
                    case MIRType.CBMI:
                        {
                            string id = element.Items[i].ID;
                            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                            if (item != null)
                            {
                                if (item.Value is CBMI)
                                {
                                    CBMI cbmi = (CBMI)item.Value;
                                    cbmi.OnView();
                                    ToolStripComboBox cb = new ToolStripComboBox();
                                    cb.AutoSize = false;
                                    cb.ToolTipText = cbmi.ToolTip;

                                    int max = cbmi.DefaultSize;
                                    foreach (string txt in cbmi.Items)
                                    {
                                        cb.Items.Add(txt);
                                        if (cbmi.SizeChangesAutomatically)
                                        {
                                            int v = TextRenderer.MeasureText(txt, cb.Font).Width;
                                            if (v > max)
                                                max = v;
                                        }
                                    }
                                    cb.Width = max;
                                    if (cbmi.SelectedItemIndex >= 0 && cbmi.SelectedItemIndex < cb.Items.Count)
                                        cb.SelectedIndex = cbmi.SelectedItemIndex;

                                    if (cbmi.OpenTextMode)
                                        cb.DropDownStyle = ComboBoxStyle.DropDown;
                                    else
                                        cb.DropDownStyle = ComboBoxStyle.DropDownList;

                                    cbmi.ChangeIndexRequest += Cbmi_ChangeIndexRequest;
                                    cbmi.ItemsReloadRequest += Cbmi_ItemsReloadRequest;
                                    cb.SelectedIndexChanged += Cb_SelectedIndexChanged;
                                    cb.DropDown += Cb_DropDown;
                                    cb.DropDownClosed += Cb_DropDownClosed;
                                    if (cbmi.OpenTextMode)
                                    {
                                        cb.TextChanged += Cb_TextChanged;
                                        cb.KeyDown += Cb_KeyDown;
                                    }
                                    cb.Tag = id;
                                    // Activation status
                                    if (cbmi.UseActiveStatus)
                                    {
                                        cb.Enabled = cbmi.ActiveStatus;
                                        cbmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;

                                        AddMIRControl(cb, id);
                                    }
                                    cbmi.Tag = cb;
                                    button.DropDownItems.Add(cb);
                                }
                            }
                            break;
                        }
                    case MIRType.PMI:
                        {
                            // Add a command menu item ! 
                            string id = element.Items[i].ID;
                            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                            if (item != null)
                            {
                                if (item.Value is PMI)
                                {
                                    // Add it now !!
                                    PMI pmi = (PMI)item.Value;
                                    ToolStripMenuItem pmiItem = new ToolStripMenuItem();

                                    pmiItem.Text = pmi.DisplayName;
                                    pmiItem.ToolTipText = pmi.ToolTip;

                                    Image icon = pmi.Icon;
                                    if (icon != null)
                                        pmiItem.Image = icon;

                                    // Add it !
                                    pmiItem.Tag = id;
                                    button.DropDownItems.Add(pmiItem);
                                    // Activation status
                                    if (pmi.UseActiveStatus)
                                    {
                                        pmiItem.Enabled = pmi.ActiveStatus;
                                        pmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;
                                    }
                                    if (pmi.UseCheckStatus)
                                    {
                                        pmiItem.Checked = pmi.CheckStatus;
                                        pmi.CheckStatusChanged += Cmi_CheckStatusChanged;
                                    }
                                    pmiItem.DropDownOpening += RootItem_DropDownOpening;
                                    if (pmi.UseActiveStatus || pmi.UseCheckStatus)
                                        AddMIRControl(pmiItem, id);
                                    // Add children
                                    ApplyMIRElement(element.Items[i], pmiItem);
                                }
                                else
                                {
                                    Trace.TraceError(Properties.Resources.Status_MIRError3);
                                }
                            }
                            break;
                        }
                    case MIRType.DMI:
                        {
                            // Add a command menu item ! 
                            string id = element.Items[i].ID;
                            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                            if (item != null)
                            {
                                if (item.Value is DMI)
                                {
                                    // Add it now !!
                                    DMI dmi = (DMI)item.Value;
                                    ToolStripMenuItem dmiItem = new ToolStripMenuItem();

                                    dmiItem.Text = dmi.DisplayName;

                                    // Activation status
                                    if (dmi.UseActiveStatus)
                                    {
                                        dmiItem.Enabled = dmi.ActiveStatus;
                                        dmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;
                                    }
                                    if (dmi.UseCheckStatus)
                                    {
                                        dmiItem.Checked = dmi.CheckStatus;
                                        dmi.CheckStatusChanged += Cmi_CheckStatusChanged;
                                    }
                                    // Add it !
                                    dmiItem.Tag = id;
                                    button.DropDownItems.Add(dmiItem);
                                    if (dmi.UseActiveStatus || dmi.UseCheckStatus)
                                        AddMIRControl(dmiItem, id);
                                    // We can't add children of this item, the children
                                    // must be updated during run time.
                                    // ApplyMIRElement(element.Items[i], dmiItem);
                                }
                                else
                                {
                                    Trace.TraceError(Properties.Resources.Status_MIRError3);
                                }
                            }
                            break;
                        }
                    case MIRType.SMI:
                        {
                            // Add a splitter !
                            ToolStripSeparator spr = new ToolStripSeparator();
                            button.DropDownItems.Add(spr);
                            // !! SMI CANNOT HAVE CHILDREN !!
                            break;
                        }
                    case MIRType.CMI:
                        {
                            // Add a command menu item ! 
                            string id = element.Items[i].ID;
                            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                            if (item != null)
                            {
                                if (item.Value is CMI)
                                {
                                    // Add it now !!
                                    CMI cmi = (CMI)item.Value;
                                    ToolStripMenuItem cmiItem = new ToolStripMenuItem();

                                    cmiItem.Text = cmi.DisplayName;
                                    cmiItem.ToolTipText = cmi.ToolTip;

                                    Image icon = cmi.Icon;
                                    if (icon != null)
                                        cmiItem.Image = icon;

                                    cmiItem.ShortcutKeyDisplayString =
                                           GUIService.GUI.CurrentShortcutsMap.GetShortcut(cmi.ID, ShortcutMode.CMI);


                                    // Activation status
                                    if (cmi.UseActiveStatus)
                                    {
                                        cmiItem.Enabled = cmi.ActiveStatus;
                                        cmi.ActiveStatusChanged += Cmi_ActiveStatusChanged;
                                    }
                                    if (cmi.UseCheckStatus)
                                    {
                                        cmiItem.Checked = cmi.CheckStatus;
                                        cmi.CheckStatusChanged += Cmi_CheckStatusChanged;
                                    }
                                    // On execute !
                                    cmiItem.Click += CmiItem_Click;
                                    if (cmi.UseActiveStatus || cmi.UseCheckStatus)
                                        AddMIRControl(cmiItem, id);
                                    // Add it !
                                    cmiItem.Tag = id;
                                    button.DropDownItems.Add(cmiItem);
                                    cmiItem.DropDownOpening += RootItem_DropDownOpening;
                                    // Add children
                                    ApplyMIRElement(element.Items[i], cmiItem);
                                }
                                else
                                {
                                    Trace.TraceError(Properties.Resources.Status_MIRError3);
                                }
                            }
                            break;
                        }
                }
            }
        }
        private void ApplyShortcutsToMenuItems()
        {
            for (int i = 0; i < menuStrip1.Items.Count; i++)
            {
                if (menuStrip1.Items[i] is ToolStripMenuItem)
                    CheckItemShortcut((ToolStripMenuItem)menuStrip1.Items[i]);
            }
        }
        private void CheckItemShortcut(ToolStripMenuItem item)
        {
            string id = item.Tag.ToString();
            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> mir =
                GUIService.GUI.GetMenuItem(id);
            if (mir != null)
            {
                if (mir.Value is CMI)
                {
                    item.ShortcutKeyDisplayString =
                    GUIService.GUI.CurrentShortcutsMap.GetShortcut(id, ShortcutMode.CMI);
                }
            }
            for (int i = 0; i < item.DropDownItems.Count; i++)
            {
                if (item.DropDownItems[i] is ToolStripMenuItem)
                    CheckItemShortcut((ToolStripMenuItem)item.DropDownItems[i]);
            }
        }
        private void AddMIRControl(ToolStripItem con, string id)
        {
            if (MIRControl == null)
                MIRControl = new Dictionary<ToolStripItem, string>();

            if (MIRControl.ContainsKey(con))
                MIRControl[con] = id;
            else
                MIRControl.Add(con, id);
        }
        /// <summary>
        /// Set a cbmi index. This may raise the cbmi index change event as well. The active status of the CBMI control MUST be enabled in order this to work.
        /// </summary>
        /// <param name="id">The ID of the CBMI</param>
        /// <param name="index">The new index of the cbmi</param>
        protected void SetCBMIIndex(string id, int index)
        {
            foreach (ToolStripItem cc in MIRControl.Keys)
            {
                if (MIRControl[cc] == id)
                {
                    ((ToolStripComboBox)cc).SelectedIndex = index;
                }
            }
        }
        // Events
        /// <summary>
        /// Raised when a cbmi selected index changed.
        /// </summary>
        protected event EventHandler<CBMIChangeIndexArgs> OnCBMIIndexChanged;
        #endregion
        #region <Shortcuts>
        private ShortcutsWinHandler shortcutsHandler;
        private void LoadShortcuts()
        {
            this.Select();
            shortcutsHandler = new ShortcutsWinHandler(this.Handle, GUIService.GUI.CurrentShortcutsMap, true);
        }
        /* private void SurpressShortcuts()
         {
             if (!shortcutsSurpressed)
             {
                 shortcutsSurpressed = true;
                 hotKey1.RemoveAllKeys();

 #if DEBUG
                 Status.WriteLine("Shortcuts suppressed !!");
 #endif
             }
         }
         private void ReloadShortcuts(bool isFormReloading)
         {
             if (!shortcutsSurpressed)
                 return;
             if (!shortcutsSupressedByForm && isFormReloading)
                 return;

             shortcutsSurpressed = false;
             hotKey1.RemoveAllKeys();
             foreach (ShortCut shrt in GUIService.GUI.CurrentShortcutsMap.Shortcuts)
             {
                 if (shrt.TheShortcut != "")
                 {
                     try
                     {
                         hotKey1.AddHotKey(shrt.TheShortcut);
                     }
                     catch (Exception ex)
                     {
                         Status.WriteError(Properties.Resources.Status_ErrorAdding + " " + shrt.TheShortcut + ": " + ex.Message);
                     }
                 }
             }
 #if DEBUG
             Status.WriteLine("Shortcuts enabled and reloaded !!");
 #endif
         }*/
        #endregion
        #region <Controls>
        private void ReloadTabControls(bool firstTime)
        {
            if (firstTime)
            {
                if (GUIService.GUI.CurrentTabsMap == null)
                    GUIService.GUI.CurrentTabsMap = new TabControlContainer();
                GUIService.GUI.CurrentTabsMap.TabDragged += CurrentTabsMap_TabDragged;
                GUIService.GUI.CurrentTabsMap.TabDropped += CurrentTabsMap_TabDropped;
                GUIService.GUI.CurrentTabsMap.SelectedControlIDChanged += CurrentTabsMap_SelectedControlIDChanged;
            }
            GUIService.GUI.CurrentTabsMap.RefreshView(toolStripContainer1.ContentPanel);
            GUIService.GUI.CurrentTabsMap.ReloadSplitters();
#if DEBUG
            //  FormDebugAddTabControls frm = new FormDebugAddTabControls();
            //  frm.Show(this);
#endif
        }
        #endregion
        #region <Theme>
        private void ReloadTheme(bool toTabsToo)
        {
            // Apply here 
            if (GUIService.GUI.CurrentTheme == null)
                return;
            menuStrip1.ForeColor = Color.FromArgb(GUIService.GUI.CurrentTheme.MenuTextsColor);
            menuStrip1.BackColor = Color.FromArgb(GUIService.GUI.CurrentTheme.MenusBackColor);
            // We need to do this little trick to change the highlight color for the main menu
            MenuColors colors = new MenuColors(
                Color.FromArgb(GUIService.GUI.CurrentTheme.MenuHighlightColor),
                Color.FromArgb(GUIService.GUI.CurrentTheme.MenuHighlightColor),
                Color.FromArgb(GUIService.GUI.CurrentTheme.MenuHighlightColor),
                Color.FromArgb(GUIService.GUI.CurrentTheme.MenusBackColor),
                Color.FromArgb(GUIService.GUI.CurrentTheme.MenusBackColor),
                Color.FromArgb(GUIService.GUI.CurrentTheme.MenusBackColor),
                Color.FromArgb(GUIService.GUI.CurrentTheme.MenusBackColor),
                Color.FromArgb(GUIService.GUI.CurrentTheme.MenusBackColor));
            menuStrip1.Renderer = new ThemeRenderer(colors);

            toolStripContainer1.BottomToolStripPanel.BackColor =
            toolStripContainer1.TopToolStripPanel.BackColor =
            toolStripContainer1.LeftToolStripPanel.BackColor =
            toolStripContainer1.ContentPanel.BackColor =
            toolStripContainer1.TopToolStripPanel.BackColor = Color.FromArgb(GUIService.GUI.CurrentTheme.PanelsBackColor);

            foreach (ToolStrip toolbar in toolStripContainer1.TopToolStripPanel.Controls)
            {
                ApplyColorsOfToolbar(toolbar);
            }
            foreach (ToolStrip toolbar in toolStripContainer1.BottomToolStripPanel.Controls)
            {
                ApplyColorsOfToolbar(toolbar);
            }
            foreach (ToolStrip toolbar in toolStripContainer1.LeftToolStripPanel.Controls)
            {
                ApplyColorsOfToolbar(toolbar);
            }
            foreach (ToolStrip toolbar in toolStripContainer1.RightToolStripPanel.Controls)
            {
                ApplyColorsOfToolbar(toolbar);
            }
            ApplyColorsOfToolbar(statusStrip1);
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                ApplyColorToMenuItem(item, GUIService.GUI.CurrentTheme);
            }

            // Apply to the tabs
            if (toTabsToo)
            {
                if (GUIService.GUI.CurrentTabsMap != null)
                {
                    GUIService.GUI.CurrentTabsMap.ApplyCurrentTheme();
                }
            }
        }
        private void ApplyColorsOfToolbar(ToolStrip toolbar)
        {
            toolbar.ForeColor = Color.FromArgb(GUIService.GUI.CurrentTheme.ToolbarTextsColor);
            toolbar.BackColor = Color.FromArgb(GUIService.GUI.CurrentTheme.ToolbarsBackColor);
            // We need to do this little trick to change the highlight color for a toolbar
            ToolbarColors tcolors = new ToolbarColors(
            Color.FromArgb(GUIService.GUI.CurrentTheme.ToolbarsHighlightColor),
            Color.FromArgb(GUIService.GUI.CurrentTheme.ToolbarsHighlightColor),
            Color.FromArgb(GUIService.GUI.CurrentTheme.ToolbarsHighlightColor));
            toolbar.Renderer = new ThemeRenderer(tcolors);
            for (int i = 0; i < toolbar.Items.Count; i++)
            {
                if (toolbar.Items[i] is ToolStripSplitButton)
                {
                    ApplyColorToSplitButtonItem((ToolStripSplitButton)toolbar.Items[i], GUIService.GUI.CurrentTheme);
                }
                else if (!(toolbar.Items[i] is ToolStripTextBox) && !(toolbar.Items[i] is ToolStripComboBox))
                {
                    toolbar.Items[i].ForeColor = Color.FromArgb(GUIService.GUI.CurrentTheme.ToolbarTextsColor);
                    toolbar.Items[i].BackColor = Color.FromArgb(GUIService.GUI.CurrentTheme.ToolbarsBackColor);
                }
            }
        }
        private void ApplyColorToMenuItem(ToolStripMenuItem item, Theme theme)
        {
            item.BackColor = Color.FromArgb(theme.MenusBackColor);
            item.ForeColor = Color.FromArgb(theme.MenuTextsColor);

            for (int i = 0; i < item.DropDownItems.Count; i++)
            {
                if (item.DropDownItems[i] is ToolStripMenuItem)
                {
                    ApplyColorToMenuItem((ToolStripMenuItem)item.DropDownItems[i], theme);
                }
                else if (!(item.DropDownItems[i] is ToolStripTextBox) && !(item.DropDownItems[i] is ToolStripComboBox))
                {
                    item.DropDownItems[i].BackColor = Color.FromArgb(theme.MenusBackColor);
                    item.DropDownItems[i].ForeColor = Color.FromArgb(theme.MenuTextsColor);
                }
            }
        }
        private void ApplyColorToSplitButtonItem(ToolStripSplitButton item, Theme theme)
        {
            item.BackColor = Color.FromArgb(theme.MenusBackColor);
            item.ForeColor = Color.FromArgb(theme.MenuTextsColor);

            for (int i = 0; i < item.DropDownItems.Count; i++)
            {
                if (item.DropDownItems[i] is ToolStripMenuItem)
                {
                    ApplyColorToMenuItem((ToolStripMenuItem)item.DropDownItems[i], theme);
                }
                else if (!(item.DropDownItems[i] is ToolStripTextBox) && !(item.DropDownItems[i] is ToolStripComboBox))
                {
                    item.DropDownItems[i].BackColor = Color.FromArgb(theme.MenusBackColor);
                    item.DropDownItems[i].ForeColor = Color.FromArgb(theme.MenuTextsColor);
                }
            }
        }
        #endregion
        #region <Status>
        private void UpdateTitle()
        {
            this.Text = MUI.ProjectTitle;
        }
        #endregion
        #region <Delegates>
        private delegate void ToggleItemCheckedDelegate(ToolStripItem item, bool status);
        private delegate void ProgressDelegate(string status, int precentage);
        private delegate void WriteStatusDelegate(string status);

        private void ToggleItemChecked(ToolStripItem item, bool status)
        {
            ((ToolStripMenuItem)item).Checked = status;
        }
        private void ToggleItemEnabled(ToolStripItem item, bool status)
        {
            item.Enabled = status;
        }

        /// <summary>
        /// Write status label
        /// </summary>
        /// <param name="status">The text to write</param>
        protected void WriteStatusLabel(string status)
        {
            StatusLabel.Text = status;
        }
        /// <summary>
        /// Advance progress
        /// </summary>
        /// <param name="status">The status string to write</param>
        /// <param name="precentage">The progress value, ranged between 0 and 100</param>
        protected void ProgressAdvance(string status, int precentage)
        {
            StatusLabel.Text = status;
            if (precentage >= 0 && precentage <= 100)
                toolStripProgressBar1.Value = precentage;
        }
        /// <summary>
        /// Show the progress bar.
        /// </summary>
        protected void ShowProgressBar()
        {
            toolStripProgressBar1.Visible = true;
        }
        /// <summary>
        /// Hide the progress bar.
        /// </summary>
        protected void HideProgressBar()
        {
            toolStripProgressBar1.Visible = false;
        }
        #endregion

        private bool appExiting;

        private void MUI_ProjectTitleChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }
        private void Cmi_CheckStatusChanged(object sender, StatusChangedArgs e)
        {
            foreach (ToolStripItem k in MIRControl.Keys)
            {
                if (MIRControl[k] == ((IMenuItemRepresentator)sender).ID)
                {
                    if (k is ToolStripMenuItem)
                    {
                        if (!this.InvokeRequired)
                            ((ToolStripMenuItem)k).Checked = e.Status;
                        else
                            this.Invoke(new ToggleItemCheckedDelegate(ToggleItemChecked), k, e.Status);
                    }
                    if (k is ToolStripButton)
                    {
                        if (!this.InvokeRequired)
                            ((ToolStripButton)k).Checked = e.Status;
                        else
                            this.Invoke(new ToggleItemCheckedDelegate(ToggleItemChecked), k, e.Status);
                    }
                    if (k is ToolStripSplitButton)
                    {
                        if (!this.InvokeRequired)
                            ((ToolStripButton)k).Checked = e.Status;
                        else
                            this.Invoke(new ToggleItemCheckedDelegate(ToggleItemChecked), k, e.Status);
                    }

                }
            }
        }
        private void Cmi_ActiveStatusChanged(object sender, StatusChangedArgs e)
        {
            foreach (ToolStripItem k in MIRControl.Keys)
            {
                if (MIRControl[k] == ((IMenuItemRepresentator)sender).ID)
                {
                    if (!this.InvokeRequired)
                        k.Enabled = e.Status;
                    else
                        this.Invoke(new ToggleItemCheckedDelegate(ToggleItemEnabled), k, e.Status);
                }
            }
        }
        private void Button_ClickCMIExecute(object sender, EventArgs e)
        {
            string id = "";
            if (sender is ToolStripButton)
                id = ((ToolStripButton)sender).Tag.ToString();
            else if (sender is ToolStripSplitButton)
                id = ((ToolStripSplitButton)sender).Tag.ToString();

            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
            if (item != null)
            {
                if (item.Value is CMI)
                {
                    CMI cmi = (CMI)item.Value;
                    // Extract the command !
                    Lazy<ICommand, ICommandInfo> theCommand = CommandsManager.CMD.GetCommand(cmi.CommandID);
                    if (theCommand != null)
                    {
                        object[] responses = new object[0];
                        if (cmi.UseParameters)
                        {
                            theCommand.Value.Execute(cmi.Parameters, out responses);
                        }
                        else
                        {
                            theCommand.Value.Execute(out responses);
                        }
                        cmi.OnCommandResponse(responses);
                    }
                }
            }
        }
        private void Button_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripSplitButton rootItem = (ToolStripSplitButton)sender;
            for (int i = 0; i < rootItem.DropDownItems.Count; i++)
            {
                if (rootItem.DropDownItems[i].Tag == null)
                    continue;

                string id = rootItem.DropDownItems[i].Tag.ToString();
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                if (item != null)
                {
                    if (item.Value is DMI)
                    {
                        DMI dmi = (DMI)item.Value;
                        dmi.OnView();
                        // Load up the children
                        ToolStripMenuItem childItem = (ToolStripMenuItem)rootItem.DropDownItems[i];
                        childItem.DropDownItems.Clear();
                        if (dmi.ChildItems.Count == 0)
                        {
                            childItem.Enabled = false;
                        }
                        else
                        {
                            childItem.Enabled = true;
                            foreach (DMIChild chld in dmi.ChildItems)
                            {
                                // Get the item
                                ToolStripMenuItem cmiItem = new ToolStripMenuItem();

                                cmiItem.Text = chld.DisplayName;
                                cmiItem.ToolTipText = chld.Tooltip;
                                cmiItem.Checked = chld.Active;
                                cmiItem.Tag = chld;
                                // On execute !
                                cmiItem.Click += DMIChildExecute;

                                // Add it !
                                childItem.DropDownItems.Add(cmiItem);
                            }
                        }
                    }
                }
            }
        }
        private void RootItem_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem rootItem = (ToolStripMenuItem)sender;
            for (int i = 0; i < rootItem.DropDownItems.Count; i++)
            {
                if (rootItem.DropDownItems[i].Tag == null)
                    continue;

                string id = rootItem.DropDownItems[i].Tag.ToString();
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                if (item != null)
                {
                    if (item.Value is DMI)
                    {
                        DMI dmi = (DMI)item.Value;
                        dmi.OnView();
                        // Load up the children
                        ToolStripMenuItem childItem = (ToolStripMenuItem)rootItem.DropDownItems[i];
                        childItem.DropDownItems.Clear();
                        if (dmi.ChildItems.Count == 0)
                        {
                            childItem.Enabled = false;
                        }
                        else
                        {
                            childItem.Enabled = true;
                            foreach (DMIChild chld in dmi.ChildItems)
                            {
                                // Get the item
                                ToolStripMenuItem cmiItem = new ToolStripMenuItem();

                                cmiItem.Text = chld.DisplayName;
                                if (chld.Icon != null)
                                    cmiItem.Image = chld.Icon;
                                cmiItem.ToolTipText = chld.Tooltip;
                                cmiItem.Checked = chld.Active;
                                cmiItem.Tag = chld;

                                cmiItem.ForeColor = Color.FromArgb(GUIService.GUI.CurrentTheme.MenuTextsColor);
                                cmiItem.BackColor = Color.FromArgb(GUIService.GUI.CurrentTheme.MenusBackColor);
                                // On execute !
                                cmiItem.Click += DMIChildExecute;

                                // Add it !
                                childItem.DropDownItems.Add(cmiItem);
                            }
                        }
                    }
                }
            }
        }
        private void DMIChildExecute(object sender, EventArgs e)
        {
            object[] resp = new object[0];
            ((DMIChild)((ToolStripMenuItem)sender).Tag).Execute(out resp);

            string id = ((DMIChild)((ToolStripMenuItem)sender).Tag).ParentID;
            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
            if (item != null)
            {
                if (item.Value is DMI)
                {
                    // Add it now !!
                    DMI dmi = (DMI)item.Value;
                    dmi.OnCommandResponse(resp);
                }
            }
        }
        private void CmiItem_Click(object sender, EventArgs e)
        {
            string id = ((ToolStripMenuItem)sender).Tag.ToString();
            Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
            if (item != null)
            {
                if (item.Value is CMI)
                {
                    CMI cmi = (CMI)item.Value;
                    // Extract the command !
                    Lazy<ICommand, ICommandInfo> theCommand = CommandsManager.CMD.GetCommand(cmi.CommandID);
                    if (theCommand != null)
                    {
                        object[] responses = new object[0];
                        if (cmi.UseParameters)
                        {
                            theCommand.Value.Execute(cmi.Parameters, out responses);
                        }
                        else
                        {
                            theCommand.Value.Execute(out responses);
                        }
                        cmi.OnCommandResponse(responses);
                    }
                }
            }
        }
        private void GUI_MenuItemsMapChanged(object sender, EventArgs e)
        {
            ReloadMenuItems();
            ReloadTheme(false);
        }
        private void GUI_ToolbarsMapAboutToBeChanged(object sender, EventArgs e)
        {
            // Dispose all events
            foreach (TBRElement tbr in GUIService.GUI.CurrentToolbarsMap.ToolBars)
            {
                tbr.VisibleChanged -= Toolbar_VisibleChanged;
                tbr.Dispose();
            }
        }
        private void GUI_ToolbarsMapChanged(object sender, EventArgs e)
        {
            ReloadToolbars(); ReloadTheme(false);
        }
        private void GUI_TabsControlsMapChanged(object sender, EventArgs e)
        {
            if (GUIService.GUI.CurrentTabsMap == null)
                GUIService.GUI.CurrentTabsMap = new TabControlContainer();
            GUIService.GUI.CurrentTabsMap.TabDragged += CurrentTabsMap_TabDragged;
            GUIService.GUI.CurrentTabsMap.TabDropped += CurrentTabsMap_TabDropped;
            GUIService.GUI.CurrentTabsMap.SelectedControlIDChanged += CurrentTabsMap_SelectedControlIDChanged;
            ReloadTabControls(false);
            ReloadTheme(true);
        }
        private void GUI_ThemeChanged(object sender, EventArgs e)
        {
            ReloadTheme(true);
        }
        private void GUI_ThemeAboutToBeChanged(object sender, EventArgs e)
        {
        }
        private void GUI_ShortcutsMapChanged(object sender, EventArgs e)
        {
            ApplyShortcutsToMenuItems();
            shortcutsHandler.Initialize(this.Handle, GUIService.GUI.CurrentShortcutsMap, true);
        }
        private void GUI_TabsControlsMapAboutToBeChanged(object sender, EventArgs e)
        {
            if (GUIService.GUI.CurrentTabsMap != null)
            {
                GUIService.GUI.CurrentTabsMap.TabDragged -= CurrentTabsMap_TabDragged;
                GUIService.GUI.CurrentTabsMap.SelectedControlIDChanged -= CurrentTabsMap_SelectedControlIDChanged;
                GUIService.GUI.CurrentTabsMap.TabDropped -= CurrentTabsMap_TabDropped;
            }
        }
        private void GUI_ProgressFinished(object sender, ProgressEventArgs e)
        {
            if (!InvokeRequired)
                HideProgressBar();
            else
                Invoke(new Action(HideProgressBar));

            if (!InvokeRequired)
                WriteStatusLabel(e.Status);
            else
                Invoke(new WriteStatusDelegate(WriteStatusLabel), e.Status);
        }
        private void GUI_ProgressBegin(object sender, ProgressEventArgs e)
        {
            if (!InvokeRequired)
                ShowProgressBar();
            else
                Invoke(new Action(ShowProgressBar));

            if (!InvokeRequired)
                WriteStatusLabel(e.Status);
            else
                Invoke(new WriteStatusDelegate(WriteStatusLabel), e.Status);
        }
        private void GUI_Progress(object sender, ProgressEventArgs e)
        {
            if (!InvokeRequired)
                ProgressAdvance(e.Status, e.Precentage);
            else
                Invoke(new ProgressDelegate(ProgressAdvance), e.Status, e.Precentage);
        }
        private void CurrentTabsMap_TabDragged(object sender, ControlAddedRemovedArgs e)
        {
            if (GUIService.GUI.CurrentTabsMap != null)
            {
                GUIService.GUI.CurrentTabsMap.EnterDraggingMode(e.ControlID);
            }
        }
        private void CurrentTabsMap_TabDropped(object sender, TabControlDroppedArgs e)
        {
            if (GUIService.GUI.CurrentTabsMap != null)
            {
                GUIService.GUI.CurrentTabsMap.LeaveDraggingMode(e.DraggingMode);
                GUIService.GUI.CurrentTabsMap.ApplyCurrentTheme();// Just in case
            }
        }
        private void CurrentTabsMap_SelectedControlIDChanged(object sender, EventArgs e)
        {
            if (GUIService.GUI.CurrentTabsMap != null)
            {
                GUIService.GUI.CurrentTabsMap.CheckActiveControl();
            }
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (appExiting)
                return;
            appExiting = true;
            e.Cancel = !MUI.ExitApplication();
            if (!e.Cancel)
            {
                SaveSettings();
                DisposeEvents();
                // Close things
                shortcutsHandler.DestroyShortcuts();
            }
            appExiting = false;
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            if (shortcutsHandler == null)
                return;
            shortcutsHandler.HaltShortcutsMapTimer = false;
        }
        private void FormMain_Deactivate(object sender, EventArgs e)
        {
            // When the main window is deactivated, remove all keys to avoid errors
            // bacause of the hotkey util will be still active outside the app and 
            // may block some keys.
            if (shortcutsHandler == null)
                return;
            shortcutsHandler.HaltShortcutsMapTimer = true;
        }
        private void Toolbar_ParentChanged(object sender, EventArgs e)
        {
            // When the parent has changed of a toolbar that's mean the location
            // of that toolbar has been changed, update this.
            foreach (TBRElement tbr in GUIService.GUI.CurrentToolbarsMap.ToolBars)
            {
                ToolStrip toolbar = (ToolStrip)sender;
                if (tbr.Name == toolbar.Text)
                {
                    if (toolbar.Parent == toolStripContainer1.TopToolStripPanel)
                        tbr.Location = TBRLocation.TOP;
                    else if (toolbar.Parent == toolStripContainer1.LeftToolStripPanel)
                        tbr.Location = TBRLocation.LEFT;
                    else if (toolbar.Parent == toolStripContainer1.RightToolStripPanel)
                        tbr.Location = TBRLocation.RIGHT;
                    else if (toolbar.Parent == toolStripContainer1.BottomToolStripPanel)
                        tbr.Location = TBRLocation.BOTTOM;
                    break;
                }
            }
        }
        private void Toolbar_VisibleChanged(object sender, EventArgs e)
        {
            // Update visibilities of all toolbars
            foreach (TBRElement tbr in GUIService.GUI.CurrentToolbarsMap.ToolBars)
            {
                bool found = false;
                foreach (ToolStrip toolbar in toolStripContainer1.TopToolStripPanel.Controls)
                {
                    if (tbr.Name == toolbar.Text)
                    {
                        toolbar.Visible = tbr.Visible;
                        found = true; break;
                    }
                }
                if (found)
                    continue;
                foreach (ToolStrip toolbar in toolStripContainer1.BottomToolStripPanel.Controls)
                {
                    if (tbr.Name == toolbar.Text)
                    {
                        toolbar.Visible = tbr.Visible;
                        found = true; break;
                    }
                }
                if (found)
                    continue;
                foreach (ToolStrip toolbar in toolStripContainer1.LeftToolStripPanel.Controls)
                {
                    if (tbr.Name == toolbar.Text)
                    {
                        toolbar.Visible = tbr.Visible;
                        found = true; break;
                    }
                }
                if (found)
                    continue;
                foreach (ToolStrip toolbar in toolStripContainer1.RightToolStripPanel.Controls)
                {
                    if (tbr.Name == toolbar.Text)
                    {
                        toolbar.Visible = tbr.Visible;
                        found = true; break;
                    }
                }
                if (found)
                    continue;
            }
        }
        private void Txt_TextChanged(object sender, EventArgs e)
        {
            if (sender is ToolStripTextBox)
            {
                ToolStripTextBox txt = (ToolStripTextBox)sender;
                string id = txt.Tag.ToString();
                if (id == null)
                    return;
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                if (item != null)
                {
                    if (item.Value is TMI)
                    {
                        TMI tmi = (TMI)item.Value;
                        tmi.OnTextChanged(txt.Text);
                    }
                }
            }
        }
        private void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
                return;
            if (sender is ToolStripTextBox)
            {
                ToolStripTextBox txt = (ToolStripTextBox)sender;
                string id = txt.Tag.ToString();
                if (id == null)
                    return;
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                if (item != null)
                {
                    if (item.Value is TMI)
                    {
                        TMI tmi = (TMI)item.Value;
                        tmi.OnEnterPressed();
                    }
                }
            }
        }
        private void Cbmi_ItemsReloadRequest(object sender, EventArgs e)
        {
            CBMI cbmi = (CBMI)sender;
            if (cbmi.Tag is ToolStripComboBox)
            {
                ToolStripComboBox cb = (ToolStripComboBox)cbmi.Tag;

                cb.Items.Clear();
                int max = cbmi.DefaultSize;
                foreach (string txt in cbmi.Items)
                {
                    cb.Items.Add(txt);
                    if (cbmi.SizeChangesAutomatically)
                    {
                        int v = TextRenderer.MeasureText(txt, cb.Font).Width;
                        if (v > max)
                            max = v;
                    }
                }
                cb.Width = max;

                if (cbmi.SelectedItemIndex >= 0 && cbmi.SelectedItemIndex < cb.Items.Count)
                    cb.SelectedIndex = cbmi.SelectedItemIndex;
            }
        }
        private void Cbmi_ChangeIndexRequest(object sender, CBMIChangeIndexArgs e)
        {
            CBMI cbmi = (CBMI)sender;
            if (cbmi.Tag is ToolStripComboBox)
            {
                ToolStripComboBox cb = (ToolStripComboBox)cbmi.Tag;

                if (e.Index >= 0 && e.Index < cb.Items.Count)
                    cb.SelectedIndex = e.Index;
            }
        }
        private void Cb_DropDownClosed(object sender, EventArgs e)
        {
            if (sender is ToolStripComboBox)
            {
                ToolStripComboBox txt = (ToolStripComboBox)sender;
                string id = txt.Tag.ToString();
                if (id == null)
                    return;
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                if (item != null)
                {
                    if (item.Value is CBMI)
                    {
                        CBMI tmi = (CBMI)item.Value;
                        tmi.OnDropDownClosed();
                    }
                }
            }
        }
        private void Cb_DropDown(object sender, EventArgs e)
        {
            if (sender is ToolStripComboBox)
            {
                ToolStripComboBox txt = (ToolStripComboBox)sender;
                string id = txt.Tag.ToString();
                if (id == null)
                    return;
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                if (item != null)
                {
                    if (item.Value is CBMI)
                    {
                        CBMI tmi = (CBMI)item.Value;
                        tmi.OnDropDownOpening();
                    }
                }
            }
        }
        private void Cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ToolStripComboBox)
            {
                ToolStripComboBox txt = (ToolStripComboBox)sender;
                string id = txt.Tag.ToString();
                if (id == null)
                    return;
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                if (item != null)
                {
                    if (item.Value is CBMI)
                    {
                        CBMI tmi = (CBMI)item.Value;
                        tmi.OnIndexChanged(txt.SelectedIndex);
                        OnCBMIIndexChanged?.Invoke(this, new CBMIChangeIndexArgs(tmi.ID, txt.SelectedIndex));
                    }
                }
            }
        }
        private void Cb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
                return;
            if (sender is ToolStripComboBox)
            {
                ToolStripComboBox txt = (ToolStripComboBox)sender;
                string id = txt.Tag.ToString();
                if (id == null)
                    return;
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                if (item != null)
                {
                    if (item.Value is CBMI)
                    {
                        CBMI tmi = (CBMI)item.Value;
                        tmi.OnEnterPressed();
                    }
                }
            }
        }
        private void Cb_TextChanged(object sender, EventArgs e)
        {
            if (sender is ToolStripComboBox)
            {
                ToolStripComboBox txt = (ToolStripComboBox)sender;
                string id = txt.Tag.ToString();
                if (id == null)
                    return;
                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> item = GUIService.GUI.GetMenuItem(id);
                if (item != null)
                {
                    if (item.Value is CBMI)
                    {
                        CBMI tmi = (CBMI)item.Value;
                        tmi.OnTextChanged(txt.Text);
                    }
                }
            }
        }
        private void GUI_SelectedTabControlIDChanged(object sender, EventArgs e)
        {
            Lazy<ITabControl, IControlInfo> con = GUIService.GUI.SelectedTabControl;
            if (con != null)
            {
                if (con.Value.SurpressHotkeys)
                {
                    // Suppress all shortcuts by stopping the timer.
                    shortcutsHandler.HaltShortcutsMapTimer = true;
                    Trace.WriteLine(Resources.Status_ShortcutsSuppressedForThisTabControl);
                }
                else
                {
                    shortcutsHandler.HaltShortcutsMapTimer = false;
                    Trace.WriteLine(Resources.Status_ShortcutsEnabledForThisTabControl);
                }
            }
        }
        private void FormMain_Shown(object sender, EventArgs e)
        {
            GUIService.GUI.CurrentTabsMap.ReloadSplitters();
        }
    }
}
