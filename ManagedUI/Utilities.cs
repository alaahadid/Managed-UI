// This file is part of AHD Subtitles Maker Professional
// A program can create and edit subtitle.
// 
// Copyright © Alaa Ibrahim Hadid 2021
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
// 
// Author email: mailto:ahdsoftwares@hotmail.com
//
using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using ManagedUI.Properties;

namespace ManagedUI
{
    /// <summary>
    /// Some helper utilities
    /// </summary>
    public class Utilities
    {
        /// <summary>
        /// Get a file size as label with unit
        /// </summary>
        /// <param name="FilePath">The file path</param>
        /// <returns>The file size label with unit</returns>
        public static string GetFileSize(string FilePath)
        {
            if (File.Exists(Path.GetFullPath(FilePath)) == true)
            {
                FileInfo Info = new FileInfo(FilePath);
                string Unit = " Byte";
                double Len = Info.Length;
                if (Info.Length >= 1024)
                {
                    Len = Info.Length / 1024.00;
                    Unit = " KB";
                }
                if (Len >= 1024)
                {
                    Len /= 1024.00;
                    Unit = " MB";
                }
                if (Len >= 1024)
                {
                    Len /= 1024.00;
                    Unit = " GB";
                }
                return Len.ToString("F2") + Unit;
            }
            return "";
        }
        /// <summary>
        /// Get full file path, if the path is a dot path, it returns actual path.
        /// </summary>
        /// <param name="FilePath">The file path</param>
        /// <returns>The full file path</returns>
        public static string GetFullPath(string FilePath)
        {
            if (FilePath == null)
                return "";
            if (FilePath.Length == 0)
                return FilePath;
            string ai_path_code = "";
            if (FilePath.StartsWith("AI"))
            {
                for (int i = 0; i < FilePath.Length; i++)
                {
                    if (FilePath[i] == ')')
                    {
                        ai_path_code = FilePath.Substring(0, i + 1);
                        FilePath = FilePath.Substring(i + 1, FilePath.Length - (i + 1));
                    }
                }
            }
            // Update
            if (FilePath.Substring(0, 1) == ".")
            {
                FilePath = MUI.StartupFolder + FilePath.Substring(1);
            }
            return ai_path_code + FilePath;
        }
        /// <summary>
        /// Get the dot path of file.
        /// </summary>
        /// <param name="FilePath">The file path</param>
        /// <returns>The dot path if the file is inside program folder otherwise it returns the same path</returns>
        public static string GetDotPath(string FilePath)
        {
            if (FilePath == "")
                return "";
            string ai_path_code = "";
            if (FilePath.StartsWith("AI"))
            {
                for (int i = 0; i < FilePath.Length; i++)
                {
                    if (FilePath[i] == ')')
                    {
                        ai_path_code = FilePath.Substring(0, i + 1);
                        FilePath = FilePath.Substring(i + 1, FilePath.Length - (i + 1));
                    }
                }

            }

            if (Path.GetDirectoryName(FilePath).Length >= MUI.StartupFolder.Length)
            {
                if (Path.GetDirectoryName(FilePath).Substring(0, MUI.StartupFolder.Length) == MUI.StartupFolder)
                {
                    FilePath = "." + FilePath.Substring(MUI.StartupFolder.Length);
                }
            }
            FilePath = ai_path_code + FilePath;
            return FilePath;
        }
        /// <summary>
        /// Get size label with unit
        /// </summary>
        /// <param name="size">The size in bytes</param>
        /// <returns>The size label with unit</returns>
        public static string GetSize(long size)
        {
            string Unit = " Byte";
            double Len = size;
            if (size >= 1024)
            {
                Len = size / 1024.00;
                Unit = " KB";
            }
            if (Len >= 1024)
            {
                Len /= 1024.00;
                Unit = " MB";
            }
            if (Len >= 1024)
            {
                Len /= 1024.00;
                Unit = " GB";
            }
            if (Len < 0)
                return "???";
            return Len.ToString("F2") + Unit;
        }
        /// <summary>
        /// Get size label with unit
        /// </summary>
        /// <param name="size">The size in bytes</param>
        /// <returns>The size label with unit</returns>
        public static string GetSize(ulong size)
        {
            string Unit = " Byte";
            double Len = size;
            if (size >= 1024)
            {
                Len = size / 1024.00;
                Unit = " KB";
            }
            if (Len >= 1024)
            {
                Len /= 1024.00;
                Unit = " MB";
            }
            if (Len >= 1024)
            {
                Len /= 1024.00;
                Unit = " GB";
            }
            if (Len < 0)
                return "???";
            return Len.ToString("F2") + Unit;
        }
        /// <summary>
        /// Get file size in bytes
        /// </summary>
        /// <param name="FilePath">The file path</param>
        /// <returns>The file size in bytes.</returns>
        public static long GetSizeAsBytes(string FilePath)
        {
            string fPath = GetFullPath(FilePath);
            if (File.Exists(fPath) == true)
            {
                FileInfo Info = new FileInfo(fPath);
                return Info.Length;
            }
            return 0;
        }
        /// <summary>
        /// Check if string contain numbers
        /// </summary>
        /// <param name="text">The string to check</param>
        /// <returns>True if given string contain numbers otherwise false</returns>
        public static bool IsStringContainsNumbers(string text)
        {
            foreach (char chr in text.ToCharArray())
            {
                int tt = 0;
                if (int.TryParse(chr.ToString(), out tt))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Get the directory path for a file
        /// </summary>
        /// <param name="FilePath">The full file path</param>
        /// <returns>The file folder path</returns>
        public static string GetDirectory(string FilePath)
        {
            string val = Path.GetDirectoryName(FilePath);
            if (val == "")
                val = Path.GetPathRoot(FilePath);
            return val;
        }

        /// <summary>
        /// Show error (trace + message dialog)
        /// </summary>
        /// <param name="ex">The exception for details providing</param>
        /// <param name="traceMessage">The trace message</param>
        /// <param name="caption">The caption</param>
        public static void ShowError(Exception ex, string traceMessage, string caption)
        {
            string logFileName = string.Format("{0}-{1}-exception.txt", DateTime.Now.ToLocalTime().ToString(), caption);
            logFileName = logFileName.Replace(":", "");
            logFileName = logFileName.Replace("/", "-");

            if (MUI.ExceptionsFolder == "" || !Directory.Exists(MUI.ExceptionsFolder))
            {
                MUI.ExceptionsFolder = "Exceptions";
                Directory.CreateDirectory(MUI.ExceptionsFolder);
            }

            string logPath = Path.Combine(MUI.ExceptionsFolder, logFileName);

            File.WriteAllLines(Path.GetFullPath(logPath), ex.ToString().Split('\n'));

            ManagedMessageBox.ShowErrorMessage(traceMessage + ": \n" + ex.Message + "\n\n" + Resources.Word_PleaseSee + " " + logPath + " " + Resources.Word_FileForDetails + ".", caption);

            Trace.TraceError(traceMessage + " (" + Resources.Word_PleaseSee + " " + logPath + " " + Resources.Word_FileForDetails + ".)");
        }
        /// <summary>
        /// Show save/open dialog
        /// </summary>
        /// <param name="isSave">True: for save, False: for open</param>
        /// <param name="dTitle">The title of the dialog</param>
        /// <param name="dFilter">The filter to use</param>
        /// <param name="dFilePath">The file path. Set with new one</param>
        /// <param name="ok">True: Save/Open ok and dFilePath is set to new path. False: Save/Open is canceled.</param>
        public static void ShowOpenSaveDialog(bool isSave, string dTitle, string dFilter, ref string dFilePath, out bool ok)
        {
            if (isSave)
            {
                SaveFileDialog sForm = new SaveFileDialog();
                sForm.Title = dTitle;
                sForm.Filter = dFilter;

                if (dFilePath != "")
                    sForm.FileName = dFilePath;

                if (sForm.ShowDialog() == DialogResult.OK)
                {
                    dFilePath = sForm.FileName;
                    ok = true;
                }
                else
                {
                    ok = false;
                }
            }
            else
            {
                OpenFileDialog oForm = new OpenFileDialog();
                oForm.Title = dTitle;
                oForm.Filter = dFilter;

                if (dFilePath != "")
                    oForm.FileName = dFilePath;

                if (oForm.ShowDialog() == DialogResult.OK)
                {
                    dFilePath = oForm.FileName;
                    ok = true;
                }
                else
                {
                    ok = false;
                }
            }
        }

    }
}
