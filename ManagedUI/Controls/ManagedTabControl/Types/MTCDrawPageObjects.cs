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
using System.Drawing;

namespace ManagedUI
{
    /// <summary>
    /// Managed Tab Control draw objects
    /// </summary>
    public struct MTCDrawPageObjects
    {
        /// <summary>
        /// Managed Tab Control draw objects
        /// </summary>
        /// <param name="text">The text</param>
        /// <param name="image">The image</param>
        public MTCDrawPageObjects(string text, Image image)
        {
            this.text = text;
            this.image = image;
        }
        private string text;
        private Image image;
        /// <summary>
        /// Get or set the text
        /// </summary>
        public string Text
        { get { return text; } set { text = value; } }
        /// <summary>
        /// Get or set the image
        /// </summary>
        public Image Image
        { get { return image; } set { image = value; } }
    }
}
