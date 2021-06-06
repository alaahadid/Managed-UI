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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ManagedUI
{
    /// <summary>
    /// Manage Tab Control
    /// </summary>
    public partial class ManagedTabControl : UserControl
    {
        /// <summary>
        /// Manage Tab Control
        /// </summary>
        public ManagedTabControl()
        {
            InitializeComponent(); CheckPages();
        }

        private bool closeTabOnCloseButtonClick = true;
        private bool showTabPageToolTip = true;
        private Panel current_control;
        private bool DraggingMODE;

        #region Properties
        /// <summary>
        /// Get or set the Managed Tab Control Tab Page collection
        /// </summary>
        [Description("Tab pages collection"), Category("ManagedTabControl")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MTCTabPagesCollection TabPages
        {
            get { return managedTabControlPanel1.TabPages; }
            set { value.OWNER = managedTabControlPanel1; managedTabControlPanel1.TabPages = value; }
        }
        /// <summary>
        /// Get or set the selected tab page index.
        /// </summary>
        [Browsable(false), Description("Selected tab page index"), Category("ManagedTabControl")]
        public int SelectedTabPageIndex
        {
            get { return managedTabControlPanel1.SelectedTabPageIndex; }
            set
            {
                managedTabControlPanel1.SelectedTabPageIndex = value;
                managedTabControlPanel1_SelectedTabPageIndexChanged(this, null);
                Invalidate();
            }
        }
        /// <summary>
        /// Get the selected tab page (calculated)
        /// </summary>
        [Browsable(false), Description("Selected tab page (calculated)"), Category("ManagedTabControl")]
        public MTCTabPage SelectedTabPage
        {
            get
            {
                if (managedTabControlPanel1.SelectedTabPageIndex >= 0 && managedTabControlPanel1.SelectedTabPageIndex < TabPages.Count)
                    return TabPages[managedTabControlPanel1.SelectedTabPageIndex];
                return null;
            }
        }
        /// <summary>
        /// Get or set the active tab index
        /// </summary>
        [Browsable(false), Description("Active tab page index"), Category("ManagedTabControl")]
        public int ActiveTabPageIndex
        {
            get { return managedTabControlPanel1.activeTabIndex; }
            set
            {
                managedTabControlPanel1.activeTabIndex = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Get or set the tab page color that will be used when drawing a tab page normally (not selected)
        /// </summary>
        [Description("Tab page color. The color that will be used when drawing a tab page normally (not selected)"), Category("ManagedTabControl")]
        public Color TabPageColor
        {
            get { return managedTabControlPanel1.TabPageColor; }
            set
            {
                managedTabControlPanel1.TabPageColor = value;
                managedTabControlPanel1.Invalidate();
            }
        }
        /// <summary>
        /// Get or set the color that will be used when drawing a tab page that is selected
        /// </summary>
        [Description("Tab page selected color. The color that will be used when drawing a tab page that is selected"), Category("ManagedTabControl")]
        public Color TabPageSelectedColor
        {
            get { return managedTabControlPanel1.TabPageSelectedColor; }
            set
            {
                managedTabControlPanel1.TabPageSelectedColor = value;
                managedTabControlPanel1.Invalidate();
            }
        }
        /// <summary>
        /// Get or set the color that will be used when the mouse cursor get over a tab page
        /// </summary>
        [Description("Tab page selected color. The color that will be used when the mouse cursor get over a tab page"), Category("ManagedTabControl")]
        public Color TabPageHighlightedColor
        { get { return managedTabControlPanel1.TabPageHighlightedColor; } set { managedTabControlPanel1.TabPageHighlightedColor = value; managedTabControlPanel1.Invalidate(); } }
        /// <summary>
        /// Get or set the color of the tab page splinters.
        /// </summary>
        [Description("The color of the tab page splitters."), Category("ManagedTabControl")]
        public Color TabPageSplitColor
        { get { return managedTabControlPanel1.TabPageSplitColor; } set { managedTabControlPanel1.TabPageSplitColor = value; managedTabControlPanel1.Invalidate(); } }
        /// <summary>
        /// Get or set the color when the control is active (TabPageActive = true).
        /// </summary>
        [Description("Get or set the color when the control is active (ActiveControl = true)."), Category("ManagedTabControl")]
        public Color TabPageActiveColor
        { get { return managedTabControlPanel1.TabPageActiveColor; } set { managedTabControlPanel1.TabPageActiveColor = value; managedTabControlPanel1.Invalidate(); } }
        /// <summary>
        /// Get or set if the control is active.
        /// </summary>
        [Description("Get or set if the control is active."), Category("ManagedTabControl")]
        public bool TabPageActive
        { get { return managedTabControlPanel1.TabPageActiveControl; } set { managedTabControlPanel1.TabPageActiveControl = value; managedTabControlPanel1.Invalidate(); } }
        /// <summary>
        /// Get or set a value indicates if the close button should be visible on each tab page
        /// </summary>
        [Description("Indicate if the close button should be visible on each tab page"), Category("ManagedTabControl")]
        public bool CloseBoxOnEachPageVisible
        { get { return managedTabControlPanel1.DrawCloseBoxOnEachPage; } set { managedTabControlPanel1.DrawCloseBoxOnEachPage = value; managedTabControlPanel1.Invalidate(); } }
        /// <summary>
        /// Get or set if the close button (if enabled via CloseBoxOnEachPageVisible) should be always visible on tab page with black color when the mouse cursor is not over that tab page.
        /// </summary>
        [Description("Indicate if the close button (if enabled via CloseBoxOnEachPageVisible) should be always visible on tab page with black color when the mouse cursor is not over that tab page."), Category("ManagedTabControl")]
        public bool CloseBoxAlwaysVisible
        {
            get { return managedTabControlPanel1.CloseBoxAlwaysVisible; }
            set { managedTabControlPanel1.CloseBoxAlwaysVisible = value; managedTabControlPanel1.Invalidate(); }
        }
        /// <summary>
        /// Get or set a value indicates if tab page can get highlighted when the mouse cursor get over it
        /// </summary>
        [Description("Indicate if tab page can get highlighted when the mouse cursor get over it"), Category("ManagedTabControl")]
        public bool DrawTabPageHighlight
        { get { return managedTabControlPanel1.DrawTabPageHighlight; } set { managedTabControlPanel1.DrawTabPageHighlight = value; managedTabControlPanel1.Invalidate(); } }
        /// <summary>
        /// Get or set if the tab pages can be reordered
        /// </summary>
        [Description("Indicate if tab page can be reordered"), Category("ManagedTabControl")]
        public bool AllowTabPagesReorder
        { get { return managedTabControlPanel1.AllowTabPagesReorder; } set { managedTabControlPanel1.AllowTabPagesReorder = value; managedTabControlPanel1.Invalidate(); } }
        /// <summary>
        /// Get or set the max width of a tab page that can get (range 50-500, default=250).
        /// </summary>
        [Description("The max width of a tab page that can get (range 50-500, default=250). This value is important when a tab page has large text so the tab page limit the width of itself to this value when caluclating width depending on string value."), Category("ManagedTabControl")]
        public int TabPageMaxWidth
        {
            get { return managedTabControlPanel1.TabPageMaxWidth; }
            set
            {
                if (value < 50)
                    value = 50;
                if (value > 500)
                    value = 500;
                managedTabControlPanel1.TabPageMaxWidth = value;
                managedTabControlPanel1.Invalidate();
            }
        }
        /// <summary>
        /// Get or set a value Indicate whether the control can perform tab page drag automatically with (Move) effect. Otherwise the event 'TabPageDrag' will raise instead and you should do drag manully.
        /// </summary>
        [Description("Indicate whether the control can perform tab page drag automatically with (Move) effect. Otherwise the event 'TabPageDrag' will raise instead and you should do drag manually."), Category("ManagedTabControl")]
        public bool AllowAutoTabPageDragAndDrop
        { get { return managedTabControlPanel1.AllowAutoTabPageDragAndDrop; } set { managedTabControlPanel1.AllowAutoTabPageDragAndDrop = value; managedTabControlPanel1.Invalidate(); } }
        /// <summary>
        /// Get or set a value Indicate whether the control can perform tab page drag.
        /// </summary>
        [Description("Indicate whether the control can perform tab page drag. If false, the 'AllowAutoTabPageDragAndDrop' get disable."), Category("ManagedTabControl")]
        public bool AllowTabPageDragAndDrop
        { get { return managedTabControlPanel1.AllowTabPageDragAndDrop; } set { managedTabControlPanel1.AllowTabPageDragAndDrop = value; managedTabControlPanel1.Invalidate(); } }
        /// <summary>
        /// Get or set if a tab page should close automatically when the user click on the close button of that tab page.
        /// </summary>
        [Description("Indicate if a tab page should close automatically when the user click on the close button of that tab page."), Category("ManagedTabControl")]
        public bool CloseTabOnCloseButtonClick
        { get { return closeTabOnCloseButtonClick; } set { closeTabOnCloseButtonClick = value; } }
        /// <summary>
        /// Get or set if the control can show a tool tip of a tab page that the text of it is not completely visible due to long char length of that text.
        /// </summary>
        [Description("Indicate if the control can show a tool tip of a tab page that the text of it is not completely visible due to large string length of that text."), Category("ManagedTabControl")]
        public bool ShowTabPageToolTip
        { get { return showTabPageToolTip; } set { showTabPageToolTip = value; } }
        /// <summary>
        /// Get or set if the control should show a tool tip of a tab page when get highlighted otherwise the tool tip get shown only when the text of that tab page is too long. ShowTabPageToolTip must be enabled
        /// </summary>
        [Description("Indicate if the control should show a tool tip of a tab page when get highlighted otherwise the tool tip get shown only when the text of that tab page is too long. ShowTabPageToolTip must be enabled."), Category("ManagedTabControl")]
        public bool ShowTabPageToolTipAlways
        { get { return managedTabControlPanel1.AlwaysShowToolTip; } set { managedTabControlPanel1.AlwaysShowToolTip = value; managedTabControlPanel1.Invalidate(); } }
        /// <summary>
        /// The images list that will be used for draw
        /// </summary>
        [Description("The images list that will be used for draw"), Category("ManagedTabControl")]
        public ImageList ImagesList
        { get { return managedTabControlPanel1.ImagesList; } set { managedTabControlPanel1.ImagesList = value; } }
        /// <summary>
        /// Get or set the font
        /// </summary>
        [Description("The font of texts of the tab pages."), Category("ManagedTabControl")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                managedTabControlPanel1.Font = value;
                managedTabControlPanel1.Invalidate();
            }
        }
        /// <summary>
        /// Get or set the draw style that should be used when drawing tab pages normally (when tab page is not selected or highlighted)
        /// </summary>
        [Description("The draw style that should be used when drawing tab pages normally (when tab page is not selected or highlighted)"), Category("ManagedTabControl")]
        public MTCDrawStyle DrawStyle
        { get { return managedTabControlPanel1.drawStyle; } set { managedTabControlPanel1.drawStyle = value; managedTabControlPanel1.Invalidate(); } }
        /// <summary>
        /// If enabled, the control will select added tab page automatically after adding it
        /// </summary>
        [Description("Indicate if the control should select added tab page automatically after adding it."), Category("ManagedTabControl")]
        public bool AutoSelectAddedTabPageAfterAddingIt
        { get { return managedTabControlPanel1.AutoSelectAddedTabPage; } set { managedTabControlPanel1.AutoSelectAddedTabPage = value; managedTabControlPanel1.Invalidate(); } }
        /// <summary>
        /// Get or set the color of texts of the tab pages.
        /// </summary>
        [Description("The color of texts of the tab pages."), Category("ManagedTabControl")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                managedTabControlPanel1.ForeColor = value;
                managedTabControlDragPanel1.ForeColor = value;
                managedTabControlPanel1.Invalidate();
            }
        }
        /// <summary>
        /// Get or set the background color
        /// </summary>
        [Description("The background color of the control."), Category("ManagedTabControl")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                panel_controls.BackColor = managedTabControlPanel1.BackColor = managedTabControlDragPanel1.BackColor = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Raised when the SelectedTabPageIndex value changed.
        /// </summary>
        [Description("Raised when the SelectedTabPageIndex value changed."), Category("ManagedTabControl")]
        public event EventHandler SelectedTabPageIndexChanged;
        /// <summary>
        /// Raised when the user click the close button of a tab page. The Args of this event gives the option to cancel close.
        /// </summary>
        [Description("Raised when the user click the close button of a tab page. The Args of this event gives the option to cancel."), Category("ManagedTabControl")]
        public event EventHandler<MTCTabPageCloseArgs> TabPageClosing;
        /// <summary>
        /// Raised when the user click the close button of a tab page and the tab page closed successfully.
        /// </summary>
        [Description("Raised when the user click the close button of a tab page and the tab page closed successfully."), Category("ManagedTabControl")]
        public event EventHandler<MTCTabPageCloseArgs> TabPageClosed;
        /// <summary>
        /// Raised when the control need to show tool tip for tab page.
        /// </summary>
        [Description("Raised when the control need to show tool tip for tab page."), Category("ManagedTabControl")]
        public event EventHandler<MTCTabPageShowToolTipArgs> ShowTabPageToolTipRequest;
        /// <summary>
        /// Raised after a tab page get reordered.
        /// </summary>
        [Description("Raised after a tab page get reordered."), Category("ManagedTabControl")]
        public event EventHandler AfterTabPageReorder;
        /// <summary>
        /// Raised when tab page get dragged. The AllowTabPageDragAndDrop must enabled and AllowAutoTabPageDragAndDrop must be disabled.
        /// </summary>
        [Description("Raised when tab page get dragged. The AllowTabPageDragAndDrop must enabled and AllowAutoTabPageDragAndDrop must be disabled in order to enable this event."), Category("Drag Drop")]
        public event EventHandler<MTCTabPageDragArgs> TabPageDrag;
        /// <summary>
        /// Raised before a tab page get dragged using auto drag and drop. The AllowTabPageDragAndDrop and AllowAutoTabPageDragAndDrop must enabled.
        /// </summary>
        [Description("Raised before a tab page get dragged using auto drag and drop. The AllowTabPageDragAndDrop and AllowAutoTabPageDragAndDrop must enabled."), Category("Drag Drop")]
        public event EventHandler BeforeAutoTabDragAndDrop;
        /// <summary>
        /// Raised after a tab page dragged using auto drag and drop. The AllowTabPageDragAndDrop and AllowAutoTabPageDragAndDrop must enabled.
        /// </summary>
        [Description("Raised after a tab page dragged using auto drag and drop. The AllowTabPageDragAndDrop and AllowAutoTabPageDragAndDrop must enabled."), Category("Drag Drop")]
        public event EventHandler AfterAutoTabDragAndDrop;
        /// <summary>
        /// Raised when tab pages collection get cleared.
        /// </summary>
        [Description("Raised when tab pages collection get cleared."), Category("ManagedTabControl")]
        public event EventHandler TabPagesCleared;
        /// <summary>
        /// Raised when tab pages has been dropped
        /// </summary>
        [Description("Raised when tab pages has been dropped"), Category("ManagedTabControl")]
        public event EventHandler<MTCDropRequestEventArgs> TabDrop;
        #endregion

        #region Methods
        /// <summary>
        /// Calculate a Tab Page width value.
        /// </summary>
        /// <param name="page">The <see cref="ManagedUI.MTCTabPage"/> to calculate value for</param>
        /// <returns>The width of that page otherwise 0 if unable to calculate</returns>
        public int CalculateTabPageWidth(MTCTabPage page)
        {
            return managedTabControlPanel1.CalculateTabPageWidth(page);
        }
        /// <summary>
        /// Get tab page index within TabPages collection
        /// </summary>
        /// <param name="xPos">The x coordinate value on view port</param>
        /// <returns>The tab page index value otherwise -1 if page not found</returns>
        public int GetTabPageIndex(int xPos)
        { return managedTabControlPanel1.GetTabPageIndex(xPos); }
        /// <summary>
        /// Get tab page x coordinate value on view port
        /// </summary>
        /// <param name="index">The tab page index within TabPages collection</param>
        /// <returns>The tab page x coordinate value on view port otherwise -1 if page not found</returns>
        public int GetTabPageXPos(int index)
        { return managedTabControlPanel1.GetTabPageXPos(index); }
        /// <summary>
        /// Indicate whether a tab is completely shown to the user
        /// </summary>
        /// <param name="index">The tab page index</param>
        /// <returns></returns>
        public bool IsTapPageShownCompletely(int index)
        { return managedTabControlPanel1.IsTapPageShownCompletely(index); }
        private void DisposeEvents()
        {
            SelectedTabPageIndexChanged = null;
            TabPageClosing = null;
            TabPageClosed = null;
            ShowTabPageToolTipRequest = null;
            AfterTabPageReorder = null;
            TabPageDrag = null;
            BeforeAutoTabDragAndDrop = null;
            AfterAutoTabDragAndDrop = null;
            TabPagesCleared = null;
        }
        /// <summary>
        /// Check pages and check for stuff visibilty
        /// </summary>
        public void CheckPages()
        {
            if (DraggingMODE)
                return;
            if (TabPages.Count == 0)
            {
                panel_pages.Visible = false;
                panel_controls.Visible = false;
                managedTabControlDragPanel1.Visible = true;
            }
            else
            {
                panel_pages.Visible = true;
                panel_controls.Visible = true;
                managedTabControlDragPanel1.Visible = false;
            }
        }
        /// <summary>
        /// Enter the dragging mode
        /// </summary>
        public void EnterDraggingMode()
        {
            if (DraggingMODE)
                return;
            DraggingMODE = true;
            managedTabControlPanel1.DraggingMode = true;
            managedTabControlDragPanel1.s_c = TabPageColor;
            managedTabControlDragPanel1.s_h_c = TabPageHighlightedColor;
            managedTabControlDragPanel1.s_s_c = TabPageSplitColor;
            Bitmap cnt_p = null;
            // Take the control image
            if (current_control != null)
            {
                cnt_p = new Bitmap(current_control.Width, current_control.Height);
                current_control.DrawToBitmap(cnt_p, new Rectangle(0, 0, cnt_p.Width, cnt_p.Height));
                // Make it invisible
                current_control.Visible = false;
            }
            panel_controls.Visible = false;
            managedTabControlDragPanel1.Visible = true;
            // Tell the surface to draw it instead
            managedTabControlDragPanel1.EnterDrag(true, cnt_p); CheckPages();
        }
        /// <summary>
        /// Leave the dragging mode
        /// </summary>
        public void LeaveDraggingMode()
        {
            if (!DraggingMODE)
                return;
            DraggingMODE = false;
            managedTabControlPanel1.DraggingMode = false;
            if (current_control != null)
                current_control.Visible = true;
            panel_controls.Visible = true;
            managedTabControlDragPanel1.Visible = false;
            managedTabControlDragPanel1.EnterDrag(false, null);
            CheckPages();
            RefreshPage();
        }
        /// <summary>
        /// Refresh the page
        /// </summary>
        public void RefreshPage()
        {
            try
            {
                panel_controls.Controls.Clear();
                if (managedTabControlPanel1.SelectedTabPageIndex >= 0)
                {
                    if (managedTabControlPanel1.TabPages.Count > 0)
                    {
                        SelectedTabPageIndexChanged?.Invoke(this, new EventArgs());
                        current_control = managedTabControlPanel1.TabPages[managedTabControlPanel1.SelectedTabPageIndex].Panel;
                        if (current_control != null)
                        {
                            if (current_control.IsDisposed)
                            {
                                //Trace.TraceError("CONTROL IS DISPOSED !! NAME = " + current_control.Name);
                                return;
                            }
                            if (IsDisposed)
                                return;
                            panel_controls.Controls.Add(current_control);
                            current_control.Dock = DockStyle.Fill;
                            current_control.Visible = true;
                        }
                        //else
                            //Trace.TraceError("CONTROL IS NULL !!");
                    }
                }
            }
            catch { }
        }
        #endregion

        /// <summary>
        /// Occur when the control get invalidated
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            managedTabControlPanel1.Invalidate();
        }
        private void managedTabControlPanel1_TabPageCloseRequest(object sender, MTCTabPageCloseArgs e)
        {
            MTCTabPageCloseArgs args = new MTCTabPageCloseArgs(e.TabPageID, e.TabPageIndex);
            args.Cancel = false;
            TabPageClosing?.Invoke(this, args);
            if (!args.Cancel)
            {
                if (closeTabOnCloseButtonClick)
                {
                    managedTabControlPanel1.TabPages.RemoveAt(e.TabPageIndex);
                    if (managedTabControlPanel1.SelectedTabPageIndex > 0)
                        managedTabControlPanel1.SelectedTabPageIndex--;
                    managedTabControlPanel1_SelectedTabPageIndexChanged(this, new EventArgs());
                    managedTabControlPanel1.Invalidate();
                    TabPageClosed?.Invoke(this, new MTCTabPageCloseArgs(e.TabPageID, -1));
                }
            }
        }
        private void managedTabControlPanel1_RefreshScrollBar(object sender, EventArgs e)
        {
            if (TabPages.Count != 1)
            {
                int size = managedTabControlPanel1.CalculateTabPagesWidth();
                if (size < this.Width)
                {
                    hScrollBar1.Visible = false;
                    managedTabControlPanel1.HScrollOffset = 0;
                }
                else
                {
                    hScrollBar1.Maximum = size - this.Width + 20 + hScrollBar1.Width;
                    hScrollBar1.Visible = true;
                }
            }
            else
            {
                hScrollBar1.Visible = false;
                managedTabControlPanel1.HScrollOffset = 0;
            }
            CheckPages();
        }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            managedTabControlPanel1.HScrollOffset = hScrollBar1.Value;
            managedTabControlPanel1.Invalidate();
        }
        private void ManagedTabControl_Resize(object sender, EventArgs e)
        {
            managedTabControlPanel1.HScrollOffset = 0;
            managedTabControlPanel1.Invalidate();
            managedTabControlPanel1_RefreshScrollBar(this, null);
        }
        private void managedTabControlPanel1_SelectedTabPageIndexChanged(object sender, EventArgs e)
        {
            RefreshPage();
        }
        private void managedTabControlPanel1_ShowTabPageToolTipRequest(object sender, MTCTabPageShowToolTipArgs e)
        {
            if (showTabPageToolTip)
            {
                MTCTabPageShowToolTipArgs args = new MTCTabPageShowToolTipArgs(e.TabPageID, e.TabPageIndex, e.ToolTipText);
                if (ShowTabPageToolTipRequest != null)
                    ShowTabPageToolTipRequest(this, args);
                toolTip1.SetToolTip(managedTabControlPanel1, args.ToolTipText);
            }
        }
        private void managedTabControlPanel1_ClearToolTip(object sender, EventArgs e)
        {
            toolTip1.RemoveAll();
        }
        private void managedTabControlPanel1_ScrollToSelectedTabPage(object sender, EventArgs e)
        {
            int x = managedTabControlPanel1.GetTabPageXPos(managedTabControlPanel1.SelectedTabPageIndex);
            int w = managedTabControlPanel1.CalculateTabPageWidth(managedTabControlPanel1.TabPages[managedTabControlPanel1.SelectedTabPageIndex]);
            int scr = hScrollBar1.Value;
            if (x > 0)
            {
                while (managedTabControlPanel1.Width - x < w)
                {
                    scr++;
                    if (scr < hScrollBar1.Maximum)
                    {
                        hScrollBar1.Value = scr;
                        hScrollBar1_Scroll(this, null);
                        x = managedTabControlPanel1.GetTabPageXPos(managedTabControlPanel1.SelectedTabPageIndex);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                while (x < 0)
                {
                    scr--;
                    if (scr > 0)
                    {
                        hScrollBar1.Value = scr;
                        hScrollBar1_Scroll(this, null);
                        x = managedTabControlPanel1.GetTabPageXPos(managedTabControlPanel1.SelectedTabPageIndex);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        private void managedTabControlPanel1_AfterTabPageReorder(object sender, EventArgs e)
        {
            if (AfterTabPageReorder != null)
                AfterTabPageReorder(this, new EventArgs());
        }
        private void managedTabControlPanel1_TabPageDrag_1(object sender, MTCTabPageDragArgs e)
        {
            if (DraggingMODE)
                return;
            TabPageDrag?.Invoke(this, e);
        }
        private void managedTabControlPanel1_AfterAutoDragAndDrop(object sender, EventArgs e)
        {
            if (AfterAutoTabDragAndDrop != null)
                AfterAutoTabDragAndDrop(this, new EventArgs());

        }
        private void managedTabControlPanel1_BeforeAutoDragAndDrop(object sender, EventArgs e)
        {
            if (BeforeAutoTabDragAndDrop != null)
                BeforeAutoTabDragAndDrop(this, new EventArgs());
        }
        private void managedTabControlPanel1_TabPagesCleared(object sender, EventArgs e)
        {
            if (TabPagesCleared != null)
                TabPagesCleared(this, new EventArgs());
            CheckPages();
        }
        private void panel_controls_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            OnGiveFeedback(e);
        }
        private void managedTabControlPanel1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            OnGiveFeedback(e);
        }
        private void managedTabControlDragPanel1_TabDrop(object sender, MTCDropRequestEventArgs e)
        {
            TabDrop?.Invoke(this, e);
        }
    }
}
