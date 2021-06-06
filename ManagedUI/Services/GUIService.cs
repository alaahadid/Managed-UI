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
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace ManagedUI
{
    /// <summary>
    /// Service that responsible for managing the gui stuff in the application
    /// </summary>
    [Export(typeof(IService))]
    [Export(typeof(GUIService))]
    [ServiceInfo("GUI", "gui", "Service that responsible for managing the gui stuff in the application", true)]
    public sealed class GUIService : IService
    {
        private MenuItemsMap menuItemsMap;
        private TBRMap toolbarMap;
        private ShortcutsMap shortcutsMap;
        private TabControlContainer tabsMap;
        private Theme theme;
        private string selectedTabControlID;
        private bool setWindowTitle;
        private string windowTitleToSet;

        // Methods
        /// <summary>
        /// Initialize the GUI core.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            HandleConfiguration();
            // Now we need to setup resources ....
            foreach (Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> it in AvailableMenuItems)
            {
                it.Value.FindResources();
            }
            foreach (Lazy<ITabControl, IControlInfo> it in AvailableTabControls)
            {
                it.Value.FindResources();
                it.Value.Initialize();
            }
            foreach (Lazy<ISettingsControl, IControlInfo> it in AvailableSettingControls)
            {
                it.Value.FindResources();
                it.Value.Initialize();
            }
            #region 1 Load current menu items map
            bool success = false;
            // Try out the documents one
            string path = Path.Combine(MUI.Documentsfolder, "menu.mim");

            if (!File.Exists(path))
                path = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "menu.mim");// Use the default one if exists

            if (!File.Exists(path))
            {
                // Both are not exist, we need to generate new one !
                menuItemsMap = MenuItemsMap.DefaultMenuItemsMap();
                WriteWarning(Properties.Resources.Status_GUI1);
                MenuItemsMap.SaveMenuItemsMap(Path.Combine(MUI.Documentsfolder, "menu.mim"), menuItemsMap);
            }
            else
            {
                // Try to load it...
                menuItemsMap = MenuItemsMap.LoadMenuItemsMap(path, out success);
                if (!success)
                {
                    //Not successed, we need to generate new one !
                    menuItemsMap = MenuItemsMap.DefaultMenuItemsMap();
                    WriteWarning(Properties.Resources.Status_GUI1);
                    MenuItemsMap.SaveMenuItemsMap(Path.Combine(MUI.Documentsfolder, "menu.mim"), menuItemsMap);
                }
            }
            #endregion
            #region 2 Load current toolbars map
            path = Path.Combine(MUI.Documentsfolder, "toolbars.tbm");

            if (!File.Exists(path))
                path = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "toolbars.tbm");// Use the default one if exists

            if (!File.Exists(path))
            {
                // We need to generate new one !
                toolbarMap = TBRMap.DefaultTBRMap();
                WriteWarning(Properties.Resources.Status_GUI2);
                TBRMap.SaveTBRMap(Path.Combine(MUI.Documentsfolder, "toolbars.tbm"), toolbarMap);
            }
            else
            {
                toolbarMap = TBRMap.LoadTBRMap(path, out success);
                if (!success)
                {
                    // We need to generate new one !
                    toolbarMap = TBRMap.DefaultTBRMap();
                    WriteWarning(Properties.Resources.Status_GUI2);
                    TBRMap.SaveTBRMap(Path.Combine(MUI.Documentsfolder, "toolbars.tbm"), toolbarMap);
                }
            }
            #endregion
            #region 3 Load current shortcuts map
            path = Path.Combine(MUI.Documentsfolder, "shortcuts.sm");

            if (!File.Exists(path))
                path = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "shortcuts.sm");// Use the default one if exists

            if (!File.Exists(path))
            {
                // We need to generate new one !
                shortcutsMap = ShortcutsMap.DefaultShortcutsMap();
                WriteWarning(Properties.Resources.Status_GUI3);
                ShortcutsMap.SaveShortcutsMap(Path.Combine(MUI.Documentsfolder, "shortcuts.sm"), shortcutsMap);
            }
            else
            {
                shortcutsMap = ShortcutsMap.LoadShortcutsMap(path, out success);
                if (!success)
                {
                    // We need to generate new one !
                    shortcutsMap = ShortcutsMap.DefaultShortcutsMap();
                    WriteWarning(Properties.Resources.Status_GUI3);
                    ShortcutsMap.SaveShortcutsMap(Path.Combine(MUI.Documentsfolder, "shortcuts.sm"), shortcutsMap);
                }
            }
            #endregion
            #region 4 Load current tab controls map
            path = Path.Combine(MUI.Documentsfolder, "controls.tcm");

            if (!File.Exists(path))
                path = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "controls.tcm");// Use the default one if exists

            if (File.Exists(path))
            {
                tabsMap = TabControlContainer.LoadTCCMap(path, out success);
            }
            #endregion
            #region 5 Load current theme
            path = Path.Combine(MUI.Documentsfolder, "theme.mt");

            if (!File.Exists(path))
                path = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "theme.mt");// Use the default one if exists

            if (!File.Exists(path))
            {
                // We need to generate new one !
                theme = Theme.DefaultTheme();
                WriteWarning(Properties.Resources.Status_GUI4);
                Theme.SaveTHMMap(Path.Combine(MUI.Documentsfolder, "theme.mt"), theme);
            }
            else
            {
                theme = Theme.LoadTHMMap(path, out success);
                if (!success)
                {
                    // We need to generate new one !
                    theme = Theme.DefaultTheme();
                    WriteWarning(Properties.Resources.Status_GUI4);
                    Theme.SaveTHMMap(Path.Combine(MUI.Documentsfolder, "theme.mt"), theme);
                }
            }
            #endregion

            // Add trace listeners
            Lazy<ITabControl, IControlInfo> tt = GetTabControl("tc.log");
            if (tt != null)
            {
                Trace.Listeners.Add(new TraceListenerLogsTabControl((TabControlLogs)tt.Value));
            }
        }
        /// <summary>
        /// Let the core handle the configurations of GUIConfiguration.
        /// </summary>
        public void HandleConfiguration()
        {
            if (!GUIConfiguration.UserCanEditMenu)
            {
                CommandsManager.CMD.RemoveCommand("edit.mir.map");
                RemoveMenuItem("cmi.edit.mir.map");
            }
            if (!GUIConfiguration.UserCanEditShortcuts)
            {
                CommandsManager.CMD.RemoveCommand("edit.shortcuts.map");
                RemoveMenuItem("cmi.edit.shortcuts.map");
            }
            if (!GUIConfiguration.UserCanEditTheme)
            {
                CommandsManager.CMD.RemoveCommand("edit.theme");
                RemoveMenuItem("cmi.edit.theme");
            }
            if (!GUIConfiguration.UserCanEditToolbars)
            {
                CommandsManager.CMD.RemoveCommand("edit.tbr.map");
                RemoveMenuItem("cmi.edit.tbr.map");
            }
            if (!GUIConfiguration.UserCanHideTabs)
            {
                CommandsManager.CMD.RemoveCommand("tabcontrol.set.visible");
                RemoveMenuItem("dmi.tabs");
            }
            if (!GUIConfiguration.UserCanHideToolbars)
            {
                CommandsManager.CMD.RemoveCommand("set.tbr.visible");
                RemoveMenuItem("dmi.toolbars");
            }
            if (!GUIConfiguration.UserCanChangeLanguage)
            {
                CommandsManager.CMD.RemoveCommand("set.language");
                RemoveMenuItem("dmi.language");
            }
            if (!GUIConfiguration.EnableExitCMI)
            {
                RemoveMenuItem("cmi.exit");
            }
            if (!GUIConfiguration.EnableHelpCMI)
            {
                RemoveMenuItem("cmi.help");
            }
            if (!GUIConfiguration.EnableShowSettingsCMI)
            {
                RemoveMenuItem("cmi.show.settings");
            }
            if (!GUIConfiguration.EnableLogBoxTabCotntrol)
            {
                RemoveTabControl("tc.log");
            }
            if (!GUIConfiguration.EnableShortcutsHotkeysSettingsControl)
            {
                RemoveSettingsControl("sc.hotkeys");
            }
            if (!GUIConfiguration.EnableMenuItemsSettingsControl)
            {
                RemoveSettingsControl("sc.menuitems");
            }
            if (!GUIConfiguration.EnableThemeSettingsControl)
            {
                RemoveSettingsControl("sc.theme");
            }
            if (!Properties.Settings.Default.FirstTime)
            {
                Properties.Settings.Default.FirstTime = true;
                Properties.Settings.Default.MainWindowSize = GUIConfiguration.MainWindowDefaultSize;
            }
#if !DEBUG
            // We don't need the debug commands and menus anymore
            RemoveMenuItem("cbmi.blank");
            RemoveMenuItem("tmi.blank");
#endif
        }
        /// <summary>
        /// Initialize the main form
        /// </summary>
        public void InitializeTheMainForm()
        {
            WriteStatus(Properties.Resources.Status_LoadingTheMainWindow);
            if (!IsMainFormInitialized)
            {
                MainForm = new FormMain();
                IsMainFormInitialized = true;

                Trace.Listeners.Add(new TraceListenerMainForm());
            }
            if (MUI.ProjectIcon != null)
                MainForm.Icon = MUI.ProjectIcon;
            if (setWindowTitle)
            {
                setWindowTitle = false;
                MainForm.Text = windowTitleToSet;
            }
            WriteStatus(Properties.Resources.Status_MainWindowLoadedSuccessfully);
        }
        /// <summary>
        /// Close the GUI core.
        /// </summary>
        public override void Close()
        {
            base.Close();
            MenuItemsMap.SaveMenuItemsMap(Path.Combine(MUI.Documentsfolder, "menu.mim"), menuItemsMap);
            TBRMap.SaveTBRMap(Path.Combine(MUI.Documentsfolder, "toolbars.tbm"), toolbarMap);
            ShortcutsMap.SaveShortcutsMap(Path.Combine(MUI.Documentsfolder, "shortcuts.sm"), shortcutsMap);
            Theme.SaveTHMMap(Path.Combine(MUI.Documentsfolder, "theme.mt"), theme);
            tabsMap.ClearContainer(false);
            TabControlContainer.SaveTCCMap(Path.Combine(MUI.Documentsfolder, "controls.tcm"), tabsMap);
        }
        /// <summary>
        /// Get a menu item from the core using id
        /// </summary>
        /// <param name="id">The mir id</param>
        /// <returns>The menu item if found otherwise null.</returns>
        public Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> GetMenuItem(string id)
        {
            if (AvailableMenuItems == null)
                return null;
            foreach (Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> it in AvailableMenuItems)
            {
                if (it.Metadata.ID == id)
                    return it;
            }
            return null;
        }
        /// <summary>
        /// Remove a menu item from the menu items list. Note that once an item removed, it cannot be
        /// accessed until the application restarts.
        /// </summary>
        /// <param name="id">The menu item id.</param>
        public void RemoveMenuItem(string id)
        {
            if (AvailableMenuItems == null)
                return;
            foreach (Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> it in AvailableMenuItems)
            {
                if (it.Metadata.ID == id)
                {
                    AvailableMenuItems.Remove(it);
                    break;
                }
            }
            foreach (Lazy<CMI, IMenuItemRepresentatorInfo> it in AvailableCMIs)
            {
                if (it.Metadata.ID == id)
                {
                    AvailableCMIs.Remove(it);
                    break;
                }
            }
            foreach (Lazy<RMI, IMenuItemRepresentatorInfo> it in AvailableRMIs)
            {
                if (it.Metadata.ID == id)
                {
                    AvailableRMIs.Remove(it);
                    break;
                }
            }
            foreach (Lazy<DMI, IMenuItemRepresentatorInfo> it in AvailableDMIs)
            {
                if (it.Metadata.ID == id)
                {
                    AvailableDMIs.Remove(it);
                    break;
                }
            }
            foreach (Lazy<PMI, IMenuItemRepresentatorInfo> it in AvailablePMIs)
            {
                if (it.Metadata.ID == id)
                {
                    AvailablePMIs.Remove(it);
                    break;
                }
            }
            foreach (Lazy<TMI, IMenuItemRepresentatorInfo> it in AvailableTMIs)
            {
                if (it.Metadata.ID == id)
                {
                    AvailableTMIs.Remove(it);
                    break;
                }
            }
            foreach (Lazy<CBMI, IMenuItemRepresentatorInfo> it in AvailableCBMIs)
            {
                if (it.Metadata.ID == id)
                {
                    AvailableCBMIs.Remove(it);
                    break;
                }
            }
        }
        /// <summary>
        /// Remove a tab control from the list. Note that once a tab control is removed, it cannot be
        /// accessed until the application restarts.
        /// </summary>
        /// <param name="id">The tab control id</param>
        public void RemoveTabControl(string id)
        {
            if (AvailableTabControls == null)
                return;
            foreach (Lazy<ITabControl, IControlInfo> it in AvailableTabControls)
            {
                if (it.Metadata.ID == id)
                {
                    AvailableTabControls.Remove(it);
                    break;
                }
            }
        }
        /// <summary>
        /// Remove a settings control
        /// </summary>
        /// <param name="id">The settings control id</param>
        public void RemoveSettingsControl(string id)
        {
            if (AvailableSettingControls == null)
                return;
            foreach (Lazy<ISettingsControl, IControlInfo> it in AvailableSettingControls)
            {
                if (it.Metadata.ID == id)
                {
                    AvailableSettingControls.Remove(it);
                    break;
                }
            }
        }
        /// <summary>
        /// Get a tab control using id
        /// </summary>
        /// <param name="id">The tab control id</param>
        /// <returns>The tab control if found otherwise false</returns>
        public Lazy<ITabControl, IControlInfo> GetTabControl(string id)
        {
            if (AvailableTabControls == null)
                return null;
            foreach (Lazy<ITabControl, IControlInfo> it in AvailableTabControls)
            {
                if (it.Metadata.ID == id)
                    return it;
            }
            return null;
        }
        /// <summary>
        /// Get a settings control using id
        /// </summary>
        /// <param name="id">The settings control id</param>
        /// <returns>The settings control if found otherwise false</returns>
        public Lazy<ISettingsControl, IControlInfo> GetSettingsControl(string id)
        {
            if (AvailableSettingControls == null)
                return null;
            foreach (Lazy<ISettingsControl, IControlInfo> it in AvailableSettingControls)
            {
                if (it.Metadata.ID == id)
                    return it;
            }
            return null;
        }
        /// <summary>
        /// Update the main form title.
        /// </summary>
        /// <param name="title">The new title for the main form.</param>
        public void UpdateMainWindowTitle(string title)
        {
            if (MainForm == null)
            {
                setWindowTitle = true;
                windowTitleToSet = title;
                return;
            }
            MainForm.Text = title;
        }
        /// <summary>
        /// Save all available tab control settings. Best place to call this method is at MUI.ApplicationExit event handling.
        /// </summary>
        public void SaveTabControlsSettings()
        {
            for (int i = 0; i < AvailableTabControls.Count; i++)
                AvailableTabControls[i].Value.SaveSettings();
        }
        /// <summary>
        /// Load all available tab control settings. The best place to call this method is at application service Initialize.
        /// </summary>
        public void LoadTabControlsSettings()
        {
            for (int i = 0; i < AvailableTabControls.Count; i++)
                AvailableTabControls[i].Value.LoadSettings();
        }
        // Properties
        /// <summary>
        /// Get or set the menu items map that will be used for the app.
        /// </summary>
        public MenuItemsMap CurrentMenuItemsMap
        {
            get { return menuItemsMap; }
            set
            {
                menuItemsMap = value;
                MenuItemsMapChanged?.Invoke(this, new EventArgs());
            }
        }
        /// <summary>
        /// Get or set the toolbars map that will be used for the app.
        /// </summary>
        public TBRMap CurrentToolbarsMap
        {
            get { return toolbarMap; }
            set
            {
                ToolbarsMapAboutToBeChanged?.Invoke(this, new EventArgs());
                toolbarMap = value;
                ToolbarsMapChanged?.Invoke(this, new EventArgs());
            }
        }
        /// <summary>
        /// Get or set the current shortcuts map to use in the application.
        /// </summary>
        public ShortcutsMap CurrentShortcutsMap
        {
            get { return shortcutsMap; }
            set
            {
                shortcutsMap = value;
                ShortcutsMapChanged?.Invoke(this, new EventArgs());
            }
        }
        /// <summary>
        /// Get or set the current tab controls map
        /// </summary>
        public TabControlContainer CurrentTabsMap
        {
            get { return tabsMap; }
            set
            {
                TabsControlsMapAboutToBeChanged?.Invoke(this, new EventArgs());
                tabsMap = value;
                TabsControlsMapChanged?.Invoke(this, new EventArgs());
            }
        }
        /// <summary>
        /// Get or set the current selected tab control
        /// </summary>
        public string SelectedTabControlID
        {
            get { return selectedTabControlID; }
            set
            {
                if (selectedTabControlID != value)
                {
                    selectedTabControlID = value;
                    SelectedTabControlIDChanged?.Invoke(this, new EventArgs());
#if DEBUG
                    WriteLine("Selected tab control changed to '" + selectedTabControlID + "'");
#endif
                }
            }
        }
        /// <summary>
        /// Get currently selected tab control (calculated)
        /// </summary>
        public Lazy<ITabControl, IControlInfo> SelectedTabControl
        {
            get
            {
                return GUIService.GUI.GetTabControl(selectedTabControlID);
            }
        }
        /// <summary>
        /// Get or set the current theme
        /// </summary>
        public Theme CurrentTheme
        {
            get { return theme; }
            set
            {
                ThemeAboutToBeChanged?.Invoke(this, new EventArgs());
                theme = value;
                ThemeChanged?.Invoke(this, new EventArgs());
            }
        }
        /// <summary>
        /// Get the main form of MUI
        /// </summary>
        public FormMain MainForm
        { get; private set; }
        /// <summary>
        /// Get if the main form in initialized.
        /// </summary>
        public bool IsMainFormInitialized { get; private set; }
        /// <summary>
        /// Get all available menu items of the app.
        /// </summary>
        [ImportMany]
        public List<Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo>> AvailableMenuItems
        {
            get; private set;
        }
        /// <summary>
        /// Get all available CMI's
        /// </summary>
        [ImportMany]
        public List<Lazy<CMI, IMenuItemRepresentatorInfo>> AvailableCMIs
        {
            get; private set;
        }
        /// <summary>
        /// Get all available RMI's
        /// </summary>
        [ImportMany]
        public List<Lazy<RMI, IMenuItemRepresentatorInfo>> AvailableRMIs
        {
            get; private set;
        }
        /// <summary>
        /// Get all available DMI's
        /// </summary>
        [ImportMany]
        public List<Lazy<DMI, IMenuItemRepresentatorInfo>> AvailableDMIs
        {
            get; private set;
        }
        /// <summary>
        /// Get all available PMI's
        /// </summary>
        [ImportMany]
        public List<Lazy<PMI, IMenuItemRepresentatorInfo>> AvailablePMIs
        {
            get; private set;
        }
        /// <summary>
        /// Get all available TMI's
        /// </summary>
        [ImportMany]
        public List<Lazy<TMI, IMenuItemRepresentatorInfo>> AvailableTMIs
        {
            get; private set;
        }
        /// <summary>
        /// Get all available CBMI's
        /// </summary>
        [ImportMany]
        public List<Lazy<CBMI, IMenuItemRepresentatorInfo>> AvailableCBMIs
        {
            get; private set;
        }

        /// <summary>
        /// Get all avaialble tab controls in the application.
        /// </summary>
        [ImportMany]
        public List<Lazy<ITabControl, IControlInfo>> AvailableTabControls
        {
            get; private set;
        }
        /// <summary>
        /// Get all avaialble setting controls in the application.
        /// </summary>
        [ImportMany]
        public List<Lazy<ISettingsControl, IControlInfo>> AvailableSettingControls
        {
            get; private set;
        }

        // Events
        /// <summary>
        /// Raised when the current menu item map is changed.
        /// </summary>
        public event EventHandler MenuItemsMapChanged;
        /// <summary>
        /// Raised when the current toolbars map is about to be changed (before get changed).
        /// </summary>
        public event EventHandler ToolbarsMapAboutToBeChanged;
        /// <summary>
        /// Raised when the current toolbars map is changed.
        /// </summary>
        public event EventHandler ToolbarsMapChanged;
        /// <summary>
        /// Raised when the CurrentShortcutsMap is changed.
        /// </summary>
        public event EventHandler ShortcutsMapChanged;
        /// <summary>
        /// Raised when the CurrentTabsMap is changed
        /// </summary>
        public event EventHandler TabsControlsMapChanged;
        /// <summary>
        /// Raised when the tabs control map is about to be changed.
        /// </summary>
        public event EventHandler TabsControlsMapAboutToBeChanged;
        /// <summary>
        /// Raised when the theme is about to be changed
        /// </summary>
        public event EventHandler ThemeAboutToBeChanged;
        /// <summary>
        /// Raised when the theme is changed
        /// </summary>
        public event EventHandler ThemeChanged;
        /// <summary>
        /// Raised when the selected tab control id is changed.
        /// </summary>
        public event EventHandler SelectedTabControlIDChanged;
        /// <summary>
        /// Raised when the settings form is opened
        /// </summary>
        public event EventHandler SettingsOpened;
        /// <summary>
        /// Raised when the settings form is closed discarding all changes
        /// </summary>
        public event EventHandler SettingsClosedDiscarded;
        /// <summary>
        /// Raised when the settings form is closed and saved all settings
        /// </summary>
        public event EventHandler SettingsClosedSaving;
        /// <summary>
        /// Raised when the settings control is resetting to defaults (one page)
        /// </summary>
        public event EventHandler<ControlAddedRemovedArgs> SettingsResetDefault;
        /// <summary>
        /// Raised when the settings control is resettings all settings to default (all pages)
        /// </summary>
        public event EventHandler SettingsResetDefaultAll;
        /// <summary>
        /// Raised when a progress occur
        /// </summary>
        public event EventHandler<ProgressEventArgs> Progress;
        /// <summary>
        /// Raised when a progress is begenning
        /// </summary>
        public event EventHandler<ProgressEventArgs> ProgressBegin;
        /// <summary>
        /// Raised when a prgress is finished.
        /// </summary>
        public event EventHandler<ProgressEventArgs> ProgressFinished;

        /// <summary>
        /// Raises the ProgressBegin event
        /// </summary>
        /// <param name="message">The message to use in the event</param>
        public void OnProgressStarted(string message)
        {
            ProgressBegin?.Invoke(this, new ProgressEventArgs(message, 0));
        }
        /// <summary>
        /// Raises the ProgressFinished event
        /// </summary>
        /// <param name="message">The message to use in the event</param>
        public void OnProgressFinished(string message)
        {
            ProgressFinished?.Invoke(this, new ProgressEventArgs(message, 100));
        }
        /// <summary>
        /// Raises the Progress event
        /// </summary>
        /// <param name="message">The message to use in the event</param>
        /// <param name="progress">The precentage value, must be between 0 and 100.</param>
        public void OnProgress(string message, int progress)
        {
            if (progress < 0 || progress > 100)
                return;

            Progress?.Invoke(this, new ProgressEventArgs(message, progress));
        }
        /// <summary>
        /// Raises the SettingsOpened event
        /// </summary>
        internal void OnSettingsOpened()
        { SettingsOpened?.Invoke(this, new EventArgs()); }
        /// <summary>
        /// Raises the SettingsClosedDiscarded event
        /// </summary>
        internal void OnSettingsClosedDiscarded()
        { SettingsClosedDiscarded?.Invoke(this, new EventArgs()); }
        /// <summary>
        /// Raises the ClosedSaving event
        /// </summary>
        internal void OnSettingsClosedSaving()
        { SettingsClosedSaving?.Invoke(this, new EventArgs()); }
        /// <summary>
        /// Raises the SettingsResetDefault event
        /// </summary>
        /// <param name="id">The settings control id that is resetting to defaults</param>
        internal void OnSettingsResetDefault(string id)
        { SettingsResetDefault?.Invoke(this, new ControlAddedRemovedArgs(id)); }
        /// <summary>
        /// Raises the SettingsResetDefaultAll event
        /// </summary>
        internal void OnSettingsResetDefaultAll()
        { SettingsResetDefaultAll?.Invoke(this, new EventArgs()); }

        #region STATIC
        private static GUIService asService;

        /// <summary>
        /// Get this service loaded from the core and ready to go.
        /// </summary>
        public static GUIService GUI
        {
            get
            {
                if (asService == null)
                {
                    Lazy<IService, IServiceInfo> ser = MUI.GetServiceByID("gui");
                    if (ser != null)
                        asService = (GUIService)ser.Value;
                }
                return asService;
            }
        }
        #endregion
    }
}
