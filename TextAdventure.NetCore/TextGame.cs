using System;
using System.Collections.Generic;
using System.Text;
using TextAdventure.Core.Concepts;
using SadConsole;
using SadConsole.Input;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using TextAdventure.Core.UI;
using SadConsole.Controls;

namespace TextAdventure
{
    public class TextGame : SadConsole.Game
    {
        HealthProgressBar HPBar;

        public TextGame() : base("", 80, 25, null)
        {
            //Instance.Window.AllowAltF4 = false;
        }

        protected override void Initialize()
        {
            // Generally you don't want to hide the mouse from the user
            IsMouseVisible = true;

            base.Initialize();

            ControlsConsole console = new ControlsConsole(80, 25);

            HPBar = new HealthProgressBar(console.Width, 1, HorizontalAlignment.Left);

            var consoleTheme = SadConsole.Themes.Library.Default.Clone();
            consoleTheme.ProgressBarTheme = new HealthProgressBarTheme();

            console.Theme = consoleTheme;

            console.Add(HPBar);

            //Global.CurrentScreen = console;
            SadConsole.Global.CurrentScreen.Children.Add(console);
        }

        bool done = false;

        protected override void Update(GameTime gameTime)
        {
            if (!done && gameTime.TotalGameTime.Seconds == 1)
            {
                done = true;
                HPBar.Progress = 0.5f;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}