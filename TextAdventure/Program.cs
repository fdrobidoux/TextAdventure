using System;
using SadConsole;

namespace TextAdventure
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            SadConsole.Settings.AllowWindowResize = true;
            SadConsole.Settings.UnlimitedFPS = false;
            SadConsole.Settings.ResizeMode = SadConsole.Settings.WindowResizeOptions.None;

            using (var game = new TextGame())
                game.Run();
        }
    }
}
