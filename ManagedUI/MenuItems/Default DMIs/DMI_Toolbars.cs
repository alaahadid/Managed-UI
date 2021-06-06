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

namespace ManagedUI
{
    [Export(typeof(DMI)), Export(typeof(IMenuItemRepresentator))]
    [MIRInfo("Language", "dmi.toolbars")]
    [MIRResourcesInfo("DMI_Name_Toolbars")]
    class DMI_Toolbars : DMI
    {
        public override void OnView()
        {
            ChildItems.Clear();
            // Get all toolbars
            for (int i = 0; i < GUIService.GUI.CurrentToolbarsMap.ToolBars.Count; i++)
            {
                DMIChild chld = new DMIChild(ID);

                chld.DisplayName = GUIService.GUI.CurrentToolbarsMap.ToolBars[i].Name;
                chld.Active = GUIService.GUI.CurrentToolbarsMap.ToolBars[i].Visible;

                chld.CommandID = "set.tbr.visible";
                chld.UseParameters = true;
                chld.Parameters = new object[] { GUIService.GUI.CurrentToolbarsMap.ToolBars[i].Name, !chld.Active };

                ChildItems.Add(chld);
            }
        }
    }
}
