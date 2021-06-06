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
using System.Reflection;
namespace ManagedUI
{
    /// <summary>
    /// Can be used to get assembly information easily.
    /// </summary>
    public sealed class ManagedUIApplicationInfo
    {
        /// <summary>
        /// Application title from assembly
        /// </summary>
        public static string ApplicationTitle { get; private set; }
        /// <summary>
        /// Application description from assembly
        /// </summary>
        public static string ApplicationDescription { get; private set; }
        /// <summary>
        /// Application version from assembly
        /// </summary>
        public static string ApplicationVersion { get; private set; }
        /// <summary>
        /// Application copyright from assembly
        /// </summary>
        public static string ApplicationCopyright { get; private set; }
        /// <summary>
        /// Initialize and extract information from assembly.
        /// </summary>
        /// <param name="assembly"></param>
        public static void Initialize(Assembly assembly)
        {
            string copyright = "N/A";
            string title = "N/A";
            string description = "N/A";
            string version = assembly.GetName().Version.ToString();
            foreach (Attribute attr in Attribute.GetCustomAttributes(assembly))
            {
                if (attr.GetType() == typeof(AssemblyCopyrightAttribute))
                {
                    AssemblyCopyrightAttribute inf = (AssemblyCopyrightAttribute)attr;
                    copyright = inf.Copyright;
                }
                if (attr.GetType() == typeof(AssemblyTitleAttribute))
                {
                    AssemblyTitleAttribute inf = (AssemblyTitleAttribute)attr;
                    title = inf.Title;
                }
                if (attr.GetType() == typeof(AssemblyDescriptionAttribute))
                {
                    AssemblyDescriptionAttribute inf = (AssemblyDescriptionAttribute)attr;
                    description = inf.Description;
                }
                if (attr.GetType() == typeof(AssemblyVersionAttribute))
                {
                    AssemblyVersionAttribute inf = (AssemblyVersionAttribute)attr;
                    version = inf.Version;
                }
            }

            Initialize(title, version, copyright, description);
        }
        /// <summary>
        /// Initialize and set information.
        /// </summary>
        public static void Initialize(string applicationTitle, string applicationVersion, string applicationCopyright, string applicationDescription)
        {
            ApplicationTitle = applicationTitle;
            Console.WriteLine("ApplicationTitle : " + ApplicationTitle, "ManagedUI");

            ApplicationVersion = applicationVersion;
            Console.WriteLine("ApplicationVersion : " + ApplicationVersion, "ManagedUI");

            ApplicationCopyright = applicationCopyright;
            Console.WriteLine("ApplicationCopyright : " + ApplicationCopyright, "ManagedUI");

            ApplicationDescription = applicationDescription;
            Console.WriteLine("ApplicationDescription : " + applicationDescription, "ManagedUI");
        }
    }
}
