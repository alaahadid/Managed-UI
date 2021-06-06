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
    /// Represents the info of MIR
    /// </summary>
    public interface IMenuItemRepresentatorInfo
    {
        /// <summary>
        /// Get the name of this item. This item will not be displayed to user, only for use in the app.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Get the id of this item. This will be used in the app only.
        /// </summary>
        string ID { get; }
    }
}
