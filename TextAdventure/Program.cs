using System;
using SadConsole;
using Microsoft.Xna.Framework;

namespace TextAdventure
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            SadConsole.Settings.AllowWindowResize = false;
            SadConsole.Settings.UnlimitedFPS = false;
            SadConsole.Settings.ResizeMode = SadConsole.Settings.WindowResizeOptions.Fit;
            SadConsole.Settings.UseDefaultExtendedFont = true;

            using (var game = new TextGame())
                game.Run();
        }
    }
}
