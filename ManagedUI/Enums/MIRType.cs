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
namespace ManagedUI
{
    /// <summary>
    /// Menu Item Representator Type
    /// </summary>
    public enum MIRType
    {
        /// <summary>
        /// Root item "RMI"
        /// </summary>
        ROOT = 0x01,
        /// <summary>
        /// Command menu item "CMI"
        /// </summary>
        CMI = 0x02,
        /// <summary>
        /// Dynamic menu item "DMI"
        /// </summary>
        DMI = 0x04,
        /// <summary>
        /// Splitter menu item "SMI"
        /// </summary>
        SMI = 0x08,
        /// <summary>
        /// Parent menu item "PMI"
        /// </summary>
        PMI = 0x10,
        /// <summary>
        /// Textbox menu item "TMI"
        /// </summary>
        TMI = 0x20,
        /// <summary>
        /// Combo Box menu item "CBMI"
        /// </summary>
        CBMI = 0x40,
    }
}
