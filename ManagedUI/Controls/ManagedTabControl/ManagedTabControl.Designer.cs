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
namespace ManagedUI
{
    partial class ManagedTabControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            DisposeEvents();
            if (disposing && (components != null))
            {
              
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel_pages = new System.Windows.Forms.Panel();
            this.managedTabControlPanel1 = new ManagedUI.ManagedTabControlPanel();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.panel_controls = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.managedTabControlDragPanel1 = new ManagedUI.ManagedTabControlDragPanel();
            this.panel_pages.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_pages
            // 
            this.panel_pages.Controls.Add(this.managedTabControlPanel1);
            this.panel_pages.Controls.Add(this.hScrollBar1);
            this.panel_pages.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_pages.Location = new System.Drawing.Point(0, 0);
            this.panel_pages.Name = "panel_pages";
            this.panel_pages.Size = new System.Drawing.Size(451, 25);
            this.panel_pages.TabIndex = 1;
            // 
            // managedTabControlPanel1
            // 
            this.managedTabControlPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.managedTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.managedTabControlPanel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.managedTabControlPanel1.Location = new System.Drawing.Point(0, 0);
            this.managedTabControlPanel1.Name = "managedTabControlPanel1";
            this.managedTabControlPanel1.Size = new System.Drawing.Size(425, 25);
            this.managedTabControlPanel1.TabIndex = 0;
            this.managedTabControlPanel1.Text = "managedTabControlPanel1";
            this.managedTabControlPanel1.SelectedTabPageIndexChanged += new System.EventHandler(this.managedTabControlPanel1_SelectedTabPageIndexChanged);
            this.managedTabControlPanel1.TabPageCloseRequest += new System.EventHandler<ManagedUI.MTCTabPageCloseArgs>(this.managedTabControlPanel1_TabPageCloseRequest);
            this.managedTabControlPanel1.RefreshScrollBar += new System.EventHandler(this.managedTabControlPanel1_RefreshScrollBar);
            this.managedTabControlPanel1.ShowTabPageToolTipRequest += new System.EventHandler<ManagedUI.MTCTabPageShowToolTipArgs>(this.managedTabControlPanel1_ShowTabPageToolTipRequest);
            this.managedTabControlPanel1.ClearToolTip += new System.EventHandler(this.managedTabControlPanel1_ClearToolTip);
            this.managedTabControlPanel1.AfterTabPageReorder += new System.EventHandler(this.managedTabControlPanel1_AfterTabPageReorder);
            this.managedTabControlPanel1.TabPageDrag += new System.EventHandler<ManagedUI.MTCTabPageDragArgs>(this.managedTabControlPanel1_TabPageDrag_1);
            this.managedTabControlPanel1.ScrollToSelectedTabPage += new System.EventHandler(this.managedTabControlPanel1_ScrollToSelectedTabPage);
            this.managedTabControlPanel1.BeforeAutoDragAndDrop += new System.EventHandler(this.managedTabControlPanel1_BeforeAutoDragAndDrop);
            this.managedTabControlPanel1.AfterAutoDragAndDrop += new System.EventHandler(this.managedTabControlPanel1_AfterAutoDragAndDrop);
            this.managedTabControlPanel1.TabPagesCleared += new System.EventHandler(this.managedTabControlPanel1_TabPagesCleared);
            this.managedTabControlPanel1.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.managedTabControlPanel1_GiveFeedback);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.hScrollBar1.Location = new System.Drawing.Point(425, 0);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(26, 25);
            this.hScrollBar1.SmallChange = 10;
            this.hScrollBar1.TabIndex = 0;
            this.hScrollBar1.Visible = false;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // panel_controls
            // 
            this.panel_controls.BackColor = System.Drawing.SystemColors.Control;
            this.panel_controls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_controls.Location = new System.Drawing.Point(0, 25);
            this.panel_controls.Name = "panel_controls";
            this.panel_controls.Size = new System.Drawing.Size(451, 406);
            this.panel_controls.TabIndex = 2;
            this.panel_controls.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.panel_controls_GiveFeedback);
            // 
            // managedTabControlDragPanel1
            // 
            this.managedTabControlDragPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.managedTabControlDragPanel1.Location = new System.Drawing.Point(0, 25);
            this.managedTabControlDragPanel1.Name = "managedTabControlDragPanel1";
            this.managedTabControlDragPanel1.Size = new System.Drawing.Size(451, 406);
            this.managedTabControlDragPanel1.TabIndex = 3;
            this.managedTabControlDragPanel1.Text = "managedTabControlDragPanel1";
            this.managedTabControlDragPanel1.Visible = false;
            this.managedTabControlDragPanel1.TabDrop += new System.EventHandler<ManagedUI.MTCDropRequestEventArgs>(this.managedTabControlDragPanel1_TabDrop);
            // 
            // ManagedTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.managedTabControlDragPanel1);
            this.Controls.Add(this.panel_controls);
            this.Controls.Add(this.panel_pages);
            this.Name = "ManagedTabControl";
            this.Size = new System.Drawing.Size(451, 431);
            this.Resize += new System.EventHandler(this.ManagedTabControl_Resize);
            this.panel_pages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_pages;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Panel panel_controls;
        private System.Windows.Forms.ToolTip toolTip1;
        private ManagedTabControlPanel managedTabControlPanel1;
        private ManagedTabControlDragPanel managedTabControlDragPanel1;
    }
}
