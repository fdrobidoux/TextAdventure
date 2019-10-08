using System;
using System.Collections.Generic;
using System.Text;
using TextAdventure.Core.Concepts;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;

namespace TextAdventure.Core
{
    public class TextGame
    {
        public TextGame()
        {
            SadConsole.Settings.AllowWindowResize = false;
            SadConsole.Settings.UnlimitedFPS = false;

            SadConsole.Game.Create("Fonts/IBM.font", 80, 25);

            SadConsole.Game.Instance.Window.AllowAltF4 = false;

            SadConsole.Game.OnInitialize = this.Initialize;
            SadConsole.Game.OnUpdate = this.Update;
            SadConsole.Game.OnDraw = this.Draw;
        }

        public void Run()
        {
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private void Initialize()
        {

        }

        private void Update(GameTime obj)
        {

        }

        private void Draw(GameTime obj)
        {

        }
    }
}
