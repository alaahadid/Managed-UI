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
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

namespace ManagedUI
{
    /// <summary>
    /// Represents a container for tab control(s)
    /// </summary>
    [Serializable]
    public sealed class TabControlContainer
    {
        /// <summary>
        /// Represents a container for tab control(s)
        /// </summary>
        public TabControlContainer()
        {
            if (ControlIDS == null)
                ControlIDS = new List<string>();
        }
        // This is our host that will contain all the controls
        [NonSerialized]
        private Control host;

        // If we have container 1 and/or container 2 this will indicates if the splitter is vert or horz
        private bool isHorizontal;
        // This is the tabs holder, we will use it when the controls count is not 0
        [NonSerialized]
        private ManagedTabControl mtc;
        // This is the split control
        [NonSerialized]
        private SplitContainer splitter;
        // This is the current page index
        [NonSerialized]
        private int currentPageIdex = -1;
        [NonSerialized]
        private bool draggingMode;
        [NonSerialized]
        private string draggedContainerID;
        [NonSerialized]
        private bool ImTheOneThatDraggedTab;
        [NonSerialized]
        private bool ImTheOneRecievedDrop;

        // Methods
        /// <summary>
        /// Refresh the view of this container by reloading all of its controls and child controls.
        /// </summary>
        /// <param name="host">The host control that will host this container.</param>
        public void RefreshView(Control host)
        {
            this.host = host;
            if (host == null)
            {
                //Trace.TraceError("TCC ERROR !! Attempting to load container to a null host !");
                return;
            }
            // Clear some stuff
            currentPageIdex = -1;
            host.Controls.Clear();
            CheckMTC(host, false, true);
            CheckSPT(host, false, true);

            // Mode 1: act like a host, no splitter.
            if (ControlIDS != null)
            {
                if (ControlIDS.Count == 0)
                    goto MAKESPLITTERS;

                CheckMTC(host, true, false);
                for (int i = 0; i < ControlIDS.Count; i++)
                {
                    AddControlToMTC(ControlIDS[i]);
                }
                return;
            }

            MAKESPLITTERS:
            // Mode 2: Act like a parent with a splitter.
            int showMode = 0;
            if (Container1 != null)
            {
                if (Container1.CanBeShown)
                    showMode |= 0x1;
            }
            if (Container2 != null)
            {
                if (Container2.CanBeShown)
                    showMode |= 0x2;
            }
            switch (showMode)
            {
                case 0:
                    {
                        // No container can be shown, this container is useless !!
                        // Add a panel with drag-drop ability so user can drop controls here
                        CheckMTC(host, true, false);

                        break;
                    }
                case 1:
                case 2:
                case 3:
                    {
                        // Both containers can be shown
                        CheckSPT(host, true, false);
                        InitializeContainer1();
                        Container1.RefreshView(splitter.Panel1);
                        InitializeContainer2();
                        Container2.RefreshView(splitter.Panel2);
                        break;
                    }
            }
            switch (showMode)
            {
                case 1:
                    {
                        splitter.Panel2Collapsed = true;
                        break;
                    }
                case 2:
                    {
                        splitter.Panel1Collapsed = true;
                        break;
                    }
            }
        }
        /// <summary>
        /// Reload splitter distances
        /// </summary>
        public void ReloadSplitters()
        {
            if (host == null)
                return;
            if (splitter == null)
                return;
            try { splitter.SplitterDistance = SplitDistance; }
            catch { }
            if (Container1 != null)
                Container1.ReloadSplitters();
            if (Container2 != null)
                Container2.ReloadSplitters();
        }
        private void InitializeContainer1()
        {
            if (Container1 == null)
                Container1 = new TabControlContainer();

            Container1.TabReordered += Container1_TabReordered;
            Container1.SelectedTabIndexChanged += Container1_SelectedTabIndexChanged;
            Container1.TabControlClosing += Container1_TabControlClosing;
            Container1.TabControlClosed += Container1_TabControlClosed;
            Container1.SplitDistanceChanged += Container1_SplitDistanceChanged;
            Container1.IsHorizontalChanged += Container1_IsHorizontalChanged;
            Container1.ControlAdded += Container1_ControlAdded;
            Container1.ControlRemoved += Container1_ControlRemoved;
            Container1.TabDragged += Container1_TabDragged;
            Container1.TabDropped += Container1_TabDropped;
            Container1.ClearContainerRequest += Container1_ClearContainerRequest;
            Container1.SelectedControlIDChanged += Container1_SelectedControlIDChanged;
        }
        private void DisposeContainer1()
        {
            if (Container1 != null)
            {
                Container1.TabReordered -= Container1_TabReordered;
                Container1.SelectedTabIndexChanged -= Container1_SelectedTabIndexChanged;
                Container1.TabControlClosing -= Container1_TabControlClosing;
                Container1.TabControlClosed -= Container1_TabControlClosed;
                Container1.SplitDistanceChanged -= Container1_SplitDistanceChanged;
                Container1.IsHorizontalChanged -= Container1_IsHorizontalChanged;
                Container1.ControlAdded -= Container1_ControlAdded;
                Container1.ControlRemoved -= Container1_ControlRemoved;
                Container1.TabDragged -= Container1_TabDragged;
                Container1.TabDropped -= Container1_TabDropped;
                Container1.ClearContainerRequest -= Container1_ClearContainerRequest;
                Container1.SelectedControlIDChanged -= Container1_SelectedControlIDChanged;
                Container1.Dispose();
            }
        }
        private void InitializeContainer2()
        {
            if (Container2 == null)
                Container2 = new TabControlContainer();

            Container2.TabReordered += Container1_TabReordered;
            Container2.SelectedTabIndexChanged += Container1_SelectedTabIndexChanged;
            Container2.TabControlClosing += Container1_TabControlClosing;
            Container2.TabControlClosed += Container1_TabControlClosed;
            Container2.SplitDistanceChanged += Container1_SplitDistanceChanged;
            Container2.IsHorizontalChanged += Container1_IsHorizontalChanged;
            Container2.ControlAdded += Container1_ControlAdded;
            Container2.ControlRemoved += Container1_ControlRemoved;
            Container2.TabDragged += Container1_TabDragged;
            Container2.TabDropped += Container1_TabDropped;
            Container2.ClearContainerRequest += Container1_ClearContainerRequest;
            Container2.SelectedControlIDChanged += Container1_SelectedControlIDChanged;
        }
        private void DisposeContainer2()
        {
            if (Container2 != null)
            {
                Container2.TabReordered -= Container1_TabReordered;
                Container2.SelectedTabIndexChanged -= Container1_SelectedTabIndexChanged;
                Container2.TabControlClosing -= Container1_TabControlClosing;
                Container2.TabControlClosed -= Container1_TabControlClosed;
                Container2.SplitDistanceChanged -= Container1_SplitDistanceChanged;
                Container2.IsHorizontalChanged -= Container1_IsHorizontalChanged;
                Container2.ControlAdded -= Container1_ControlAdded;
                Container2.ControlRemoved -= Container1_ControlRemoved;
                Container2.TabDragged -= Container1_TabDragged;
                Container2.TabDropped -= Container1_TabDropped;
                Container2.ClearContainerRequest -= Container1_ClearContainerRequest;
                Container2.SelectedControlIDChanged -= Container1_SelectedControlIDChanged;
                Container2.Dispose();
            }
        }
        /// <summary>
        /// Add new control to the controls list in this container
        /// </summary>
        /// <param name="controlID">The control id to add.</param>
        /// <param name="directShow">Indicates if the control should be shown directly otherwise will wait for a refresh.</param>
        /// <param name="useChildren">Indicates when one of the panels can be shown we cannot add control here, we need to add the control to one of the children</param>
        public void AddControl(string controlID, bool directShow, bool useChildren)
        {
            // When one of the panels can be shown we cannot add control here, we need to add the control to
            // one of the children
            if (useChildren)
            {
                if (Container1 != null)
                {
                    if (Container1.CanBeShown)
                    {
                        Trace.WriteLine("Adding control to container 1 because of this this container is in mode 2 (parent mode)");
                        Container1.AddControl(controlID, directShow, useChildren);
                        return;
                    }
                }
                if (Container2 != null)
                {
                    if (Container2.CanBeShown)
                    {
                        Trace.WriteLine("Adding control to container 2 because of this this container is in mode 2 (parent mode)");
                        Container2.AddControl(controlID, directShow, useChildren);
                        return;
                    }
                }
            }
            // Reached here means this container is the one we should add the control to !
            if (ControlIDS == null)
                ControlIDS = new List<string>();
            if (ControlIDS.Contains(controlID))
            {
                // Already in the list !!
                // Show it to user
                Trace.WriteLine("Control found here, selecting it ..");
                SelectControl(controlID);
                return;
            }
            ControlIDS.Add(controlID);
            // Add it to the mtc
            if (directShow)
            {
                Trace.WriteLine("Adding the control as tab page to mtc of this container");
                Lazy<ITabControl, IControlInfo> con = GUIService.GUI.GetTabControl(controlID);
                if (con != null)
                {
                    if (mtc == null)
                        CheckMTC(this.host, true, false);
                    AddControlToMTC(controlID);
                    SelectControl(controlID);
                    ApplyCurrentTheme();
                    Trace.TraceInformation("Control added successfully.");
                    ControlAdded?.Invoke(this, new ControlAddedRemovedArgs(controlID));
                }
                else
                {
                    Trace.TraceError("ERROR: Control id is invalid !!");
                }
            }
        }
        /// <summary>
        /// Add new control to the container 1, initiailize the container 1 if neccessary. Please don't use 
        /// this method unless you mane sure that control ids list is clear
        /// </summary>
        /// <param name="controlID">The control id to add.</param>
        /// <param name="useChildren">Indicates when one of the panels can be shown we cannot add control here, we need to add the control to one of the children</param>
        public void AddControlToContainer1(string controlID, bool useChildren)
        {
            if (ControlIDS != null)
            {
                if (ControlIDS.Count > 0)
                {
                    // Transfer the controls to the container 2
                    if (Container2 == null)
                        InitializeContainer2();
                    foreach (string cid in ControlIDS)
                    {
                        if (cid != controlID)
                            Container2.AddControl(cid, false, useChildren);
                    }
                    // Clear all controls
                    ControlIDS.Clear();
                    // return;
                }
            }
            // Now add the control to the container 1
            if (Container1 == null)
                InitializeContainer1();
            Container1.AddControl(controlID, false, useChildren);

            if (Container1 != null)
                Container1.DisposeView();
            if (Container2 != null)
                Container2.DisposeView();

            RefreshView(host);
        }
        /// <summary>
        /// Add new control to the container 2, initiailize the container 2 if neccessary. Please don't use 
        /// this method unless you mane sure that control ids list is clear
        /// </summary>
        /// <param name="controlID">The control id to add.</param>
        /// <param name="useChildren">Indicates when one of the panels can be shown we cannot add control here, we need to add the control to one of the children</param>
        public void AddControlToContainer2(string controlID, bool useChildren)
        {
            if (ControlIDS != null)
            {
                if (ControlIDS.Count > 0)
                {
                    // Transfer the controls to the container 1
                    if (Container1 == null)
                        InitializeContainer1();
                    foreach (string cid in ControlIDS)
                    {
                        if (cid != controlID)
                            Container1.AddControl(cid, false, useChildren);
                    }
                    // Clear all controls
                    ControlIDS.Clear();
                }
            }
            if (Container2 == null)
                InitializeContainer2();
            Container2.AddControl(controlID, false, useChildren);
            if (Container1 != null)
                Container1.DisposeView();
            if (Container2 != null)
                Container2.DisposeView();

            RefreshView(host);
        }
        /// <summary>
        /// Remove (hide) a control from this container or children containers.
        /// </summary>
        /// <param name="controlID">The control id to remove (hide)</param>
        public void RemoveControl(string controlID)
        {
            if (ControlIDS != null)
            {
                if (ControlIDS.Contains(controlID))
                {
                    ControlIDS.Remove(controlID);
                    if (mtc != null)
                    {
                        if (!mtc.IsDisposed)
                        {
                            for (int i = 0; i < mtc.TabPages.Count; i++)
                            {
                                if (mtc.TabPages[i].Tag.ToString() == controlID)
                                {
                                    mtc.TabPages.RemoveAt(i);
                                    if (mtc.TabPages.Count == 0)
                                    {
                                        // Clear the host !!
                                        RefreshView(host);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    ControlRemoved?.Invoke(this, new ControlAddedRemovedArgs(controlID));
                    return;
                }
            }
            if (Container1 != null)
            {
                if (Container1.ContainsControl(controlID))
                {
                    Container1.RemoveControl(controlID);
                    return;
                }
            }
            if (Container2 != null)
            {
                if (Container2.ContainsControl(controlID))
                {
                    Container2.RemoveControl(controlID);
                    return;
                }
            }
        }
        /// <summary>
        /// Get if a control is contained in this container or in one of its children
        /// </summary>
        /// <param name="controlID">The control id to search for</param>
        /// <returns>True if the control is contained otherwise false</returns>
        public bool ContainsControl(string controlID)
        {
            if (ControlIDS != null)
            {
                if (ControlIDS.Contains(controlID))
                { return true; }
            }
            if (Container1 != null)
            {
                if (Container1.ContainsControl(controlID))
                { return true; }
            }
            if (Container2 != null)
            {
                if (Container2.ContainsControl(controlID))
                { return true; }
            }
            return false;
        }
        /// <summary>
        /// Select a control and make it visible to user
        /// </summary>
        /// <param name="controlID">The control id to select</param>
        /// <returns>True if the control found and selected otherwise false.</returns>
        public bool SelectControl(string controlID)
        {
            if (Container1 != null)
            {
                if (Container1.ContainsControl(controlID))
                {
                    return Container1.SelectControl(controlID);
                }
            }
            if (Container2 != null)
            {
                if (Container2.ContainsControl(controlID))
                {
                    return Container2.SelectControl(controlID);
                }
            }
            if (ControlIDS != null)
            {
                if (ControlIDS.Contains(controlID))
                {
                    if (mtc == null)
                        return false;
                    if (mtc.IsDisposed)
                        return false;
                    for (int i = 0; i < mtc.TabPages.Count; i++)
                    {
                        if (mtc.TabPages[i].Tag.ToString() == controlID)
                        {
                            mtc.SelectedTabPageIndex = i;
                            currentPageIdex = i;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Dispose this container and release all resources
        /// </summary>
        public void Dispose()
        {
            CheckMTC(null, false, true);
            CheckSPT(null, false, true);

            DisposeContainer1();
            DisposeContainer2();
            // currentPageIdex = -1;
            // controlIDS.Clear();
            //  controlIDS = null;
            TabControlClosing = null;
            TabReordered = null;
            SplitDistanceChanged = null;
            IsHorizontalChanged = null;
            ControlAdded = null;
            ControlRemoved = null;
        }
        /// <summary>
        /// Dispose the view
        /// </summary>
        public void DisposeView()
        {
            if (host == null)
            {
                //Trace.WriteLine("TCC ERROR !! Attempting to load container to a null host !");
                return;
            }
            // Clear some stuff
            currentPageIdex = -1;
            host.Controls.Clear();
            CheckMTC(host, false, true);
            CheckSPT(host, false, true);

            if (Container1 != null)
                Container1.DisposeView();
            if (Container2 != null)
                Container2.DisposeView();
        }
        /// <summary>
        /// Fix all the panels and make ids in the write places (remove empty panels)
        /// </summary>
        public void FixPanels()
        {
            int controls = 0;
            if (Container1 != null)
                if (Container1.CanBeShown)
                    controls |= 0x1;

            if (Container2 != null)
                if (Container2.CanBeShown)
                    controls |= 0x2;

            if (ControlIDS != null)
                if (ControlIDS.Count > 0)
                    controls |= 0x4;

            if ((controls & 4) != 4)
            {
                // We should be using splitter here ...
                if (controls == 1)// Only container 1 had controls ...
                {
                    if (Container1.ControlIDS.Count > 0)
                    {
                        ControlIDS = new List<string>(Container1.ControlIDS);
                        Container1.ControlIDS.Clear();
                        Container1.FixPanels();
                    }
                }
                else if (controls == 2)// Only container 2 had controls ...
                {
                    if (Container2.ControlIDS.Count > 0)
                    {
                        ControlIDS = new List<string>(Container2.ControlIDS);
                        Container2.ControlIDS.Clear();
                        Container2.FixPanels();
                    }
                }
                else if (controls == 3)// 2 containers can be shown .. fix both
                {
                    Container1.FixPanels();
                    Container2.FixPanels();
                }
            }
        }
        /// <summary>
        /// Load a tbr map from file
        /// </summary>
        /// <param name="filePath">The complete file path</param>
        /// <param name="success">Set to true when the load successed. Otherwise set to false.</param>
        /// <returns>The tbr map loaded if load is successed. </returns>
        public static TabControlContainer LoadTCCMap(string filePath, out bool success)
        {
            success = false;
            Trace.WriteLine(Properties.Resources.Status_LoadingTCCMap + " " + filePath + " ...");
            if (!File.Exists(filePath))
            {
                Trace.WriteLine(Properties.Resources.Status_UnableToLoadTCCMapFile +
                    ", " + Properties.Resources.Status_fileIsNotExistAt + " " + filePath,StatusMode.Error);
                return null;
            }
            XmlReaderSettings sett = new XmlReaderSettings();
            sett.DtdProcessing = DtdProcessing.Ignore;
            sett.IgnoreWhitespace = true;
            XmlReader XMLread = XmlReader.Create(filePath, sett);
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(TabControlContainer));

                TabControlContainer map = (TabControlContainer)ser.Deserialize(XMLread);

                XMLread.Close();
                Trace.WriteLine(Properties.Resources.Status_TCCLoadedSuccessAt + " " + filePath + ".", StatusMode.Error);
                success = map != null;
                if (map == null)
                {
                    Trace.WriteLine(Properties.Resources.Status_UnableToLoadTCCMapFile + " " + filePath + ": " + Properties.Resources.Status_FileIsDamagedOrNotTCC, StatusMode.Error);
                    return null;
                }
                return map;
            }
            catch (Exception ex)
            {
                XMLread.Close();
                Trace.WriteLine(Properties.Resources.Status_UnableToLoadTCCMapFile + " " + filePath + ": " + ex.Message + "/n" + ex.ToString(), StatusMode.Error);
            }
            return null;
        }
        /// <summary>
        /// Save a toolbar representators map into file
        /// </summary>
        /// <param name="filePath">The complete path where to save the map file.</param>
        /// <param name="map">The toolbar representators map to save.</param>
        /// <returns>True if save operation successed otherwise false.</returns>
        public static bool SaveTCCMap(string filePath, TabControlContainer map)
        {
            Trace.WriteLine(Properties.Resources.Status_SavingTCCAt + ": " + filePath + " ...");

            try
            {
                XmlWriterSettings sett = new XmlWriterSettings();
                sett.Indent = true;
                XmlWriter XMLwrt = XmlWriter.Create(filePath, sett);
                XmlSerializer ser = new XmlSerializer(typeof(TabControlContainer));

                ser.Serialize(XMLwrt, map);
                XMLwrt.Flush();
                XMLwrt.Close();

                Trace.WriteLine(Properties.Resources.Status_TCCSavedAt + " " + filePath + ".", StatusMode.Error);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(Properties.Resources.Status_UnableToSaveTheMapFile + " " + filePath + ": " + ex.Message + "/n" + ex.ToString(), StatusMode.Error);
            }
            return false;
        }
        /// <summary>
        /// Dispose mtc then create new mtc
        /// </summary>
        /// <param name="host">The host that will contain the mtc</param>
        /// <param name="createNew">Indicates if a new mtc should be created or just dispose current mtc.</param>
        /// <param name="disposeMTC">Indicates if the container should dispose the mtc first</param>
        private void CheckMTC(Control host, bool createNew, bool disposeMTC)
        {
            // Dispose the old one
            if (mtc != null && disposeMTC)
            {
                // Clear all pages and tell the controls about it
                foreach (MTCTabPage page in mtc.TabPages)
                {
                    Lazy<ITabControl, IControlInfo> con = GUIService.GUI.GetTabControl(page.Tag.ToString());
                    if (con != null)
                    {
                        con.Value.OnTabClose();
                        //     con.Value.Parent = null;
                    }
                    // page.Panel.Controls.Clear();
                    //page.Panel.Dispose();
                }
                mtc.TabPages.Clear();
                mtc.TabPageClosing -= Mtc_TabPageClosing;
                mtc.TabPageClosed -= Mtc_TabPageClosed;
                mtc.AfterTabPageReorder -= Mtc_TabReordered;
                mtc.SelectedTabPageIndexChanged -= Mtc_SelectedTabPageIndexChanged;
                mtc.TabPageDrag -= Mtc_TabPageDrag;
                mtc.TabDrop -= Mtc_TabDrop;
                mtc.Dispose();
                mtc = null;
            }
            if (host == null)
            {
                //Trace.WriteLine("TCC ERROR !! Attempting to load container to a null host !");
                return;
            }
            host.Controls.Clear();
            if (!createNew)
                return;
            // Create new
            mtc = new ManagedTabControl();
            mtc.ImagesList = new ImageList();
            mtc.ImagesList.ColorDepth = ColorDepth.Depth32Bit;
            host.Controls.Add(mtc);

            //mtc.ImagesList = null;
            mtc.Dock = DockStyle.Fill;
            mtc.AllowAutoTabPageDragAndDrop = false;
            // mtc.AllowDrop = true;
            mtc.DrawStyle = MTCDrawStyle.Normal;
            mtc.CloseBoxAlwaysVisible = false;
            mtc.TabPageMaxWidth = 250;
            mtc.ShowTabPageToolTip = true;
            //mtc.ShowTabPageToolTipAlways = true;

            // Events
            mtc.TabPageClosing += Mtc_TabPageClosing;
            mtc.TabPageClosed += Mtc_TabPageClosed;
            mtc.AfterTabPageReorder += Mtc_TabReordered;
            mtc.SelectedTabPageIndexChanged += Mtc_SelectedTabPageIndexChanged;
            mtc.TabPageDrag += Mtc_TabPageDrag;
            mtc.TabDrop += Mtc_TabDrop;
        }
        private void CheckSPT(Control host, bool createNew, bool disposeSPL)
        {
            if (disposeSPL)
            {
                if (host != null)
                {
                    if (splitter != null)
                        host.Controls.Remove(host);
                }
                if (splitter != null)
                {
                    splitter.Dispose();
                }
            }
            if (host == null)
                return;
            if (createNew)
            {
                splitter = new SplitContainer();
                splitter.Dock = DockStyle.Fill;
                host.Controls.Add(splitter);
                splitter.FixedPanel = FixedPanel.Panel1;
                // Horizontal or vertical ?
                splitter.Orientation = isHorizontal ? Orientation.Horizontal : Orientation.Vertical;
                try { splitter.SplitterDistance = SplitDistance; }
                catch { }
                splitter.SplitterMoved += Splitter_SplitterMoved;
            }
        }
        private void AddControlToMTC(string controlID)
        {
            if (host == null)
                return;
            if (mtc == null)
                return;
            if (mtc.IsDisposed)
                return;
            // Get the control
            Lazy<ITabControl, IControlInfo> con = GUIService.GUI.GetTabControl(controlID);
            if (con != null)
            {
                MTCTabPage page = new MTCTabPage();
                page.DrawType = MTCTabPageDrawType.TextAndImage;
                page.Text = con.Value.DisplayName;
                //page.Tooltip = con.Value.ToolTip;
                page.Panel = new Panel();
                page.Panel.Controls.Add(con.Value);
                page.Panel.Enter += Panel_Enter;
                foreach (Control pcon in con.Value.Controls)
                {
                    if (pcon is Panel)
                    {
                        pcon.Enter += Panel_Enter;
                        pcon.Click += Panel_Enter;
                    }
                }
                if (con.Value.Icon == null)
                {
                    page.ImageIndex = -1;
                }
                else
                {
                    mtc.ImagesList.Images.Add(con.Value.Icon);
                    page.ImageIndex = mtc.ImagesList.Images.Count - 1;
                }
                con.Value.Dock = DockStyle.Fill;
                con.Value.AllowDrop = true;
                //con.Value.DragOver += mtc_DragOver;
                //con.Value.DragDrop += mtc_DragDrop;
                page.Tag = controlID;
                mtc.TabPages.Add(page);
            }
        }
        /// <summary>
        /// Clear all controls
        /// </summary>
        /// <param name="willBeStillActive"></param>
        public void ClearContainer(bool willBeStillActive)
        {
            if (Container1 != null || Container2 != null)
            {
                int controls = 0;
                if (Container1.CanBeShown)
                    controls |= 1;
                if (Container2.CanBeShown)
                    controls |= 2;

                switch (controls)
                {
                    case 0:
                        {
                            // Remove the splitter
                            CheckSPT(host, false, true);
                            DisposeContainer1();
                            DisposeContainer2();
                            // Add draggable mtc
                            if (willBeStillActive)
                            {
                                CheckMTC(host, true, false);

                                for (int i = 0; i < ControlIDS.Count; i++)
                                {
                                    AddControlToMTC(ControlIDS[i]);
                                }
                            }
                            else
                            {
                                CheckMTC(host, false, true);
                                Container1 = Container2 = null;
                            }

                            ApplyCurrentTheme();
                            break;
                        }
                    case 1:
                        {
                            splitter.Panel2Collapsed = true;
                            break;
                        }
                    case 2:
                        {
                            splitter.Panel1Collapsed = true;
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// Called by the handler when a tab is dragged to handle the dragging
        /// </summary>
        public void EnterDraggingMode(string draggedContainerID)
        {
            this.draggedContainerID = draggedContainerID;
            if (draggingMode)
                return;
            draggingMode = true;
            // We only accept draggin page when the container is showing mtc
            if (mtc != null)
            {
                // Make the mtc enters the dragging mode
                mtc.EnterDraggingMode();
            }
            if (Container1 != null)
                Container1.EnterDraggingMode(draggedContainerID);
            if (Container2 != null)
                Container2.EnterDraggingMode(draggedContainerID);
        }
        /// <summary>
        /// Called by the handler to handle the leave of the dragging mode
        /// </summary>
        public void LeaveDraggingMode(int mode)
        {
            // We only accept draggin page when the container is showing mtc
            if (draggingMode)
            {
                if (mtc != null)
                {
                    // Make the mtc enters the dragging mode
                    mtc.LeaveDraggingMode();
                    if (mode > 0)
                    {
                        // Check if this is the control that dragged the tab page !
                        if (ImTheOneThatDraggedTab && !ImTheOneRecievedDrop && draggedContainerID != "")
                        {
                            if (ControlIDS.Contains(draggedContainerID))
                            {
                                ControlIDS.Remove(draggedContainerID);
                                foreach (MTCTabPage page in mtc.TabPages)
                                {
                                    if (page.Tag.ToString() == draggedContainerID)
                                    {
                                        mtc.TabPages.Remove(page);
                                        break;
                                    }
                                }
                            }
                        }
                        if (mtc.TabPages.Count == 0)
                        {
                            // Clear the host !!
                            RefreshView(host);
                        }
                    }
                }
                if (Container1 != null)
                    Container1.LeaveDraggingMode(mode);
                if (Container2 != null)
                    Container2.LeaveDraggingMode(mode);

                ClearContainer(true);
                ImTheOneThatDraggedTab = ImTheOneRecievedDrop = false;
                draggingMode = false;
                draggedContainerID = "";
            }
        }
        /// <summary>
        /// Apply current theme of the GUI to this container
        /// </summary>
        public void ApplyCurrentTheme()
        {
            ApplTheme(GUIService.GUI.CurrentTheme);
        }
        /// <summary>
        /// Apply a theme
        /// </summary>
        /// <param name="theme">The theme to apply</param>
        public void ApplTheme(Theme theme)
        {
            if (theme == null)
                return;
            if (mtc != null)
            {
                mtc.TabPageColor = Color.FromArgb(theme.TabPageColor);
                mtc.TabPageSelectedColor = Color.FromArgb(theme.TabPageSelectedColor);
                mtc.TabPageSplitColor = Color.FromArgb(theme.TabPageSplitColor);
                mtc.TabPageHighlightedColor = Color.FromArgb(theme.TabPageHighlightedColor);
                mtc.TabPageActiveColor = Color.FromArgb(theme.TabPageSelectedActiveColor);
                mtc.ForeColor = Color.FromArgb(theme.PanelTextsColor);
                mtc.BackColor = Color.FromArgb(theme.PanelsBackColor);

                mtc.CloseBoxOnEachPageVisible = theme.PagesCanBeClosed;
                mtc.AllowTabPagesReorder = theme.PagesCanBeReordered;
                mtc.AllowTabPageDragAndDrop = theme.PagesCanBeDragged;
            }
            // Apply to child
            if (Container1 != null)
                Container1.ApplTheme(theme);
            if (Container2 != null)
                Container2.ApplTheme(theme);
        }
        /// <summary>
        /// Check active control
        /// </summary>
        public void CheckActiveControl()
        {
            if (Container1 != null)
                Container1.CheckActiveControl();

            if (Container2 != null)
                Container2.CheckActiveControl();

            if (mtc == null)
                return;
            if (mtc.IsDisposed)
                return;
            mtc.TabPageActive = false;
            mtc.ActiveTabPageIndex = -1;
            if (ControlIDS != null)
            {
                if (ControlIDS.Contains(GUIService.GUI.SelectedTabControlID))
                {
                    for (int i = 0; i < mtc.TabPages.Count; i++)
                    {
                        if (mtc.TabPages[i].Tag.ToString() == GUIService.GUI.SelectedTabControlID)
                        {
                            mtc.TabPageActive = true;
                            mtc.ActiveTabPageIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        // Properties
        /// <summary>
        /// Get or set the split distance (if splitter is used)
        /// </summary>
        public int SplitDistance
        { get; set; }
        /// <summary>
        /// Get if this container have controls or not (child containers are not included).
        /// </summary>
        public bool HasControls
        {
            get
            {
                if (ControlIDS != null)
                    if (ControlIDS.Count > 0)
                        return true;
                return false;
            }
        }
        /// <summary>
        /// Get if this control can be shown to user
        /// </summary>
        public bool CanBeShown
        {
            get
            {
                // It can be shown if it has/have control(s)
                if (HasControls)
                    return true;
                // It can be shown if one of the containers at least can be shown
                if (Container1 != null)
                    if (Container1.CanBeShown)
                        return true;
                if (Container2 != null)
                    if (Container2.CanBeShown)
                        return true;
                // This container is useless and cannot be shown.
                return false;
            }
        }
        /// <summary>
        /// Get or set if this control should be in horizontal (if true) layout or vertical (when false)
        /// </summary>
        public bool IsHorizontal
        {
            get
            { return isHorizontal; }
            set
            {
                isHorizontal = value;
                IsHorizontalChanged?.Invoke(this, new EventArgs());
                // Apply
                if (splitter != null)
                    splitter.Orientation = isHorizontal ? Orientation.Horizontal : Orientation.Vertical;
            }
        }
        /// <summary>
        /// Get the control ids that are used in this container. Never edit this unless you know what are you doing !!
        /// </summary>
        public List<string> ControlIDS
        { get; set; }
        // Here are the controls we want to show to the user in this container.
        /// <summary>
        /// The container 1, will be in the left, in the top if isHorizontal is set.
        /// </summary>
        public TabControlContainer Container1
        { get; set; }
        /// <summary>
        /// The container 2, will be in the right, in the bottom if isHorizontal is set.
        /// </summary>
        public TabControlContainer Container2
        { get; set; }
        // Events
        /// <summary>
        /// Raised when a tab control is about to get closed with an option in the args to cancel.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<TabControlClosedArgs> TabControlClosing;
        /// <summary>
        /// Raised when a tab control is closed.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler TabControlClosed;
        /// <summary>
        /// Raised when a tab control reordered
        /// </summary>
        [field: NonSerialized]
        public event EventHandler TabReordered;
        /// <summary>
        /// Raised when a tab dragged to inform the parent container or handler to handle the dragging event.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<ControlAddedRemovedArgs> TabDragged;
        /// <summary>
        /// Raised when a tab dropped
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<TabControlDroppedArgs> TabDropped;
        /// <summary>
        /// Raised when a tab control selected index changed
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SelectedTabIndexChanged;
        /// <summary>
        /// Raised when the splitter distance is changed (if a splitter is used)
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SplitDistanceChanged;
        /// <summary>
        /// Raised when IsHorizontal is changed.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler IsHorizontalChanged;
        /// <summary>
        /// Raised when a control added successfuly to this contrainer.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<ControlAddedRemovedArgs> ControlAdded;
        /// <summary>
        /// Raised when a control removed from the control ids list
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<ControlAddedRemovedArgs> ControlRemoved;
        /// <summary>
        /// Raised when this container needs to be cleared
        /// </summary>
        [field: NonSerialized]
        public event EventHandler ClearContainerRequest;
        /// <summary>
        /// Raised when the selected control id changed
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SelectedControlIDChanged;

        private void Mtc_TabPageClosing(object sender, MTCTabPageCloseArgs e)
        {
            string id = mtc.TabPages[e.TabPageIndex].Tag.ToString();
            TabControlClosedArgs args = new TabControlClosedArgs(id);

            TabControlClosing?.Invoke(this, args);

            if (args.Cancel)
            {
                e.Cancel = true;
                return;
            }

            // Tell the control that we are closing it
            Lazy<ITabControl, IControlInfo> con = GUIService.GUI.GetTabControl(id);
            if (con != null)
            {
                con.Value.OnTabClose();
            }
            // Now remove it from the list
            if (ControlIDS != null)
                ControlIDS.Remove(id);
        }
        private void Mtc_TabPageClosed(object sender, MTCTabPageCloseArgs e)
        {
            if (mtc.TabPages.Count == 0)
            {
                // Clear the host !!
                RefreshView(host);
            }
            TabControlClosed?.Invoke(this, new EventArgs());
        }
        private void Mtc_TabReordered(object sender, EventArgs e)
        {
            // Clear all
            ControlIDS.Clear();
            // Add page by page
            foreach (MTCTabPage t in mtc.TabPages)
                ControlIDS.Add((string)t.Tag);
            // Raise the event
            TabReordered?.Invoke(this, new EventArgs());
        }
        private void Mtc_SelectedTabPageIndexChanged(object sender, EventArgs e)
        {
            // Tell the old one that we are hiding it
            if (currentPageIdex >= 0 && currentPageIdex < ControlIDS.Count)
            {
                Lazy<ITabControl, IControlInfo> con = GUIService.GUI.GetTabControl(ControlIDS[currentPageIdex]);
                if (con != null)
                {
                    con.Value.OnHide();
                }
            }
            // Tell the current one that we are displaying it
            Lazy<ITabControl, IControlInfo> con1 = GUIService.GUI.GetTabControl(mtc.TabPages[mtc.SelectedTabPageIndex].Tag.ToString());
            if (con1 != null)
            {
                con1.Value.OnDisplay();
            }
            // Update
            currentPageIdex = mtc.SelectedTabPageIndex;

            // Raise the event
            SelectedTabIndexChanged?.Invoke(this, new EventArgs());
        }
        private void Mtc_ControlCloseRequest(object sender, EventArgs e)
        {
            ClearContainerRequest?.Invoke(this, new EventArgs());
        }
        private void Mtc_TabPageDrag(object sender, MTCTabPageDragArgs e)
        {
            if (draggingMode)
                return;
            ImTheOneThatDraggedTab = true;
            string id = mtc.TabPages[e.TabPageIndex].Tag.ToString();
            TabDragged?.Invoke(sender, new ControlAddedRemovedArgs(id));
        }
        private void Mtc_TabDrop(object sender, MTCDropRequestEventArgs e)
        {
            if (!draggingMode)
                return;
            // The page has been dropped here !!
            /*
             *     +---------------+
             *     |      1        |
             * +---+---------------+---+
             * |   |               |   |
             * | 2 |      5        | 4 |
             * |   |               |   |
             * +---+---------------+---+
             *     |      3        |  
             *     +---------------+
             */

            // 1 Add the control
            switch (e.DropLocation)
            {
                case 1:
                    {
                        AddControlToContainer1(draggedContainerID, false);
                        this.IsHorizontal = true;
                        break;
                    }
                case 2:
                    {
                        AddControlToContainer1(draggedContainerID, false);
                        this.IsHorizontal = false;
                        break;
                    }
                case 3:
                    {
                        AddControlToContainer2(draggedContainerID, false);
                        this.IsHorizontal = true;
                        break;
                    }
                case 4:
                    {
                        AddControlToContainer2(draggedContainerID, false);
                        this.IsHorizontal = false;
                        break;
                    }
                case 5:
                    {
                        if (!ImTheOneThatDraggedTab)
                            AddControl(draggedContainerID, true, false);

                        ImTheOneRecievedDrop = true;
                        break;
                    }

            }

            // Raise the event !!
            TabDropped?.Invoke(sender, new TabControlDroppedArgs(draggedContainerID, e.DropLocation));
            // Leave the dragging mode
            LeaveDraggingMode(e.DropLocation);
        }
        private void Container1_ControlAdded(object sender, ControlAddedRemovedArgs e)
        {
            ControlAdded?.Invoke(sender, e);
        }
        private void Container1_ControlRemoved(object sender, ControlAddedRemovedArgs e)
        {
            ControlRemoved?.Invoke(sender, e);
        }
        private void Container1_ReuqestRefresh(object sender, EventArgs e)
        {
        }
        private void Container1_TabReordered(object sender, EventArgs e)
        {
            TabReordered?.Invoke(sender, new EventArgs());
        }
        private void Container1_SelectedTabIndexChanged(object sender, EventArgs e)
        {
            SelectedTabIndexChanged?.Invoke(sender, e);
        }
        private void Container1_TabControlClosing(object sender, TabControlClosedArgs e)
        {
            TabControlClosing?.Invoke(sender, e);
        }
        private void Container1_TabControlClosed(object sender, EventArgs e)
        {
            ClearContainer(true);
            TabControlClosed?.Invoke(sender, e);
        }
        private void Splitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SplitDistance = splitter.SplitterDistance;
            SplitDistanceChanged?.Invoke(this, new EventArgs());
        }
        private void Container1_SplitDistanceChanged(object sender, EventArgs e)
        {
            SplitDistanceChanged?.Invoke(sender, new EventArgs());
        }
        private void Container1_IsHorizontalChanged(object sender, EventArgs e)
        {
            IsHorizontalChanged?.Invoke(sender, new EventArgs());
        }
        private void Container1_TabDragged(object sender, ControlAddedRemovedArgs e)
        {
            if (draggingMode)
                return;
            TabDragged?.Invoke(sender, e);
            // Leave the handling for the handler.
        }
        private void Container1_TabDropped(object sender, TabControlDroppedArgs e)
        {
            if (!draggingMode)
                return;
            // Raise the event !!
            TabDropped?.Invoke(sender, new TabControlDroppedArgs(draggedContainerID, e.DraggingMode));
            // Leave the dragging mode
            LeaveDraggingMode(e.DraggingMode);
        }
        private void Container1_ClearContainerRequest(object sender, EventArgs e)
        {
            if (Container1 != null)
            {
                if (Container2 != null)
                {
                    // We have both containers loaded
                    if (Container1.CanBeShown && !Container2.CanBeShown)
                    {
                        // Tnasfer the controls from the container 1 to this container, dispose the container 2
                        // 1 Dispose the container 2
                        DisposeContainer2();
                        // 2 Get the container 1 controls and containers
                        Container1.DisposeView();
                        this.ControlIDS = Container1.ControlIDS;
                        this.Container2 = Container1.Container2;
                        this.Container1 = Container1.Container1;

                        RefreshView(host);
                        return;
                    }
                    else if (!Container1.CanBeShown && Container2.CanBeShown)
                    {
                        // Tnasfer the controls from the container 2 to this container, dispose the container 1
                        // 1 Dispose the container 1
                        DisposeContainer1();
                        // 2 Get the container 2 controls and containers
                        Container2.DisposeView();
                        this.ControlIDS = Container2.ControlIDS;
                        this.Container1 = Container2.Container1;
                        this.Container2 = Container2.Container2;

                        RefreshView(host);
                        return;
                    }
                }
            }
            // Reached here means something is worng about the map.
            ClearContainer(true);
        }
        private void Panel_Enter(object sender, EventArgs e)
        {
            /*if (sender is Panel)
            {
                if (!((Panel)sender).IsDisposed)
                {
                    if (GUIService.GUI.SelectedTabControlID != ((Panel)sender).Tag.ToString())
                    {
                        GUIService.GUI.SelectedTabControlID = ((Panel)sender).Tag.ToString();
                        SelectedControlIDChanged?.Invoke(sender, new EventArgs());
                        CheckActiveControl();
                    }
                }
            }*/
            if (mtc == null)
                return;
            MTCTabPage page = mtc.SelectedTabPage;
            if (page == null)
                return;
            string id = page.Tag.ToString();
            if (GUIService.GUI.SelectedTabControlID != id)
            {
                GUIService.GUI.SelectedTabControlID = id;
                SelectedControlIDChanged?.Invoke(sender, new EventArgs());
                CheckActiveControl();
            }
        }
        private void Container1_SelectedControlIDChanged(object sender, EventArgs e)
        {
            SelectedControlIDChanged?.Invoke(sender, new EventArgs());
            CheckActiveControl();
        }
    }
}
