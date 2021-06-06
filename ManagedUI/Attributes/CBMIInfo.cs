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
    /// Atrribute for descriping Combo Box Menu Item (CBMI)
    /// </summary>
    public class CBMIInfoAttribute : Attribute
    {
        /// <summary>
        /// Atrribute for descriping Combo Box Menu Item (CBMI)
        /// </summary>
        /// <param name="openTextMode">Indicates if this combobox menu item is an open text box or not. Default is false.</param>
        /// <param name="default_size">The default size of this combobox.</param>
        /// <param name="size_changes_automatically">Indicates if this combobox size should be set automatically by the main window.</param>
        public CBMIInfoAttribute(bool openTextMode, int default_size, bool size_changes_automatically)
        {
            OpenTextMode = openTextMode;
            DefaultSize = default_size;
            SizeChangesAutomatically = size_changes_automatically;
        }
        /// <summary>
        /// Indicates if this combobox menu item is an open text box or not. Default is false.
        /// </summary>
        public bool OpenTextMode { get; private set; }
        /// <summary>
        /// Get the default size of this combobox.
        /// </summary>
        public int DefaultSize { get; private set; }
        /// <summary>
        /// Indicates if this combobox size should be set automatically by the main window.
        /// </summary>
        public bool SizeChangesAutomatically { get; private set; }
    }
}
