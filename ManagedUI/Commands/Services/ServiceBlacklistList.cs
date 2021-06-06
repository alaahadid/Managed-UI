

using ManagedUI.Properties;
using System;
using System.Diagnostics;
using System.ComponentModel.Composition;

/*
 * This command list blacklisted services
 */
namespace ManagedUI
{
    [Export(typeof(ICommand))]
    [CommandInfo("Blacklist Service List", "service.blacklist.list", "cmd")]
    class ServiceBlacklistList : ICommand
    {
        public override void Execute(object[] parameters, out object[] responses)
        {
            responses = new object[0];

            // Load blacklisted services from settings
            if (Settings.Default.BlackListedServices == null)
                Settings.Default.BlackListedServices = new System.Collections.Specialized.StringCollection();

            if (Settings.Default.BlackListedServices.Count == 0)
            {
                Trace.WriteLine(Resources.Status_NoServiceIsListedAsBlacklisted);
                return;
            }
            foreach (string ser in Settings.Default.BlackListedServices)
            {
                if (MUI.IsServiceExist(ser))
                {
                    Lazy<IService, IServiceInfo> service = MUI.GetService(ser);

                    Trace.WriteLine(string.Format(". {0} [{1}]: {2}", service.Metadata.Name, service.Metadata.ID, service.Metadata.Description));
                }
                else
                {
                    Trace.WriteLine(string.Format(". " + Resources.Status_ServiceIDInBlackListCannotBeFound + ": '{0}' " + "(" + Resources.Status_IEServiceRemoved + ")", ser));
                }
            }
        }
    }
}
