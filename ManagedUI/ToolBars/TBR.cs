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
using System.Drawing;

namespace ManagedUI
{
    /// <summary>
    /// Toolbar representator.
    /// </summary>
    [Serializable]
    public class TBRElement
    {
        /// <summary>
        /// Toolbar representator.
        /// </summary>
        public TBRElement()
        {
            RootItems = new List<ManagedUI.MenuItemsMapElement>();
        }
        private bool visible;
        /// <summary>
        /// Dispose the element
        /// </summary>
        public void Dispose()
        {
            VisibleChanged = null;
        }
        /// <summary>
        /// Get or set the name of this toolbar as set by user.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Get or set the root items collection. Can be a coolection of CMI, PMI and/or SMI. CMI 
        /// will be presented as a button, same for PMI. SMI will be presented as splitter.
        /// </summary>
        public List<MenuItemsMapElement> RootItems { get; set; }
        /// <summary>
        /// Get or set the location of this toolbar
        /// </summary>
        public TBRLocation Location { get; set; }
        /// <summary>
        /// Get or set if this toolbar is visible to user or not.
        /// </summary>
        public bool Visible
        {
            get
            { return visible; }
            set
            { visible = value; VisibleChanged?.Invoke(this, new EventArgs()); }
        }
        /// <summary>
        /// Get or set if this toolbar should use a custom style specified by user.
        /// </summary>
        public bool CustomStyle { get; set; }
        /// <summary>
        /// Get or set the image size of the buttons (CustomStyle MUST be set to true)
        /// </summary>
        public Size ImageSize { get; set; }
        /// <summary>
        /// TBRElement.ToString()
        /// </summary>
        /// <returns>Name [Location]</returns>
        public override string ToString()
        {
            return string.Format("{0} [{1}]", Name, Location.ToString());
        }
        /// <summary>
        /// Raised when the Visible is changed
        /// </summary>
        public event EventHandler VisibleChanged;
    }
}
