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
using System.Drawing;
using ManagedUI;

namespace ManagedUIDemo
{
    // 1 We need to make this item visible by exporting the type of it. We export the mir type first
    // (CMI, PMI ...etc) then we export the IMenuItemRepresentator type.
    [Export(typeof(CMI)), Export(typeof(IMenuItemRepresentator))]
    // 2 We need to specify MIR basic info, the name and the id. Name and id must not be exist in
    // other places in your app. Also, the id should not content spaces.
    [MIRInfo("Demo test item 2", "demo.test2")]
    // 3 Setup the names to show to user. Here, we will not setup resources, instead, we will
    // use the normal way. Note that we should set the icon manually if we would like to use icon.
    // Warning !!: MIRNoResourceProperties and MIRResourcesInfo MUST not be used togather for the same item.
    [MIRNoResourceProperties("This is demo 2 with command", "Executes the exit command")]
    // 4 For CMI, we need to add additional attribute to setup the command we want to be executed
    // when the item is used. Note that you must enter the id of the command and parameters
    // (if any) correctly.
    [CMIInfo("exit")]
    class CMI_Demo : CMI
    {
        /// <summary>
        /// Get the icon of this item
        /// </summary>
        public override Image Icon
        {
            get
            {
                // Since we chosen not to use resources, we can't setup icon using attr so
                // we must override the icon property or set it manually somewhere else.
                // Here, i use resources but can be an object of your choice.
                return Properties.Resources.control_eject_blue;
            }
        }
    }
}
