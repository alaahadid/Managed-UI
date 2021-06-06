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
    /// Represent a shortcut
    /// </summary>
    [Serializable]
    public class ShortCut
    {
        /// <summary>
        /// Get or set the id of this shortcut
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Get or set the command combination/CMI ID
        /// </summary>
        public string TargetID { get; set; }
        /// <summary>
        /// Get or set the mode of this shortcut:
        /// </summary>
        public ShortcutMode Mode { get; set; }
        /// <summary>
        /// Get or set the shortcut string. 
        /// </summary>
        public string TheShortcut { get; set; }
        /// <summary>
        /// Get or set if this shortcut can be changed by user.
        /// </summary>
        public bool Changable { get; set; }
    }
}
