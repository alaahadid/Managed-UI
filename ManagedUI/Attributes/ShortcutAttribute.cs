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
    /// Attribute describes the shortcut properties for a CMI or Command Combination.
    /// </summary>
    public class ShortcutAttribute : Attribute
    {
        /// <summary>
        /// Attribute describes the shortcut properties for a CMI or Command Combination.
        /// </summary>
        /// <param name="defaultShortcut">The default shortcut of this CMI/CC</param>
        /// <param name="changable">Indicates if this shortcut can be changed or not.</param>
        public ShortcutAttribute(string defaultShortcut, bool changable)
        {
            DefaultShortcut = defaultShortcut;
            ChangableShortcut = changable;
        }
        /// <summary>
        /// Attribute describes a changeable shortcut properties for a CMI or Command Combination.
        /// </summary>
        /// <param name="defaultShortcut">The default shortcut of this CMI/CC</param>
        public ShortcutAttribute(string defaultShortcut)
        {
            DefaultShortcut = defaultShortcut;
            ChangableShortcut = true;
        }
        /// <summary>
        /// Get the default shortcut of this CMI/CC
        /// </summary>
        public string DefaultShortcut { get; private set; }
        /// <summary>
        /// Indicates if this shortcut can be changed or not.
        /// </summary>
        public bool ChangableShortcut { get; private set; }
    }
}
