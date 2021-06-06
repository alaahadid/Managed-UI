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
using System.Windows.Forms;

namespace ManagedUI
{
    /// <summary>
    /// Event for key pressed events
    /// </summary>
    public class KeyPressedArgs : EventArgs
    {
        /// <summary>
        /// Event for key pressed events
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="keyString">The string of the key</param>
        public KeyPressedArgs(Keys key, string keyString)
        {
            this.Key = key;
            KeyText = keyString;
        }
        /// <summary>
        /// Get the key data
        /// </summary>
        public Keys Key { get; private set; }
        /// <summary>
        /// Get the key string
        /// </summary>
        public string KeyText { get; private set; }
        /// <summary>
        /// Get or set if the operation should be canceled or not
        /// </summary>
        public bool Cancel { get; set; }
    }
}
