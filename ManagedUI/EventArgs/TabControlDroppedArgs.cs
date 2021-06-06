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
    /// Args for tab control dropped events
    /// </summary>
    public class TabControlDroppedArgs : EventArgs
    {
        /// <summary>
        /// Args for tab control dropped events
        /// </summary>
        /// <param name="id">he control id</param>
        /// <param name="mode">The dragging mode</param>
        public TabControlDroppedArgs(string id, int mode)
        {
            ControlID = id;
            DraggingMode = mode;
        }
        /// <summary>
        /// Get the control id
        /// </summary>
        public string ControlID { get; private set; }
        /// <summary>
        /// Get the dragging mode
        /// </summary>
        public int DraggingMode { get; private set; }
    }
}
