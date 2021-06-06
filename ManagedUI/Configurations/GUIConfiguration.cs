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
    /// <summary>
    /// This class contain static member which the GUI core use when it is initializing.
    /// </summary>
    public sealed class GUIConfiguration
    {
        /// <summary>
        /// This class contain static member which the GUI core use when it is initializing.
        /// </summary>
        static GUIConfiguration()
        {
            UserCanEditMenu = true;
            UserCanEditToolbars = true;
            UserCanHideToolbars = true;
            UserCanEditShortcuts = true;
            UserCanHideTabs = true;
            UserCanEditTheme = true;
            UserCanChangeLanguage = true;
            EnableExitCMI = true;
            EnableHelpCMI = true;
            EnableShowSettingsCMI = true;
            EnableLogBoxTabCotntrol = true;
            EnableShortcutsHotkeysSettingsControl = true;
            EnableMenuItemsSettingsControl = true;
            EnableThemeSettingsControl = true;
            MainWindowDefaultSize = new System.Drawing.Size(1444, 906);
            SplashWindowTextColor = System.Drawing.Color.Black;
        }

        /// <summary>
        /// Get or set if the user can edit main menu. This will disable the command and
        /// the menu item that allows user to edit the main menu.
        /// </summary>
        public static bool UserCanEditMenu { get; set; }
        /// <summary>
        /// Get or set if the user can edit toolbars. This will disable the command and
        /// the menu item that allows user to edit the toolbars.
        /// </summary>
        public static bool UserCanEditToolbars { get; set; }
        /// <summary>
        /// Get or set if the user can toggle the visible of a toolbar.
        /// </summary>
        public static bool UserCanHideToolbars { get; set; }
        /// <summary>
        /// Get or set if the user can edit shortcuts. This will disable the command and
        /// the menu item that allows user to edit the shortcuts.
        /// </summary>
        public static bool UserCanEditShortcuts { get; set; }
        /// <summary>
        /// Get or set if the user can hide or show tab controls. This will disable the commands and
        /// the menu items that allows user to show/hide tab controls.
        /// </summary>
        public static bool UserCanHideTabs { get; set; }
        /// <summary>
        /// Get or set if the user can edit theme. This will disable the command and
        /// the menu item that allows user to edit the theme.
        /// </summary>
        public static bool UserCanEditTheme { get; set; }
        /// <summary>
        /// Get or set if the user can change the interface language or not. This will disable the command and
        /// the menu item that allows user to change interface language.
        /// </summary>
        public static bool UserCanChangeLanguage { get; set; }
        /// <summary>
        /// Get or set if the exit menu item should be enabled or not. 
        /// </summary>
        public static bool EnableExitCMI { get; set; }
        /// <summary>
        /// Get or set if the help menu item should be enabled or not.
        /// </summary>
        public static bool EnableHelpCMI { get; set; }
        /// <summary>
        /// Get or set if the settings menu item should be enabled or not.
        /// </summary>
        public static bool EnableShowSettingsCMI { get; set; }
        /// <summary>
        /// Get or set if the logs tab control should be enabled or not.
        /// </summary>
        public static bool EnableLogBoxTabCotntrol { get; set; }
        /// <summary>
        /// Get or set if the user can edit the hotkeys (shortcuts) in the settings. This will disable the settings control for editing hotkeys.
        /// </summary>
        public static bool EnableShortcutsHotkeysSettingsControl { get; set; }
        /// <summary>
        /// Get or set if the user can edit the main menu items in the settings. This will disable the settings control for editing main menu items.
        /// </summary>
        public static bool EnableMenuItemsSettingsControl { get; set; }
        /// <summary>
        /// Get or set if the user can edit the theme in the settings. This will disable the settings control for editing theme.
        /// </summary>
        public static bool EnableThemeSettingsControl { get; set; }
        /// <summary>
        /// Get or set the default main window size.
        /// </summary>
        public static System.Drawing.Size MainWindowDefaultSize { get; set; }
        /// <summary>
        /// Get or set the default splash window text color. Default is black.
        /// </summary>
        public static System.Drawing.Color SplashWindowTextColor { get; set; }
    }
}
