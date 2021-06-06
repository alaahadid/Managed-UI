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
    /// Atrribute for descriping Commandable Menu Item (CMI)
    /// </summary>
    public class CMIInfoAttribute : Attribute
    {
        /// <summary>
        /// Atrribute for descriping Commandable Menu Item (CMI)
        /// </summary>
        /// <param name="commandID">The command id.</param>
        public CMIInfoAttribute(string commandID)
        {
            CommandID = commandID;
            Parameters = new object[0];
            UseParameters = false;
        }
        /// <summary>
        /// Atrribute for descriping Commandable Menu Item (CMI)
        /// </summary>
        /// <param name="commandID">The command id.</param>
        /// <param name="parameters">The parameters to use when executing the command.</param>
        public CMIInfoAttribute(string commandID, object[] parameters)
        {
            CommandID = commandID;
            Parameters = parameters;
            if (parameters != null)
                if (parameters.Length > 0)
                    UseParameters = true;
        }
        /// <summary>
        /// Get the command id.
        /// </summary>
        public string CommandID { get; private set; }
        /// <summary>
        /// Get the parameters to use when executing this command.
        /// </summary>
        public object[] Parameters { get; private set; }
        /// <summary>
        /// Indicates if the command should use parameters. Set automatically when "Parameters" property is set.
        /// </summary>
        public bool UseParameters { get; private set; }

    }
}
