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
    /// Attributes descripes a menu item representator.
    /// </summary>
    [MetadataAttribute]
    public class MIRInfoAttribute : Attribute
    {
        /// <summary>
        /// Attributes descripes a menu item representator.
        /// </summary>
        /// <param name="name">The name of this item. This will not be visible to user and will be used only in tha app</param>
        /// <param name="id">The id of this item, this will not be visible to user and will be used only in the app</param>
        public MIRInfoAttribute(string name, string id)
        {
            Name = name;
            ID = id;
            ActiveActiveStatus = false;
            ActiveCheckStatus = false;
        }
        /// <summary>
        /// Attributes descripes a menu item representator.
        /// </summary>
        /// <param name="name">The name of this item. This will not be visible to user and will be used only in tha app</param>
        /// <param name="id">The id of this item, this will not be visible to user and will be used only in the app</param>
        /// <param name="useActiveStatus">Indicate if this CMI should use the ative status indicator of the command. If set, the event will be invoked in the CMI.</param>
        /// <param name="useCheckStatus">Indicate if this CMI should use the check status indicator of the command. If set, the event will be invoked in the CMI.</param>
        public MIRInfoAttribute(string name, string id, bool useActiveStatus, bool useCheckStatus)
        {
            Name = name;
            ID = id;
            ActiveActiveStatus = useActiveStatus;
            ActiveCheckStatus = useCheckStatus;
        }
        /// <summary>
        /// Get the name of this item. This item will not be displayed to user, only for use in the app.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Get the id of this item. This will be used in the app only.
        /// </summary>
        public string ID { get; private set; }
        /// <summary>
        /// Get if this CMI should use the ative status indicator of the command. If set, the event will be invoked in the CMI.
        /// </summary>
        public bool ActiveActiveStatus { get; private set; }
        /// <summary>
        /// Get if this CMI should use the check status indicator of the command. If set, the event will be invoked in the CMI.
        /// </summary>
        public bool ActiveCheckStatus { get; private set; }
    }
}
