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
using System.Collections.Generic;

namespace ManagedUI
{
    /// <summary>
    /// Element for representing menu item.
    /// </summary>
    [Serializable]
    public class MenuItemsMapElement
    {
        /// <summary>
        /// Create new menu item element
        /// </summary>
        public MenuItemsMapElement()
        {
            ID = "";
            Type = MIRType.ROOT;
            Items = new List<MenuItemsMapElement>();
        }
        /// <summary>
        /// Create new menu item element
        /// </summary>
        /// <param name="mirID">The id of original menu item representator</param>
        /// <param name="type">The type of the MIR</param>
        public MenuItemsMapElement(string mirID, MIRType type)
        {
            ID = mirID;
            Type = type;
            Items = new List<MenuItemsMapElement>();
        }
        /// <summary>
        /// Get or set the menu item representator id
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Get or set the type of this item.
        /// </summary>
        public MIRType Type { get; set; }
        /// <summary>
        /// Get or set the child items of this element.
        /// </summary>
        public List<MenuItemsMapElement> Items { get; set; }
    }
}
