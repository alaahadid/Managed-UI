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
    /// Args for index changing events of CBMI
    /// </summary>
    public class CBMIChangeIndexArgs : EventArgs
    {
        /// <summary>
        /// Args for index changing events of CBMI
        /// </summary>
        public CBMIChangeIndexArgs(string id, int index)
        {
            Index = index;
            CBMIID = id;
        }
        /// <summary>
        /// The new index
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// CBMI ID
        /// </summary>
        public string CBMIID { get; private set; }
    }
}
