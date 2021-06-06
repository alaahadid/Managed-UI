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
    /// Events for dropping MTC request events
    /// </summary>
    public class MTCDropRequestEventArgs : EventArgs
    {
        /// <summary>
        /// Events for dropping MTC request events
        /// </summary>
        /// <param name="location">The location</param>
        public MTCDropRequestEventArgs(int location)
        {
            DropLocation = location;
        }
        /// <summary>
        /// The drop location 
        /// </summary>
        public int DropLocation { get; private set; }
    }
}
