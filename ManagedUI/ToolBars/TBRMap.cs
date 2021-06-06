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
using System.Diagnostics;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ManagedUI
{
    /// <summary>
    /// Represents a map of toolbar representator.
    /// </summary>
    [Serializable]
    public class TBRMap
    {
        /// <summary>
        /// Get or set the name of this map as set by user.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Get or set the toolbars collection.c
        /// </summary>
        public List<TBRElement> ToolBars { get; set; }

        /// <summary>
        /// Load a tbr map from file
        /// </summary>
        /// <param name="filePath">The complete file path</param>
        /// <param name="success">Set to true when the load successed. Otherwise set to false.</param>
        /// <returns>The tbr map loaded if load is successed. </returns>
        public static TBRMap LoadTBRMap(string filePath, out bool success)
        {
            success = false;
            Trace.WriteLine(Properties.Resources.Status_LoadingTBRMapFile + " " + filePath + " ...");
            if (!File.Exists(filePath))
            {
                Trace.WriteLine(Properties.Resources.Status_UnableToLoadTBRMapFile +
                    ", " + Properties.Resources.Status_fileIsNotExistAt + " " + filePath,StatusMode.Error);
                return null;
            }
            XmlReaderSettings sett = new XmlReaderSettings();
            sett.DtdProcessing = DtdProcessing.Ignore;
            sett.IgnoreWhitespace = true;
            XmlReader XMLread = XmlReader.Create(filePath, sett);
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(TBRMap));

                TBRMap map = (TBRMap)ser.Deserialize(XMLread);

                XMLread.Close();
                Trace.WriteLine(Properties.Resources.Status_TBRLoadedSuccessAt + " " + filePath + ".", StatusMode.Information);
                success = map != null;
                if (map == null)
                {
                    Trace.WriteLine(Properties.Resources.Status_UnableToLoadTBRMapFile + " " + filePath + ": The file is damaged or not toolbar representators map file.", StatusMode.Error);
                    return null;
                }
                return map;
            }
            catch (Exception ex)
            {
                XMLread.Close();
                Trace.WriteLine(Properties.Resources.Status_UnableToLoadTBRMapFile + " " + filePath + ": " + ex.Message + "/n" + ex.ToString(), StatusMode.Error);
            }
            return null;
        }
        /// <summary>
        /// Save a toolbar representators map into file
        /// </summary>
        /// <param name="filePath">The complete path where to save the map file.</param>
        /// <param name="map">The toolbar representators map to save.</param>
        /// <returns>True if save operation successed otherwise false.</returns>
        public static bool SaveTBRMap(string filePath, TBRMap map)
        {
            Trace.WriteLine(Properties.Resources.Status_SavingTBRAt + ": " + filePath + " ...");

            try
            {
                XmlWriterSettings sett = new XmlWriterSettings();
                sett.Indent = true;
                XmlWriter XMLwrt = XmlWriter.Create(filePath, sett);
                XmlSerializer ser = new XmlSerializer(typeof(TBRMap));

                ser.Serialize(XMLwrt, map);
                XMLwrt.Flush();
                XMLwrt.Close();

                Trace.WriteLine(Properties.Resources.Status_TBRSavedAt + " " + filePath + ".", StatusMode.Information);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(Properties.Resources.Status_UnableToSaveTheMapFile + " " + filePath + ": " + ex.Message + "/n" + ex.ToString(), StatusMode.Error);
            }
            return false;
        }
        /// <summary>
        /// Generate a new default toolbar representators map.
        /// </summary>
        /// <returns>The new menu items map.</returns>
        public static TBRMap DefaultTBRMap()
        {
            TBRMap map = new TBRMap();
            map.Name = "Defaut";
            map.ToolBars = new List<TBRElement>();

            TBRElement defaultTBR = new TBRElement();
            defaultTBR.Name = Properties.Resources.Word_Main;
            defaultTBR.Visible = true;
            defaultTBR.CustomStyle = false;
            defaultTBR.ImageSize = new System.Drawing.Size(16, 16);
            defaultTBR.RootItems = new List<MenuItemsMapElement>();
            // Add buttons
            defaultTBR.RootItems.Add(new MenuItemsMapElement("cmi.help", MIRType.CMI));

            map.ToolBars.Add(defaultTBR);
            return map;
        }
    }
}
