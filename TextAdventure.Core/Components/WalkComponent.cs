using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;

namespace TextAdventure.Core.Components
{
    public class WalkComponent : ConsoleComponent
    {


        public WalkComponent() : base()
        {
            
        }

        public override void Update(SadConsole.Console console, TimeSpan delta)
        {
            throw new NotImplementedException();
        }

        public override void ProcessKeyboard(SadConsole.Console console, Keyboard info, out bool handled)
        {
            throw new NotImplementedException();
        }

        #region "Unused methods"
        [Obsolete]
        public override void Draw(SadConsole.Console console, TimeSpan delta) => throw new NotImplementedException();
        [Obsolete]
        public override void ProcessMouse(SadConsole.Console console, MouseConsoleState state, out bool handled) => throw new NotImplementedException();
        #endregion
    }
}
