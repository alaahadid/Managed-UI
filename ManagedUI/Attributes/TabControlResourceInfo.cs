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

namespace ManagedUI
{
    /// <summary>
    /// Attribute descripe the resource info of a tab control.
    /// </summary>
    public class TabControlResourceInfoAttribute : Attribute
    {
        /// <summary>
        /// Attribute descripe the resource info of a tab control.
        /// </summary>
        /// <param name="kDisplayName">The key resource of display name.</param>
        public TabControlResourceInfoAttribute(string kDisplayName)
        {
            KDisplayName = kDisplayName;
            KIcon = "";
        }
        /// <summary>
        /// Attribute descripe the resource info of a tab control
        /// </summary>
        /// <param name="kDisplayName">The key resource of display name.</param>
        /// <param name="kIcon">The key resource of icon</param>
        public TabControlResourceInfoAttribute(string kDisplayName, string kIcon)
        {
            KDisplayName = kDisplayName;
            KIcon = kIcon;
        }
        /// <summary>
        /// Get the key resource of display name.
        /// </summary>
        public string KDisplayName { get; private set; }
        /// <summary>
        /// Get the key resource of icon
        /// </summary>
        public string KIcon { get; private set; }
    }
}
