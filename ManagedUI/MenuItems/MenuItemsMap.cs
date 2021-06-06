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
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace ManagedUI
{
    /// <summary>
    /// Represents a map of menu items.
    /// </summary>
    [Serializable]
    public class MenuItemsMap
    {
        /// <summary>
        /// Represents a map of menu items.
        /// </summary>
        public MenuItemsMap()
        {
            RootItems = new List<ManagedUI.MenuItemsMapElement>();
        }
        /// <summary>
        /// Get or set the name of this map as set by user.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Get or set the root items collection. MUST be collection of RMI only.
        /// </summary>
        public List<MenuItemsMapElement> RootItems { get; set; }

        /// <summary>
        /// Load a menu item map from file
        /// </summary>
        /// <param name="filePath">The complete file path</param>
        /// <param name="success">Set to true when the load successed. Otherwise set to false.</param>
        /// <returns>The menu item map loaded if load is successed. </returns>
        public static MenuItemsMap LoadMenuItemsMap(string filePath, out bool success)
        {
            success = false;
            Trace.WriteLine(Properties.Resources.Status_LoadingMenuItemsMapAt + " " + filePath + " ...");
            if (!File.Exists(filePath))
            {
                Trace.WriteLine(Properties.Resources.Status_UnableToLoadMIRMap +
                    ", " + Properties.Resources.Status_fileIsNotExistAt + " " + filePath, StatusMode.Error);
                return null;
            }
            XmlReaderSettings sett = new XmlReaderSettings();
            sett.DtdProcessing = DtdProcessing.Ignore;
            sett.IgnoreWhitespace = true;
            XmlReader XMLread = XmlReader.Create(filePath, sett);
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(MenuItemsMap));

                MenuItemsMap map = (MenuItemsMap)ser.Deserialize(XMLread);

                XMLread.Close();
                Trace.WriteLine(Properties.Resources.Status_MenuItemsMapLoadedSuccessfullyFromPath + " " + filePath + ".", StatusMode.Information);
                success = map != null;
                if (map == null)
                {
                    Trace.WriteLine(Properties.Resources.Status_UnableToLoadTheMapFile + " " +
                        filePath + ": " + Properties.Resources.Status_TheFileIsDamagedOrNotMIR, StatusMode.Error);
                    return null;
                }
                return map;
            }
            catch (Exception ex)
            {
                XMLread.Close();
                Trace.WriteLine(Properties.Resources.Status_UnableToLoadTheMapFile + " " + filePath + ": " + ex.Message + "/n" + ex.ToString(), StatusMode.Error);
            }
            return null;
        }
        /// <summary>
        /// Save a menu items map into file
        /// </summary>
        /// <param name="filePath">The complete path where to save the map file.</param>
        /// <param name="map">The menu items map to save.</param>
        /// <returns>True if save operation successed otherwise false.</returns>
        public static bool SaveMenuItemsMap(string filePath, MenuItemsMap map)
        {
            Trace.WriteLine(Properties.Resources.Status_SavingMenuItemsMapAtPath + ": " + filePath + " ...");

            try
            {
                XmlWriterSettings sett = new XmlWriterSettings();
                sett.Indent = true;
                XmlWriter XMLwrt = XmlWriter.Create(filePath, sett);
                XmlSerializer ser = new XmlSerializer(typeof(MenuItemsMap));

                ser.Serialize(XMLwrt, map);
                XMLwrt.Flush();
                XMLwrt.Close();

                Trace.WriteLine(Properties.Resources.Status_MenuItemsMapSavedSuccessfullyAtPath +
                    " " + filePath + ".", StatusMode.Information);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(Properties.Resources.Status_UnableToLoadTheMapFile + " " + filePath
                    + ": " + ex.Message + "/n" + ex.ToString(), StatusMode.Error);
            }
            return false;
        }
        /// <summary>
        /// Generate a new default menu items map.
        /// </summary>
        /// <returns>The new menu items map.</returns>
        public static MenuItemsMap DefaultMenuItemsMap()
        {
            MenuItemsMap map = new MenuItemsMap();
            map.Name = "Defaut";
            map.RootItems = new List<MenuItemsMapElement>();

            // Add file
            MenuItemsMapElement ff = new MenuItemsMapElement("root.file", MIRType.ROOT);
            ff.Items.Add(new MenuItemsMapElement("cmi.exit", MIRType.CMI));
            map.RootItems.Add(ff);
            // --

            map.RootItems.Add(new MenuItemsMapElement("root.edit", MIRType.ROOT));

            ff = new MenuItemsMapElement("root.view", MIRType.ROOT);
            ff.Items.Add(new MenuItemsMapElement("dmi.tabs", MIRType.DMI));
            ff.Items.Add(new MenuItemsMapElement("dmi.toolbars", MIRType.DMI));
            ff.Items.Add(new MenuItemsMapElement("", MIRType.SMI));
            ff.Items.Add(new MenuItemsMapElement("cmi.edit.theme", MIRType.CMI));
            ff.Items.Add(new MenuItemsMapElement("cmi.edit.mir.map", MIRType.CMI));
            ff.Items.Add(new MenuItemsMapElement("cmi.edit.tbr.map", MIRType.CMI));
            ff.Items.Add(new MenuItemsMapElement("cmi.edit.shortcuts.map", MIRType.CMI));
            map.RootItems.Add(ff);

            ff = new MenuItemsMapElement("root.tools", MIRType.ROOT);
            ff.Items.Add(new MenuItemsMapElement("dmi.language", MIRType.DMI));
            ff.Items.Add(new MenuItemsMapElement("", MIRType.SMI));
            ff.Items.Add(new MenuItemsMapElement("cmi.show.settings", MIRType.CMI));

            map.RootItems.Add(ff);

            ff = new MenuItemsMapElement("root.help", MIRType.ROOT);
            ff.Items.Add(new MenuItemsMapElement("cmi.help", MIRType.CMI));
            map.RootItems.Add(ff);
            return map;
        }
    }
}
