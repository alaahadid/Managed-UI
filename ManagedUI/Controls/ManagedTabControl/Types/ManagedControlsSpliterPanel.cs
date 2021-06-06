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
using System.ComponentModel;
using System.Windows.Forms;

namespace ManagedUI
{
    /// <summary>
    /// Splitter panel
    /// </summary>
    [Serializable()]
    public class ManagedControlsSpliterPanel : IDisposable
    {
        private string name = "";
        private int thickness = 0;
        [NonSerialized]
        private Panel panel = new Panel();

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        { get { return name; } set { name = value; } }
        /// <summary>
        /// Thickness
        /// </summary>
        public int Thickness
        { get { return thickness; } set { thickness = value; } }
        /// <summary>
        /// The panel
        /// </summary>
        [Browsable(false)]
        public Panel Panel
        { get { return panel; } set { panel = value; } }
        /// <summary>
        /// Dispose this panel
        /// </summary>
        public void Dispose()
        {

        }
    }
}
