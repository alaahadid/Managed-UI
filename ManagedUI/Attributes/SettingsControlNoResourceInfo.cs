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
    /// Attribute descripe the inf of a settings control when no resources used.
    /// </summary>
    public class SettingsControlNoResourceInfo : Attribute
    {
        /// <summary>
        /// Attribute descripe the inf of a settings control when no resources used.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="category">The category.</param>
        public SettingsControlNoResourceInfo(string displayName, string category)
        {
            DisplayName = displayName;
            Category = category;
            Describtion = "";
        }
        /// <summary>
        /// Attribute descripe the resource info of a settings control
        /// </summary>
        /// <param name="displayName">The key resource of display name.</param>
        /// <param name="category">The key resource of category.</param>
        /// <param name="desc">The key resource of describtion</param>
        public SettingsControlNoResourceInfo(string displayName, string category, string desc)
        {
            DisplayName = displayName;
            Category = category;
            Describtion = desc;
        }
        /// <summary>
        /// Get the display name.
        /// </summary>
        public string DisplayName { get; private set; }
        /// <summary>
        /// Get the category
        /// </summary>
        public string Category { get; private set; }
        /// <summary>
        /// Get the describtion
        /// </summary>
        public string Describtion { get; private set; }
    }
}
