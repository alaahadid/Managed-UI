// ManagedUI (Managed User Interface)
// A managed user interface framework for .net desktop applications.
// 
// Copyright © Alaa Ibrahim Hadid 2021
//
// This library is free software; you can redistribute it and/or modify 
// it under the terms of the GNU Lesser General Public License as published 
// by the Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful, but 
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
using ManagedUI;
using System.ComponentModel.Composition;

namespace MyProject
{
    [Export(typeof(IService))]
    [Export(typeof(MainService))]
    [ServiceInfo("MainService", "main.service", "The core service of the application.")]
    class MainService : IService
    {
        // We need this field to hold on the disk value
        private string disk;
        private string folder;
        private string file;

        // Properties
        /// <summary>
        /// Get or set current selected disk
        /// </summary>
        public string Disk
        {
            get { return disk; }
            set
            {
                if (disk != value)// To avoid unneeded event raises.
                {
                    disk = value;
                    // Raise the event.
                    DiskChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        /// <summary>
        /// Get or set current selected folder
        /// </summary>
        public string Folder
        {
            get { return folder; }
            set
            {
                if (folder != value)// To avoid unneeded event raises.
                {
                    folder = value;
                    // Raise the event.
                    FolderChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        /// <summary>
        /// Get or set current selected file
        /// </summary>
        public string File
        {
            get { return file; }
            set
            {
                if (file != value)// To avoid unneeded event raises.
                {
                    file = value;
                    // Raise the event.
                    FileChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        // Events
        /// <summary>
        /// Raised when the Disk property is changed.
        /// </summary>
        public event EventHandler DiskChanged;
        /// <summary>
        /// Raised when the Folder property is changed.
        /// </summary>
        public event EventHandler FolderChanged;
        /// <summary>
        /// Raised when the File property is changed.
        /// </summary>
        public event EventHandler FileChanged;
    }
}
