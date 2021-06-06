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
using System.ComponentModel.Composition;

namespace ManagedUI
{
    [Export(typeof(DMI)), Export(typeof(IMenuItemRepresentator))]
    [MIRInfo("Tabs", "dmi.tabs")]
    [MIRResourcesInfo("DMI_Name_Tabs")]
    class DMI_Tabs : DMI
    {
        public override void OnView()
        {
            ChildItems.Clear();
            // Get all toolbars
            foreach (Lazy<ITabControl, IControlInfo> tab in GUIService.GUI.AvailableTabControls)
            {
                DMIChild chld = new DMIChild(ID);

                chld.DisplayName = tab.Value.DisplayName;
                chld.Icon = tab.Value.Icon;
                chld.Active = GUIService.GUI.CurrentTabsMap.ContainsControl(tab.Value.ID);

                chld.CommandID = "tabcontrol.set.visible";
                chld.UseParameters = true;
                chld.Parameters = new object[] { tab.Value.ID };

                ChildItems.Add(chld);
            }
        }
    }
}
