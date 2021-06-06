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
    /// Represents map of shortcuts
    /// </summary>
    [Serializable]
    public class ShortcutsMap
    {
        /// <summary>
        /// Get or set the name of this map
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Get or set the shortcuts collection
        /// </summary>
        public List<ShortCut> Shortcuts { get; set; }
        /// <summary>
        /// Get a shortcut for a CMI/CC
        /// </summary>
        /// <param name="targetID">The target CMI/CC object</param>
        /// <param name="type">The type of the object, CMI or CC</param>
        /// <returns>The shortcut if found otherwise empty string.</returns>
        public string GetShortcut(string targetID, ShortcutMode type)
        {
            foreach (ShortCut shrt in Shortcuts)
            {
                if (shrt.Mode == type && shrt.TargetID == targetID)
                    return shrt.TheShortcut;
            }
            return "";
        }
        /// <summary>
        /// Execute a command from hotkey
        /// </summary>
        /// <param name="shortcut">The shortcut (hotkey)</param>
        /// <returns>True if the shortcut found and executed, otherwise false.</returns>
        public bool ExecuteShortcut(string shortcut)
        {
            if (shortcut == null || shortcut == "")
            {
                Trace.TraceError(Properties.Resources.Status_AttembtingToExecuteAnEmptyShortcut);
                return false;
            }
            foreach (ShortCut shrt in Shortcuts)
            {
                if (shortcut == shrt.TheShortcut)
                {
                    // This is it !!
                    switch (shrt.Mode)
                    {
                        case ShortcutMode.CMI:
                            {
                                Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> mir =
                                    GUIService.GUI.GetMenuItem(shrt.TargetID);
                                if (mir != null)
                                {
                                    if (mir.Value is CMI)
                                    {
                                        CMI cmi = (CMI)mir.Value;
                                        // Get the command
                                        Lazy<ICommand, ICommandInfo> cmd = CommandsManager.CMD.GetCommand(cmi.CommandID);
                                        if (cmd != null)
                                        {
                                            object[] responses = new object[0];
                                            if (cmi.UseParameters)
                                                cmd.Value.Execute(cmi.Parameters, out responses);
                                            else
                                                cmd.Value.Execute(out responses);
                                            cmi.OnCommandResponse(responses);
#if DEBUG
                                            Trace.WriteLine("Shortcut executed : " + shortcut + ", command id : " + cmd.Metadata.ID);
#endif
                                            return true;
                                        }
                                    }
                                }
                                break;
                            }
                        case ShortcutMode.CC:
                            {
                                Lazy<ICommandCombination, ICommandCombinationInfo> cc = CommandsManager.CMD.GetCommandCombination(shrt.TargetID);
                                if (cc != null)
                                {
                                    // Get the command
                                    Lazy<ICommand, ICommandInfo> cmd = CommandsManager.CMD.GetCommand(cc.Value.CommandID);
                                    if (cmd != null)
                                    {
                                        object[] responses = new object[0];
                                        if (cc.Value.UseParameters)
                                            cmd.Value.Execute(cc.Value.Parameters, out responses);
                                        else
                                            cmd.Value.Execute(out responses);
                                        cc.Value.OnCommandResponse(responses);
#if DEBUG
                                        Trace.WriteLine("Shortcut executed : " + shortcut + ", command id : " + cmd.Metadata.ID);
#endif
                                        return true;
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Load a shortcuts map from file
        /// </summary>
        /// <param name="filePath">The complete file path</param>
        /// <param name="success">Set to true when the load successed. Otherwise set to false.</param>
        /// <returns>The shortcuts map loaded if load is successed. </returns>
        public static ShortcutsMap LoadShortcutsMap(string filePath, out bool success)
        {
            success = false;
            Trace.WriteLine(Properties.Resources.Status_LoadingShortcutsMapAt + " " + filePath + " ...");
            if (!File.Exists(filePath))
            {
                Trace.WriteLine(Properties.Resources.Status_UnableToLoadTheShortcutsMap + ", " +
                    Properties.Resources.Status_fileIsNotExistAt + " " + filePath, StatusMode.Error);
                return null;
            }
            XmlReaderSettings sett = new XmlReaderSettings();
            sett.DtdProcessing = DtdProcessing.Ignore;
            sett.IgnoreWhitespace = true;
            XmlReader XMLread = XmlReader.Create(filePath, sett);
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(ShortcutsMap));

                ShortcutsMap map = (ShortcutsMap)ser.Deserialize(XMLread);

                XMLread.Close();
                Trace.WriteLine(Properties.Resources.Status_ShortcutsMapFileLoadedSuccessfullyFromPath + " " + filePath + ".", StatusMode.Information);
                success = map != null;
                if (map == null)
                {
                    Trace.WriteLine(Properties.Resources.Status_UnableToLoadTheShortcutsMap + " " + filePath + ": " + Properties.Resources.Status_FileIsDamagedOrNotAShrt, StatusMode.Error);
                    return null;
                }
                return map;
            }
            catch (Exception ex)
            {
                XMLread.Close();
                Trace.WriteLine(Properties.Resources.Status_UnableToLoadTheShortcutsMap + " " + filePath + ": " + ex.Message + "/n" + ex.ToString(), StatusMode.Error);
            }
            return null;
        }
        /// <summary>
        /// Save a shortcuts map into file
        /// </summary>
        /// <param name="filePath">The complete path where to save the map file.</param>
        /// <param name="map">The menu items map to save.</param>
        /// <returns>True if save operation successed otherwise false.</returns>
        public static bool SaveShortcutsMap(string filePath, ShortcutsMap map)
        {
            Trace.WriteLine(Properties.Resources.Status_SavingShortcutsMap + ": " + filePath + " ...");

            try
            {
                XmlWriterSettings sett = new XmlWriterSettings();
                sett.Indent = true;
                XmlWriter XMLwrt = XmlWriter.Create(filePath, sett);
                XmlSerializer ser = new XmlSerializer(typeof(ShortcutsMap));

                ser.Serialize(XMLwrt, map);
                XMLwrt.Flush();
                XMLwrt.Close();

                Trace.WriteLine(Properties.Resources.Status_ShortcutsMapSavedSuccessfully +
                    " " + filePath + ".", StatusMode.Information);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(Properties.Resources.Status_UnableToSaveTheMapFile + " " + filePath + ": " + ex.Message + "/n" + ex.ToString(), StatusMode.Error);
            }
            return false;
        }
        /// <summary>
        /// Generate a new default shortcuts map.
        /// </summary>
        /// <returns>The new menu items map.</returns>
        public static ShortcutsMap DefaultShortcutsMap()
        {
            ShortcutsMap map = new ShortcutsMap();
            map.Name = "Default";
            map.Shortcuts = new List<ShortCut>();
            // Get the default shortcuts of CMI's
            foreach (Lazy<CMI, IMenuItemRepresentatorInfo> cmi in GUIService.GUI.AvailableCMIs)
            {
                // Get the shortcut attribute of this cmi
                foreach (Attribute attr in Attribute.GetCustomAttributes(cmi.Value.GetType()))
                {
                    if (attr.GetType() == typeof(ShortcutAttribute))
                    {
                        ShortcutAttribute inf = (ShortcutAttribute)attr;
                        // Create a new shortcut for this cmi
                        ShortCut shrt = new ShortCut();
                        shrt.Changable = inf.ChangableShortcut;
                        shrt.ID = cmi.Metadata.ID + "[shortcut]";
                        shrt.Mode = ShortcutMode.CMI;
                        shrt.TargetID = cmi.Metadata.ID;
                        shrt.TheShortcut = inf.DefaultShortcut;

                        map.Shortcuts.Add(shrt);
                    }
                }
            }
            // Get the default shortcuts of CC's
            foreach (Lazy<ICommandCombination, ICommandCombinationInfo> cc in CommandsManager.CMD.AvaialableCommandCombinations)
            {
                // Get the shortcut attribute of this cc
                foreach (Attribute attr in Attribute.GetCustomAttributes(cc.Value.GetType()))
                {
                    if (attr.GetType() == typeof(ShortcutAttribute))
                    {
                        ShortcutAttribute inf = (ShortcutAttribute)attr;
                        // Create a new shortcut for this cmi
                        ShortCut shrt = new ShortCut();
                        shrt.Changable = inf.ChangableShortcut;
                        shrt.ID = cc.Metadata.ID + "[shortcut]";
                        shrt.Mode = ShortcutMode.CC;
                        shrt.TargetID = cc.Metadata.ID;
                        shrt.TheShortcut = inf.DefaultShortcut;

                        map.Shortcuts.Add(shrt);
                    }
                }
            }
            return map;
        }
    }
}
