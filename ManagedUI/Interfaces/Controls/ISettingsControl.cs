// ManagedUI.Interfaces.Settings (Managed User Interface)
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
using System.IO;
using System.Resources;
using System.Reflection;
using System.Drawing;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace ManagedUI
{
    /// <summary>
    /// Represents the base class for a settings control
    /// </summary>
    public class ISettingsControl : UserControl
    {
        /// <summary>
        /// Represents the base class for a settings control
        /// </summary>
        public ISettingsControl()
               : base()
        {
            LoadAttributes();
            // We need to call InitializeComponent method if found
            MethodInfo inf = this.GetType().GetMethod("InitializeComponent",
                BindingFlags.NonPublic | BindingFlags.Instance);
            if (inf != null)
                inf.Invoke(this, null);
        }

        /// <summary>
        /// The resource key for display name.
        /// </summary>
        protected string kDisplayName;
        /// <summary>
        /// The resource key for category.
        /// </summary>
        protected string kCategory;
        /// <summary>
        /// The resource key for describtion.
        /// </summary>
        protected string kDescribtion;
        /// <summary>
        /// The resource key for icon
        /// </summary>
        protected string kIcon;
        /// <summary>
        /// The icon to use when no resource used
        /// </summary>
        protected Image icon;
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

        /// <summary>
        /// Load attributes and apply them for this control.
        /// </summary>
        protected virtual void LoadAttributes()
        {
            foreach (Attribute attr in Attribute.GetCustomAttributes(this.GetType()))
            {
                if (attr.GetType() == typeof(ControlInfoAttribute))
                {
                    ControlInfoAttribute inf = (ControlInfoAttribute)attr;
                    Name = inf.Name;
                    ID = inf.ID;
                }

                if (attr.GetType() == typeof(SettingsControlResourceInfo))
                {
                    useResource = true;
                    SettingsControlResourceInfo inf = (SettingsControlResourceInfo)attr;
                    kDisplayName = inf.KDisplayName;
                    kDescribtion = inf.KDescribtion;
                    kCategory = inf.KCategory;
                    kIcon = inf.KIcon;
                }
                else if (attr.GetType() == typeof(SettingsControlNoResourceInfo))
                {
                    useResource = false;
                    SettingsControlNoResourceInfo inf = (SettingsControlNoResourceInfo)attr;
                    kDisplayName = inf.DisplayName;
                    kDescribtion = inf.Describtion;
                    kCategory = inf.Category;
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
            foreach (Lazy<ISettingsControl, IControlInfo> it in GUIService.GUI.AvailableSettingControls)
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
        /// This method should be called when the control is first loaded and detected.
        /// </summary>
        public virtual void Initialize()
        {
        }
        /// <summary>
        /// This method should be called when this control become visible to user.
        /// </summary>
        public virtual void OnDisplay()
        {
        }
        /// <summary>
        /// This method should be called when this control is hiden from user.
        /// </summary>
        public virtual void OnHide()
        {
        }
        /// <summary>
        /// Called to load the settings on this control
        /// </summary>
        public virtual void LoadSettings()
        { }
        /// <summary>
        /// Called to save the settings on this control
        /// </summary>
        public virtual void SaveSettings()
        { }
        /// <summary>
        /// Called to reset the settings on this control to defaults
        /// </summary>
        public virtual void DefaultSettings()
        { }
        /// <summary>
        /// Export the settings to a stream
        /// </summary>
        /// <param name="stream">The stream to use for writing setting values</param>
        public virtual void ExportSettings(ref BinaryWriter stream)
        {
        }
        /// <summary>
        /// Import the settings from a stream
        /// </summary>
        /// <param name="stream">The stream to use for reading setting values</param>
        public virtual void ImportSettings(ref BinaryReader stream)
        {
        }
        /// <summary>
        /// Write an ASCII string into a stream
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="stream">The stream to use for writing</param>
        protected void WriteStringASCII(string text, BinaryWriter stream)
        {
            stream.Write(ASCIIEncoding.ASCII.GetByteCount(text));
            stream.Write(ASCIIEncoding.ASCII.GetBytes(text));
        }
        /// <summary>
        /// Read an ASCII string from stream
        /// </summary>
        /// <param name="stream">The stream to use for reading</param>
        /// <returns>The string value as read from the stream</returns>
        protected string ReadStringASCII(BinaryReader stream)
        {
            int count = stream.ReadInt32();
            byte[] stringBytes = new byte[count];
            stream.Read(stringBytes, 0, count);
            return ASCIIEncoding.ASCII.GetString(stringBytes);
        }
        /// <summary>
        /// Write an UTF8 string into a stream
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="stream">The stream to use for writing</param>
        protected void WriteStringUTF8(string text, BinaryWriter stream)
        {
            stream.Write(UTF8Encoding.ASCII.GetByteCount(text));
            stream.Write(UTF8Encoding.ASCII.GetBytes(text));
        }
        /// <summary>
        /// Read an UTF8 string from stream
        /// </summary>
        /// <param name="stream">The stream to use for reading</param>
        /// <returns>The string value as read from the stream</returns>
        protected string ReadStringUTF8(BinaryReader stream)
        {
            int count = stream.ReadInt32();
            byte[] stringBytes = new byte[count];
            stream.Read(stringBytes, 0, count);
            return UTF8Encoding.ASCII.GetString(stringBytes);
        }
        /// <summary>
        /// TabControl.ToString(): show the display name of the control.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DisplayName;
        }

        // Properties
        /// <summary>
        /// Get the control id. This id is important so that this control can be recognized in the GUI core. 
        /// Make sure this id is not duplicated or equal other.
        /// </summary>
        [Browsable(false)]
        public string ID
        { get; protected set; }
        /// <summary>
        /// Get the display name of this control, the name that will be displayed to user.
        /// </summary>
        public string DisplayName
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
                return Properties.Resources.Status_TCError1 + " '" + kDisplayName + "'";
            }
        }
        /// <summary>
        /// Get the category of this control in the settings list.
        /// </summary>
        public string Category
        {
            get
            {
                if (!useResource)
                    return kCategory;
                try
                {
                    return resource.GetString(kCategory);
                }
                catch { }
                return Properties.Resources.Status_TCError1 + " '" + kCategory + "'";
            }
        }
        /// <summary>
        /// Get the description of this control.
        /// </summary>
        public string Description
        {
            get
            {
                if (!useResource)
                    return kDescribtion;
                try
                {
                    return resource.GetString(kDescribtion);
                }
                catch { }
                return Properties.Resources.Status_TCError1 + " '" + kDescribtion + "'";
            }
        }
        /// <summary>
        /// Get or set (set only when MIRNoResourceProperties attribute is used) the icon of this 
        /// control that will be displayed to the user.
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
        /// <summary>
        /// Get if this control can save settings.
        /// </summary>
        public virtual bool CanSave
        {
            get { return true; }
        }
    }
}
