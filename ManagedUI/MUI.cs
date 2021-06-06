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
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Linq;
using ManagedUI.Properties;

namespace ManagedUI
{
    /// <summary>
    /// The Core of ManagedUI. This class is the most important class of the library.
    /// </summary>
    public sealed class MUI
    {
        private static CompositionContainer _container;
        private static string _application_title;
        private static Icon _application_icon;
        private static string _application_version;
        private static string _application_copyright;
        private static string _application_copyright_message;

        // Methods
        /// <summary>
        /// Initialize the core. This method must be called at program's start up.
        /// </summary>
        /// <param name="parameters">Parameters to use when initializing MUI</param>
        public static void Initialize(MUIParameters parameters)
        {
            Trace.WriteLine(Properties.Resources.Status_InitializingTheCore, StatusMode.Normal);

            if (parameters.RequiredServiceIDS == null)
            {
                Utilities.ShowError(new Exception(Resources.Error_NotValidRequiredServicesList), Resources.Error_NotValidRequiredServicesList, Resources.MessageCaption_CoreInitialize);
                return;
            }
            if (parameters.RequiredServiceIDS.Count == 0)
            {
                Utilities.ShowError(new Exception(Resources.Error_NotValidRequiredServicesListAtLeastOneService), Resources.Error_NotValidRequiredServicesListAtLeastOneService, Resources.MessageCaption_CoreInitialize);
                return;
            }
            // Set the parameters
            _application_title = parameters.ProjectTitle;
            if (_application_title == null)
                _application_title = "ManagedUI";

            _application_version = parameters.ProjectVersion;
            if (_application_version == null)
                _application_version = "1.0.0";

            _application_icon = parameters.ProjectIcon;

            _application_copyright = parameters.ProjectCopyright;
            if (_application_copyright == null)
                _application_copyright = "Copyright (C) <Year> <Author name>";

            _application_copyright_message = parameters.ProjectCopyrightMessage;
            if (_application_copyright_message == null)
                _application_copyright_message = "This program is protected by the law. See the about box for more details.";

            InitializeFolders();

            // Add the start up listener
            Trace.Listeners.Clear();
#if DEBUG
            // Add the console listener to see what's going on in visual studio output window
            Trace.Listeners.Add(new ConsoleTraceListener());
#endif
            // Load settings
            Trace.WriteLine(Properties.Resources.Status_LoadingSettings, StatusMode.Normal);
            Properties.Settings.Default.Reload();
            Trace.WriteLine(Properties.Resources.Status_SettingsLoadedSuccessfully, StatusMode.Normal);

            // Load languages
            Trace.WriteLine(Properties.Resources.Status_LoadingLanguageInterfeces, StatusMode.Normal);
            LocalizationManager.DetectSupportedLanguages(Application.StartupPath);
            if (LocalizationManager.SupportedLanguages != null)
                Trace.WriteLine(Properties.Resources.Status_TotalOf + " " +
                    LocalizationManager.SupportedLanguages.Length / 3 + " " +
                    Properties.Resources.Status_LanguageInterfaceFound, StatusMode.Normal);
            else
                Trace.WriteLine(Properties.Resources.Status_NoLanguageInterfaceFound, StatusMode.Normal);
            // Set the language
            Trace.WriteLine(Properties.Resources.Status_SettingLanguageInterface, StatusMode.Normal);
            LocalizationManager.CurrentLanguageID = Properties.Settings.Default.CurrentLanguage;
            Trace.WriteLine(Properties.Resources.Status_LanguageInterfaceSetTo + " " + LocalizationManager.CurrentLanguageID, StatusMode.Normal);

            // Load the splash
            FormStartup startUp = null;
            TraceListenerStartupForm splashTraceListener = null;
            if (parameters.ShowSplash)
            {
                startUp = new FormStartup();
                Trace.Listeners.Add(splashTraceListener = new TraceListenerStartupForm(startUp));
                if (parameters.ProjectIcon != null)
                {
                    startUp.Icon = parameters.ProjectIcon;
                    startUp.ShowIcon = true;
                }

                if (parameters.UseColorForSplashBackground)
                {
                    if (parameters.SplashBackgroundColor != null)
                        startUp.BackColor = parameters.SplashBackgroundColor;
                    else
                        startUp.BackColor = Color.White;
                }
                else
                {
                    if (parameters.SplashBackgroundImage != null)
                        startUp.BackgroundImage = parameters.SplashBackgroundImage;
                }

                if (parameters.SplashSize != null)
                {
                    if (parameters.SplashSize.Height > 0 && parameters.SplashSize.Width > 0)
                        startUp.Size = parameters.SplashSize;
                }

                // Show the splash window
                startUp.Show();
                startUp.Refresh();
                System.Threading.Thread.Sleep(1000);
            }
            // Load services and stuff
            LoadServices();
            // Check required services
            if (parameters.RequiredServiceIDS.Contains("cmd"))
                parameters.RequiredServiceIDS.Remove("cmd");
            if (parameters.RequiredServiceIDS.Contains("gui"))
                parameters.RequiredServiceIDS.Remove("gui");
            foreach (string id in parameters.RequiredServiceIDS)
            {
                if (!IsServiceExist(id))
                {
                    Utilities.ShowError(new Exception(Resources.Error_NotValidRequiredServicesListAtMissingService + "\n" + Resources.Word_MissingServiceID + ": '" + id + "'"), Resources.Error_NotValidRequiredServicesListAtMissingService, Resources.MessageCaption_CoreInitialize);

                    Trace.WriteLine(Properties.Resources.Error_CoreCannotInitialize, StatusMode.Error);
                    return;
                }
            }
            // If not all services are allowed (i.e. add ons allow), Close all other services that: 1 not built in 2 not in the required list
            if (!parameters.AllowAllServices)
            {
                for (int i = 0; i < Services.Count(); i++)
                {
                    if (Services[i].Metadata.ID != "cmd" && Services[i].Metadata.ID != "gui" && !parameters.RequiredServiceIDS.Contains(Services[i].Metadata.ID))
                    {
                        // Remove it !!
                        Services[i].Value.Close();
                        Services.RemoveAt(i);
                        i--;
                    }
                }
            }
            Trace.WriteLine(Properties.Resources.Status_CoreIsInitialized, StatusMode.Normal);

            // Load the main form
            GUIService.GUI.InitializeTheMainForm();
            GUIService.GUI.MainForm.Show();

            // Run arguments
            if (parameters.StartupArguments != null)
            {
                if (parameters.StartupArguments.Count > 0)
                {
                    Trace.WriteLine("->" + Properties.Resources.Status_ExecutingStartupCommands + " ...", StatusMode.Normal);
                    CommandsManager.CMD.Execute(parameters.StartupArguments.ToArray());
                }
            }
            // Run command lines
            Trace.WriteLine("->" + Properties.Resources.Status_ExecutingCommandLines + " ...", StatusMode.Normal);
            CommandsManager.CMD.Execute(Environment.GetCommandLineArgs());

            // Ready !!
            Trace.WriteLine(Properties.Resources.Status_Ready, StatusMode.Normal);
            // Close the splash and run the application
            if (parameters.ShowSplash)
            {
                if (splashTraceListener != null)
                    Trace.Listeners.Remove(splashTraceListener);
                startUp.Close();
            }

            //Application.Run(GUIService.GUI.MainForm);
            Application.Run();

            // Reached here means the application is finished.
            // Run arguments
            if (parameters.CloseupArguments != null)
            {
                if (parameters.CloseupArguments.Count > 0)
                {
                    Trace.WriteLine("->" + Properties.Resources.Status_ExecutingCloseupCommands + " ...", StatusMode.Normal);
                    CommandsManager.CMD.Execute(parameters.CloseupArguments.ToArray());
                }
            }

            // Save settings
            Trace.WriteLine(Properties.Resources.Status_SavingSettings, StatusMode.Normal);
            // Get some settings before closing
            Properties.Settings.Default.CurrentLanguage = LocalizationManager.CurrentLanguageID;
            // Save now !!
            Properties.Settings.Default.Save();
            Trace.WriteLine(Properties.Resources.Status_SettingsSavedSuccessfully, StatusMode.Normal);

            // Close the core
            Close();
        }
        /// <summary>
        /// Load all available services. No need to call this if your application already use 
        /// the Initialize() method. Use it only if you want to use MUI services without loading
        /// the main form.
        /// </summary>
        public static void LoadServices()
        {
            Trace.WriteLine(Properties.Resources.Status_LoadingServices, StatusMode.Normal);
            // An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            // Add start up folder and Extensions folder.
            catalog.Catalogs.Add(new DirectoryCatalog(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0])));
            catalog.Catalogs.Add(new DirectoryCatalog(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "*.exe"));
            if (Directory.Exists("Extensions"))
                catalog.Catalogs.Add(new DirectoryCatalog("Extensions"));

            if (Settings.Default.BlackListedServices == null)
                Settings.Default.BlackListedServices = new System.Collections.Specialized.StringCollection();

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            // Get the exports
            Services = _container.GetExports<IService, IServiceInfo>().ToList();
            for (int i = 0; i < Services.Count(); i++)
            {
                Trace.WriteLine("--> " + Properties.Resources.Status_ServiceFound + " [" + Services[i].Metadata.Name + "]", StatusMode.Normal);

                // This will create the service, calling it's ctor then call the Initialize method.
                // In other word, load all services and extensions and tell 'em that the program is initializing.
                if (Services[i].Metadata.IsDefault)
                {
                    // This service is a default service, that's mean we cannot blacklist it. Add it regardless of blacklist.
                    Services[i].Value.Initialize();
                }
                else
                {
                    // Add this service if it is not blacklisted. If it is blacklisted, remove it and not initialize it.
                    if (!Settings.Default.BlackListedServices.Contains(Services[i].Metadata.ID))
                    {
                        // This service is not blacklisted. Add it normally.
                        Services[i].Value.Initialize();
                    }
                    else
                    {
                        // Blacklisted service, remove it and do not initialize it.
                        Services.RemoveAt(i);
                        i--;
                        Trace.WriteLine("--> " + Properties.Resources.Status_ServiceIsBlacklistedAndRemoved + " [" + Services[i].Metadata.Name + "]", StatusMode.Normal);
                    }
                }
            }
        }
        /// <summary>
        /// Initialize folders required by the application (i.e. StartupFolder, Documentsfolder and ExceptionsFolder)
        /// </summary>
        public static void InitializeFolders()
        {
            Trace.WriteLine(Resources.Status_CheckingApplicationFolders);
            StartupFolder = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);

            Documentsfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _application_title);
            Directory.CreateDirectory(Documentsfolder);

            ExceptionsFolder = Path.Combine(Documentsfolder, "Exceptions");
            Directory.CreateDirectory(ExceptionsFolder);

            Trace.WriteLine(Resources.Status_ApplicationFoldersCheckedSuccessfully);
        }
        /// <summary>
        /// Close the core. This method must be called at program's close.
        /// </summary>
        public static void Close()
        {
            Trace.WriteLine(Properties.Resources.Status_ClosingServices, StatusMode.Normal);
            foreach (Lazy<IService, IServiceInfo> service in Services)
            {
                service.Value.Close();
            }
            Trace.WriteLine(Properties.Resources.Status_AllServicesClosed, StatusMode.Normal);
        }
        /// <summary>
        /// Exit the application.
        /// </summary>
        public static bool ExitApplication()
        {
            ApplicationExittingArgs args = new ApplicationExittingArgs();
            ApplicationExitting?.Invoke(null, args);

            if (args.Cancel)
            {
                Trace.WriteLine("Application exit canceled.");
                return false;
            }
            Application.Exit();
            return true;
        }
        /// <summary>
        /// Get service using name
        /// </summary>
        /// <param name="name">The service name as provided by service metadata (not case sensitive)</param>
        /// <returns>The service if found otherwise null</returns>
        public static Lazy<IService, IServiceInfo> GetService(string name)
        {
            foreach (Lazy<IService, IServiceInfo> service in Services)
            {
                if (service.Metadata.Name.ToLower() == name.ToLower())
                    return service;// Service already loaded and ready !
            }
            return null;
        }
        /// <summary>
        /// Get service using service id
        /// </summary>
        /// <param name="id">The service id as provided by service metadata (not case sensitive)</param>
        /// <returns>The service if found otherwise null</returns>
        public static Lazy<IService, IServiceInfo> GetServiceByID(string id)
        {
            if (Services == null)
                return null;
            foreach (Lazy<IService, IServiceInfo> service in Services)
            {
                if (service.Metadata.ID.ToLower() == id.ToLower())
                    return service;// Service already loaded and ready !
            }
            return null;
        }
        /// <summary>
        /// Get if service is exist
        /// </summary>
        /// <param name="id">The service id</param>
        /// <returns>True if service is loaded and exist otherwise false.</returns>
        public static bool IsServiceExist(string id)
        {
            foreach (Lazy<IService, IServiceInfo> service in Services)
            {
                if (service.Metadata.ID.ToLower() == id.ToLower())
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Get if a service is default (cannot be disabled) or not.
        /// </summary>
        /// <param name="id">The service id (not case sensitive).</param>
        /// <returns>True if service is default (cannot be disabled) otherwise false.</returns>
        public static bool IsServiceDefault(string id)
        {
            foreach (Lazy<IService, IServiceInfo> service in Services)
            {
                if (service.Metadata.ID.ToLower() == id.ToLower())
                    return service.Metadata.IsDefault;
            }
            return false;
        }
        // Properties
        /// <summary>
        /// Get the temp folder of the application
        /// </summary>
        public static string Documentsfolder { get; set; }
        /// <summary>
        /// The start up path of the application
        /// </summary>
        public static string StartupFolder { get; set; }
        /// <summary>
        /// The exception folder of the application, where exception files will be saved.
        /// </summary>
        public static string ExceptionsFolder { get; set; }
        /// <summary>
        /// Get or set the project title. This will be used in the main form and for settings. If set to null, MUI will use ManagedUI as a project title.
        /// </summary>
        public static string ProjectTitle
        {
            get { return _application_title; }
            set
            {
                _application_title = value;
                if (_application_title == null)
                    _application_title = "ManagedUI";
                ProjectTitleChanged?.Invoke(null, new EventArgs());
            }
        }
        /// <summary>
        /// Get or set the icon to use in the main form. If set to null, MUI will use the default vs icon for the form.
        /// </summary>
        public static Icon ProjectIcon
        {
            get { return _application_icon; }
            set
            {
                _application_icon = value;

                ProjectIconChanged?.Invoke(null, new EventArgs());
            }
        }
        /// <summary>
        /// Get or set the version of the project. If set null, MUI will use 1.0.0 as a project version.
        /// </summary>
        public static string ProjectVersion
        {
            get { return _application_version; }
            set
            {
                _application_version = value;
                if (_application_version == null)
                    _application_version = "1.0.0";
                ProjectVersionChanged?.Invoke(null, new EventArgs());
            }
        }
        /// <summary>
        /// Get or set the project copyright, for example: Copyright (C) [Year] [Author or name]
        /// </summary>
        public static string ProjectCopyright
        {
            get { return _application_copyright; }
            set
            {
                _application_copyright = value;
                if (_application_copyright == null)
                    _application_copyright = "";
                ProjectCopyrightChanged?.Invoke(null, new EventArgs());
            }
        }
        /// <summary>
        /// Get or set the project copyright message, this will be appeared to user in the splash screen and the about window.
        /// </summary>
        public static string ProjectCopyrightMessage
        {
            get { return _application_copyright_message; }
            set
            {
                _application_copyright_message = value;
                if (_application_copyright_message == null)
                    _application_copyright_message = "";
                ProjectCopyrightMessageChanged?.Invoke(null, new EventArgs());
            }
        }
        /// <summary>
        /// Get available services.
        /// </summary>
        [ImportMany]
        public static List<Lazy<IService, IServiceInfo>> Services
        {
            get;
            private set;
        }

        // Events
        /// <summary>
        /// Raised when the application is about to be closed.
        /// </summary>
        public static event EventHandler<ApplicationExittingArgs> ApplicationExitting;
        /// <summary>
        /// Raised when the ProjectTitle is changed.
        /// </summary>
        public static event EventHandler ProjectTitleChanged;
        /// <summary>
        /// Raised when the ProjectVersion is changed.
        /// </summary>
        public static event EventHandler ProjectVersionChanged;
        /// <summary>
        /// Raised when the ProjectIcon is changed.
        /// </summary>
        public static event EventHandler ProjectIconChanged;
        /// <summary>
        /// Raised when the ProjectCopyright is changed.
        /// </summary>
        public static event EventHandler ProjectCopyrightChanged;
        /// <summary>
        /// Raised when the ProjectCopyrightMessage is changed.
        /// </summary>
        public static event EventHandler ProjectCopyrightMessageChanged;
    }
}
