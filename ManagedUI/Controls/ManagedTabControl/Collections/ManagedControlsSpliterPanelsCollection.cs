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
using System.Collections.Generic;

namespace ManagedUI
{
    /// <summary>
    /// Collection of managed controls splitter panels
    /// </summary>
    public class ManagedControlsSpliterPanelsCollection : IList<ManagedControlsSpliterPanel>
    {
        /// <summary>
        /// Collection of managed controls splitter panels 
        /// </summary>
        /// <param name="owner">The owner control</param>
        public ManagedControlsSpliterPanelsCollection(ManagedControlsSpliter owner)
        {
            this.owner = owner;
        }

        private ManagedControlsSpliter owner;
        private List<ManagedControlsSpliterPanel> items = new List<ManagedControlsSpliterPanel>();
        /// <summary>
        /// Index of item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(ManagedControlsSpliterPanel item)
        {
            return items.IndexOf(item);
        }
        /// <summary>
        /// Insert item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, ManagedControlsSpliterPanel item)
        {
            items.Insert(index, item);
            owner.OnPanelAdd(item);
        }
        /// <summary>
        /// Remove item at 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
            owner.OnPanelRemove();
        }
        /// <summary>
        /// Get a ManagedControlsSpliterPanel
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ManagedControlsSpliterPanel this[int index]
        {
            get
            {
                if (index < items.Count)
                    return items[index];
                return null;
            }
            set
            {
                if (index < items.Count)
                    items[index] = value;
            }
        }
        /// <summary>
        /// Add item
        /// </summary>
        /// <param name="item"></param>
        public void Add(ManagedControlsSpliterPanel item)
        {
            items.Add(item);
            owner.OnPanelAdd(item);
        }
        /// <summary>
        /// Clear all items
        /// </summary>
        public void Clear()
        {
            items.Clear();
            owner.OnPanelsClear();
        }
        /// <summary>
        /// Indicates if an item is contained
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(ManagedControlsSpliterPanel item)
        {
            return items.Contains(item);
        }
        /// <summary>
        /// Copy to
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(ManagedControlsSpliterPanel[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// Get the items count
        /// </summary>
        public int Count
        {
            get { return items.Count; }
        }
        /// <summary>
        /// Get if it is read only
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        /// <summary>
        /// Remove an item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(ManagedControlsSpliterPanel item)
        {
            bool val = items.Remove(item);
            owner.OnPanelRemove();
            return val;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ManagedControlsSpliterPanel> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
