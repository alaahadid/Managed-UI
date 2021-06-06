// ManagedUI.Enums (Managed User Interface)
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
    /// The trace status mode (use these key words in the trace line writing as a category in order to make the message visible to user in forms)
    /// </summary>
    public class StatusMode
    {
        /// <summary>
        /// Normal message. i.e. visible as black color to end-user (depends on the form/control how handle these messages).
        /// </summary>
        public const string Normal = "NORMAL";
        /// <summary>
        /// Error message. i.e. visible as red color to end-user (depends on the form/control how handle these messages).
        /// </summary>
        public const string Error = "ERROR";
        /// <summary>
        /// Warning message. i.e. visible as yellow color to end-user (depends on the form/control how handle these messages).
        /// </summary>
        public const string Warning = "WARNING";
        /// <summary>
        /// Information message. i.e. visible as green color to end user (depends on the form/control how handle these messages).
        /// </summary>
        public const string Information = "INFORMATION";
    }
}
