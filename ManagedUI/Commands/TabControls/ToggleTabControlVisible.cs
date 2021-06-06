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
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace ManagedUI
{
    [Export(typeof(ICommand))]
    [CommandInfo("Toggle Tab Control Visible", "tabcontrol.set.visible", "gui")]
    class ToggleTabControlVisible : ICommand
    {
        public override void Execute(object[] parameters, out object[] responses)
        {
            responses = new object[0];
            if (parameters == null)
            {
                Trace.TraceError(Properties.Resources.Status_UnableToExecuteCommand +
                    " tabcontrol.set.visible: " + Properties.Resources.Status_NoParamPassed);
                return;
            }
            if (parameters.Length <= 0)
            {
                Trace.TraceError(Properties.Resources.Status_UnableToExecuteCommand +
                    " tabcontrol.set.visible: " + Properties.Resources.Status_NoParamPassed);
                return;
            }

            if (GUIService.GUI.CurrentTabsMap != null)
            {
                if (!GUIService.GUI.CurrentTabsMap.ContainsControl(parameters[0].ToString()))
                    GUIService.GUI.CurrentTabsMap.AddControl(parameters[0].ToString(), true, true);
                else
                    GUIService.GUI.CurrentTabsMap.RemoveControl(parameters[0].ToString());
            }
            else
            {
                Trace.TraceError(Properties.Resources.Status_UnableToExecuteCommand +
                 " tabcontrol.set.visible: " + Properties.Resources.Status_NoTCCLoadedInCore);
            }
        }
    }
}
