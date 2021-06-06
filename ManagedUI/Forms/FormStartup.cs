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
using System.Drawing;
using System.Windows.Forms;

namespace ManagedUI
{
    /// <summary>
    /// The splash window.
    /// </summary>
    public partial class FormStartup : Form
    {
        /// <summary>
        /// The splash window.
        /// </summary>
        public FormStartup()
        {
            InitializeComponent();
            this.label1.ForeColor =
            this.label_status.ForeColor =
            this.label_version.ForeColor =
            this.label_copyright.ForeColor =
            this.label_copyright_message.ForeColor = GUIConfiguration.SplashWindowTextColor;

            this.label1.Text = MUI.ProjectTitle;
            this.label_version.Text = Properties.Resources.Label_Version + " " + MUI.ProjectVersion;
            this.label_copyright.Text = MUI.ProjectCopyright;
            this.label_copyright_message.Text = MUI.ProjectCopyrightMessage;
        }
        private delegate void WriteStatusText(string message, string category);
        /// <summary>
        /// Write a line in the status bar
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="category">The category of the message (see StatusMode)</param>
        public void WriteLine(string message, string category)
        {
            if (!this.InvokeRequired)
                WriteLineNormal(message, category);
            else
                this.Invoke(new WriteStatusText(WriteLineNormal), message, category);
        }
        private void WriteLineNormal(string message, string category)
        {
            Color color = GUIConfiguration.SplashWindowTextColor;

            switch (category)
            {
                case StatusMode.Error: color = Color.Red; break;
                case StatusMode.Warning: color = Color.Yellow; break;
                case StatusMode.Information: color = Color.LimeGreen; break;
                default: return;
            }

            label_status.ForeColor = color;
            label_status.Text = message;
            label_status.Refresh();
        }
    }
}
