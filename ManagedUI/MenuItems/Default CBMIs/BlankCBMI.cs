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
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace ManagedUI
{
    [Export(typeof(CBMI)), Export(typeof(IMenuItemRepresentator))]
    [MIRInfo("Blank CBMI", "cbmi.blank")]
    [MIRResourcesInfo("CBMI_Name_Blank", "CBMI_Tooltip_Blank")]
    [CBMIInfo(false, 50, true)]
    class BlankCBMI : CBMI
    {
        public override void OnDropDownClosed()
        {
            Trace.WriteLine("CBMI dropdown closed");
        }
        public override void OnDropDownOpening()
        {
            Trace.WriteLine("CBMI dropdown is opening ...");
        }
        public override void OnEnterPressed()
        {
            Trace.WriteLine("CBMI enter pressed");
        }
        public override void OnIndexChanged(int newIndex)
        {
            Trace.WriteLine("CBMI item index changed to " + newIndex);
        }
        public override void OnTextChanged(string newText)
        {
            Trace.WriteLine("CBMI text changed to " + newText);
        }
        public override void OnView()
        {
            if (Items == null)
                Items = new List<string>();
            Items.Clear();
            for (int i = 1; i < 5; i++)
                Items.Add("Item " + i);
        }
    }
}
