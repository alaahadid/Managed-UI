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
using System.Diagnostics;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace ManagedUI
{
    [Export(typeof(ICommand))]
    [CommandInfo("Show Help", "help.show", "gui")]
    class ShowHelp : ICommand
    {
        /// <summary>
        /// Show help to user
        /// </summary>
        /// <param name="parameters">Accept one parameter, bool value indicates if the 
        /// file is .chm (true = chm) otherwise will use normal html (index file).</param>
        /// <param name="responses"></param>
        public override void Execute(object[] parameters, out object[] responses)
        {
            responses = new object[0];
            bool isCHM = true;

            if (parameters != null)
                if (parameters.Length > 0)
                    isCHM = (bool)parameters[0];

            string helpPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]),
                System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
            helpPath = Path.Combine(helpPath, isCHM ? "Help.chm" : "index.htm");

            if (!File.Exists(helpPath))
            {
                helpPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]),
                System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
                helpPath = Path.Combine(helpPath, "en-US");
                helpPath = Path.Combine(helpPath, isCHM ? "Help.chm" : "index.htm");
            }
            // Try help normal
            if (!File.Exists(helpPath))
            {
                isCHM = true;
                helpPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "Help.chm");
            }
            // Try manual
            if (!File.Exists(helpPath))
            {
                isCHM = false;
                helpPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "Manual.pdf");
            }
            // Try readme
            if (!File.Exists(helpPath))
            {
                isCHM = false;
                helpPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "Readme.txt");
            }

            if (isCHM)
                Help.ShowHelp(null, helpPath, HelpNavigator.TableOfContents);
            else
                Process.Start(helpPath);
        }
    }
}
