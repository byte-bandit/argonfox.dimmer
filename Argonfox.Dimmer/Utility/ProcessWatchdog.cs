/*
    ArgonFox Dimmer
    Copyright (C) 2017 ArgonFox

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
 */

using Argonfox.Dimmer.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace Argonfox.Dimmer.Utility
{
    internal class ProcessWatchdog
    {
        public static event ProcessStateChangedEventArgs OnProcessStarted;

        public static event ProcessStateChangedEventArgs OnProcessClosed;

        public delegate void ProcessStateChangedEventArgs(int screenId);

        private static readonly Timer Timer = new Timer(1000);
        private static List<AutomaticDimmerSettingsModel> AutomaticDimmerSettings;
        private static List<string> RunningProcesses = new List<string>();

        public static void Initialize()
        {
            UpdateSettings();

            Timer.AutoReset = true;
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }

        public static void UpdateSettings()
        {
            AutomaticDimmerSettings = SettingsFileAccess.Retrieve();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var running = Process.GetProcesses().Select(x => x.ProcessName).ToList();

            foreach (var setting in AutomaticDimmerSettings)
            {
                if (running.Contains(setting.ProcessName) && !RunningProcesses.Contains(setting.ProcessName))
                {
                    RunningProcesses.Add(setting.ProcessName);
                    OnProcessStarted?.Invoke(setting.DimmedScreen);
                }
                else if (!running.Contains(setting.ProcessName) && RunningProcesses.Contains(setting.ProcessName))
                {
                    RunningProcesses.Remove(setting.ProcessName);
                    OnProcessClosed?.Invoke(setting.DimmedScreen);
                }
            }
        }
    }
}