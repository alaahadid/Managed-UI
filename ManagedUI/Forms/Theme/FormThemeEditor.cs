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
using System.Drawing;
using System.Windows.Forms;

namespace ManagedUI
{
    /// <summary>
    /// The theme edit form
    /// </summary>
    public partial class FormThemeEditor : Form
    {
        /// <summary>
        /// The theme edit form
        /// </summary>
        /// <param name="isDocumentsFile">Indicates if the file should be loaded from documents or not</param>
        public FormThemeEditor(bool isDocumentsFile)
        {
            InitializeComponent();
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
            if (isDocumentsFile)
            {
                currentTheme = Theme.LoadTHMMap(Path.Combine(MUI.Documentsfolder, "theme.mt"), out success);
                if (success)
                {
                    this.Text = currentTheme.Name + " - " + Properties.Resources.Title_ThemeEditor;
                    textBox_theme_file.Text = Path.Combine(MUI.Documentsfolder, "theme.mt");
                }
                else
                {
                    ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToLoadCurrentTheme);
                }
            }
            else
            {
                currentTheme = Theme.LoadTHMMap(Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "theme.mt"), out success);
                if (success)
                {
                    this.Text = currentTheme.Name + " - " + Properties.Resources.Title_ThemeEditor;
                    textBox_theme_file.Text = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "theme.mt");
                }
                else
                {
                    ManagedMessageBox.ShowMessage(Properties.Resources.Message_UnableToLoadCurrentTheme);
                }
            }
            RefreshTheme();
            UpdatePreview();
        }

        private Theme currentTheme;

        private void RefreshTheme()
        {
            if (currentTheme != null)
            {
                groupBox1.Enabled = groupBox2.Enabled = groupBox3.Enabled = groupBox4.Enabled = groupBox5.Enabled = true;
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
                groupBox1.Enabled = groupBox2.Enabled = groupBox3.Enabled = groupBox4.Enabled = groupBox5.Enabled = false;
            }
            UpdatePreview();
        }
        private void ApplyAndSave()
        {
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
                ManagedMessageBox.ShowMessage(Properties.Resources.Status_THMSavedSuccess);
            }
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
        // Change file path
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = Properties.Resources.Title_OpenThemeFile;
            op.Filter = Properties.Resources.FilterName_ThemeFile + " (*.mt)|*.mt;*.MT;";
            op.FileName = textBox_theme_file.Text;

            if (op.ShowDialog(this) == DialogResult.OK)
            {
                textBox_theme_file.Text = op.FileName;
                RefreshTheme();
            }
        }
        // Close
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Save
        private void button3_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBox_theme_file.Text))
            {
                OpenFileDialog save = new OpenFileDialog();
                save.Title = Properties.Resources.Title_SaveThemeFile;
                save.Filter = Properties.Resources.FilterName_ThemeFile + " (*.mt)|*.mt;*.MT;";
                save.FileName = textBox_theme_file.Text;
                if (save.ShowDialog(this) == DialogResult.OK)
                {
                    textBox_theme_file.Text = save.FileName;
                    ApplyAndSave();
                }
            }
            else
            {
                ApplyAndSave();
            }
        }
        // Reload
        private void button5_Click(object sender, EventArgs e)
        {
            RefreshTheme();
        }
        // Reset
        private void button4_Click(object sender, EventArgs e)
        {
            currentTheme = Theme.DefaultTheme();
            RefreshTheme();
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
