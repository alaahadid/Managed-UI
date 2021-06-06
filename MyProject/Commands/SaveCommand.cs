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
using System;
using System.ComponentModel.Composition;
using ManagedUI;

namespace MyProject
{
    [Export(typeof(ICommand))]
    [CommandInfo("Save", "save", "main.service")]
    class SaveCommand : ICommand
    {
        public override void Execute(object[] parameters, out object[] responses)
        {
            responses = new object[0];
            // The "tc.textedit" is the id of the TCTextControl
            // GUService.GUI is the already imported GUIService object. You can do this instead:
            // GUIService gui = (GUIService)MUI.GetServiceByID("gui").Value;
            // 
            // We can change the code below like this:
            // TCTextEdit textedit = (TCTextEdit)GUIService.GUI.GetTabControl("tc.textedit").Value;
            // Both will work.
            Lazy<ITabControl, IControlInfo> control = GUIService.GUI.GetTabControl("tc.textedit");

            // Now call the method !!
            // Convert type (Value here is ITabControl)
            TCTextEdit textedit = (TCTextEdit)control.Value;
            // Call the method.
            textedit.SaveChanges();
        }
    }
}
