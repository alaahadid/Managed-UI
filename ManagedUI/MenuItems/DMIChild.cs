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
    /// Represent a dynamic menu item child.
    /// </summary>
    public class DMIChild
    {
        /// <summary>
        /// Represent a dynamic menu item child.
        /// </summary>
        /// <param name="parentID">The parent DMI id</param>
        public DMIChild(string parentID)
        {
            ParentID = parentID;
        }
        /// <summary>
        /// Get or set the display name
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Get or set the icon
        /// </summary>
        public System.Drawing.Image Icon { get; set; }
        /// <summary>
        /// Get or set the tooltip
        /// </summary>
        public string Tooltip { get; set; }
        /// <summary>
        /// Get or set the parent dmi id for this child.
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// Get or set if this item is active (displayed checked to the user.)
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Get the command id within the app. (if any)
        /// </summary>
        public string CommandID { get; set; }
        /// <summary>
        /// The parameters to use when executing this command (if any)
        /// </summary>
        public object[] Parameters { get; set; }
        /// <summary>
        /// Indicates if the parameters should be used when executing this command. (if any)
        /// </summary>
        public bool UseParameters { get; set; }
        /// <summary>
        /// Execute the command along with all parameters !
        /// </summary>
        /// <param name="repsonses">Command responses (if any)</param>
        public void Execute(out object[] repsonses)
        {
            repsonses = new object[0];
            // Extract the command !
            Lazy<ICommand, ICommandInfo> theCommand = CommandsManager.CMD.GetCommand(CommandID);
            if (theCommand != null)
            {
                if (UseParameters)
                {
                    theCommand.Value.Execute(Parameters, out repsonses);
                }
                else
                {
                    theCommand.Value.Execute(out repsonses);
                }
            }
        }
    }
}
