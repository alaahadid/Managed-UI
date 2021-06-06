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
using System.IO;
using System.ComponentModel.Composition;

namespace ManagedUI
{
    [Export(typeof(DMI)), Export(typeof(IMenuItemRepresentator))]
    [MIRInfo("Shortcuts", "dmi.shortcuts")]
    [MIRResourcesInfo("DMI_Name_Shortcuts")]
    class DMI_Shortcuts : DMI
    {
        public override void OnView()
        {
            ChildItems.Clear();
            // Get all toolbars
            string file_directory = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "Shortcuts");
            Directory.CreateDirectory(file_directory);
            string[] files = Directory.GetFiles(file_directory, "*.sm", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                DMIChild chld = new DMIChild(ID);

                chld.DisplayName = Path.GetFileName(file);
                //chld.Icon = tab.Value.Icon;
                //chld.Active = GUIService.GUI.CurrentTabsMap.ContainsControl(tab.Value.ID);

                chld.CommandID = "tabcontrol.load.shortcuts";
                chld.UseParameters = true;
                chld.Parameters = new object[] { file };

                ChildItems.Add(chld);
            }
        }
    }
}
