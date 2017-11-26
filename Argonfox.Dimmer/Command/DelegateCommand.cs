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

using System;
using System.Windows.Input;

namespace Argonfox.Dimmer.Command
{
    internal class DelegateCommand : ICommand
    {
        private static readonly Func<object, bool> defaultCanExecute = o => true;
        private readonly Action<object> action;
        private readonly Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> action, Func<object, bool> canExecuteFunc = null)
        {
            this.action = action;
            this.canExecute = canExecuteFunc ?? defaultCanExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.action(parameter);
        }
    }
}