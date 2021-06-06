// ManagedUI.EventArgs (Managed User Interface)
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
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace ManagedUI.Commands
{
    [Export(typeof(ICommand))]
    [CommandInfo("Save current layout into a file.", "tabcontrol.save.layout", "gui")]
    class SaveLayout : ICommand
    {
        public override void Execute(object[] parameters, out object[] responses)
        {
            List<object> ress = new List<object>();

            SaveFileDialog sav = new SaveFileDialog();
            sav.Title = Properties.Resources.Title_SaveTabsControlsLayout;
            sav.Filter = Properties.Resources.FilterName_ControlsLayout + " (*.tcm)|*.tcm;*.TCM;";
            sav.FileName = System.IO.Path.GetFullPath(".\\Layouts\\Default.tcm");
            if (sav.ShowDialog() == DialogResult.OK)
            {
                TabControlContainer.SaveTCCMap(sav.FileName, GUIService.GUI.CurrentTabsMap);

                ress.Add(true);
            }
            else
                ress.Add(false);
            responses = ress.ToArray();
        }
    }
}
