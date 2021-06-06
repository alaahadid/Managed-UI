/* 
   This file is part of AHD (Attributed Header Design)
   A framework for .net gui-based applications.

   Copyright © Alaa Ibrahim Hadid 2021

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.

   Author email: mailto:alaahadid@online.de
 */
using System;
using System.Windows.Forms;
using SlimDX.DirectInput;

namespace ManagedUI
{
    public partial class FormChangeKey : Form
    {
        public FormChangeKey(string keyName)
        {
            InitializeComponent();

            DirectInput di = new DirectInput();
            keyboard = new Keyboard(di);
            keyboard.SetCooperativeLevel(this.Handle, CooperativeLevel.Nonexclusive | CooperativeLevel.Foreground);


            timer_hold.Start();
            label1.Text = string.Format(Properties.Resources.Word_PressKeysFor + "\n [{0}]", keyName);
            stopTimer = 10;
            label_cancel.Text = string.Format(Properties.Resources.Word_CancelIn + " {0} " + Properties.Resources.Word_Seconds, stopTimer);
            timer2.Start();
            this.Select();
        }

        private Keyboard keyboard;
        private KeyboardState keyboardState;
        private string _inputName;
        private int stopTimer = 0;

        public string InputName { get { return _inputName; } }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (keyboard.Acquire().IsSuccess)
            {
                keyboardState = keyboard.GetCurrentState();
                if (keyboardState.PressedKeys.Count > 0)
                {
                    if (keyboardState.PressedKeys.Count == 1)
                    {
                        if (keyboardState.PressedKeys[0] == Key.LeftAlt || keyboardState.PressedKeys[0] == Key.LeftControl || keyboardState.PressedKeys[0] == Key.LeftShift ||
                            keyboardState.PressedKeys[0] == Key.RightAlt || keyboardState.PressedKeys[0] == Key.RightControl || keyboardState.PressedKeys[0] == Key.RightShift)
                            return;
                        _inputName = keyboardState.PressedKeys[0].ToString();

                    }
                    else
                    {
                        _inputName = "";
                        for (int i = 0; i < keyboardState.PressedKeys.Count; i++)
                        {
                            _inputName += keyboardState.PressedKeys[i].ToString() + "+";
                        }
                        if (_inputName.Length > 0)
                            _inputName = _inputName.Substring(0, _inputName.Length - 1);
                    }

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    timer1.Enabled = false;
                    this.Close();
                    return;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (stopTimer > 0)
            {
                stopTimer--;
                label_cancel.Text = string.Format(Properties.Resources.Word_CancelIn + " {0} " + Properties.Resources.Word_Seconds, stopTimer);
            }
            else
            {
                timer2.Stop();

                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                timer1.Enabled = false;
                this.Close();
                return;
            }
        }
        private void timer_hold_Tick(object sender, EventArgs e)
        {
            timer_hold.Stop();
            timer1.Interval = 1000 / 30;
            timer1.Start();
        }
    }
}
