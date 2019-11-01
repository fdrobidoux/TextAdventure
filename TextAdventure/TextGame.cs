using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadConsole.Input;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using TextAdventure.Core.UI;
using SadConsole.Controls;
using SadConsole.Themes;
using TextAdventure.Core.Console;
using TextAdventure.Core.Consoles;

namespace TextAdventure
{
    public class TextGame : SadConsole.Game
    {
        HUDConsole MyHUDConsole;
        TestHealthConsole someTestHealthConsole;
        TestHealthConsole someTestManaConsole;

        public TextGame() : base("", 50, 100, null)
        {
            
        }

        protected override void Initialize()
        {
            // Generally you don't want to hide the mouse from the user
            IsMouseVisible = true;

            base.Initialize();

            Add(MyHUDConsole = new HUDConsole(Global.CurrentScreen.Width, 4));

            // Collection of buttons for testing.
            Add(someTestHealthConsole = new TestHealthConsole(80, 20, MyHUDConsole.HpProgressBar) { Position = new Point(0, 5) });
            Add(someTestManaConsole = new TestHealthConsole(80, 20, MyHUDConsole.ManaProgressBar) { Position = new Point(30, 5) });
        }

        private void Add(Console screen) 
            => Global.CurrentScreen.Children.Add(screen);

        [Obsolete]
        private void Program_WindowResized(object sender, EventArgs e)
        {
            SadConsole.Global.CurrentScreen.Resize(Global.WindowWidth / SadConsole.Global.CurrentScreen.Font.Size.X, Global.WindowHeight / SadConsole.Global.CurrentScreen.Font.Size.Y, true);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            someTestHealthConsole.Update(gameTime);
            someTestManaConsole.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}