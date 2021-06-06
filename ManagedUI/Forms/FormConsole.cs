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
using System.Diagnostics;
using System.Windows.Forms;

namespace ManagedUI
{
    public partial class FormConsole : Form
    {
        /// <summary>
        /// The form that allows to enter commands manually.
        /// </summary>
        public FormConsole()
        {
            InitializeComponent();

            tt = new TraceListenerConsoleForm(this);
            Trace.Listeners.Add(tt);
        }
        private TraceListenerConsoleForm tt;

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
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            switch (category)
            {
                case StatusMode.Error:
                    {
                        richTextBox1.SelectionColor = System.Drawing.Color.Red;
                        break;
                    }
                case StatusMode.Information:
                    {
                        richTextBox1.SelectionColor = System.Drawing.Color.Lime;
                        break;
                    }
                case StatusMode.Warning:
                    {
                        richTextBox1.SelectionColor = System.Drawing.Color.Gold;
                        break;
                    }
                case StatusMode.Normal:
                    {
                        richTextBox1.SelectionColor = System.Drawing.Color.White;
                        break;
                    }
            }
            richTextBox1.SelectedText = message + "\n";
            try
            {
                // This throws an exception when a log is writen and user active this control.
                richTextBox1.ScrollToCaret();
            }
            catch { }
        }

        private void ExecuteCommand()
        {
            if (comboBox1.Text == "")
                return;
            // Execute 'em !!
            CommandsManager.CMD.Execute(comboBox1.Text.Split(new char[] { ' ' }));
            if (!comboBox1.Items.Contains(comboBox1.Text))
                comboBox1.Items.Add(comboBox1.Text);

            comboBox1.Text = "";
        }

        // Execute an command
        private void button1_Click(object sender, System.EventArgs e)
        {
            ExecuteCommand();
        }
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ExecuteCommand();
            }
        }
        private void FormConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tt != null)
                Trace.Listeners.Remove(tt);
        }
    }
}
