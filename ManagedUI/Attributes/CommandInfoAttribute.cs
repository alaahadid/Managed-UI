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
    /// Attribute descripes a command.
    /// </summary>
    [MetadataAttribute]
    public class CommandInfoAttribute : Attribute
    {
        /// <summary>
        /// Attribute descripes a command.
        /// </summary>
        /// <param name="name">The name of this command. This name will be used to identfy this command and to execute it within the app.</param>
        /// <param name="id"> The id of this command. This id is important and will be used to identfy this command and to execute
        /// this command within the app.</param>
        /// <param name="parentServiceID"> The parent service id of this command. This id is very important, only commands with discovered and 
        /// initialized services can be used.</param>
        public CommandInfoAttribute(string name, string id, string parentServiceID)
        {
            Name = name;
            ID = id;
            ParentServiceID = parentServiceID;
        }
        /// <summary>
        /// Get the name of this command. This name will be used to identfy this command and to execute it within the app.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Get the id of this command. This id is important and will be used to identfy this command and to execute
        /// this command within the app.
        /// </summary>
        public string ID { get; private set; }
        /// <summary>
        /// Get the parent service id of this command. This id is very important, only commands with discovered and 
        /// initialized services can be used.
        /// </summary>
        public string ParentServiceID { get; private set; }
    }
}
