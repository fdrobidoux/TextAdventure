using System;
using Microsoft.Xna.Framework;
using SadConsole;

namespace SadConsole
{
    public static class ConsoleExtensions
    {
        public static ConsoleCollection RootChildren(this Console thisConsole)
        {
            return SadConsole.Global.CurrentScreen.Children;
        }

        public static void SetNamedGlyph(this Console thisConsole, int x, int y, string namedGlyph, Color Foreground, Color Background, bool expanded = true)
        {

        }
    }
}
