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
    /// Managed Message Box icon
    /// </summary>
    public enum ManagedMessageBoxIcon
    {
        /// <summary>
        /// Show no icon.
        /// </summary>
        None,
        /// <summary>
        /// Shows info icon
        /// </summary>
        Info,
        /// <summary>
        /// Shows a question mark icon
        /// </summary>
        Question,
        /// <summary>
        /// Shows a save icon (big old yellow floppy icon)
        /// </summary>
        Save,
        /// <summary>
        /// Shows an error icon
        /// </summary>
        Error,
        /// <summary>
        /// Shows warning icon
        /// </summary>
        Warning,
        /// <summary>
        /// Shows red stop icon
        /// </summary>
        Stop,
    }
}