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
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace ManagedUI
{
    /// <summary>
    /// Localization manager
    /// </summary>
    public class LocalizationManager
    {
        /// <summary>
        /// Get or set the current language id.
        /// </summary>
        public static string CurrentLanguageID
        {
            get { return Thread.CurrentThread.CurrentUICulture.Name; }
            set { Thread.CurrentThread.CurrentUICulture = new CultureInfo(value); }
        }
        /// <summary>
        /// Get supported language interfeces in format [Language English Name, Language ID, Language Native Name]
        /// </summary>
        public static string[,] SupportedLanguages
        { get; private set; }

        /// <summary>
        /// Detect all supported languages in a folder.
        /// </summary>
        /// <param name="searchFolder">The folder to search.</param>
        public static void DetectSupportedLanguages(string searchFolder)
        {
            SupportedLanguages = new string[0, 0];
            string[] langsFolders = Directory.GetDirectories(searchFolder);
            List<string> ids = new List<string>();
            List<string> englishNames = new List<string>();
            List<string> NativeNames = new List<string>();
            foreach (string folder in langsFolders)
            {
                try
                {
                    CultureInfo inf = new CultureInfo(Path.GetFileName(folder));
                    // no errors lol add the id
                    ids.Add(Path.GetFileName(folder));
                    englishNames.Add(inf.EnglishName);
                    NativeNames.Add(inf.NativeName);
                }
                catch { }
            }
            if (ids.Count > 0)
            {
                SupportedLanguages = new string[ids.Count, 3];
                for (int i = 0; i < ids.Count; i++)
                {
                    SupportedLanguages[i, 0] = englishNames[i];
                    SupportedLanguages[i, 1] = ids[i];
                    SupportedLanguages[i, 2] = NativeNames[i];
                }
            }
        }
    }
}
