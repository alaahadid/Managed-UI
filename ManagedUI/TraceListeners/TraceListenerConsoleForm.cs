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

namespace ManagedUI
{
    class TraceListenerConsoleForm : TraceListener
    {
        public TraceListenerConsoleForm(FormConsole frm)
        {
            this.frm = frm;
        }
        FormConsole frm;
        public override void Write(string message)
        {
            if (frm != null)
                frm.WriteLine(message, "");
        }
        public override void WriteLine(string message)
        {
            if (frm != null)
                frm.WriteLine(message, "");
        }
        public override void WriteLine(string message, string category)
        {
            if (frm != null)
                frm.WriteLine(message, category);
        }
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            switch (eventType)
            {
                case TraceEventType.Information:
                    if (frm != null)
                        frm.WriteLine(message, StatusMode.Information);
                    break;
                case TraceEventType.Warning:
                    if (frm != null)
                        frm.WriteLine(message, StatusMode.Warning);
                    break;
                case TraceEventType.Error:
                    if (frm != null)
                        frm.WriteLine(message, StatusMode.Error);
                    break;
                default:
                    if (frm != null)
                        frm.WriteLine(message, StatusMode.Normal);
                    break;
            }
        }
    }
}
