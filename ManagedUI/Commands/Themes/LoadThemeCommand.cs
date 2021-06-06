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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace ManagedUI.Commands
{
    [Export(typeof(ICommand))]
    [CommandInfo("Open a theme and make it current", "tabcontrol.load.theme", "gui")]
    class LoadThemeCommand : ICommand
    {
        public override void Execute(object[] parameters, out object[] responses)
        {
            List<object> ress = new List<object>();

            if (parameters != null)
            {
                if (parameters.Length > 0)
                {
                    if (parameters[0] is string)
                    {
                        bool success = false;
                        GUIService.GUI.CurrentTheme = Theme.LoadTHMMap((string)parameters[0], out success);
                        ress.Add(true);
                        responses = ress.ToArray();
                        return;
                    }
                }
            }

            OpenFileDialog op = new OpenFileDialog();
            op.Title = Properties.Resources.Title_OpenThemeFile;
            op.Filter = Properties.Resources.FilterName_ThemeFile + " (*.mt)|*.mt;*.MT;";
            op.FileName = System.IO.Path.GetFullPath(".\\Themes\\Default.mt");
            if (op.ShowDialog() == DialogResult.OK)
            {
                bool success = false;
                GUIService.GUI.CurrentTheme = Theme.LoadTHMMap(op.FileName, out success);

                ress.Add(success);
                responses = ress.ToArray();
                return;
            }
            // Reached here means all false
            ress.Add(false);
            responses = ress.ToArray();
        }
    }
}
