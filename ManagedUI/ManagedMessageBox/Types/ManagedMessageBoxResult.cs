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
    /// The Managed Message Box Result
    /// </summary>
    public struct ManagedMessageBoxResult
    {
        /// <summary>
        /// The Managed Message Box Result
        /// </summary>
        /// <param name="clickedButton">The clicked button</param>
        /// <param name="clickedButtonIndex">The clicked button index</param>
        /// <param name="isChecked">Indicate if the check box is checked.</param>
        public ManagedMessageBoxResult(string clickedButton, int clickedButtonIndex, bool isChecked)
        {
            this.clickedButton = clickedButton;
            this.isChecked = isChecked;
            this.clickedButtonIndex = clickedButtonIndex;
        }

        private bool isChecked;
        private string clickedButton;
        private int clickedButtonIndex;

        /// <summary>
        /// Get if the check box checked.
        /// </summary>
        public bool Checked
        { get { return isChecked; } }
        /// <summary>
        /// Get the clicked button text.
        /// </summary>
        public string ClickedButton
        { get { return clickedButton; } }
        /// <summary>
        /// Get the clicked button zero based index within given buttons array.
        /// </summary>
        public int ClickedButtonIndex
        { get { return clickedButtonIndex; } }
    }
}
