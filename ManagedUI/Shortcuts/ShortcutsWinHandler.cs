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
using System.Windows.Forms;
using SlimDX.DirectInput;

namespace ManagedUI
{
    /// <summary>
    /// Shortcuts handler for a window form (or a control) for handling current shortcuts.
    /// </summary>
    public class ShortcutsWinHandler
    {
        /// <summary>
        /// Shortcuts handler for a window form (or a control) for handling current shortcuts.
        /// </summary>
        /// <param name="windowHandle">The window or control handle</param>
        /// <param name="map">The shortcuts map to use</param>
        /// <param name="startCheckTimer">True: start shortcuts timer internally (i.e. handle executing shortcuts internally). False: don't use timer (i.e. handle shortcuts check manually).</param>
        public ShortcutsWinHandler(IntPtr windowHandle, ShortcutsMap map, bool startCheckTimer = true)
        {
            Initialize(windowHandle, map, startCheckTimer);
        }
        private Keyboard keyboard;
        private KeyboardState state;
        private int timerCounter = 0;
        private const int timerReload = 15;
        private Key[][] input_keys;
        private Timer timer_input;
        private ShortcutsMap map;

        /// <summary>
        /// Get if a shortcuts map is loaded or not.
        /// </summary>
        public bool IsShortcutsMapLoaded { get; private set; }
        /// <summary>
        /// Set to true to halt the shortcuts timer (if timer is running)
        /// </summary>
        public bool HaltShortcutsMapTimer { get; set; }

        /// <summary>
        /// Load a shortcuts map file into a windows form
        /// </summary>
        /// <param name="windowHandle">The Windows form handle</param>
        /// <param name="startCheckTimer">True to start timer and start checking for shortcuts. False to initialize shortcuts without timer (CheckShortcuts() can be used to check for shortcuts anytime)</param>
        public void Initialize(IntPtr windowHandle, ShortcutsMap map, bool startCheckTimer)
        {
            IsShortcutsMapLoaded = false;

            if (map == null)
                return;
            this.map = map;
            if (startCheckTimer)
            {
                if (timer_input == null)
                {
                    timer_input = new Timer();
                    timer_input.Interval = 10;
                    timer_input.Tick += new EventHandler(timer_input_Tick);
                }
                timer_input.Stop();
            }

            input_keys = new Key[map.Shortcuts.Count][];
            for (int i = 0; i < map.Shortcuts.Count; i++)
            {
                string[] kkkk = map.Shortcuts[i].TheShortcut.Split(new char[] { '+' });
                input_keys[i] = new Key[kkkk.Length];
                for (int k = 0; k < kkkk.Length; k++)
                {
                    if (kkkk[k] != "")
                        input_keys[i][k] = ((SlimDX.DirectInput.Key)Enum.Parse(typeof(SlimDX.DirectInput.Key), kkkk[k]));
                    else
                        input_keys[i][k] = Key.Unknown;
                }
            }

            DirectInput di = new DirectInput();
            keyboard = new Keyboard(di);
            keyboard.SetCooperativeLevel(windowHandle, CooperativeLevel.Nonexclusive | CooperativeLevel.Foreground);

            IsShortcutsMapLoaded = true;

            if (startCheckTimer)
            {
                timer_input.Start();
                HaltShortcutsMapTimer = false;
            }
        }
        /// <summary>
        /// Check shortcuts map for key press (should be called in a clock, if check timer is set, you don't need to use this).
        /// </summary>
        public void CheckShortcuts()
        {
            if (!IsShortcutsMapLoaded)
                return;
            if (timerCounter > 0)
            { timerCounter--; return; }
            if (keyboard.Acquire().IsSuccess)
            {
                state = keyboard.GetCurrentState();
                for (int i = 0; i < input_keys.Length; i++)
                {
                    string[] kkkk = map.Shortcuts[i].TheShortcut.Split(new char[] { '+' });
                    int accessed = 0;
                    for (int j = 0; j < input_keys[i].Length; j++)
                    {
                        if (state.IsPressed(input_keys[i][j]))
                        {
                            accessed++;
                        }
                    }
                    if (accessed == kkkk.Length)
                    {
                        timerCounter = timerReload;
                        map.ExecuteShortcut(map.Shortcuts[i].TheShortcut);
                        /*IAHDMenuItem mi = AHDGUI.GetMenuItem(sss.MenuItemID);
                        if (mi != null)
                        {
                            if (mi is AHDCommandMenuItem)
                            {
                                if (mi.Activated)// Can be executed only when it is activated.
                                    ((AHDCommandMenuItem)mi).ExecuteCommand();
                            }
                        }*/

                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Destroy shortcuts map and timer
        /// </summary>
        public void DestroyShortcuts()
        {
            IsShortcutsMapLoaded = false;
            if (timer_input != null)
            {
                timer_input.Stop();
                timer_input.Dispose();
                timer_input = null;
            }

            input_keys = new Key[0][];
            timerCounter = 0;

            keyboard.Dispose();
        }
        private void timer_input_Tick(object sender, EventArgs e)
        {
            if (!HaltShortcutsMapTimer)
                CheckShortcuts();
        }
    }
}
