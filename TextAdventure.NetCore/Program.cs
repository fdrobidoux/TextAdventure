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
            SadConsole.Settings.ResizeMode = SadConsole.Settings.WindowResizeOptions.Scale;

            using (var game = new TextGame())
                game.Run();
        }
    }
}
