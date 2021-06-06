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
using System.Collections.Generic;
using System.Drawing;

namespace ManagedUI
{
    /// <summary>
    /// The parameters to use when initializing the ManagedUI core (MUI)
    /// </summary>
    public struct MUIParameters
    {
        /// <summary>
        /// Get or set the project title. This will be used in the main form and for settings. If set to null, MUI will use ManagedUI as a project title.
        /// </summary>
        public string ProjectTitle
        { get; set; }
        /// <summary>
        /// Get or set the project copyright, for example: Copyright (C) [Year] [Author or name]
        /// </summary>
        public string ProjectCopyright
        { get; set; }
        /// <summary>
        /// Get or set the project copyright message, this will be appeared to user in the splash screen and the about window.
        /// </summary>
        public string ProjectCopyrightMessage
        { get; set; }
        /// <summary>
        /// Get or set the icon to use in the main form. If set to null, MUI will use the default vs icon for the form.
        /// </summary>
        public Icon ProjectIcon
        { get; set; }
        /// <summary>
        /// Get or set the splash window size. If set to null or zero, MUI will use default window size.
        /// </summary>
        public Size SplashSize
        { get; set; }
        /// <summary>
        /// Get or set the version of the project. If set null, MUI will use 1.0 as a project version.
        /// </summary>
        public string ProjectVersion
        { get; set; }
        /// <summary>
        /// Get or set the splash window background image when UseBackgroundColorForSplash is false. If set to null, the splash window will use no image for background.
        /// </summary>
        public Image SplashBackgroundImage
        { get; set; }
        /// <summary>
        /// Get or set if MUI should show the splash window when initializing.
        /// </summary>
        public bool ShowSplash
        { get; set; }
        /// <summary>
        /// Get or set if MUI should use a background color instead of image.
        /// </summary>
        public bool UseColorForSplashBackground
        { get; set; }
        /// <summary>
        /// Get or set the color to use for splash window background when UseBackgroundColorForSplash is set.
        /// </summary>
        public Color SplashBackgroundColor
        { get; set; }
        /// <summary>
        /// Get or set a collection of commands to execute when MUI is ready, these are executed at start-up along with the command-lines which will be executed after these (if any)
        /// </summary>
        public List<string> StartupArguments
        { get; set; }
        /// <summary>
        /// Get or set a collection of commands to execute when MUI is closing. This commands get executed after the user closes the main window, the services will still be loaded and commands can be executed.
        /// </summary>
        public List<string> CloseupArguments
        { get; set; }
        /// <summary>
        /// Get or set an array of required service ids in order the application to work. This is list REQUIRED, at least one service should be listed.
        /// YOU CANNOT USE BUILT IN SERVICES, such as "cmd" or "gui".
        /// </summary>
        public List<string> RequiredServiceIDS
        { get; set; }
        /// <summary>
        /// Get or set if all services are allowed in the application. If false, all services will removed except for: 1 MUI built in services 2 required services.
        /// </summary>
        public bool AllowAllServices
        { get; set; }
    }
}
