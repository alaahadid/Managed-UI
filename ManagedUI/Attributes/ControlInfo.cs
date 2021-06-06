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
using System.ComponentModel.Composition;

namespace ManagedUI
{
    /// <summary>
    /// Attribute to describe a tab/settings control
    /// </summary>
    [MetadataAttribute]
    public class ControlInfoAttribute : Attribute
    {
        /// <summary>
        /// Attribute to describe a tab/settings control
        /// </summary>
        /// <param name="controlName">The control name</param>
        /// <param name="controlID">The control id</param>
        public ControlInfoAttribute(string controlName, string controlID)
        {
            Name = controlName;
            ID = controlID;
        }
        /// <summary>
        /// Get the control name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Get the control id
        /// </summary>
        public string ID { get; private set; }
    }
}
