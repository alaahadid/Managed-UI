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
using System.IO;
using System.ComponentModel.Composition;
using ManagedUI;

namespace MyProject
{
    [Export(typeof(IMenuItemRepresentator)), Export(typeof(DMI))]
    [MIRInfo("Recent Files", "dmi.recent.files")]
    [MIRResourcesInfo("DMI_Name_RecentFiles")]
    class DMI_RecentFiles : DMI
    {
        [Import]
        MainService service;
        public override void OnView()
        {
            // Clear the menu item children
            ChildItems.Clear();
            // This code should loop throgh all recent files. But here, we will use one file.
            // for (int i = 0; i < service.Recents.Count; i++)
            // {
            DMIChild chld = new DMIChild(ID);

            chld.DisplayName = Path.GetFileName(service.File);// The name that will be shown for user
            chld.Active = false;// Indicates if it is checked or not. Let it be not checked.

            chld.CommandID = "open";// The command id to use when this item is clicked. Here we will use open since we want it to open the file.
            chld.UseParameters = true;// Indicates that this item will use parameters for the command when it executes.
            chld.Parameters = new object[] { service.File };// The parameters !! we have only one parameter, the file path.

            ChildItems.Add(chld);// Add it !
            //}
        }
    }
}
