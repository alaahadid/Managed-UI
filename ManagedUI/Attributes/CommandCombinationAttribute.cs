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
    /// Descripes a command combination.
    /// </summary>
    [MetadataAttribute]
    public class CommandCombinationAttribute : Attribute
    {
        /// <summary>
        /// Descripes a command combination.
        /// </summary>
        /// <param name="name">The name of this command combination</param>
        /// <param name="id">The id of this command combination</param>
        /// <param name="commandID">The command id to execute</param>
        /// <param name="parentServiceID">The parent service id of this command combination. This id is very important, only command combinations 
        /// with discovered and initialized services can be used.</param>
        /// <param name="parameters">The parameters to use. Can be null and thus the command will be executed without parameters.</param>
        public CommandCombinationAttribute(string name, string id, string commandID, string parentServiceID, object[] parameters)
        {
            Name = name;
            ID = id;
            CommandID = commandID;
            ParentServiceID = parentServiceID;
            if (parameters == null)
            {
                Parameters = new object[0];
                UseParameters = false;
            }
            else
            {
                Parameters = parameters;
                UseParameters = Parameters.Length > 0;
            }
        }
        /// <summary>
        /// Get the name of this command combination.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Get the id of this command combination.
        /// </summary>
        public string ID { get; private set; }
        /// <summary>
        /// Get the parent service id of this command combination. This id is very important, only command combinations 
        /// with discovered and initialized services can be used.
        /// </summary>
        public string ParentServiceID { get; protected set; }
        /// <summary>
        /// Get the command id within the app.
        /// </summary>
        public string CommandID { get; private set; }
        /// <summary>
        /// The parameters to use when executing this command
        /// </summary>
        public object[] Parameters { get; private set; }
        /// <summary>
        /// Indicates if the parameters should be used when executing this command.
        /// </summary>
        public bool UseParameters { get; private set; }
    }
}
