using System;

namespace TextAdventure
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            SadConsole.Settings.AllowWindowResize = false;
            SadConsole.Settings.UnlimitedFPS = false;

            using (var game = new TextGame())
                game.Run();
        }
    }
}
