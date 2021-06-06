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
using System.Collections.Generic;

namespace ManagedUI
{
    /// <summary>
    /// Combo Box Menu Item
    /// </summary>
    public abstract class CBMI : IMenuItemRepresentator
    {
        /// <summary>
        /// Combo Box Menu Item
        /// </summary>
        public CBMI() : base()
        {
        }
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
                if (attr.GetType() == typeof(CBMIInfoAttribute))
                {
                    CBMIInfoAttribute inf = (CBMIInfoAttribute)attr;
                    OpenTextMode = inf.OpenTextMode;
                    DefaultSize = inf.DefaultSize;
                    SizeChangesAutomatically = inf.SizeChangesAutomatically;
                }
            }
        }
        private int selectedIndex;
        /// <summary>
        /// The resource key for tooltip. 
        /// </summary>
        protected string kToolTip;
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
        /// Indicates if this combobox menu item is an open text box or not. 
        /// </summary>
        public bool OpenTextMode
        { get; protected set; }
        /// <summary>
        /// Get or set the items of this combobox.
        /// </summary>
        public List<string> Items
        { get; protected set; }
        /// <summary>
        /// Get or set the selected item index
        /// </summary>
        public virtual int SelectedItemIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnChangeIndex(selectedIndex);
            }
        }
        /// <summary>
        /// Get the default size of this combobox.
        /// </summary>
        public int DefaultSize { get; private set; }
        /// <summary>
        /// Indicates if this combobox size should be set automatically by the main window.
        /// </summary>
        public bool SizeChangesAutomatically { get; private set; }
        /// <summary>
        /// Get or set tag, an object that can be useful.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Called when the combobox is about to be viewed to user and the items must be loaded.
        /// </summary>
        public abstract void OnView();
        /// <summary>
        /// Called when the text of the box get changed
        /// </summary>
        /// <param name="newText">The new text of the textbox</param>
        public abstract void OnTextChanged(string newText);
        /// <summary>
        /// Called when the Return button get pressed on the textbox
        /// </summary>
        public abstract void OnEnterPressed();
        /// <summary>
        /// Called when the combobox index is changed
        /// </summary>
        /// <param name="newIndex">The new index of the combobox</param>
        public abstract void OnIndexChanged(int newIndex);
        /// <summary>
        /// Raised when the drop down is opening
        /// </summary>
        public abstract void OnDropDownOpening();
        /// <summary>
        /// Raised when the drop down is closed.
        /// </summary>
        public abstract void OnDropDownClosed();

        /// <summary>
        /// Raises the ChangeIndexRequest event.
        /// </summary>
        /// <param name="index">The index to set in the combobox</param>
        protected virtual void OnChangeIndex(int index)
        {
            ChangeIndexRequest?.Invoke(this, new CBMIChangeIndexArgs(ID, index));
        }
        /// <summary>
        /// Raises the ItemsReloadRequest event.
        /// </summary>
        protected virtual void OnItemsReloadRequest()
        {
            ItemsReloadRequest?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Raised when this item requires the GUI to change the index of the combobox.
        /// </summary>
        public event EventHandler<CBMIChangeIndexArgs> ChangeIndexRequest;
        /// <summary>
        /// Raised when this combobox is requesting an update for the items
        /// </summary>
        public event EventHandler ItemsReloadRequest;
    }
}
