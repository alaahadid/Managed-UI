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
namespace ManagedUI
{
    /// <summary>
    /// Present const arrays for buttons.
    /// </summary>
    public sealed class ManagedMessageBoxButtons
    {
        /// <summary>
        /// Ok button
        /// </summary>
        public static string[] OK = { Properties.Resources.Button_OK };
        /// <summary>
        /// Ok + No buttons
        /// </summary>
        public static string[] OKNo = { Properties.Resources.Button_OK, Properties.Resources.Button_NO };
        /// <summary>
        /// Ok + No + Cancel buttons
        /// </summary>
        public static string[] OKNoCancel = { Properties.Resources.Button_OK, Properties.Resources.Button_NO, Properties.Resources.Button_Cancel };
        /// <summary>
        /// Ok + Cancel buttons
        /// </summary>
        public static string[] OKCancel = { Properties.Resources.Button_OK, Properties.Resources.Button_Cancel };
        /// <summary>
        /// Yes button
        /// </summary>
        public static string[] Yes = { Properties.Resources.Button_Yes };
        /// <summary>
        /// Yes + No buttons
        /// </summary>
        public static string[] YesNo = { Properties.Resources.Button_Yes, Properties.Resources.Button_NO };
        /// <summary>
        /// Yes + No + Cancel buttons
        /// </summary>
        public static string[] YesNoCancel = { Properties.Resources.Button_Yes, Properties.Resources.Button_NO, Properties.Resources.Button_Cancel };
        /// <summary>
        /// Save + Don't save + Cancel buttons
        /// </summary>
        public static string[] SaveDontsaveCancel = { Properties.Resources.Button_Save, Properties.Resources.Button_DontSave, Properties.Resources.Button_Cancel };
        /// <summary>
        /// Save + Don't save buttons
        /// </summary>
        public static string[] SaveDontsave = { Properties.Resources.Button_Save, Properties.Resources.Button_DontSave };
        /// <summary>
        /// Abort button
        /// </summary>
        public static string[] Abort = { Properties.Resources.Button_Abort };
        /// <summary>
        /// Abort + Retry + Ignore buttons
        /// </summary>
        public static string[] AbortRetryIgnore = { Properties.Resources.Button_Abort, Properties.Resources.Button_Retry, Properties.Resources.Button_Ignore };
        /// <summary>
        /// Retry + Cancel buttons
        /// </summary>
        public static string[] RetryCancel = { Properties.Resources.Button_Retry, Properties.Resources.Button_Cancel };
    }
}
