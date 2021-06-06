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
    /// Args for tab control closing events
    /// </summary>
    public class TabControlClosedArgs : EventArgs
    {
        /// <summary>
        /// Args for tab control closing events
        /// </summary>
        /// <param name="controlID">The control id that is about to close.</param>
        public TabControlClosedArgs(string controlID)
        {
            ControlID = controlID;
        }
        /// <summary>
        /// Get the control id that is about to close.
        /// </summary>
        public string ControlID { get; private set; }
        /// <summary>
        /// Get or set if the closing should be canceled.
        /// </summary>
        public bool Cancel { get; set; }
    }
}
