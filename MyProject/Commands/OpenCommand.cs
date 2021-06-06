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
using System.Windows.Forms;
using System.ComponentModel.Composition;
using ManagedUI;

namespace MyProject
{
    [Export(typeof(ICommand))]
    [CommandInfo("Open", "open", "main.service")]
    class OpenCommand : ICommand
    {
        [Import]
        MainService service;

        public override void Execute(object[] parameters, out object[] responses)
        {
            responses = new object[0];

            string file = "";
            // Check parameters ...
            if (parameters != null)// Parameters are not null
                if (parameters.Length > 0)// We have at least one parameter
                    if (parameters[0] is string)// Check the type
                        file = (string)parameters[0];// Got it !

            if (!File.Exists(file))
            {
                // The file is not exist, open a file open dialog and use it to set the file.
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Open text file";
                op.Filter = "Text File (*.txt)|*.txt";
                if (op.ShowDialog() == DialogResult.OK)
                    service.File = op.FileName;
            }
            else// Simply set the file !!
                service.File = file;
        }
    }
}
