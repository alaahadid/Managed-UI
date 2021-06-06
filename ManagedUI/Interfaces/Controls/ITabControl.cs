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
using System.Resources;
using System.Reflection;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace ManagedUI
{
    /// <summary>
    /// Tab Control base class.
    /// </summary>
    public class ITabControl : UserControl
    {
        /// <summary>
        /// Tab Control base class.
        /// </summary>
        public ITabControl()
            : base()
        {
            SurpressHotkeys = false;
            LoadAttributes();
            // We need to call InitializeComponent method if found
            MethodInfo inf = this.GetType().GetMethod("InitializeComponent",
                BindingFlags.NonPublic | BindingFlags.Instance);
            if (inf != null)
                inf.Invoke(this, null);
        }
        /// <summary>
        /// The resource key for display name.
        /// </summary>
        protected string kDisplayName;
        /// <summary>
        /// The resource key for tooltip. 
        /// </summary>
        protected string kToolTip;
        /// <summary>
        /// The resource key for icon
        /// </summary>
        protected string kIcon;
        /// <summary>
        /// The icon to use when no resource used
        /// </summary>
        protected Image icon;
        /// <summary>
        /// The resources manager object to use for this item. Should be set automatically by the GUI service.
        /// </summary>
        protected ResourceManager resource;
        /// <summary>
        /// Indicates if the item should use resource for display name and others.
        /// </summary>
        protected bool useResource;
        /// <summary>
        /// Indicates if the resource manager is set or not.
        /// </summary>
        protected bool resourceSet;
        private Dictionary<ToolStripItem, string> MIRControl;
        private delegate void ToggleItemCheckedDelegate(ToolStripItem item, bool status);

        // Methods
        /// <summary>
        /// Load attributes and apply them for this control.
        /// </summary>
        protected virtual void LoadAttributes()
        {
            foreach (Attribute attr in Attribute.GetCustomAttributes(this.GetType()))
            {
                if (attr.GetType() == typeof(SurpressHotKeysAttribute))
                {
                    SurpressHotkeys = true;
                }
                if (attr.GetType() == typeof(ControlInfoAttribute))
                {
                    ControlInfoAttribute inf = (ControlInfoAttribute)attr;
                    Name = inf.Name;
                    ID = inf.ID;
                }

                if (attr.GetType() == typeof(TabControlResourceInfoAttribute))
                {
                    useResource = true;
                    TabControlResourceInfoAttribute inf = (TabControlResourceInfoAttribute)attr;
                    kDisplayName = inf.KDisplayName;
                    kIcon = inf.KIcon;
                }
                else if (attr.GetType() == typeof(TabControlNoResourceInfoAttribute))
                {
                    useResource = false;
                    TabControlNoResourceInfoAttribute inf = (TabControlNoResourceInfoAttribute)attr;
                    kDisplayName = inf.DisplayName;
                }
            }
        }
        /// <summary>
        /// This method is called by the gui core when initializing. Never change this unless you know what are you doing !!
        /// </summary>
        public virtual void FindResources()
        {
            if (!useResource)
                return;
            if (resourceSet)
                return;
            // First of all, we need to locate the resource class that comes in a .net project by default
            Type[] types = this.GetType().Assembly.GetTypes();
            foreach (Type tp in types)
            {
                if (tp.Name == "Resources")
                {
                    // this is it !!
                    resource = (ResourceManager)tp.GetProperty("ResourceManager", BindingFlags.Static | BindingFlags.NonPublic).GetValue(tp, null);
                    resourceSet = true;
                    break;
                }
            }
            // Now let's set this found resource to all items that already found and located in the 
            // same library !
            foreach (Lazy<ITabControl, IControlInfo> it in GUIService.GUI.AvailableTabControls)
            {
                if (!it.Value.resourceSet)
                {
                    // Let's see if this item in the same library
                    if (it.Value.GetType().Assembly.FullName == this.GetType().Assembly.FullName)
                    {
                        // This is it !!
                        it.Value.resource = resource;
                        it.Value.resourceSet = true;
                    }
                    else
                    {
                        // This item need to locate the resource for itself and for others in it's library
                        it.Value.FindResources();
                    }
                }
            }
        }
        /// <summary>
        /// This method should be called when the control is first loaded and detected.
        /// </summary>
        public virtual void Initialize()
        {
        }
        /// <summary>
        /// Can be used to load settings of tab control
        /// </summary>
        public virtual void LoadSettings()
        {

        }
        /// <summary>
        /// Can be used to save settings of tab control
        /// </summary>
        public virtual void SaveSettings()
        {

        }
        /// <summary>
        /// This method should be called when this control become visible to user.
        /// </summary>
        public virtual void OnDisplay()
        {
        }
        /// <summary>
        /// This method should be called when this control is hiden from user.
        /// </summary>
        public virtual void OnHide()
        {
        }
        /// <summary>
        /// This method should be called when the user closes this tab control from the tabs control.
        /// </summary>
        public virtual void OnTabClose()
        { }
        /// <summary>
        /// TabControl.ToString(): show the display name of the control.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DisplayName;
        }
        /// <summary>
        /// Apply a Mune Items Map for a context menu related to this control.
        /// </summary>
        /// <param name="map">The map to use</param>
        /// <param name="contextMap">The context menu strip to apply into</param>
        protected void MIRMapToContextMenu(MenuItemsMap map, ContextMenuStrip contextMap)
        {
            contextMap.Items.Clear();
            // Loop through the child items of this element
            for (int i = 0; i < map.RootItems.Count; i++)
            {
                switch (map.RootItems[i].Type)
                {
                    case MIRType.ROOT:
                        {
                            // !! ROOT ITEMS CANNOT BE ADDED IN ROOT !!
                            Trace.TraceError(Properties.Resources.Status_MIRError2);
                            break;
                        }
                    case MIRType.TMI:
                        {
                            string id = map.RootItems[i].ID;
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

                                    contextMap.Items.Add(txt);
                                }
                            }
                            break;
                        }
                    case MIRType.CBMI:
                        {
                            string id = map.RootItems[i].ID;
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
                                    contextMap.Items.Add(cb);
                                }
                            }
                            break;
                        }
                    case MIRType.PMI:
                        {
                            // Add a command menu item ! 
                            string id = map.RootItems[i].ID;
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
                                    contextMap.Items.Add(pmiItem);
                                    if (pmi.UseActiveStatus || pmi.UseCheckStatus)
                                        AddMIRControl(pmiItem, id);
                                    pmiItem.DropDownOpening += RootItem_DropDownOpening;
                                    // Add children
                                    ApplyMIRElement(map.RootItems[i], pmiItem);
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
                            string id = map.RootItems[i].ID;
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
                                    contextMap.Items.Add(dmiItem);
                                    // We can't add children of this item, the children
                                    // must be updated during run time.
                                    // ApplyMIRElement(map.RootItems[i], dmiItem);
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
                            contextMap.Items.Add(spr);
                            // !! SMI CANNOT HAVE CHILDREN !!
                            break;
                        }
                    case MIRType.CMI:
                        {
                            // Add a command menu item ! 
                            string id = map.RootItems[i].ID;
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
                                    contextMap.Items.Add(cmiItem);
                                    cmiItem.DropDownOpening += RootItem_DropDownOpening;
                                    // Add children
                                    ApplyMIRElement(map.RootItems[i], cmiItem);
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
        /// <summary>
        /// Apply a toolbar map into a toolstrip
        /// </summary>
        /// <param name="tbr">The toolbar map</param>
        /// <param name="toolbar">The toolstrip to apply into</param>
        protected void TBRElementToToolstrip(TBRElement tbr, ToolStrip toolbar)
        {
            toolbar.Text = tbr.Name;
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
        }
        /// <summary>
        /// Apply a toolbar map into a statusstrip
        /// </summary>
        /// <param name="tbr">The toolbar map</param>
        /// <param name="strip">The toolstrip to apply into</param>
        protected void TBRElementToStatusStrip(TBRElement tbr, StatusStrip strip)
        {
            strip.Text = tbr.Name;
            if (tbr.CustomStyle)
                if (tbr.ImageSize.Height > 0 && tbr.ImageSize.Width > 0)
                    strip.ImageScalingSize = tbr.ImageSize;

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
                                        strip.Items.Add(button);
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
                                        strip.Items.Add(button);
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
                                    strip.Items.Add(txt);
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
                                    strip.Items.Add(cb);
                                }
                            }
                            break;
                        }
                    case MIRType.SMI:
                        {
                            ToolStripSeparator seperator = new ToolStripSeparator();
                            strip.Items.Add(seperator);
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
                                        strip.Items.Add(button);
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
                                        strip.Items.Add(button);

                                        ApplySplitButtonChildren(tbr.RootItems[i], button);
                                    }
                                }
                            }
                            break;
                        }
                }
            }
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
        private void AddMIRControl(ToolStripItem con, string id)
        {
            if (MIRControl == null)
                MIRControl = new Dictionary<ToolStripItem, string>();

            if (MIRControl.ContainsKey(con))
                MIRControl[con] = id;
            else
                MIRControl.Add(con, id);
        }
        private void ToggleItemChecked(ToolStripItem item, bool status)
        {
            ((ToolStripMenuItem)item).Checked = status;
        }
        private void ToggleItemEnabled(ToolStripItem item, bool status)
        {
            item.Enabled = status;
        }

        // Properties
        /// <summary>
        /// Get the control id. This id is important so that this control can be recognized in the GUI core. 
        /// Make sure this id is not duplicated or equal other.
        /// </summary>
        [Browsable(false)]
        public string ID
        { get; protected set; }
        /// <summary>
        /// Get the display name of this control, the name that will be displayed to user.
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (!useResource)
                    return kDisplayName;
                try
                {
                    return resource.GetString(kDisplayName);
                }
                catch { }
                return Properties.Resources.Status_TCError1 + " '" + kDisplayName + "'";
            }
        }
        /// <summary>
        /// Get the tooltip of this control that will be displayed to the user.
        /// </summary>
        public virtual string ToolTip
        {
            get
            {
                if (!useResource)
                    return kToolTip;
                try
                {
                    return resource.GetString(kToolTip);
                }
                catch { }
                return Properties.Resources.Status_MIEError4 + " '" + kToolTip + "'";
            }
        }
        /// <summary>
        /// Get or set (set only when MIRNoResourceProperties attribute is used) the icon of this 
        /// control that will be displayed to the user.
        /// </summary>
        public virtual Image Icon
        {
            get
            {
                if (!useResource)
                    return icon;
                try
                {
                    object ic = resource.GetObject(kIcon);
                    if (ic is Image)
                        return (Image)ic;
                    else if (ic is Icon)
                        return ((Icon)ic).ToBitmap();
                }
                catch { }
                return null;
            }
            set
            {
                if (!useResource)
                    icon = value;
            }
        }
        /// <summary>
        /// Get if the hotkeys should be disabled when this control become active
        /// </summary>
        public bool SurpressHotkeys
        {
            get; protected set;
        }

        // Events
        /// <summary>
        /// Raised when a cbmi selected index changed.
        /// </summary>
        protected event EventHandler<CBMIChangeIndexArgs> OnCBMIIndexChanged;

        // Event handlers
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
                {
                    cb.SelectedIndex = e.Index;
                }
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
    }
}
