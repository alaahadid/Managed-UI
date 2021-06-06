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
    /// Represents a root menu item.
    /// </summary>
    public abstract class RMI : IMenuItemRepresentator
    {
        /// <summary>
        /// Represents a root menu item.
        /// </summary>
        public RMI() : base()
        {
        }

        /// <summary>
        /// The resource key for tooltip. 
        /// </summary>
        protected string kToolTip;

        /// <summary>
        /// Load default attributes for this item. This called at item contrustor.
        /// </summary>
        protected override void LoadAttributes()
        {
            base.LoadAttributes();
            foreach (Attribute attr in Attribute.GetCustomAttributes(this.GetType()))
            {
                if (attr.GetType() == typeof(MIRResourcesInfoAttribute))
                {
                    useResource = true;
                    MIRResourcesInfoAttribute inf = (MIRResourcesInfoAttribute)attr;
                    kToolTip = inf.KToolTip;
                }
                else if (attr.GetType() == typeof(MIRNoResourcePropertiesAttribute))
                {
                    useResource = false;
                    MIRNoResourcePropertiesAttribute inf = (MIRNoResourcePropertiesAttribute)attr;
                    kToolTip = inf.ToolTip;
                }
            }
        }

        /// <summary>
        /// Get the tooltip of this item that will be displayed to the user.
        /// </summary>
        public virtual string ToolTip
        {
            get
            {
                if (!useResource)
                    return kToolTip;
                try
                {
                    return resource.GetString(kToolTip);
                }
                catch { }
                return Properties.Resources.Status_MIEError4 + " '" + kToolTip + "'";
            }
        }
    }
}
