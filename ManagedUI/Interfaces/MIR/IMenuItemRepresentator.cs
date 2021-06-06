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
using System.Resources;
using System.Reflection;
using System.ComponentModel;

namespace ManagedUI
{
    /// <summary>
    /// Represents a menu item 
    /// </summary>
    public abstract class IMenuItemRepresentator
    {
        /// <summary>
        /// Represents a menu item.
        /// </summary>
        public IMenuItemRepresentator()
        {
            LoadAttributes();
        }

        /// <summary>
        /// The resource key for display name.
        /// </summary>
        protected string kDisplayName;
        /// <summary>
        /// The resources manager object to use for this item. Should be set automatically by the GUI service.
        /// </summary>
        protected ResourceManager resource;
        /// <summary>
        /// Indicates if the item should use resource for display name and others.
        /// </summary>
        protected bool useResource;
        /// <summary>
        /// Indicates if the resource manager is set or not.
        /// </summary>
        protected bool resourceSet;

        // Methods
        /// <summary>
        /// Load default attributes for this item. This called at item contrustor.
        /// </summary>
        protected virtual void LoadAttributes()
        {
            foreach (Attribute attr in Attribute.GetCustomAttributes(this.GetType()))
            {
                if (attr.GetType() == typeof(MIRInfoAttribute))
                {
                    MIRInfoAttribute inf = (MIRInfoAttribute)attr;
                    Name = inf.Name;
                    ID = inf.ID;
                    UseActiveStatus = inf.ActiveActiveStatus;
                    UseCheckStatus = inf.ActiveCheckStatus;
                }

                if (attr.GetType() == typeof(MIRResourcesInfoAttribute))
                {
                    useResource = true;
                    MIRResourcesInfoAttribute inf = (MIRResourcesInfoAttribute)attr;
                    kDisplayName = inf.KDisplayName;
                }
                else if (attr.GetType() == typeof(MIRNoResourcePropertiesAttribute))
                {
                    useResource = false;
                    MIRNoResourcePropertiesAttribute inf = (MIRNoResourcePropertiesAttribute)attr;
                    kDisplayName = inf.DisplayName;
                }
            }
        }
        /// <summary>
        /// This method is called by the gui core when initializing. Never change this unless you know what are you doing !!
        /// </summary>
        public virtual void FindResources()
        {
            if (!useResource)
                return;
            if (resourceSet)
                return;
            // First of all, we need to locate the resource class that comes in a .net project by default
            Type[] types = this.GetType().Assembly.GetTypes();
            foreach (Type tp in types)
            {
                if (tp.Name == "Resources")
                {
                    // this is it !!
                    resource = (ResourceManager)tp.GetProperty("ResourceManager", BindingFlags.Static | BindingFlags.NonPublic).GetValue(tp, null);
                    resourceSet = true;
                    break;
                }
            }
            // Now let's set this found resource to all items that already found and located in the 
            // same library !
            foreach (Lazy<IMenuItemRepresentator, IMenuItemRepresentatorInfo> it in GUIService.GUI.AvailableMenuItems)
            {
                if (!it.Value.resourceSet)
                {
                    // Let's see if this item in the same library
                    if (it.Value.GetType().Assembly.FullName == this.GetType().Assembly.FullName)
                    {
                        // This is it !!
                        it.Value.resource = resource;
                        it.Value.resourceSet = true;
                    }
                    else
                    {
                        // This item need to locate the resource for itself and for others in it's library
                        it.Value.FindResources();
                    }
                }
            }
        }
        /// <summary>
        /// Raises the event 'CheckStatusChanged'
        /// </summary>
        /// <param name="active">The active status</param>
        protected void OnCheckStatusChanged(bool active)
        {
            CheckStatusChanged?.Invoke(this, new StatusChangedArgs(active));
        }
        /// <summary>
        /// Raises the event 'ActiveStatusChanged'
        /// </summary>
        /// <param name="checkedStatus">The check status</param>
        protected void OnActiveStatusChanged(bool checkedStatus)
        {
            ActiveStatusChanged?.Invoke(this, new StatusChangedArgs(checkedStatus));
        }

        // Properties
        /// <summary>
        /// Get the name of this item. This item will not be displayed to user, only for use in the app.
        /// </summary>
        [Browsable(false)]
        public string Name
        { get; protected set; }
        /// <summary>
        /// Get the id of this item. This will be used in the app only.
        /// </summary>
        [Browsable(false)]
        public string ID
        { get; protected set; }
        /// <summary>
        /// Get the name that will be displayed to the user.
        /// </summary>
        public virtual string DisplayName
        {
            get
            {
                if (!useResource)
                    return kDisplayName;
                try
                {
                    return resource.GetString(kDisplayName);
                }
                catch { }
                return Properties.Resources.Status_MIEError4 + " '" + kDisplayName + "'";
            }
        }
        /// <summary>
        /// Get the check status of this item.
        /// </summary>
        [Browsable(false)]
        public virtual bool CheckStatus
        {
            get { return false; }
        }
        /// <summary>
        /// Get if this item is active or not.
        /// </summary>
        [Browsable(false)]
        public virtual bool ActiveStatus
        {
            get { return true; }
        }
        /// <summary>
        /// Indicates if this item should be affected by the active status of the command
        /// </summary>
        [Browsable(false)]
        public bool UseActiveStatus
        { get; protected set; }
        /// <summary>
        /// Indicates if this item should be affected by the check status of the command
        /// </summary>
        [Browsable(false)]
        public bool UseCheckStatus
        { get; protected set; }

        // Events
        /// <summary>
        /// Raised when the "CheckStatus" propery value is changed.
        /// </summary>
        public event EventHandler<StatusChangedArgs> CheckStatusChanged;
        /// <summary>
        /// Raised when the 'ActiveStatus' property value is changed.
        /// </summary>
        public event EventHandler<StatusChangedArgs> ActiveStatusChanged;
    }
}
