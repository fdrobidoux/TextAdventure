using System;
using SadConsole;

namespace SadConsole
{
    public static class ConsoleExtensions
    {
        public static ConsoleCollection RootChildren(this Console thisConsole)
        {
            return SadConsole.Global.CurrentScreen.Children;
        }
    }
}
