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
using System.Windows.Forms;
using System.Drawing;

namespace ManagedUI
{
    class ThemeRenderer : ToolStripProfessionalRenderer
    {
        public ThemeRenderer(ProfessionalColorTable colors) : base(colors) { }
    }
    class MenuColors : ProfessionalColorTable
    {
        public MenuColors(Color menuItemSelected, Color menuItemSelectedGradientBegin, Color menuItemSelectedGradientEnd,
            Color menuItemPressedGradientBegin, Color menuItemPressedGradientEnd, Color menuItemPressedGradientMiddle,
            Color toolStripDropDownBackground, Color checkBackground)
        {
            this.menuItemSelected = menuItemSelected;
            this.menuItemSelectedGradientBegin = menuItemSelectedGradientBegin;
            this.menuItemSelectedGradientEnd = menuItemSelectedGradientEnd;
            this.menuItemPressedGradientBegin = menuItemPressedGradientBegin;
            this.menuItemPressedGradientEnd = menuItemPressedGradientEnd;
            this.menuItemPressedGradientMiddle = menuItemPressedGradientMiddle;
            this.toolStripDropDownBackground = toolStripDropDownBackground;
            this.checkBackground = checkBackground;
        }

        private Color menuItemSelected;
        private Color menuItemSelectedGradientBegin;
        private Color menuItemSelectedGradientEnd;
        private Color menuItemPressedGradientBegin;
        private Color menuItemPressedGradientEnd;
        private Color menuItemPressedGradientMiddle;
        private Color toolStripDropDownBackground;
        private Color checkBackground;

        public override Color MenuItemSelected
        {
            get { return menuItemSelected; }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return menuItemSelectedGradientBegin; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return menuItemSelectedGradientEnd; }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                return menuItemPressedGradientBegin;
            }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                return menuItemPressedGradientEnd;
            }
        }
        public override Color MenuItemPressedGradientMiddle
        {
            get
            {
                return menuItemPressedGradientMiddle;
            }
        }
        public override Color ToolStripDropDownBackground
        {
            get
            {
                return toolStripDropDownBackground;
            }
        }
        public override Color ImageMarginGradientBegin
        {
            get
            {
                return checkBackground;
            }
        }
        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return checkBackground;
            }
        }
        public override Color ImageMarginGradientEnd
        {
            get
            {
                return checkBackground;
            }
        }
    }
    class ToolbarColors : ProfessionalColorTable
    {
        public ToolbarColors(Color toolStripGradientBegin, Color toolStripGradientEnd, Color toolStripGradientMiddle)
        {
            this.toolStripGradientBegin = toolStripGradientBegin;
            this.toolStripGradientEnd = toolStripGradientEnd;
            this.toolStripGradientMiddle = toolStripGradientMiddle;
        }
        private Color toolStripGradientBegin;
        private Color toolStripGradientEnd;
        private Color toolStripGradientMiddle;
        public override Color ToolStripGradientBegin
        {
            get
            {
                return toolStripGradientBegin;
            }
        }
        public override Color ToolStripGradientEnd
        {
            get
            {
                return toolStripGradientEnd;
            }
        }
        public override Color ToolStripGradientMiddle
        {
            get
            {
                return toolStripGradientMiddle;
            }
        }
    }
}
