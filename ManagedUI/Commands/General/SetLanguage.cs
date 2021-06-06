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

using System.ComponentModel.Composition;
using System.Diagnostics;

namespace ManagedUI
{
    [Export(typeof(ICommand))]
    [CommandInfo("Set Language", "set.language", "gui")]
    class SetLanguage : ICommand
    {
        public override void Execute(object[] parameters, out object[] responses)
        {
            responses = new object[0];

            if (parameters == null)
            {
                Trace.TraceError(Properties.Resources.Status_UnableToExecuteCommand +
                    " set.language: " + Properties.Resources.Status_NoParamPassed);
                return;
            }
            if (parameters.Length <= 0)
            {
                Trace.TraceError(Properties.Resources.Status_UnableToExecuteCommand +
                    " set.language: " + Properties.Resources.Status_NoParamPassed);
                return;
            }
            bool found = false;
            for (int i = 0; i < LocalizationManager.SupportedLanguages.Length / 3; i++)
            {
                if (parameters[0].ToString() == LocalizationManager.SupportedLanguages[i, 1])
                {
                    found = true;
                    Trace.WriteLine(Properties.Resources.Status_SettingLanguageInterface, StatusMode.Normal);
                    LocalizationManager.CurrentLanguageID = LocalizationManager.SupportedLanguages[i, 1];
                    Trace.WriteLine(Properties.Resources.Status_LanguageInterfaceSetTo + " " +
                        LocalizationManager.CurrentLanguageID, "status");
                    ManagedMessageBox.ShowMessage(Properties.Resources.Status_LanguageInterfaceSetTo + " " +
                  LocalizationManager.SupportedLanguages[i, 0] + " [" + LocalizationManager.CurrentLanguageID + "] \n" + Properties.Resources.Message_RestartToApply);
                }
            }

            if (!found)
                Trace.TraceError(Properties.Resources.Status_UnableToExecuteCommand +
                    " set.language: " + Properties.Resources.Status_LanguageIdIsNotExist + ", " +
                    Properties.Resources.Status_InvalidParameter);
        }
    }
}
