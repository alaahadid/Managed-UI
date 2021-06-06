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
using System.Drawing;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace ManagedUI
{
    [Export(typeof(ISettingsControl))]
    [ControlInfo("Theme", "sc.theme")]
    [SettingsControlResourceInfo("SC_Name_Theme", "Category_Enviroment", "application_view_tile", "SC_Description_Theme")]
    class SCTheme : ISettingsControl
    {
        private TextBox textBox_theme_file;
        private TextBox textBox_theme_name;
        private GroupBox groupBox4;
        private ManagedTabControl managedTabControl1;
        private System.ComponentModel.IContainer components;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripLabel toolStripLabel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem test11ToolStripMenuItem;
        private ToolStripMenuItem test12ToolStripMenuItem;
        private ToolStripMenuItem test2ToolStripMenuItem;
        private ToolStripMenuItem test21ToolStripMenuItem;
        private GroupBox groupBox3;
        private LinkLabel linkLabel2;
        private Button button_menu_highlight_color;
        private Label label12;
        private Button button_toolbars_highlight_color;
        private Label label11;
        private Button button_menu_background_color;
        private Label label10;
        private Button button_menus_text_color;
        private Label label9;
        private Button button_toolbars_back_color;
        private Label label5;
        private Button button_toolbars_text_color;
        private Label label6;
        private Button button_panels_background_color;
        private Label label7;
        private Button button_panel_texts_color;
        private Label label8;
        private GroupBox groupBox2;
        private Button button_tab_active_color;
        private Label label13;
        private LinkLabel linkLabel1;
        private Button button_page_split_color;
        private Label label4;
        private Button button_page_selected_color;
        private Label label3;
        private Button button_page_highlight_color;
        private Label label2;
        private Button button_page_color;
        private Label label1;
        private GroupBox groupBox1;
        private LinkLabel linkLabel3;
        private CheckBox checkBox_pages_can_be_dragged;
        private CheckBox checkBox_tabs_can_be_reordered;
        private CheckBox checkBox_pages_can_be_closed;
        private Label label14;
        private Label label15;
        private Label label16;
        private Theme currentTheme;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SCTheme));
            this.textBox_theme_file = new System.Windows.Forms.TextBox();
            this.textBox_theme_name = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.managedTabControl1 = new ManagedUI.ManagedTabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.test11ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test12ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test21ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.button_menu_highlight_color = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.button_toolbars_highlight_color = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.button_menu_background_color = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.button_menus_text_color = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button_toolbars_back_color = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button_toolbars_text_color = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button_panels_background_color = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button_panel_texts_color = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_tab_active_color = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button_page_split_color = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button_page_selected_color = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button_page_highlight_color = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button_page_color = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.checkBox_pages_can_be_dragged = new System.Windows.Forms.CheckBox();
            this.checkBox_tabs_can_be_reordered = new System.Windows.Forms.CheckBox();
            this.checkBox_pages_can_be_closed = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_theme_file
            // 
            resources.ApplyResources(this.textBox_theme_file, "textBox_theme_file");
            this.textBox_theme_file.Name = "textBox_theme_file";
            this.textBox_theme_file.ReadOnly = true;
            // 
            // textBox_theme_name
            // 
            resources.ApplyResources(this.textBox_theme_name, "textBox_theme_name");
            this.textBox_theme_name.Name = "textBox_theme_name";
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.managedTabControl1);
            this.groupBox4.Controls.Add(this.toolStrip1);
            this.groupBox4.Controls.Add(this.menuStrip1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // managedTabControl1
            // 
            resources.ApplyResources(this.managedTabControl1, "managedTabControl1");
            this.managedTabControl1.ActiveTabPageIndex = -1;
            this.managedTabControl1.AllowAutoTabPageDragAndDrop = true;
            this.managedTabControl1.AllowTabPageDragAndDrop = true;
            this.managedTabControl1.AllowTabPagesReorder = true;
            this.managedTabControl1.AutoSelectAddedTabPageAfterAddingIt = false;
            this.managedTabControl1.CloseBoxAlwaysVisible = false;
            this.managedTabControl1.CloseBoxOnEachPageVisible = true;
            this.managedTabControl1.CloseTabOnCloseButtonClick = true;
            this.managedTabControl1.DrawStyle = ManagedUI.MTCDrawStyle.Flat;
            this.managedTabControl1.DrawTabPageHighlight = true;
            this.managedTabControl1.Name = "managedTabControl1";
            this.managedTabControl1.SelectedTabPageIndex = 0;
            this.managedTabControl1.ShowTabPageToolTip = true;
            this.managedTabControl1.ShowTabPageToolTipAlways = false;
            this.managedTabControl1.TabPageActive = false;
            this.managedTabControl1.TabPageActiveColor = System.Drawing.Color.Yellow;
            this.managedTabControl1.TabPageColor = System.Drawing.Color.Silver;
            this.managedTabControl1.TabPageHighlightedColor = System.Drawing.Color.LightBlue;
            this.managedTabControl1.TabPageMaxWidth = 250;
            this.managedTabControl1.TabPageSelectedColor = System.Drawing.Color.SkyBlue;
            this.managedTabControl1.TabPageSplitColor = System.Drawing.Color.Gray;
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripTextBox1,
            this.toolStripLabel1});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton1
            // 
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Name = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            resources.ApplyResources(this.toolStripButton2, "toolStripButton2");
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Name = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            resources.ApplyResources(this.toolStripButton3, "toolStripButton3");
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Name = "toolStripButton3";
            // 
            // toolStripTextBox1
            // 
            resources.ApplyResources(this.toolStripTextBox1, "toolStripTextBox1");
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.test2ToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test11ToolStripMenuItem,
            this.test12ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // test11ToolStripMenuItem
            // 
            resources.ApplyResources(this.test11ToolStripMenuItem, "test11ToolStripMenuItem");
            this.test11ToolStripMenuItem.Name = "test11ToolStripMenuItem";
            // 
            // test12ToolStripMenuItem
            // 
            resources.ApplyResources(this.test12ToolStripMenuItem, "test12ToolStripMenuItem");
            this.test12ToolStripMenuItem.Name = "test12ToolStripMenuItem";
            // 
            // test2ToolStripMenuItem
            // 
            resources.ApplyResources(this.test2ToolStripMenuItem, "test2ToolStripMenuItem");
            this.test2ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test21ToolStripMenuItem});
            this.test2ToolStripMenuItem.Name = "test2ToolStripMenuItem";
            // 
            // test21ToolStripMenuItem
            // 
            resources.ApplyResources(this.test21ToolStripMenuItem, "test21ToolStripMenuItem");
            this.test21ToolStripMenuItem.Name = "test21ToolStripMenuItem";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.linkLabel2);
            this.groupBox3.Controls.Add(this.button_menu_highlight_color);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.button_toolbars_highlight_color);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.button_menu_background_color);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.button_menus_text_color);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.button_toolbars_back_color);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.button_toolbars_text_color);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.button_panels_background_color);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.button_panel_texts_color);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // linkLabel2
            // 
            resources.ApplyResources(this.linkLabel2, "linkLabel2");
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.TabStop = true;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // button_menu_highlight_color
            // 
            resources.ApplyResources(this.button_menu_highlight_color, "button_menu_highlight_color");
            this.button_menu_highlight_color.Name = "button_menu_highlight_color";
            this.button_menu_highlight_color.UseVisualStyleBackColor = true;
            this.button_menu_highlight_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // button_toolbars_highlight_color
            // 
            resources.ApplyResources(this.button_toolbars_highlight_color, "button_toolbars_highlight_color");
            this.button_toolbars_highlight_color.Name = "button_toolbars_highlight_color";
            this.button_toolbars_highlight_color.UseVisualStyleBackColor = true;
            this.button_toolbars_highlight_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // button_menu_background_color
            // 
            resources.ApplyResources(this.button_menu_background_color, "button_menu_background_color");
            this.button_menu_background_color.Name = "button_menu_background_color";
            this.button_menu_background_color.UseVisualStyleBackColor = true;
            this.button_menu_background_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // button_menus_text_color
            // 
            resources.ApplyResources(this.button_menus_text_color, "button_menus_text_color");
            this.button_menus_text_color.Name = "button_menus_text_color";
            this.button_menus_text_color.UseVisualStyleBackColor = true;
            this.button_menus_text_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // button_toolbars_back_color
            // 
            resources.ApplyResources(this.button_toolbars_back_color, "button_toolbars_back_color");
            this.button_toolbars_back_color.Name = "button_toolbars_back_color";
            this.button_toolbars_back_color.UseVisualStyleBackColor = true;
            this.button_toolbars_back_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // button_toolbars_text_color
            // 
            resources.ApplyResources(this.button_toolbars_text_color, "button_toolbars_text_color");
            this.button_toolbars_text_color.Name = "button_toolbars_text_color";
            this.button_toolbars_text_color.UseVisualStyleBackColor = true;
            this.button_toolbars_text_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // button_panels_background_color
            // 
            resources.ApplyResources(this.button_panels_background_color, "button_panels_background_color");
            this.button_panels_background_color.Name = "button_panels_background_color";
            this.button_panels_background_color.UseVisualStyleBackColor = true;
            this.button_panels_background_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // button_panel_texts_color
            // 
            resources.ApplyResources(this.button_panel_texts_color, "button_panel_texts_color");
            this.button_panel_texts_color.Name = "button_panel_texts_color";
            this.button_panel_texts_color.UseVisualStyleBackColor = true;
            this.button_panel_texts_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.button_tab_active_color);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.linkLabel1);
            this.groupBox2.Controls.Add(this.button_page_split_color);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.button_page_selected_color);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button_page_highlight_color);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button_page_color);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // button_tab_active_color
            // 
            resources.ApplyResources(this.button_tab_active_color, "button_tab_active_color");
            this.button_tab_active_color.Name = "button_tab_active_color";
            this.button_tab_active_color.UseVisualStyleBackColor = true;
            this.button_tab_active_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // button_page_split_color
            // 
            resources.ApplyResources(this.button_page_split_color, "button_page_split_color");
            this.button_page_split_color.Name = "button_page_split_color";
            this.button_page_split_color.UseVisualStyleBackColor = true;
            this.button_page_split_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // button_page_selected_color
            // 
            resources.ApplyResources(this.button_page_selected_color, "button_page_selected_color");
            this.button_page_selected_color.Name = "button_page_selected_color";
            this.button_page_selected_color.UseVisualStyleBackColor = true;
            this.button_page_selected_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // button_page_highlight_color
            // 
            resources.ApplyResources(this.button_page_highlight_color, "button_page_highlight_color");
            this.button_page_highlight_color.Name = "button_page_highlight_color";
            this.button_page_highlight_color.UseVisualStyleBackColor = true;
            this.button_page_highlight_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button_page_color
            // 
            resources.ApplyResources(this.button_page_color, "button_page_color");
            this.button_page_color.Name = "button_page_color";
            this.button_page_color.UseVisualStyleBackColor = true;
            this.button_page_color.Click += new System.EventHandler(this.button_page_color_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.linkLabel3);
            this.groupBox1.Controls.Add(this.checkBox_pages_can_be_dragged);
            this.groupBox1.Controls.Add(this.checkBox_tabs_can_be_reordered);
            this.groupBox1.Controls.Add(this.checkBox_pages_can_be_closed);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // linkLabel3
            // 
            resources.ApplyResources(this.linkLabel3, "linkLabel3");
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.TabStop = true;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // checkBox_pages_can_be_dragged
            // 
            resources.ApplyResources(this.checkBox_pages_can_be_dragged, "checkBox_pages_can_be_dragged");
            this.checkBox_pages_can_be_dragged.Name = "checkBox_pages_can_be_dragged";
            this.checkBox_pages_can_be_dragged.UseVisualStyleBackColor = true;
            this.checkBox_pages_can_be_dragged.CheckedChanged += new System.EventHandler(this.checkBox_pages_can_be_closed_CheckedChanged);
            // 
            // checkBox_tabs_can_be_reordered
            // 
            resources.ApplyResources(this.checkBox_tabs_can_be_reordered, "checkBox_tabs_can_be_reordered");
            this.checkBox_tabs_can_be_reordered.Name = "checkBox_tabs_can_be_reordered";
            this.checkBox_tabs_can_be_reordered.UseVisualStyleBackColor = true;
            this.checkBox_tabs_can_be_reordered.CheckedChanged += new System.EventHandler(this.checkBox_pages_can_be_closed_CheckedChanged);
            // 
            // checkBox_pages_can_be_closed
            // 
            resources.ApplyResources(this.checkBox_pages_can_be_closed, "checkBox_pages_can_be_closed");
            this.checkBox_pages_can_be_closed.Name = "checkBox_pages_can_be_closed";
            this.checkBox_pages_can_be_closed.UseVisualStyleBackColor = true;
            this.checkBox_pages_can_be_closed.CheckedChanged += new System.EventHandler(this.checkBox_pages_can_be_closed_CheckedChanged);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // SCTheme
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBox_theme_file);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBox_theme_name);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SCTheme";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public override void LoadSettings()
        {
            base.LoadSettings();
            // Add some page in the tab pages control
            for (int i = 1; i < 4; i++)
            {
                MTCTabPage page = new MTCTabPage();
                page.Text = "Page " + i;
                page.Panel = new Panel();
                page.ID = "page" + i;

                managedTabControl1.TabPages.Add(page);
            }
            bool success = false;

            currentTheme = Theme.LoadTHMMap(Path.Combine(MUI.Documentsfolder, "theme.mt"), out success);
            textBox_theme_file.Text = Path.Combine(MUI.Documentsfolder, "theme.mt");
            if (success)
            {
                this.Text = currentTheme.Name + " - " + Properties.Resources.Title_ThemeEditor;

            }
            RefreshTheme();
            UpdatePreview();
        }
        private void RefreshTheme()
        {
            if (currentTheme != null)
            {
                groupBox1.Enabled = groupBox2.Enabled = groupBox3.Enabled = groupBox4.Enabled = textBox_theme_name.Enabled = true;
                textBox_theme_name.Text = currentTheme.Name;
                checkBox_pages_can_be_closed.Checked = currentTheme.PagesCanBeClosed;
                checkBox_pages_can_be_dragged.Checked = currentTheme.PagesCanBeDragged;
                checkBox_tabs_can_be_reordered.Checked = currentTheme.PagesCanBeReordered;

                button_menus_text_color.BackColor = Color.FromArgb(currentTheme.MenuTextsColor);
                button_menus_text_color.Text = currentTheme.MenuTextsColor.ToString("X6");

                button_menu_background_color.BackColor = Color.FromArgb(currentTheme.MenusBackColor);
                button_menu_background_color.Text = currentTheme.MenusBackColor.ToString("X6");

                button_menu_highlight_color.BackColor = Color.FromArgb(currentTheme.MenuHighlightColor);
                button_menu_highlight_color.Text = currentTheme.MenuHighlightColor.ToString("X6");

                button_page_color.BackColor = Color.FromArgb(currentTheme.TabPageColor);
                button_page_color.Text = currentTheme.TabPageColor.ToString("X6");

                button_page_highlight_color.BackColor = Color.FromArgb(currentTheme.TabPageHighlightedColor);
                button_page_highlight_color.Text = currentTheme.TabPageHighlightedColor.ToString("X6");

                button_page_selected_color.BackColor = Color.FromArgb(currentTheme.TabPageSelectedColor);
                button_page_selected_color.Text = currentTheme.TabPageSelectedColor.ToString("X6");

                button_page_split_color.BackColor = Color.FromArgb(currentTheme.TabPageSplitColor);
                button_page_split_color.Text = currentTheme.TabPageSplitColor.ToString("X6");

                button_tab_active_color.BackColor = Color.FromArgb(currentTheme.TabPageSelectedActiveColor);
                button_tab_active_color.Text = currentTheme.TabPageSelectedActiveColor.ToString("X6");

                button_panels_background_color.BackColor = Color.FromArgb(currentTheme.PanelsBackColor);
                button_panels_background_color.Text = currentTheme.PanelsBackColor.ToString("X6");

                button_panel_texts_color.BackColor = Color.FromArgb(currentTheme.PanelTextsColor);
                button_panel_texts_color.Text = currentTheme.PanelTextsColor.ToString("X6");

                button_toolbars_back_color.BackColor = Color.FromArgb(currentTheme.ToolbarsBackColor);
                button_toolbars_back_color.Text = currentTheme.ToolbarsBackColor.ToString("X6");

                button_toolbars_text_color.BackColor = Color.FromArgb(currentTheme.ToolbarTextsColor);
                button_toolbars_text_color.Text = currentTheme.ToolbarTextsColor.ToString("X6");

                button_toolbars_highlight_color.BackColor = Color.FromArgb(currentTheme.ToolbarsHighlightColor);
                button_toolbars_highlight_color.Text = currentTheme.ToolbarsHighlightColor.ToString("X6");
            }
            else
            {
                groupBox1.Enabled = groupBox2.Enabled = groupBox3.Enabled = groupBox4.Enabled = textBox_theme_name.Enabled = false;
            }
            UpdatePreview();
        }
        public override void SaveSettings()
        {
            base.SaveSettings();
            if (currentTheme == null)
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToSaveTheThemeFile);
                return;
            }
            currentTheme.Name = textBox_theme_name.Text;

            currentTheme.PagesCanBeClosed = checkBox_pages_can_be_closed.Checked;
            currentTheme.PagesCanBeDragged = checkBox_pages_can_be_dragged.Checked;
            currentTheme.PagesCanBeReordered = checkBox_tabs_can_be_reordered.Checked;
            currentTheme.MenuTextsColor = button_menus_text_color.BackColor.ToArgb();
            currentTheme.MenusBackColor = button_menu_background_color.BackColor.ToArgb();
            currentTheme.MenuHighlightColor = button_menu_highlight_color.BackColor.ToArgb();
            currentTheme.TabPageColor = button_page_color.BackColor.ToArgb();
            currentTheme.TabPageHighlightedColor = button_page_highlight_color.BackColor.ToArgb();
            currentTheme.TabPageSelectedColor = button_page_selected_color.BackColor.ToArgb();
            currentTheme.TabPageSplitColor = button_page_split_color.BackColor.ToArgb();
            currentTheme.TabPageSelectedActiveColor = button_tab_active_color.BackColor.ToArgb();
            currentTheme.PanelsBackColor = button_panels_background_color.BackColor.ToArgb();
            currentTheme.PanelTextsColor = button_panel_texts_color.BackColor.ToArgb();
            currentTheme.ToolbarsBackColor = button_toolbars_back_color.BackColor.ToArgb();
            currentTheme.ToolbarTextsColor = button_toolbars_text_color.BackColor.ToArgb();
            currentTheme.ToolbarsHighlightColor = button_toolbars_highlight_color.BackColor.ToArgb();

            // Save
            if (!Theme.SaveTHMMap(textBox_theme_file.Text, currentTheme))
            {
                ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToSaveTheThemeFile);
            }
            else
            {
                if (Path.Combine(MUI.Documentsfolder, "theme.mt") == textBox_theme_file.Text)
                {
                    // Apply the map to the current !!
                    GUIService.GUI.CurrentTheme = currentTheme;
                }
              //  ManagedMessageBox.ShowMessage(Properties.Resources.Status_THMSavedSuccess);
            }
        }
        public override void DefaultSettings()
        {
            base.DefaultSettings();
            currentTheme = Theme.DefaultTheme();
            RefreshTheme();
        }
        private void UpdatePreview()
        {
            menuStrip1.ForeColor = button_menus_text_color.BackColor;
            menuStrip1.BackColor = button_menu_background_color.BackColor;
            // We need to do this little trick to change the highlight color for the main menu
            MenuColors colors = new MenuColors(
            button_menu_highlight_color.BackColor,
            button_menu_highlight_color.BackColor,
            button_menu_highlight_color.BackColor,
            button_menu_background_color.BackColor,
            button_menu_background_color.BackColor,
            button_menu_background_color.BackColor,
            button_menu_background_color.BackColor,
            button_menu_background_color.BackColor);
            menuStrip1.Renderer = new ThemeRenderer(colors);
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                ApplyColorToMenuItem(item);
            }
            // Save trick for the toolbar
            toolStrip1.BackColor = button_toolbars_back_color.BackColor;
            toolStrip1.ForeColor = button_toolbars_text_color.BackColor;
            ToolbarColors tcolors = new ToolbarColors(
            button_toolbars_highlight_color.BackColor,
            button_toolbars_highlight_color.BackColor,
            button_toolbars_highlight_color.BackColor);
            toolStrip1.Renderer = new ThemeRenderer(tcolors);
            for (int i = 0; i < toolStrip1.Items.Count; i++)
            {
                toolStrip1.Items[i].ForeColor = button_toolbars_text_color.BackColor;
                toolStrip1.Items[i].BackColor = button_toolbars_back_color.BackColor;
            }
            managedTabControl1.TabPageActive = true;
            managedTabControl1.TabPageColor = button_page_color.BackColor;
            managedTabControl1.TabPageHighlightedColor = button_page_highlight_color.BackColor;
            managedTabControl1.TabPageSelectedColor = button_page_selected_color.BackColor;
            managedTabControl1.TabPageSplitColor = button_page_split_color.BackColor;
            managedTabControl1.TabPageActiveColor = button_tab_active_color.BackColor;
            managedTabControl1.ForeColor = button_panel_texts_color.BackColor;
            managedTabControl1.BackColor = button_panels_background_color.BackColor;

            managedTabControl1.CloseBoxOnEachPageVisible = checkBox_pages_can_be_closed.Checked;
            managedTabControl1.AllowTabPageDragAndDrop = checkBox_pages_can_be_dragged.Checked;
            managedTabControl1.AllowTabPagesReorder = checkBox_tabs_can_be_reordered.Checked;
        }
        private void ApplyColorToMenuItem(ToolStripMenuItem item)
        {
            item.BackColor = button_menu_background_color.BackColor;
            item.ForeColor = button_menus_text_color.BackColor;

            for (int i = 0; i < item.DropDownItems.Count; i++)
            {
                if (item.DropDownItems[i] is ToolStripMenuItem)
                {
                    ApplyColorToMenuItem((ToolStripMenuItem)item.DropDownItems[i]);
                }
                else
                {
                    item.DropDownItems[i].BackColor = button_menu_background_color.BackColor;
                    item.DropDownItems[i].ForeColor = button_menus_text_color.BackColor;
                }
            }
        }
        public override bool CanSave
        {
            get
            {
                if (!File.Exists(textBox_theme_file.Text))
                    ManagedMessageBox.ShowErrorMessage(Properties.Resources.Message_PleaseSpecifyTheThemeFile);
                return File.Exists(textBox_theme_file.Text);
            }
        }
        // Change the color of a button
        private void button_page_color_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = ((Button)sender).BackColor;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ((Button)sender).BackColor = dialog.Color;
                ((Button)sender).Text = dialog.Color.ToArgb().ToString("X6");
                UpdatePreview();
            }
        }
        // To show review ...
        private void checkBox_pages_can_be_closed_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }
        // Reset colors for tabpages only
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            button_page_color.BackColor = Color.Silver;
            button_page_selected_color.BackColor = Color.SkyBlue;
            button_page_highlight_color.BackColor = Color.LightBlue;
            button_page_split_color.BackColor = Color.Gray;
            button_tab_active_color.BackColor = Color.Red;
            UpdatePreview();
        }
        // Reset colors for menus and toolbars only
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            button_panel_texts_color.BackColor = Color.Black;
            button_panels_background_color.BackColor = SystemColors.Control;

            button_toolbars_text_color.BackColor = Color.Black;
            button_toolbars_back_color.BackColor = SystemColors.Control;
            button_toolbars_highlight_color.BackColor = SystemColors.MenuHighlight;

            button_menus_text_color.BackColor = Color.Black;
            button_menu_background_color.BackColor = SystemColors.Control;
            button_menu_highlight_color.BackColor = SystemColors.MenuHighlight;
        }
        // Default config
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            checkBox_pages_can_be_closed.Checked = true;
            checkBox_pages_can_be_dragged.Checked = true;
            checkBox_tabs_can_be_reordered.Checked = true;
        }
    }
}
