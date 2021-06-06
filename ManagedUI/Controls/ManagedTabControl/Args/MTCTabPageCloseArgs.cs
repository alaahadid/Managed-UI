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
    /// Args can be used in tab page close events
    /// </summary>
    public class MTCTabPageCloseArgs : EventArgs
    {
        /// <summary>
        /// Args can be used in tab page close events
        /// </summary>
        /// <param name="pageID">The tab page id</param>
        /// <param name="pageIndex">The tab page index</param>
        public MTCTabPageCloseArgs(string pageID, int pageIndex)
        {
            this.pageID = pageID;
            this.pageIndex = pageIndex;
        }
        private string pageID;
        private int pageIndex;
        private bool cancel = false;
        /// <summary>
        /// Get the target tab page id
        /// </summary>
        public string TabPageID
        { get { return pageID; } }
        /// <summary>
        /// Get the target tab page index within collection, this will be set to -1 if the page is already closed.
        /// </summary>
        public int TabPageIndex
        { get { return pageIndex; } }
        /// <summary>
        /// Get or set whether to cancel the operation
        /// </summary>
        public bool Cancel
        { get { return cancel; } set { cancel = value; } }
    }
}
