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
using System.Collections.Generic;
using System.ComponentModel;
namespace ManagedUI
{
    /// <summary>
    /// Dynamic menu item
    /// </summary>
    public abstract class DMI : IMenuItemRepresentator
    {
        /// <summary>
        /// Dynamic menu item
        /// </summary>
        public DMI() : base()
        {
            ChildItems = new List<DMIChild>();
        }
        /// <summary>
        /// This called by the invoker when this item is first visible to the user, this method should update the ChildItems list.
        /// </summary>
        public abstract void OnView();
        /// <summary>
        /// This method is called after executing a child's command so that the dmi can handle the command responses (if any)
        /// </summary>
        /// <param name="responses">The responses of the command (if any)</param>
        public virtual void OnCommandResponse(object[] responses)
        {
        }
        /// <summary>
        /// Get the items that should be loaded as children after viewing this item
        /// </summary>
        [Browsable(false)]
        public List<DMIChild> ChildItems { get; set; }
    }
}
