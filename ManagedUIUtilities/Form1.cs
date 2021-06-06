// ManagedUI (Managed User Interface)
// A managed user interface framework for .net desktop applications.
// 
// Copyright © Alaa Ibrahim Hadid 2021
//
// This library is free software; you can redistribute it and/or modify 
// it under the terms of the GNU Lesser General Public License as published 
// by the Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful, but 
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
using System.Windows.Forms;
using ManagedUI;

namespace ManagedUIUtilities
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Edit mir map
        private void button1_Click(object sender, EventArgs e)
        {
            FormEditMenuItemsMap frm = new FormEditMenuItemsMap(false);
            frm.ShowDialog(this);
        }
        // Edit tbr map
        private void button2_Click(object sender, EventArgs e)
        {
            FormEditToolBars frm = new FormEditToolBars(false);
            frm.ShowDialog();
        }
        // Close
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Edit shortcuts
        private void button4_Click(object sender, EventArgs e)
        {
            FormEditShortcuts frm = new FormEditShortcuts(false);
            frm.ShowDialog(this);
        }
        // Edit theme
        private void button5_Click(object sender, EventArgs e)
        {
            FormThemeEditor frm = new FormThemeEditor(false);
            frm.ShowDialog(this);
        }
    }
}
