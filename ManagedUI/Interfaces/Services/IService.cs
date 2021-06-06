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
using System.Diagnostics;

namespace ManagedUI
{
    /// <summary>
    /// Representing a service.
    /// </summary>
    public abstract class IService
    {
        /// <summary>
        /// Representing a service.
        /// </summary>
        public IService()
        {
            LoadAttributes();
        }
        /// <summary>
        /// Initialize this service. This method should be called when this service is first detected.
        /// </summary>
        public virtual void Initialize()
        {
        }
        /// <summary>
        /// Close this service. This method should be called when the application is closed.
        /// </summary>
        public virtual void Close()
        {
        }
        /// <summary>
        /// Load all attributes related to this format.
        /// </summary>
        protected virtual void LoadAttributes()
        {
            foreach (Attribute attr in Attribute.GetCustomAttributes(this.GetType()))
            {
                if (attr.GetType() == typeof(ServiceInfoAttribute))
                {
                    ServiceInfoAttribute inf = (ServiceInfoAttribute)attr;
                    this.Name = inf.Name;
                    this.ID = inf.ID;
                    this.IsDefault = inf.IsDefault;
                    this.Description = inf.Description;
                }
            }
        }

        /// <summary>
        /// Get the name of this service.
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Get the id of this service.
        /// </summary>
        public string ID { get; protected set; }
        /// <summary>
        /// Get a short description about this service.
        /// </summary>
        public string Description { get; protected set; }
        /// <summary>
        /// Get if this service is default. Default services cannot be disabled. (NOT IMPLEMENTED, reserved for future use)
        /// </summary>
        public bool IsDefault { get; protected set; }

        /// <summary>
        /// Represent this service as string.
        /// </summary>
        /// <returns>String of this service is format "Name [ID]"</returns>
        public override string ToString()
        {
            return string.Format("{0} [{1}]", Name, ID);
        }
        /// <summary>
        /// Write a status line with service name
        /// </summary>
        /// <param name="message">The message</param>
        protected void WriteLine(string message)
        {
            Trace.WriteLine(Name + ": " + message);
        }
        /// <summary>
        /// Write information trace
        /// </summary>
        /// <param name="message"></param>
        protected void WriteInformation(string message)
        {
            Trace.TraceInformation(Name + ": " + message);
        }
        /// <summary>
        /// Write trace error
        /// </summary>
        /// <param name="message"></param>
        protected void WriteError(string message)
        {
            Trace.TraceError(Name + ": " + message);
        }
        /// <summary>
        /// Write warning trace
        /// </summary>
        /// <param name="message"></param>
        protected void WriteWarning(string message)
        {
            Trace.TraceWarning(Name + ": " + message);
        }
        /// <summary>
        /// Write trace message and make it visible to user through gui
        /// </summary>
        /// <param name="message"></param>
        protected void WriteStatus(string message)
        {
            Trace.WriteLine(Name + ": " + message, StatusMode.Normal);
        }
        /// <summary>
        /// Write error trace message and make it visible to user through gui
        /// </summary>
        /// <param name="message"></param>
        protected void WriteStatusError(string message)
        {
            Trace.WriteLine(Name + ": " + message, StatusMode.Error);
        }
        /// <summary>
        /// Write trace info message and make it visible to user through gui
        /// </summary>
        /// <param name="message"></param>
        protected void WriteStatusInfo(string message)
        {
            Trace.WriteLine(Name + ": " + message, StatusMode.Information);
        }
        /// <summary>
        /// Write trace warning message and make it visible to user through gui
        /// </summary>
        /// <param name="message"></param>
        protected void WriteStatusWarning(string message)
        {
            Trace.WriteLine(Name + ": " + message, StatusMode.Warning);
        }
    }
}
