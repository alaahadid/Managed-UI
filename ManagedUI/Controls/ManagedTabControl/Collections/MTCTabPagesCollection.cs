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
using System.Collections.Generic;

namespace ManagedUI
{
    /// <summary>
    /// The Managed Tab Control Tab Pages Collection. Should be used only with Managed Tab Control controls.
    /// </summary>
    [Serializable]
    public class MTCTabPagesCollection : List<MTCTabPage>
    {
        /// <summary>
        /// The Managed Tab Control Tab Pages Collection. Should be used only with Managed Tab Control controls.
        /// </summary>
        public MTCTabPagesCollection()
        {

        }
        /// <summary>
        /// The Managed Tab Control Tab Pages Collection. Should be used only with Managed Tab Control controls.
        /// </summary>
        /// <param name="owner">The <see cref="ManagedTabControlPanel"/>, the parent of this collection.</param>
        public MTCTabPagesCollection(ManagedTabControlPanel owner)
        {
            this.owner = owner;
        }
        [NonSerialized]
        private ManagedTabControlPanel owner;
        /// <summary>
        /// Insert an item to the collection
        /// </summary>
        /// <param name="index">The index where to insert</param>
        /// <param name="item">The item to insert</param>
        public new void Insert(int index, MTCTabPage item)
        {
            base.Insert(index, item);
            owner.OnTabPagesItemAdded();
        }
        /// <summary>
        /// Remove item at index
        /// </summary>
        /// <param name="index">The index to remove at</param>
        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            owner.OnTabPagesItemRemoved();
            if (base.Count == 0)
                owner.OnTabPagesCollectionClear();
        }
        /// <summary>
        /// Add item to the collection
        /// </summary>
        /// <param name="item">The item to add</param>
        public new void Add(MTCTabPage item)
        {
            base.Add(item);
            owner.OnTabPagesItemAdded();
        }
        /// <summary>
        /// Clear the collection
        /// </summary>
        public new void Clear()
        {
            base.Clear();
            owner.OnTabPagesCollectionClear();
        }
        /// <summary>
        /// Remove item from the collection
        /// </summary>
        /// <param name="item">The item to remove</param>
        public new void Remove(MTCTabPage item)
        {
            base.Remove(item);
            owner.OnTabPagesItemRemoved();
            if (base.Count == 0)
                owner.OnTabPagesCollectionClear();
        }
        /// <summary>
        /// Get or set the owner control
        /// </summary>
        public ManagedTabControlPanel OWNER
        { get { return owner; } set { owner = value; } }
    }
}
