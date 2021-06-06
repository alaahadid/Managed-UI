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
    /// Represent a command to execute along with the parameters (if any)
    /// </summary>
    public abstract class ICommandCombination
    {
        /// <summary>
        /// Represent a command to execute along with the parameters (if any) 
        /// </summary>
        public ICommandCombination()
        {
            LoadAttributes();
        }
        /// <summary>
        /// Load all attributes related to this format.
        /// </summary>
        protected virtual void LoadAttributes()
        {
            foreach (Attribute attr in Attribute.GetCustomAttributes(this.GetType()))
            {
                if (attr.GetType() == typeof(CommandCombinationAttribute))
                {
                    CommandCombinationAttribute inf = (CommandCombinationAttribute)attr;
                    this.Name = inf.Name;
                    this.ID = inf.ID;
                    this.CommandID = inf.CommandID;
                    this.Parameters = inf.Parameters;
                    this.UseParameters = inf.UseParameters;
                }
            }
        }
        /// <summary>
        /// This method is called after executing the command so that the cc can handle the command responses (if any)
        /// </summary>
        /// <param name="responses">The responses of the command (if any)</param>
        public virtual void OnCommandResponse(object[] responses)
        {
        }
        /// <summary>
        /// Get the name of this command combination.
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Get the id of this command combination.
        /// </summary>
        public string ID { get; protected set; }
        /// <summary>
        /// Get the parent service id of this command combination. This id is very important, only command combinations 
        /// with discovered and initialized services can be used.
        /// </summary>
        public string ParentServiceID { get; protected set; }
        /// <summary>
        /// Get the command id within the app.
        /// </summary>
        public string CommandID { get; protected set; }
        /// <summary>
        /// The parameters to use when executing this command
        /// </summary>
        public object[] Parameters { get; protected set; }
        /// <summary>
        /// Indicates if the parameters should be used when executing this command.
        /// </summary>
        public bool UseParameters { get; protected set; }
    }
}
