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

using Argonfox.Dimmer.View;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Argonfox.Dimmer.Utility
{
    public static class Dimmer
    {
        private static Dictionary<int, Tuple<Window, bool>> DimmedScreens = new Dictionary<int, Tuple<Window, bool>>();

        public static void DimScreen(int id, bool manuallyDimmed = false)
        {
            if (!DimmedScreens.ContainsKey(id))
            {
                var rectangle = ScreenModelRetriever.GetScreenById(id).Bounds;
                var window = new MainWindow { Top = rectangle.Top, Left = rectangle.Left, Width = rectangle.Width, Height = rectangle.Height };
                window.Show();
                DimmedScreens.Add(id, new Tuple<Window, bool>(window, manuallyDimmed));
            }
        }

        public static void ClearScreen(int id, bool manuallyCleared = false)
        {
            if (DimmedScreens.ContainsKey(id))
            {
                if (!manuallyCleared && DimmedScreens[id].Item2)
                {
                    // Prevent the automatic dimmer to clear a manually dimmed screen
                    return;
                }

                DimmedScreens[id].Item1.Close();
                DimmedScreens.Remove(id);
            }
        }
    }
}