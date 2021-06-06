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
    [MIRInfo("Language", "dmi.language")]
    [MIRResourcesInfo("DMI_Name_Language")]
    class DMI_Language : DMI
    {
        public override void OnView()
        {
            ChildItems.Clear();
            // Get all supported languages
            for (int i = 0; i < LocalizationManager.SupportedLanguages.Length / 3; i++)
            {
                DMIChild chld = new DMIChild(ID);

                chld.DisplayName = LocalizationManager.SupportedLanguages[i, 2];
                chld.Active = LocalizationManager.SupportedLanguages[i, 1] == LocalizationManager.CurrentLanguageID;

                chld.CommandID = "set.language";
                chld.UseParameters = true;
                chld.Parameters = new object[] { LocalizationManager.SupportedLanguages[i, 1] };

                ChildItems.Add(chld);
            }
        }
    }
}
