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
    [Export(typeof(RMI)), Export(typeof(IMenuItemRepresentator))]
    [MIRInfo("File", "root.file")]
    [MIRResourcesInfo("MIR_Name_File")]
    class RMI_File : RMI
    {
    }
    [Export(typeof(RMI)), Export(typeof(IMenuItemRepresentator))]
    [MIRInfo("Edit", "root.edit")]
    [MIRResourcesInfo("MIR_Name_Edit")]
    class RMI_Edit : RMI
    {
    }
    [Export(typeof(RMI)), Export(typeof(IMenuItemRepresentator))]
    [MIRInfo("View", "root.view")]
    [MIRResourcesInfo("MIR_Name_View")]
    class RMI_View : RMI
    {
    }
    [Export(typeof(RMI)), Export(typeof(IMenuItemRepresentator))]
    [MIRInfo("Tools", "root.tools")]
    [MIRResourcesInfo("MIR_Name_Tools")]
    class RMI_Tools : RMI
    {
    }
    [Export(typeof(RMI)), Export(typeof(IMenuItemRepresentator))]
    [MIRInfo("Help", "root.help")]
    [MIRResourcesInfo("MIR_Name_Help")]
    class RMI_Help : RMI
    {
    }
}
