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
using System.Drawing;
using System.ComponentModel;
namespace ManagedUI
{
    /// <summary>
    /// Command Menu Item
    /// </summary>
    public abstract class CMI : IMenuItemRepresentator
    {
        /// <summary>
        /// Command Menu Item
        /// </summary>
        public CMI() : base()
        {
        }
        /// <summary>
        /// The resource key for tooltip. 
        /// </summary>
        protected string kToolTip;
        /// <summary>
        /// The resource key for icon
        /// </summary>
        protected string kIcon;
        /// <summary>
        /// The icon to use when no resource used
        /// </summary>
        protected Image icon;

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
                    kIcon = inf.KIcon;
                }
                else if (attr.GetType() == typeof(MIRNoResourcePropertiesAttribute))
                {
                    useResource = false;
                    MIRNoResourcePropertiesAttribute inf = (MIRNoResourcePropertiesAttribute)attr;
                    kToolTip = inf.ToolTip;
                }
                if (attr.GetType() == typeof(CMIInfoAttribute))
                {
                    CMIInfoAttribute inf = (CMIInfoAttribute)attr;
                    CommandID = inf.CommandID;
                    Parameters = inf.Parameters;
                    UseParameters = inf.UseParameters;
                }
            }
        }
        /// <summary>
        /// This method is called after executing the command so that the cmi can handle the command responses (if any)
        /// </summary>
        /// <param name="responses">The responses of the command (if any)</param>
        public virtual void OnCommandResponse(object[] responses)
        {
        }
        /// <summary>
        /// Get the command id within the app.
        /// </summary>
        [Browsable(false)]
        public string CommandID { get; protected set; }
        /// <summary>
        /// The parameters to use when executing this command
        /// </summary>
        [Browsable(false)]
        public object[] Parameters { get; protected set; }
        /// <summary>
        /// Indicates if the parameters should be used when executing this command.
        /// </summary>
        [Browsable(false)]
        public bool UseParameters { get; protected set; }
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
        /// <summary>
        /// Get or set (set only when MIRNoResourceProperties attribute is used) the icon of this 
        /// item that will be displayed to the user.
        /// </summary>
        public virtual Image Icon
        {
            get
            {
                if (!useResource)
                    return icon;
                try
                {
                    object ic = resource.GetObject(kIcon);
                    if (ic is Image)
                        return (Image)ic;
                    else if (ic is Icon)
                        return ((Icon)ic).ToBitmap();
                }
                catch { }
                return null;
            }
            set
            {
                if (!useResource)
                    icon = value;
            }
        }
    }
}
