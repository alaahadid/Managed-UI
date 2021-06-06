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
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace ManagedUI
{
    [Export(typeof(ICommand))]
    [CommandInfo("Set TBR Visible", "set.tbr.visible", "gui")]
    class SetTBRVisible : ICommand
    {
        public override void Execute(object[] parameters, out object[] responses)
        {
            responses = new object[0];

            if (parameters == null)
            {
                Trace.TraceError(Properties.Resources.Status_UnableToExecuteCommand +
                    " set.tbr.visible: " + Properties.Resources.Status_NoParamPassed);
                return;
            }
            if (parameters.Length < 2)
            {
                Trace.TraceError(Properties.Resources.Status_UnableToExecuteCommand +
                    " set.tbr.visible: " + Properties.Resources.Status_NoParamPassed);
                return;
            }
            bool found = false;
            for (int i = 0; i < GUIService.GUI.CurrentToolbarsMap.ToolBars.Count; i++)
            {
                if (GUIService.GUI.CurrentToolbarsMap.ToolBars[i].Name == parameters[0].ToString())
                {
                    found = true;
                    GUIService.GUI.CurrentToolbarsMap.ToolBars[i].Visible = (bool)parameters[1];
                    break;
                }
            }
            if (!found)
                Trace.TraceError(Properties.Resources.Status_UnableToExecuteCommand +
                    " set.tbr.visible: " + Properties.Resources.Status_TBRCannotBeFound + ", " +
                    Properties.Resources.Status_InvalidParameter);
        }
    }
}
