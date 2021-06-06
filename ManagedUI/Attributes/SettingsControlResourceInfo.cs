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
    /// Attribute descripe the resource info of a settings control.
    /// </summary>
    public class SettingsControlResourceInfo : Attribute
    {
        /// <summary>
        /// Attribute descripe the resource info of a settings control.
        /// </summary>
        /// <param name="kDisplayName">The key resource of display name.</param>
        /// <param name="kCategory">The key resource of category.</param>
        public SettingsControlResourceInfo(string kDisplayName, string kCategory)
        {
            KDisplayName = kDisplayName;
            KCategory = kCategory;
            KIcon = "";
            KDescribtion = "";
        }
        /// <summary>
        /// Attribute descripe the resource info of a settings control
        /// </summary>
        /// <param name="kDisplayName">The key resource of display name.</param>
        /// <param name="kCategory">The key resource of category.</param>
        /// <param name="kIcon">The key resource of icon</param>
        public SettingsControlResourceInfo(string kDisplayName, string kCategory, string kIcon)
        {
            KDisplayName = kDisplayName;
            KCategory = kCategory;
            KIcon = kIcon;
            KDescribtion = "";
        }
        /// <summary>
        /// Attribute descripe the resource info of a settings control
        /// </summary>
        /// <param name="kDisplayName">The key resource of display name.</param>
        /// <param name="kCategory">The key resource of category.</param>
        /// <param name="kIcon">The key resource of icon</param>
        /// <param name="kDesc">The key resource of describtion</param>
        public SettingsControlResourceInfo(string kDisplayName, string kCategory, string kIcon, string kDesc)
        {
            KDisplayName = kDisplayName;
            KCategory = kCategory;
            KIcon = kIcon;
            KDescribtion = kDesc;
        }
        /// <summary>
        /// Get the key resource of display name.
        /// </summary>
        public string KDisplayName { get; private set; }
        /// <summary>
        /// Get the key resource of icon
        /// </summary>
        public string KIcon { get; private set; }
        /// <summary>
        /// Get the key resource of category
        /// </summary>
        public string KCategory { get; private set; }
        /// <summary>
        /// Get the key resource of describtion
        /// </summary>
        public string KDescribtion { get; private set; }
    }
}
