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
namespace ManagedUI
{
    /// <summary>
    /// Represent info about a command combination 
    /// </summary>
    public interface ICommandCombinationInfo
    {
        /// <summary>
        /// Get the name of this command combination.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Get the id of this command combination.
        /// </summary>
        string ID { get; }
        /// <summary>
        /// Get the command id within the app.
        /// </summary>
        string CommandID { get; }
        /// <summary>
        /// Get the parent service id of this command combination. This id is very important, only command combinations 
        /// with discovered and initialized services can be used.
        /// </summary>
        string ParentServiceID { get; }
        /// <summary>
        /// The parameters to use when executing this command
        /// </summary>
        object[] Parameters { get; }
        /// <summary>
        /// Indicates if the parameters should be used when executing this command.
        /// </summary>
        bool UseParameters { get; }
    }
}
