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
using System.ComponentModel.Composition;
using ManagedUI;

namespace ManagedUIDemo
{
    // 1 We need to make this item visible by exporting the type of it. We export the mir type first
    // (CMI, PMI ...etc) then we export the IMenuItemRepresentator type.
    [Export(typeof(PMI)), Export(typeof(IMenuItemRepresentator))]
    // 2 We need to specify MIR basic info, the name and the id. Name and id must not be exist in
    // other places in your app. Also, the id should not content spaces.
    [MIRInfo("Demo test item 1", "demo.test1")]
    // 3 Setup the resource keys. Depending on mir type, we need to setup the DisplayName, Tooltip
    // and the Icon that would be displayed to user. Note that the keys you add here MUST be exist
    // in the default resources (Properties.Resources), if your project doesn't contain one and
    // you need to use MIR items YOU MUST create one using Properties of the project, and use it 
    // for mir item keys. Otherwise, you can override the properties of this item and use the names 
    // of your own.
    // Warning !!: MIRNoResourceProperties and MIRResourcesInfo MUST not be used togather for the same item.
    [MIRResourcesInfo("PMI_Name_Test1", "PMI_Tooltip_Test1", "MUI")]
    class PMI_Demo_Test : PMI
    {
    }
}
