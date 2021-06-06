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
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace ManagedUI
{
    /// <summary>
    /// Represents a theme for the tab controls map.
    /// </summary>
    [Serializable]
    public sealed class Theme
    {
        /// <summary>
        /// Get or set the name of this theme
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Get or set if the end-user can close pages or not. Default is true (user can close pages)
        /// </summary>
        public bool PagesCanBeClosed { get; set; }
        /// <summary>
        /// Get or set if the end-user can reorder pages. Default is true (user can reorder pages)
        /// </summary>
        public bool PagesCanBeReordered { get; set; }
        /// <summary>
        /// Get or set if the end-user can drag and drop pages to change the layout. Default is true (user can drag and drop pages)
        /// </summary>
        public bool PagesCanBeDragged { get; set; }
        /// <summary>
        /// Get or set the tab page color when not highlighted nor selected
        /// </summary>
        public int TabPageColor { get; set; }
        /// <summary>
        /// Get or set the color that will be used when drawing a tab page that is selected
        /// </summary>
        public int TabPageSelectedColor { get; set; }
        /// <summary>
        /// Get or set the color that will be used when drawing a tab page that is selected and active
        /// </summary>
        public int TabPageSelectedActiveColor { get; set; }
        /// <summary>
        /// Get or set the color that will be used when the mouse cursor get over a tab page
        /// </summary>
        public int TabPageHighlightedColor { get; set; }
        /// <summary>
        /// Get or set the color of the tab page splinters.
        /// </summary>
        public int TabPageSplitColor { get; set; }
        /// <summary>
        /// The color of the texts and names of the pages
        /// </summary>
        public int PanelTextsColor { get; set; }
        /// <summary>
        /// Get or set the background color for panels and controls.
        /// </summary>
        public int PanelsBackColor { get; set; }
        /// <summary>
        /// Get or set the background color to use for toolbars.
        /// </summary>
        public int ToolbarsBackColor { get; set; }
        /// <summary>
        /// Get or set the color to use for toolbars texts.
        /// </summary>
        public int ToolbarTextsColor { get; set; }
        /// <summary>
        /// Get or set the color to use for toolbars highlight
        /// </summary>
        public int ToolbarsHighlightColor { get; set; }
        /// <summary>
        /// Get or set the background color to use for menus (main menu and other menus).
        /// </summary>
        public int MenusBackColor { get; set; }
        /// <summary>
        /// Get or set the color to use for menus texts (main menu and other menus).
        /// </summary>
        public int MenuTextsColor { get; set; }
        /// <summary>
        /// Get or set the color to use for menus highlight
        /// </summary>
        public int MenuHighlightColor { get; set; }

        #region STATIC
        /// <summary>
        /// Load a theme file from file
        /// </summary>
        /// <param name="filePath">The complete file path</param>
        /// <param name="success">Set to true when the load is a success. Otherwise set to false.</param>
        /// <returns>The theme file loaded if load is a success. </returns>
        public static Theme LoadTHMMap(string filePath, out bool success)
        {
            success = false;
            Trace.WriteLine(Properties.Resources.Status_LoadingTheThemeFileAt + " " + filePath + " ...");
            if (!File.Exists(filePath))
            {
                Trace.WriteLine(Properties.Resources.Status_THMError1 +
                    ", " + Properties.Resources.Status_fileIsNotExistAt + " " + filePath, StatusMode.Error);
                return null;
            }
            XmlReaderSettings sett = new XmlReaderSettings();
            sett.DtdProcessing = DtdProcessing.Ignore;
            sett.IgnoreWhitespace = true;
            XmlReader XMLread = XmlReader.Create(filePath, sett);
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(Theme));

                Theme thm = (Theme)ser.Deserialize(XMLread);

                XMLread.Close();
                Trace.WriteLine(Properties.Resources.Status_ThemeLoadedSuccessfullyFromFile + " " + filePath + ".", StatusMode.Information);
                success = thm != null;
                if (thm == null)
                {
                    Trace.WriteLine(Properties.Resources.Status_THMError1 + " " + filePath + ": " + Properties.Resources.Status_THMError2, StatusMode.Error);
                    return null;
                }
                return thm;
            }
            catch (Exception ex)
            {
                XMLread.Close();
                Trace.WriteLine(Properties.Resources.Status_THMError1 + " " + filePath + ": " + ex.Message + "/n" + ex.ToString(), StatusMode.Error);
            }
            return null;
        }
        /// <summary>
        /// Save a theme into file
        /// </summary>
        /// <param name="filePath">The complete path where to save the theme file.</param>
        /// <param name="thm">The theme to save.</param>
        /// <returns>True if save operation successed otherwise false.</returns>
        public static bool SaveTHMMap(string filePath, Theme thm)
        {
            Trace.WriteLine(Properties.Resources.Status_SavingTHMFile + ": " + filePath + " ...");

            try
            {
                XmlWriterSettings sett = new XmlWriterSettings();
                sett.Indent = true;
                XmlWriter XMLwrt = XmlWriter.Create(filePath, sett);
                XmlSerializer ser = new XmlSerializer(typeof(Theme));

                ser.Serialize(XMLwrt, thm);
                XMLwrt.Flush();
                XMLwrt.Close();

                Trace.WriteLine(Properties.Resources.Status_THMSavedSuccess + " " + filePath + ".", StatusMode.Information);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(Properties.Resources.Status_THMError3 + " " + filePath + ": " + ex.Message + "/n" + ex.ToString(), StatusMode.Error);
            }
            return false;
        }
        /// <summary>
        /// Generate a new default theme
        /// </summary>
        /// <returns>new theme with default style</returns>
        public static Theme DefaultTheme()
        {
            Theme thm = new Theme();
            thm.Name = "Default";

            // Settings
            thm.PagesCanBeClosed = true;
            thm.PagesCanBeDragged = true;
            thm.PagesCanBeReordered = true;
            // Tab pages
            thm.TabPageColor = -1;
            thm.TabPageSelectedColor = -8798977;
            thm.TabPageHighlightedColor = -6566401;
            thm.TabPageSelectedActiveColor = Color.Red.ToArgb();
            thm.TabPageSplitColor = -16777216;
            // Other colors
            thm.PanelTextsColor = -16777216;
            thm.PanelsBackColor = SystemColors.Control.ToArgb();

            thm.ToolbarTextsColor = Color.Black.ToArgb();
            thm.ToolbarsBackColor = SystemColors.Control.ToArgb();
            thm.ToolbarsHighlightColor = -8798977;

            thm.MenuTextsColor = Color.Black.ToArgb();
            thm.MenusBackColor = SystemColors.Control.ToArgb();
            thm.MenuHighlightColor = -8798977;

            return thm;
        }
        #endregion
    }
}
