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

using Argonfox.Dimmer.Command;
using Argonfox.Dimmer.Model;
using Argonfox.Dimmer.Utility;
using System.Collections.Generic;
using System.Windows.Input;

namespace Argonfox.Dimmer.ViewModel
{
    internal class SettingsWindowViewModel
    {
        public List<AutomaticDimmerSettingsModel> Settings { get; set; }

        public ICommand SaveSettingsCommand => new DelegateCommand((o) =>
        {
            SettingsFileAccess.Save(this.Settings);
            ProcessWatchdog.UpdateSettings();
        });

        public SettingsWindowViewModel()
        {
            this.Settings = SettingsFileAccess.Retrieve();
        }
    }
}