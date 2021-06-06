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
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace ManagedUI
{
    /// <summary>
    /// Service that responsible for managing commands of the app.
    /// </summary>
    [Export(typeof(IService))]
    [Export(typeof(CommandsManager))]
    [ServiceInfo("Commands Manager", "cmd", "Service that responsible for managing commands of the app.", true)]
    public class CommandsManager : IService
    {
        /// <summary>
        /// Get available commands.
        /// </summary>
        [ImportMany]
        public List<Lazy<ICommand, ICommandInfo>> AvailableCommands
        {
            get;
            private set;
        }
        /// <summary>
        /// Get available command combinations.
        /// </summary>
        [ImportMany]
        public List<Lazy<ICommandCombination, ICommandCombinationInfo>> AvaialableCommandCombinations
        {
            get;
            private set;
        }

        /// <summary>
        /// Initialize the commands manager service.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            // Remove commands with no parent service
            for (int i = 0; i < AvailableCommands.Count; i++)
            {
                if (!MUI.IsServiceExist(AvailableCommands[i].Metadata.ParentServiceID))
                {
                    AvailableCommands.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < AvaialableCommandCombinations.Count; i++)
            {
                if (!MUI.IsServiceExist(AvaialableCommandCombinations[i].Metadata.ParentServiceID))
                {
                    AvaialableCommandCombinations.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Get a command using the ID
        /// </summary>
        /// <param name="id">The id of the desired command</param>
        /// <returns>The command if found otherwise null</returns>
        public Lazy<ICommand, ICommandInfo> GetCommand(string id)
        {
            if (AvailableCommands == null)
                return null;
            foreach (Lazy<ICommand, ICommandInfo> it in AvailableCommands)
            {
                if (it.Metadata.ID == id)
                    return it;
            }
            return null;
        }
        /// <summary>
        /// Get a value indicates that a command is exist or not.
        /// </summary>
        /// <param name="id">The comand's id</param>
        /// <returns>True if the command is exist otherwise false.</returns>
        public bool IsCommandExist(string id)
        {
            if (AvailableCommands == null)
                return false;
            foreach (Lazy<ICommand, ICommandInfo> it in AvailableCommands)
            {
                if (it.Metadata.ID == id)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Remove a command from the list. This will remove the command and cannot be undone unless the application restarted.
        /// </summary>
        /// <param name="id">The command id</param>
        public void RemoveCommand(string id)
        {
            if (AvailableCommands == null)
                return;
            foreach (Lazy<ICommand, ICommandInfo> it in AvailableCommands)
            {
                if (it.Metadata.ID == id)
                {
                    AvailableCommands.Remove(it);
                    break;
                }
            }
        }
        /// <summary>
        /// Get command combination using id
        /// </summary>
        /// <param name="id">The id of the command combination</param>
        /// <returns>The command combination if found otherwise false.</returns>
        public Lazy<ICommandCombination, ICommandCombinationInfo> GetCommandCombination(string id)
        {
            if (AvaialableCommandCombinations == null)
                return null;
            foreach (Lazy<ICommandCombination, ICommandCombinationInfo> it in AvaialableCommandCombinations)
            {
                if (it.Metadata.ID == id)
                    return it;
            }
            return null;
        }

        /// <summary>
        /// Execute a command.
        /// </summary>
        /// <param name="commandID">The command id. </param>
        public void Execute(string commandID)
        {
            foreach (Lazy<ICommand, ICommandInfo> cm in AvailableCommands)
            {
                if (cm.Metadata.ID == commandID)
                {
                    WriteLine("-->" + Properties.Resources.Status_ExecutingCommand + ": " + commandID);
                    cm.Value.Execute();
                    return;
                }
            }
            // Reached here mean the command didn't found.
            WriteLine(Properties.Resources.Status_TheCommandCannotBeFound + ": " + commandID);
        }
        /// <summary>
        /// Execute a command.
        /// </summary>
        /// <param name="commandID">The command id. </param>
        /// <param name="responses">The expected responses (if any)</param>
        public void Execute(string commandID, out object[] responses)
        {
            foreach (Lazy<ICommand, ICommandInfo> cm in AvailableCommands)
            {
                if (cm.Metadata.ID == commandID)
                {
                    WriteLine("-->" + Properties.Resources.Status_ExecutingCommand + ": " + commandID);
                    cm.Value.Execute(out responses);
                    return;
                }
            }
            // Reached here mean the command didn't found.
            WriteLine(Properties.Resources.Status_TheCommandCannotBeFound + ": " + commandID);
            responses = new object[0];
        }
        /// <summary>
        /// Execute a command.
        /// </summary>
        /// <param name="commandID">The command id. </param>
        /// <param name="parameters">The parameters to use. The parameters should be accepted by the command of course.</param>
        public void Execute(string commandID, object[] parameters)
        {
            foreach (Lazy<ICommand, ICommandInfo> cm in AvailableCommands)
            {
                if (cm.Metadata.ID == commandID)
                {
                    WriteLine("-->" + Properties.Resources.Status_ExecutingCommand + ": " + commandID);
                    cm.Value.Execute(parameters);
                    return;
                }
            }
            // Reached here mean the command didn't found.
            WriteLine(Properties.Resources.Status_TheCommandCannotBeFound + ": " + commandID);
        }
        /// <summary>
        /// Execute a command.
        /// </summary>
        /// <param name="commandID">The command id. </param>
        /// <param name="parameters">The parameters to use. The parameters should be accepted by the command of course.</param>
        /// <param name="responses">The expected responses (if any)</param>
        public void Execute(string commandID, object[] parameters, out object[] responses)
        {
            foreach (Lazy<ICommand, ICommandInfo> cm in AvailableCommands)
            {
                if (cm.Metadata.ID == commandID)
                {
                    WriteLine("-->" + Properties.Resources.Status_ExecutingCommand + ": " + commandID);
                    cm.Value.Execute(parameters, out responses);
                    return;
                }
            }
            // Reached here mean the command didn't found.
            WriteLine(Properties.Resources.Status_TheCommandCannotBeFound + ": " + commandID);
            responses = new object[0];
        }
        /// <summary>
        /// Execute an array of commands. The array can include command ids and parameters (after each command should come the parameters if any)
        /// </summary>
        /// <param name="commands">The array can include command ids and parameters (after each command should come the parameters if any)</param>
        public void Execute(string[] commands)
        {
            if (commands == null)
                return;
            if (commands.Length == 0)
                return;
            List<string> param = new List<string>();
            for (int i = 0; i < commands.Length; i++)
            {
                if (IsCommandExist(commands[i]))
                {
                    string command = commands[i];
                    param = new List<string>();
                    // Look for the parameters
                    i++;
                    if (i >= commands.Length)
                    {
                        // Execute the command and exit
                        Execute(command);
                        break;
                    }
                    else
                    {
                        while (!IsCommandExist(commands[i]) && i < commands.Length)
                        {
                            param.Add(commands[i]);
                            i++;
                            if (i >= commands.Length)
                                break;
                        }
                        Execute(command, param.ToArray());
                        i--;
                    }
                }
            }
        }

        /// <summary>
        /// Execute a command combination.
        /// </summary>
        /// <param name="commandCombinationID">The command combination id. </param>
        public void ExecuteCommandCombination(string commandCombinationID)
        {
            if (AvaialableCommandCombinations == null)
                return;
            foreach (Lazy<ICommandCombination, ICommandCombinationInfo> cm in AvaialableCommandCombinations)
            {
                if (cm.Metadata.ID == commandCombinationID)
                {

                    object[] ress = new object[0];
                    if (cm.Value.UseParameters)
                    {
                        string ss = "";
                        for (int i = 0; i < cm.Metadata.Parameters.Length; i++)
                            ss += cm.Metadata.Parameters[i].ToString() + " ";
                        WriteLine("-->" + Properties.Resources.Status_ExecutingCommand + ": " + cm.Metadata.ID + " [" + cm.Metadata.CommandID + " " + ss + "]");
                        Execute(cm.Value.CommandID, cm.Value.Parameters, out ress);
                    }
                    else
                    {
                        WriteLine("-->" + Properties.Resources.Status_ExecutingCommand + ": " + cm.Metadata.ID + " [" + cm.Metadata.CommandID + "] ");
                        Execute(cm.Value.CommandID, out ress);
                    }

                    cm.Value.OnCommandResponse(ress);
                    return;
                }
            }
            // Reached here mean the command didn't found.
            WriteLine(Properties.Resources.Status_TheCommandCombinationCannotBeFound + ": " + commandCombinationID);
        }
        /// <summary>
        /// Remove a command combination from the list. This will remove the command combination and cannot be undone unless the application restarted.
        /// </summary>
        /// <param name="id">The command combination id</param>
        public void RemoveCommandCombination(string id)
        {
            if (AvaialableCommandCombinations == null)
                return;
            foreach (Lazy<ICommandCombination, ICommandCombinationInfo> cm in AvaialableCommandCombinations)
            {
                if (cm.Metadata.ID == id)
                {
                    AvaialableCommandCombinations.Remove(cm);
                    break;
                }
            }
        }
        /// <summary>
        /// Get a value indicates that a command combination is exist or not.
        /// </summary>
        /// <param name="id">The command combination's id</param>
        /// <returns>True if the command combination is exist otherwise false.</returns>
        public bool IsCommandCombinationExist(string id)
        {
            if (AvaialableCommandCombinations == null)
                return false;
            foreach (Lazy<ICommandCombination, ICommandCombinationInfo> cm in AvaialableCommandCombinations)
            {
                if (cm.Metadata.ID == id)
                    return true;
            }
            return false;
        }
        #region STATIC
        private static CommandsManager commandsManager;

        /// <summary>
        /// Get the Commands Manager service. This will load it into memory once.
        /// </summary>
        public static CommandsManager CMD
        {
            get
            {
                if (commandsManager == null)
                {
                    Lazy<IService, IServiceInfo> ser = MUI.GetServiceByID("cmd");
                    if (ser != null)
                        commandsManager = (CommandsManager)ser.Value;
                }
                return commandsManager;
            }
        }
        #endregion
    }
}
