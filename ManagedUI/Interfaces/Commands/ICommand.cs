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
    /// Represents a command.
    /// </summary>
    public abstract class ICommand
    {
        /// <summary>
        /// Represents a command.
        /// </summary>
        public ICommand()
        {
            LoadAttributes();
        }

        /// <summary>
        /// Get the name of this command. This name is not displayed to user, used to identify and execute this command 
        /// within the app.
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Get the id of this command. This id is very important to execute this command and to identify this command
        ///  in the app.
        /// </summary>
        public string ID { get; protected set; }
        /// <summary>
        /// Get the parent service id of this command. This id is very important, only commands with discovered and 
        /// initialized services can be used.
        /// </summary>
        public string ParentServiceID { get; protected set; }

        /// <summary>
        /// Load all attributes related to this format.
        /// </summary>
        protected virtual void LoadAttributes()
        {
            foreach (Attribute attr in Attribute.GetCustomAttributes(this.GetType()))
            {
                if (attr.GetType() == typeof(CommandInfoAttribute))
                {
                    CommandInfoAttribute inf = (CommandInfoAttribute)attr;
                    this.Name = inf.Name;
                    this.ID = inf.ID;
                    this.ParentServiceID = inf.ParentServiceID;
                }
            }
        }

        /// <summary>
        /// Execute this command with no parameters. Responses (if any) will be thrown away.
        /// </summary>
        public virtual void Execute()
        {
            object[] responses = new object[0];
            Execute(new object[0], out responses);
        }
        /// <summary>
        /// Execute this command with no parameters. Responses will be set in 'responses'
        /// </summary>
        /// <param name="responses">The expected responses</param>
        public virtual void Execute(out object[] responses)
        {
            Execute(new object[0], out responses);
        }
        /// <summary>
        /// Execute this command. Responses (if any) will be thrown away.
        /// </summary>
        /// <param name="parameters">The parameters to use.</param>
        public virtual void Execute(object[] parameters)
        {
            object[] responses = new object[0];
            Execute(parameters, out responses);
        }
        /// <summary>
        /// Execute this command.
        /// </summary>
        /// <param name="parameters">The parameters to use.</param>
        /// <param name="responses">The expected responses.</param>
        public abstract void Execute(object[] parameters, out object[] responses);
    }
}
