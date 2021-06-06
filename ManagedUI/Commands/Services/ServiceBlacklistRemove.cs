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
using System.Diagnostics;
using ManagedUI.Properties;

/*
 * This command remove blacklist for a service (i.e. enable a service and all related components (i.e. commands))
 * Parameters:
 * string serviceID: the service id to remove blacklist (can be send multiple times to enable more than one service in a time)
 */
namespace ManagedUI
{
    [Export(typeof(ICommand))]
    [CommandInfo("Remove Blacklist Service", "service.blacklist.remove", "cmd")]
    class ServiceBlacklistRemove : ICommand
    {
        public override void Execute(object[] parameters, out object[] responses)
        {
            responses = new object[0];

            // Expected parameters are the services ids !
            if (parameters == null)
            {
                Trace.TraceError(Properties.Resources.Status_UnableToExecuteCommand +
                        " " + ID + ": " + Properties.Resources.Status_NoParamPassed);
                return;
            }
            if (parameters.Length == 0)
            {
                Trace.TraceError(Properties.Resources.Status_UnableToExecuteCommand +
                " " + ID + ": " + Properties.Resources.Status_NoParamPassed);
                return;
            }
            // Load blacklisted services from settings
            if (Settings.Default.BlackListedServices == null)
            {
                Settings.Default.BlackListedServices = new System.Collections.Specialized.StringCollection();
                Trace.TraceError(Properties.Resources.Status_UnableToExecuteCommand +
                      " " + ID + ": " + Properties.Resources.Status_NoServiceInBlacklist);
                return;
            }

            // Each parameter must be string, if there is a command that is not a string, exception will raise.
            foreach (string id in parameters)
            {
                if (Settings.Default.BlackListedServices.Contains(id))
                {
                    if (MUI.IsServiceExist(id))
                    {
                        if (!MUI.IsServiceDefault(id))
                        {
                            Settings.Default.BlackListedServices.Remove(id);
                            Trace.WriteLine(Resources.Word_ServiceWithID + " '" + id + "' " + Resources.Status_HasBeenRemovedFromBlacklist);
                        }
                        else
                        {
                            Trace.TraceWarning(Resources.Word_ServiceWithID + " '" + id + "' " + Resources.Status_CannotBeEnabledTheServiceIsDefault);
                        }
                    }
                    else
                    {
                        Trace.TraceWarning(Resources.Word_ServiceWithID + " '" + id + "' " + Resources.Status_CannotBeEnabledServiceDoesntExist);
                    }
                }
                else
                {
                    Trace.TraceWarning(Resources.Word_ServiceWithID + " '" + id + "' " + Resources.Status_AlreadyEnabled);
                }
            }
        }
    }
}
